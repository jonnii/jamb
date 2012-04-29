using System.Linq;
using NUnit.Framework;

namespace Jamb.IntegrationTests
{
    [TestFixture]
    public class Processing
    {
        [Test]
        public void ApplyingProcessorToEntireTable()
        {
            var table = new Table();

            table.CreateColumn("firstname", new[] { "bob", "ben" });
            table.CreateColumn("lastname", new[] { "smith", "jenkins" });

            table.Apply(new GenerateFullName());

            var fullnames = table.GetData<string>("fullname");

            Assert.That(fullnames.Count(), Is.EqualTo(2));
            Assert.That(fullnames.First(), Is.EqualTo("bob smith"));
        }

        [Test]
        public void ApplyingProcessorToEntireTableWithColumnsOfDifferentLengths()
        {
            var table = new Table();

            table.CreateColumn("firstname", new[] { "bob", "ben", "bill" });
            table.CreateColumn("lastname", new[] { "smith", "jenkins" });

            table.Apply(new GenerateFullName());

            var fullnames = table.GetData<string>("fullname");

            Assert.That(fullnames.Count(), Is.EqualTo(3));
            Assert.That(fullnames.Last(), Is.EqualTo("bill unknown"));
        }

        [Test]
        public void ApplyingProcessorToSingleColumn()
        {
            var table = new Table();

            table.CreateColumn("percentage", new[] { 1.0m, 0.8m, 0.35m });
            table.Apply(new MultiplyPercentages());

            var columnHeader = table.GetColumnHeader("percentage");

            Assert.That(columnHeader.DataColumns.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ApplyProcessorShort()
        {
            var table = new Table();

            var column = table.CreateColumn("nums", new int?[] { 3, 4, 5, 6, null, null, null, null, 3, 5 });

            table.Apply(column, i => i * 2);

            Assert.That(table.GetData(column).First(), Is.EqualTo(6));
        }

        public class MultiplyPercentages : ITableProcessor
        {
            public void Run(ITableFrame tableFrame)
            {
                var percentages = tableFrame.GetData<decimal>("percentage");

                var normalized = percentages.Select(p => p * 100m);

                tableFrame.SetData("percentage", normalized);
            }
        }

        public class GenerateFullName : ITableProcessor
        {
            public void Run(ITableFrame tableFrame)
            {
                var firstnames = tableFrame.GetData<string>("firstname");
                var lastnames = tableFrame.GetData<string>("lastname");

                var fullnames = firstnames.Zip(lastnames, FormatFullName);

                tableFrame.CreateColumn("fullname", fullnames);
            }

            private string FormatFullName(string firstname, string lastname)
            {
                return string.Concat(firstname, " ", lastname ?? "unknown");
            }
        }
    }
}