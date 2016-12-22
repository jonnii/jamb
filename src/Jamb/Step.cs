namespace Jamb
{
    public interface IStep
    {
    }

    public class Step
    {
        public interface IStepBuilder
        {
            IStepBuilder WithCalculator(MarketValueCalculator marketValueCalculator);
        }

        public class StepBuilder : IStepBuilder
        {
            public StepBuilder(string name)
            {
            }

            public IStepBuilder WithCalculator(MarketValueCalculator marketValueCalculator)
            {
                return this;
            }
        }

        public static IStepBuilder WithName(string name)
        {
            return new StepBuilder(name);
        }

        public Step(string name, ICalculator calculator)
        {
        }

        public string Name { get; }


    }
}
