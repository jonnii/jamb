using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class DataColumnBuilder
    {
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

            foreach (var item in source)
            {
                if (Equals(default(T), item))
                {
                    if (writingSegment)
                    {
                        yield return new DataColumnSegment<T>(start, segmentData);
                        writingSegment = false;
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
                }

                currentPosition++;
            }

            yield return new DataColumnSegment<T>(start, segmentData);
        }
    }
}