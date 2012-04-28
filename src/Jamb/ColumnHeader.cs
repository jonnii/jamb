using System;

namespace Jamb
{
    public class ColumnHeader<T> : IColumnHeader<T>
    {
        public ColumnHeader(string name)
        {
            Name = name;
            DataColumnIndex = -1;
        }

        public string Name { get; set; }

        public Type UnderlyingType
        {
            get { return typeof(T); }
        }

        public bool HasData
        {
            get { return DataColumnIndex >= 0; }
        }

        public int DataColumnIndex { get; set; }
    }
}