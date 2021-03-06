using System.Collections.Generic;

namespace Jamb
{
    public interface ITableFrame
    {
        IEnumerable<T> GetData<T>(string name);

        IColumnHeader<T> CreateColumn<T>(string name, IEnumerable<T> data);

        void SetData<T>(string name, IEnumerable<T> data);
    }
}