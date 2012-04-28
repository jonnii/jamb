using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class DataColumn<T> : IDataColumn
    {
        private readonly List<DataColumnSegment<T>> segments;

        public DataColumn(List<DataColumnSegment<T>> segments)
        {
            this.segments = segments;
        }

        public int Length
        {
            get { return segments.Last().End + 1; }
        }

        public int NumSegments
        {
            get { return segments.Count; }
        }

        public IEnumerable<T> GetEnumerable()
        {
            var currentStart = 0;
            foreach (var segment in segments)
            {
                var segmentPadding = segment.Start - currentStart;
                if (segmentPadding > 0)
                {
                    var padding = Enumerable.Range(0, segmentPadding).Select(s => default(T));

                    foreach (var item in padding)
                    {
                        yield return item;
                    }
                }

                foreach (var item in segment.SegmentData)
                {
                    yield return item;
                }

                currentStart = segment.End + 1;
            }
        }
    }
}
