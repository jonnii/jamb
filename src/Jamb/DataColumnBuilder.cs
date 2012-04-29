using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class DataColumnBuilder
    {
        public DataColumnBuilder()
        {
            DefaultValueTolerance = 2;
        }

        public int DefaultValueTolerance { get; set; }

        public DataColumn<T> Build<T>(IEnumerable<T> source)
        {
            var segments = CreateSegments(source);

            return new DataColumn<T>(segments.ToList());
        }

        private IEnumerable<DataColumnSegment<T>> CreateSegments<T>(IEnumerable<T> source)
        {
            var start = 0;
            var segmentData = new List<T>();
            var writingSegment = false;
            var currentPosition = 0;
            var padding = 0;

            foreach (var item in source)
            {
                var isDefaultValue = Equals(default(T), item);

                if (isDefaultValue && padding >= DefaultValueTolerance)
                {
                    if (writingSegment)
                    {
                        yield return new DataColumnSegment<T>(start, segmentData.ToArray());
                        writingSegment = false;
                        padding = 0;
                    }
                }
                else
                {
                    if (!writingSegment)
                    {
                        segmentData = new List<T>();
                        writingSegment = true;
                        start = currentPosition;
                    }

                    segmentData.Add(item);

                    if (isDefaultValue)
                    {
                        ++padding;
                    }
                    else
                    {
                        padding = 0;
                    }
                }

                currentPosition++;
            }

            yield return new DataColumnSegment<T>(start, segmentData.ToArray());
        }
    }
}