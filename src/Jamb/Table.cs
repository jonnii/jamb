using System;
using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class Table : ITable
    {
        private readonly Dictionary<string, IColumnHeader> columnHeaders =
            new Dictionary<string, IColumnHeader>();

        private readonly List<IDataColumn> dataColumns = new List<IDataColumn>();

        public Table(int rows)
        {
            NumRows = rows;
        }

        public int NumRows { get; private set; }

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

        public IColumnHeader<T> CreateColumn<T>(string name, T[] data)
        {
            if (data.Length != NumRows)
            {
                throw new ArgumentException(
                    "The number of items in a data column must be equal to the number of rows in the table",
                    "data");
            }

            var columnHeader = CreateColumn<T>(name);

            SetData(columnHeader, data);

            return columnHeader;
        }

        private void SetData<T>(IColumnHeader<T> header, T[] data)
        {
            if (header.HasData)
            {
                throw new NotImplementedException();
            }

            var dataColumn = new DataColumn<T>(data);
            dataColumns.Add(dataColumn);
            header.DataColumnIndex = dataColumns.Count - 1;
        }

        public IEnumerable<T> GetData<T>(string name)
        {
            var columnHeader = columnHeaders[name];

            if (columnHeader.HasData)
            {
                var index = columnHeader.DataColumnIndex;
                var dataColumn = (DataColumn<T>)dataColumns[index];
                return dataColumn.Data;
            }

            return CreateEmptyDataColumn<T>();
        }

        private IEnumerable<T> CreateEmptyDataColumn<T>()
        {
            return Enumerable.Range(0, NumRows).Select(i => default(T));
        }
    }
}