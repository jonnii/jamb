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

        public class GenerateFullName : ITableProcessor
        {
            public void Run(IDataAdapter dataAdapter)
            {
                var firstnames = dataAdapter.GetData<string>("firstname");
                var lastnames = dataAdapter.GetData<string>("lastname");

                var fullnames = firstnames.Zip(lastnames, FormatFullName);

                dataAdapter.CreateColumn("fullname", fullnames);
            }

            private string FormatFullName(string firstname, string lastname)
            {
                return string.Concat(firstname, " ", lastname);
            }
        }
    }
}