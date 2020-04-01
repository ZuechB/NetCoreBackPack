using System;

namespace NetCoreBackPack.MathBackpack
{
    public static class MoneyExtender
    {
        public static int ConvertToCents(this decimal amount)
        {
            var change = amount * 100;
            var newchange = Convert.ToInt32(change);
            return newchange;
        }

        public static decimal ConvertToDollars(this int cents)
        {
            decimal dollar = cents * 0.01m;
            return dollar;
        }
    }
}