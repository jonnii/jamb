using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Jamb.Extensions;

namespace Jamb
{
    public class Table : ITable, ITableFrame
    {
        private readonly DataColumnBuilder dataColumnBuilder = new DataColumnBuilder();

        private readonly IDictionary<string, IColumnHeader> columnHeaders = new ConcurrentDictionary<string, IColumnHeader>();

        private readonly List<IDataColumn> dataColumns = new List<IDataColumn>();

        private int numRows;

        public int NumColumns
        {
            get { return columnHeaders.Count; }
        }

        public IColumnHeader<T> CreateColumn<T>(string name)
        {
            if (columnHeaders.ContainsKey(name))
            {
                var message = string.Format(
                    "A column with the name '{0}' already exists in this table", name);

                throw new ArgumentException(message, "name");
            }

            var header = new ColumnHeader<T>(name);
            columnHeaders.Add(name, header);
            return header;
        }

        public IColumnHeader<T> CreateColumn<T>(string name, IEnumerable<T> data)
        {
            var columnHeader = CreateColumn<T>(name);

            SetData(columnHeader, data);

            return columnHeader;
        }

        public IColumnHeader GetColumnHeader(string name)
        {
            return columnHeaders[name];
        }

        public void SetData<T>(string columnName, IEnumerable<T> data)
        {
            var header = GetColumnHeader(columnName);
            SetData(header, data);
        }

        private void SetData<T>(IColumnHeader header, IEnumerable<T> data)
        {
            var dataColumn = dataColumnBuilder.Build(data);
            dataColumns.Add(dataColumn);

            header.SetDataColumn(dataColumns.Count - 1);
            numRows = Math.Max(dataColumn.Length, numRows);
        }

        public IEnumerable<T> GetData<T>(IColumnHeader<T> columnHeader)
        {
            if (columnHeader.HasData)
            {
                var index = columnHeader.DataColumns.First();
                var dataColumn = (DataColumn<T>)dataColumns[index];
                return dataColumn.GetEnumerable(numRows);
            }

            return CreateEmptyDataColumn<T>();
        }

        public IEnumerable<T> GetData<T>(string columnName)
        {
            var columnHeader = (IColumnHeader<T>)columnHeaders[columnName];

            return GetData(columnHeader);
        }

        private IEnumerable<T> CreateEmptyDataColumn<T>()
        {
            return Padding.Create<T>(numRows);
        }

        public void Apply(ITableProcessor processor)
        {
            processor.Run(this);
        }

        public void Apply<T>(IColumnHeader<T> columnHeader, Func<T, T> func)
        {
            var processor = new SimpleTableProcessor<T>(columnHeader.Name, func);
            processor.Run(this);
        }
    }
}