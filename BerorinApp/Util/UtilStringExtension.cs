using System.Text;
using System.Text.RegularExpressions;

namespace BerorinApp
{
    public static class UtilStringExtension
    {
        /// <summary>
        /// 文字列を "!string.IsNullOrEmpty(文字列)" で判定します。
        /// </summary>
        public static bool Val(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 文字列が null もしくは Empty の場合 null を返します。
        /// それ以外は文字列をそのまま返します。
        /// </summary>
        public static string ValOrNull(this string value)
        {
            return value.Val() ? value : null;
        }

        /// <summary>
        /// 文字列が指定された正規表現にマッチするか判定します。
        /// </summary>
        public static bool IsMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// 文字列を正規表現でキャプチャします。 (0起算)
        /// </summary>
        public static string Match(this string value, string pattern, int index = 0)
        {
            return Regex.Match(value, pattern).Groups.Cast<Group>().Select(x => x.Value).Skip(index + 1).FirstOrDefault() ?? "";
        }

        /// <summary>
        /// 文字列を正規表現でキャプチャします。
        /// </summary>
        public static bool Match(this string value, string pattern, out string grp1, out string grp2)
        {
            var m = Regex.Match(value, pattern);
            var groups = m.Groups.Cast<Group>().Select(x => x.Value).Skip(1).ToList();
            grp1 = (groups.Count > 0) ? groups[0] : "";
            grp2 = (groups.Count > 1) ? groups[1] : "";

            return m.Success;
        }

        /// <summary>
        /// 文字列を正規表現で置き換えします。
        /// </summary>
        public static string RegexReplace(this string value, string pattern, string replacement)
        {
            return Regex.Replace(value, pattern, replacement);
        }

        /// <summary>
        /// 文字列をInt型にキャストします。失敗した場合は null を返します。
        /// </summary>
        public static int? ToInt(this string source)
        {
            return (source.Val() && int.TryParse(source, out int value)) ? value : default(int?);
        }

        public static string GetSha1Hash(this string source)
        {
            return Util.GetSha1Hash(Encoding.UTF8.GetBytes(source));
        }
    }
}
