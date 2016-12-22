using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class SampleCalculator : ICalculator<double, double>
    {
        public ICalculationResult Execute(IEnumerable<double> quantity, IEnumerable<double> price)
        {
            var mv = quantity.Zip(price, (q, p) => q * p);

            return CalculationResult.Create(nameof(mv), mv);
        }
    }
}