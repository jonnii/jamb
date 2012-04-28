using System.Collections.Generic;

namespace Jamb
{
    public class DataColumnSegment<T>
    {
        public DataColumnSegment(int start, ICollection<T> segmentData)
        {
            Start = start;
            SegmentData = segmentData;
        }

        public int Start { get; private set; }

        public ICollection<T> SegmentData { get; private set; }

        public int End
        {
            get { return Start + SegmentData.Count - 1; }
        }
    }
}