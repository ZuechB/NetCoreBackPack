namespace NetCoreBackPack.Currency
{
    public class Dollars
    {
        public static long ConvertToCents(decimal dollar)
        {
            long cents = (long)(dollar * 100m);
            return cents;
        }

        public static decimal CentsToDollar(long cents)
        {
            decimal dollar = (decimal)(cents / 100m);
            return dollar;
        }
    }
}