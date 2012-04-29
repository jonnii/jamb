using System;
using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class ColumnHeader<T> : IColumnHeader<T>
    {
        private readonly LinkedList<int> columns = new LinkedList<int>();

        public ColumnHeader(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public Type UnderlyingType
        {
            get { return typeof(T); }
        }

        public bool HasData
        {
            get { return columns.Any(); }
        }

        public IEnumerable<int> DataColumns
        {
            get { return columns; }
        }

        public void SetDataColumn(int dataColumnIndex)
        {
            columns.AddFirst(dataColumnIndex);
        }
    }
}