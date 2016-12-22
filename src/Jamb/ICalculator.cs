namespace Jamb
{
    public interface ICalculator { }

    public interface ICalculator<T1> : ICalculator
    {
    }

    public interface ICalculator<T1, T2> : ICalculator
    {
    }
}