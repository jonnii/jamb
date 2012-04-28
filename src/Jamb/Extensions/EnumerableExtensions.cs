using System.Collections.Generic;
using System.Linq;

namespace Jamb.Extensions
{
    public static class Padding
    {
        public static IEnumerable<T> Create<T>(int length)
        {
            return Enumerable.Range(0, length).Select(_ => default(T));
        }
    }
}
