using System;

namespace NetCoreBackPack.TimeBackpack
{
    public class SystemTime
    {
        public static DateTimeOffset Now
        {
            get
            {
                return DateTimeOffset.UtcNow.LocalDateTime;
            }
        }
    }
}