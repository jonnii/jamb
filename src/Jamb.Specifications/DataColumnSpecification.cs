using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Jamb.Specifications
{
    public class DataColumnSpecification
    {
        [Subject(typeof(DataColumn<>))]
        public class when_column_has_single_segment
        {
            Establish context = () =>
            {
                var segments = new List<DataColumnSegment<int>>
                {
                    new DataColumnSegment<int>(0, new []{1,2,3,4,5})
                };

                column = new DataColumn<int>(segments);
            };

            It should_calculate_length = () =>
                column.Length.ShouldEqual(5);

            static DataColumn<int> column;
        }

        [Subject(typeof(DataColumn<>))]
        public class when_column_has_multiple_segments
        {
            Establish context = () =>
            {
                var segments = new List<DataColumnSegment<int?>>
                {
                    new DataColumnSegment<int?>(0, new int?[]{0,1,2,3,4}),
                    new DataColumnSegment<int?>(10, new int?[]{5,6,7,8,9})
                };

                column = new DataColumn<int?>(segments);
            };

            It should_calculate_length = () =>
                column.Length.ShouldEqual(15);

            It should_have_same_enumerable_count_as_length = () =>
                column.GetEnumerable().Count().ShouldEqual(15);

            It should_get_item_at_start = () =>
                column.GetEnumerable().ElementAt(0).ShouldEqual(0);

            It should_get_item_at_end_of_first_segment = () =>
                column.GetEnumerable().ElementAt(4).ShouldEqual(4);

            It should_get_item_in_middle_of_dead_space = () =>
                column.GetEnumerable().ElementAt(7).ShouldBeNull();

            It should_get_item_at_start_of_second_segment = () =>
                column.GetEnumerable().ElementAt(10).ShouldEqual(5);

            It should_get_last_item = () =>
                column.GetEnumerable().Last().ShouldEqual(9);

            static DataColumn<int?> column;
        }
    }
}
