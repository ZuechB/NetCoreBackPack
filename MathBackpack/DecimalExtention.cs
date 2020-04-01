using System;

namespace NetCoreBackPack.MathBackpack
{
    public static class DecimalExtention
    {
        public static decimal GetDecimalsValues(this decimal number)
        {
            var x = number - Math.Truncate(number);
            return x;
        }

        public static int GetWholeNumber(this decimal number)
        {
            var x = number - Math.Truncate(number);
            var wholeNum = number - x;
            return (int)wholeNum;
        }
    }
}