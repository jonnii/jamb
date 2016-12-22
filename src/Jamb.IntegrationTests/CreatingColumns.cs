using System.Linq;
using NUnit.Framework;

namespace Jamb.IntegrationTests
{
    [TestFixture]
    public class CreatingColumns
    {
        [Test]
        public void Should()
        {
            var step = new Step("Calculate Market Value", new SampleCalculator());
        }

        [Test]
        public void ShouldCreateColumnWithoutData()
        {
            var table = new Table();

            var columnDefinition = table.CreateColumn<string>("firstname");

            Assert.That(columnDefinition.Name, Is.EqualTo("firstname"));
            Assert.That(columnDefinition.UnderlyingType, Is.EqualTo(typeof(string)));

            var data = table.GetData<string>("firstname");

            Assert.That(data.Count(), Is.EqualTo(0));
        }

        [Test]
        public void ShouldCreateColumnWithData()
        {
            var table = new Table();

            var columnDefinition = table.CreateColumn("firstname", new[] { "bob", "ben" });

            Assert.That(columnDefinition.Name, Is.EqualTo("firstname"));
            Assert.That(columnDefinition.UnderlyingType, Is.EqualTo(typeof(string)));
            Assert.That(columnDefinition.HasData);

            var data = table.GetData<string>("firstname");

            Assert.That(data.Count(), Is.EqualTo(2));
        }
    }
}
