using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class DataColumn<T> : IDataColumn
    {
        public DataColumn(IEnumerable<T> data)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }

        public int Length
        {
            get { return Data.Count(); }
        }
    }
}
