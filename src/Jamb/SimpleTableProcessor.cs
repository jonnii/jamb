using System;
using System.Linq;

namespace Jamb
{
    public class SimpleTableProcessor<T> : ITableProcessor
    {
        private readonly string name;

        private readonly Func<T, T> func;

        public SimpleTableProcessor(string name, Func<T, T> func)
        {
            this.name = name;
            this.func = func;
        }

        public void Run(ITableFrame tableFrame)
        {
            var data = tableFrame.GetData<T>(name);

            var transformed = data.Select(func);

            tableFrame.SetData(name, transformed);
        }
    }
}