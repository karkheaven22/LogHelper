namespace LogHelper
{
    public static partial class ExtensionCommon
    {
        public static string ToCapitalize(this String value) => System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(value);

        public static long UnixTimeSeconds() => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        public static DateTime UnixDateTime(this long value) => DateTimeOffset.FromUnixTimeSeconds(value).UtcDateTime;

        public static int DayOfMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            int LastDay = value.DayOfMonth();
            return new DateTime(value.Year, value.Month, LastDay);
        }

        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime SetDayMonth(this DateTime value, int DayValue)
        {
            int daysInMonth = value.DayOfMonth();
            int day = Math.Min(DayValue, daysInMonth);
            return new DateTime(value.Year, value.Month, day);
        }

        public static char ToHexChar(int value)
        {
            return (char)(value + (value < 10 ? '0' : 'a' - 10));
        }

        private static bool TryParseHexChar(char c, out int value)
        {
            if (c >= '0' && c <= '9')
            {
                value = c - '0';
                return true;
            }

            if (c >= 'a' && c <= 'f')
            {
                value = 10 + (c - 'a');
                return true;
            }

            if (c >= 'A' && c <= 'F')
            {
                value = 10 + (c - 'A');
                return true;
            }

            value = 0;
            return false;
        }

        private static bool TryParseHexString(string s, out byte[] bytes)
        {
            bytes = [];
            if (s == null)
            {
                return false;
            }

            var buffer = new byte[(s.Length + 1) / 2];

            var i = 0;
            var j = 0;

            if ((s.Length % 2) == 1)
            {
                // if s has an odd length assume an implied leading "0"
                int y;
                if (!TryParseHexChar(s[i++], out y))
                {
                    return false;
                }
                buffer[j++] = (byte)y;
            }

            while (i < s.Length)
            {
                int x, y;
                if (!TryParseHexChar(s[i++], out x))
                {
                    return false;
                }
                if (!TryParseHexChar(s[i++], out y))
                {
                    return false;
                }
                buffer[j++] = (byte)((x << 4) | y);
            }

            bytes = buffer;
            return true;
        }

        public static string ObjectId(this int value)
        {
            var timeStamp = (int)ExtensionCommon.UnixTimeSeconds();
            char[] bytes = new char[16];
            bytes[0] = ToHexChar((timeStamp >> 28) & 0x0f);
            bytes[1] = ToHexChar((timeStamp >> 24) & 0x0f);
            bytes[2] = ToHexChar((timeStamp >> 20) & 0x0f);
            bytes[3] = ToHexChar((timeStamp >> 16) & 0x0f);
            bytes[4] = ToHexChar((timeStamp >> 12) & 0x0f);
            bytes[5] = ToHexChar((timeStamp >> 8) & 0x0f);
            bytes[6] = ToHexChar((timeStamp >> 4) & 0x0f);
            bytes[7] = ToHexChar(timeStamp & 0x0f);
            bytes[8] = ToHexChar((value >> 28) & 0x0f);
            bytes[9] = ToHexChar((value >> 24) & 0x0f);
            bytes[10] = ToHexChar((value >> 20) & 0x0f);
            bytes[11] = ToHexChar((value >> 16) & 0x0f);
            bytes[12] = ToHexChar((value >> 12) & 0x0f);
            bytes[13] = ToHexChar((value >> 8) & 0x0f);
            bytes[14] = ToHexChar((value >> 4) & 0x0f);
            bytes[15] = ToHexChar(value & 0x0f);
            return new string(bytes);
        }

        public static (DateTime, int) ParseObjectId(this string objectId)
        {
            if (objectId == null || objectId.Length != 16)
            {
                throw new ArgumentException("Invalid objectId format", nameof(objectId));
            }

            if (TryParseHexString(objectId, out byte[] bytes))
            {
                int timestamp = (bytes[0] << 24) | (bytes[0 + 1] << 16) | (bytes[0 + 2] << 8) | bytes[0 + 3];
                int value = (bytes[0 + 4] << 24) | (bytes[0 + 5] << 16) | (bytes[0 + 6] << 8) | bytes[0 + 7];
                return (UnixDateTime((long)timestamp), value);
            }
            return (DateTime.UtcNow, 0);
        }
    }
}
