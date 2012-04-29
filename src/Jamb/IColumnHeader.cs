using System;
using System.Collections.Generic;

namespace Jamb
{
    public interface IColumnHeader
    {
        string Name { get; }

        Type UnderlyingType { get; }

        bool HasData { get; }

        IEnumerable<int> DataColumns { get; }

        void SetDataColumn(int dataColumnIndex);
    }

    public interface IColumnHeader<T> : IColumnHeader { }
}