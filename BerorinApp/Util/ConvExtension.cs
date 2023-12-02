using System.Text.RegularExpressions;

namespace BerorinApp
{
    public static partial class ConvExtension
    {
        [GeneratedRegex("([0-9A-Fa-f]{2})")]
        private static partial Regex HexMatch();

        /// <summary>
        /// Hex -> int
        /// </summary>
        public static int HexToInt(this string value)
        {
            return Convert.ToInt32(value, 16);
        }

        /// <summary>
        /// Hex -> byte
        /// </summary>
        public static byte HexToByte(this string value)
        {
            return (byte)value.HexToInt();
        }

        /// <summary>
        /// Hex -> byteArray
        /// </summary>
        public static byte[] HexToByteArray(this string value)
        {
            if (value.Length % 2 == 1)
                value = "0" + value;

            return HexMatch().Matches(value).Select(x => x.Value.HexToByte()).ToArray();
        }

        /// <summary>
        /// int -> Hex
        /// </summary>
        public static string ToHex(this int value)
        {
            return value.ToString("X2");
        }

        /// <summary>
        /// byte -> int
        /// </summary>
        public static int ToInt(this byte value)
        {
            return value;
        }

        /// <summary>
        /// byte -> Hex
        /// </summary>
        public static string ToHex(this byte value)
        {
            return value.ToString("X2");
        }
    }
}
