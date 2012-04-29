using System.Collections.Generic;

namespace Jamb
{
    public interface ITable
    {
        IEnumerable<T> GetData<T>(string columnName);

        void SetData<T>(string columnName, IEnumerable<T> data);
    }
}