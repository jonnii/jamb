using System.Collections.Generic;
using System.Linq;
using Jamb.Extensions;

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

        public IEnumerable<T> GetEnumerable(int? length = null)
        {
            var currentStart = 0;
            foreach (var segment in segments)
            {
                var segmentPadding = segment.Start - currentStart;
                if (segmentPadding > 0)
                {
                    foreach (var item in Padding.Create<T>(segmentPadding))
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

            if (!length.HasValue || !(currentStart < length))
            {
                yield break;
            }

            foreach (var item in Padding.Create<T>(length.Value - currentStart))
            {
                yield return item;
            }
        }
    }
}
