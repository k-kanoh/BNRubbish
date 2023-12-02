using Csv;
using System.Security.Cryptography;
using System.Text;
using YamlDotNet.Serialization;

namespace BerorinApp
{
    public static class Util
    {
        public static readonly Encoding Shift_JIS;

        static Util()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Shift_JIS = Encoding.GetEncoding(932);
        }

        /// <summary>
        /// デスクトップ
        /// </summary>
        public static DirectoryInfo Desktop => new(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

        /// <summary>
        /// ベースディレクトリ
        /// </summary>
        public static DirectoryInfo BaseDir => new(AppDomain.CurrentDomain.BaseDirectory);

        public static string GetSha1Hash(byte[] bytes)
        {
            return string.Join("", SHA1.HashData(bytes).Select(x => x.ToString("x2")));
        }

        public static Keys KeyParse(Keys keyData, out bool ctrl, out bool alt, out bool shift)
        {
            alt = (keyData & Keys.Modifiers & Keys.Alt) == Keys.Alt;
            ctrl = (keyData & Keys.Modifiers & Keys.Control) == Keys.Control;
            shift = (keyData & Keys.Modifiers & Keys.Shift) == Keys.Shift;
            return keyData & Keys.KeyCode;
        }

        /// <summary>
        /// StringBuilder のインスタンスを返します。
        /// </summary>
        public static StringBuilder CreateStringBuilder()
        {
            return new StringBuilder();
        }

        private static readonly ISerializer YamlSerializer =
            new SerializerBuilder().ConfigureDefaultValuesHandling((DefaultValuesHandling)7).Build();

        public static void YamlSave(string path, object obj)
        {
            File.WriteAllText(path, YamlSerializer.Serialize(obj));
        }

        private static readonly IDeserializer YamlDeserializer = new DeserializerBuilder().Build();

        public static T YamlLoadOrNew<T>(string path) where T : new()
        {
            if (!File.Exists(path))
                return new T();

            var obj = YamlDeserializer.Deserialize<T>(File.ReadAllText(path));
            return (obj == null) ? new T() : obj;
        }

        public static List<T> CsvRead<T>(IEnumerable<string> lines, bool skipHeaderRow = false) where T : new()
        {
            var props = from p in typeof(T).GetProperties()
                        where p.AnyCustomAttribute<CsvFieldAttribute>()
                        select new { prop = p, p.FirstCustomAttribute<CsvFieldAttribute>().FieldNo };

            var list = new List<T>();

            var options = new CsvOptions()
            {
                SkipRow = (row, idx) => row.Span.IsEmpty,
                TrimData = true,
                HeaderMode = skipHeaderRow ? HeaderMode.HeaderAbsent : HeaderMode.HeaderPresent
            };

            foreach (var line in CsvReader.ReadFromText(string.Join("\r\n", lines), options))
            {
                var instance = new T();

                foreach (var p in props)
                    p.prop.SetValue(instance, line[p.FieldNo]);

                list.Add(instance);
            }

            return list;
        }

        public static string CsvWrite<T>(IEnumerable<T> data, bool skipHeaderRow = false)
        {
            var headers = from p in typeof(T).GetProperties()
                          where p.AnyCustomAttribute<CsvFieldAttribute>()
                          orderby p.FirstCustomAttribute<CsvFieldAttribute>().FieldNo
                          select p.Name;

            var contents = data.Select(x => (from p in x.GetType().GetProperties()
                                             where p.AnyCustomAttribute<CsvFieldAttribute>()
                                             orderby p.FirstCustomAttribute<CsvFieldAttribute>().FieldNo
                                             select (string)p.GetValue(x) ?? "").ToArray());

            return CsvWriter.WriteToText(headers.ToArray(), contents, skipHeaderRow: skipHeaderRow);
        }
    }
}
