using Machine.Specifications;

namespace Jamb.Specifications
{
    public class DataColumnBuilderSpecification
    {
        [Subject(typeof(DataColumnBuilder))]
        public class when_created_from_array : with_builder
        {
            Because of = () =>
                column = builder.Build(new[] { 1, 2, 3, 4, 5 });

            It should_have_length = () =>
                column.Length.ShouldEqual(5);

            It should_have_single_segment = () =>
                column.NumSegments.ShouldEqual(1);

            static DataColumn<int> column;
        }

        [Subject(typeof(DataColumnBuilder))]
        public class when_created_from_array_of_reference_types : with_builder
        {
            Because of = () =>
                column = builder.Build(new[] { "a", "b", "c", "d", "e" });

            It should_have_length = () =>
                column.Length.ShouldEqual(5);

            It should_have_single_segment = () =>
                column.NumSegments.ShouldEqual(1);

            static DataColumn<string> column;
        }

        [Subject(typeof(DataColumnBuilder))]
        public class when_created_from_array_of_reference_types_with_nulls : with_builder
        {
            Because of = () =>
                column = builder.Build(new[] { "a", "b", null, null, null, null, "d", "e" });

            It should_have_length = () =>
                column.Length.ShouldEqual(8);

            It should_have_multiple_segment = () =>
                column.NumSegments.ShouldEqual(2);

            static DataColumn<string> column;
        }

        [Subject(typeof(DataColumnBuilder))]
        public class when_created_from_array_of_reference_types_with_nulls_with_gap_less_than_tollerance : with_builder
        {
            Establish context = () =>
                builder.DefaultValueTolerance = 4;

            Because of = () =>
                column = builder.Build(new[] { "a", "b", null, null, null, null, "d", "e" });

            It should_have_length = () =>
                column.Length.ShouldEqual(8);

            It should_have_single_segment = () =>
                column.NumSegments.ShouldEqual(1);

            static DataColumn<string> column;
        }

        public class with_builder
        {
            Establish context = () =>
                builder = new DataColumnBuilder
                {
                    DefaultValueTolerance = 2
                };

            protected static DataColumnBuilder builder;
        }
    }
}