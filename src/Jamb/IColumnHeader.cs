using System;

namespace Jamb
{
    public interface IColumnHeader
    {
        string Name { get; }

        Type UnderlyingType { get; }

        bool HasData { get; }

        int DataColumnIndex { get; set; }
    }

    public interface IColumnHeader<T> : IColumnHeader
    {
        
    }
}