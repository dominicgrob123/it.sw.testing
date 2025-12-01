namespace MoqedCalculator
{
    public enum Currency
    {
        UNKNOWN = 0,
        CHF,
        USD,
    }

    public interface IExchangeRateProvider
    {
        double GetRate(Currency dst, Currency per);
    }
}