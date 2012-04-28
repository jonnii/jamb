namespace Jamb
{
    public class DataColumn<T> : IDataColumn
    {
        public DataColumn(T[] data)
        {
            Data = data;
        }

        public T[] Data { get; set; }
    }
}
