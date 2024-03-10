namespace MauiTestingDemo.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Increment(int value)
        {
            return ++value;
        }

        public decimal Sum(decimal summand1, decimal summand2)
        {
            return summand1 + summand2;
        }
    }
}
