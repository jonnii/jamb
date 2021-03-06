﻿using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;

namespace Jamb.Specifications
{
    public class TableSpecifications
    {
        [Subject(typeof(Table))]
        public class when_definining_column
        {
            Establish context = () =>
                table = new Table();

            Because of = () =>
                header = table.CreateColumn<string>("firstname");

            It should_set_header_name = () =>
                header.Name.ShouldEqual("firstname");

            It should_set_header_underlying_type = () =>
                header.UnderlyingType.ShouldEqual(typeof(string));

            It should_define_column_without_data = () =>
                header.HasData.ShouldBeFalse();

            It should_set_data_column_index = () =>
                header.DataColumns.Any().ShouldBeFalse();

            static Table table;

            static IColumnHeader header;
        }

        [Subject(typeof(Table))]
        public class when_defining_column_that_already_exists : with_table
        {
            Because of = () =>
                exception = Catch.Exception(() => table.CreateColumn<string>("firstname"));

            It should_throw_argument_exception = () =>
                exception.ShouldBeOfExactType<ArgumentException>();

            static Exception exception;
        }

        [Subject(typeof(Table))]
        public class when_getting_data_for_empty_reference_type_column : with_table
        {
            Establish context = () =>
                table.CreateColumn("creditscore", new[] { 1, 2, 3, 4, 5 });

            Because of = () =>
                data = table.GetData<string>("firstname");

            It should_return_enumerable_with_same_number_rows_as_table = () =>
                data.Count().ShouldEqual(5);

            It should_return_all_null_values = () =>
                data.ShouldEachConformTo(d => d == null);

            static IEnumerable<string> data;
        }

        [Subject(typeof(Table))]
        public class when_getting_data_for_empty_value_type_column : with_table
        {
            Because of = () =>
                data = table.GetData<int>("age");

            It should_return_all_default_values = () =>
                data.ShouldEachConformTo(d => d == 0);

            static IEnumerable<int> data;
        }

        [Subject(typeof(Table))]
        public class when_creating_column_with_data
        {
            Establish context = () =>
                table = new Table();

            Because of = () =>
                column = table.CreateColumn("name", new[] { 1, 2 });

            It should_create_column_with_data = () =>
                column.HasData.ShouldBeTrue();

            It should_set_data_column_index = () =>
                column.DataColumns.First().ShouldEqual(0);

            static Table table;

            static IColumnHeader column;
        }

        [Subject(typeof(Table))]
        public class when_applying_processor : with_table
        {
            Establish context = () =>
                processor = An<ITableProcessor>();

            Because of = () =>
                table.Apply(processor);

            It should_run_processor = () =>
                processor.WasToldTo(p => p.Run(Param.IsAny<Table>()));

            static ITableProcessor processor;
        }

        public class with_table : WithFakes
        {
            Establish context = () =>
            {
                table = new Table();
                table.CreateColumn<string>("firstname");
                table.CreateColumn<int>("age");
            };

            protected static Table table;
        }
    }
}
