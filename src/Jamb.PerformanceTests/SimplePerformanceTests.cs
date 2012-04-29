using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Jamb.PerformanceTests
{
    [TestFixture]
    public class SimplePerformanceTests
    {
        [Test]
        public void ShouldBuildDataColumns()
        {
            var builder = new DataColumnBuilder();
            var range = Enumerable.Range(0, 500000).Select(i => i % 2 == 0 ? null : (int?)i);

            var ms = Instrument.Do(() => builder.Build(range));

            Console.WriteLine("Built data column in {0}ms", ms);
        }

        [Test]
        public void ShouldPopulateTable()
        {
            var table = new Table();

            var ms = Instrument.Do(() =>
            {
                ConstructStringColumns(table, 100, 50000);
                ConstructDecimalColumns(table, 100, 50000);
            });

            Console.WriteLine(
                "Built table with {0} columns in {1}ms",
                table.NumColumns,
                ms);
        }

        private void ConstructStringColumns(Table table, int numColumns, int numRows)
        {
            Parallel.For(0, numColumns, i =>
            {
                var strings = Enumerable.Range(0, numRows).Select(s => "string" + s);
                table.CreateColumn("string" + i, strings);
            });
        }

        private void ConstructDecimalColumns(Table table, int numColumns, int numRows)
        {
            Parallel.For(0, numColumns, i =>
            {
                var decimals = Enumerable.Range(0, numRows).Select(s => (decimal?)s);
                table.CreateColumn("decimal" + i, decimals);
            });
        }
    }
}
