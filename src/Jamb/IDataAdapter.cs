using System.Collections.Generic;

namespace Jamb
{
    public interface IDataAdapter
    {
        IEnumerable<T> GetData<T>(string name);

        IColumnHeader<T> CreateColumn<T>(string name, IEnumerable<T> data);
    }
}