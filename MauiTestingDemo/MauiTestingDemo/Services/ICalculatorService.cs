namespace MauiTestingDemo.Services
{
    public interface ICalculatorService
    {
        int Increment(int value);

        decimal Sum(decimal summand1, decimal summand2);
    }
}
