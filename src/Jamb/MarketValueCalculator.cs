using System.Collections.Generic;
using System.Linq;

namespace Jamb
{
    public class MarketValueCalculator : ICalculator<double, double>
    {
        public ICalculationResult Execute(IEnumerable<double> quantity, IEnumerable<double> price)
        {
            var marketValue = quantity.Zip(price, (q, p) => q * p);

            return CalculationResult.Create(nameof(marketValue), marketValue);
        }
    }
}