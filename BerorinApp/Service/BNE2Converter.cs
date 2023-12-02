namespace BerorinApp
{
    public class BNE2Converter
    {
        public bool ConvertBne2SettingFiles(DirectoryInfo source)
        {
            var dest = Util.Desktop.SubDirectory(source.Name);

            if (!dest.Exists)
                dest.Create();

            foreach (var finfo in source.GetFiles())
            {
                var name = finfo.GetFileNameWithoutExtension();

                switch (finfo.Extension.ToLower())
                {
                    case ".ini":
                        var recipes = LoadBne2SettingFile(finfo.FullName);
                        Util.YamlSave(dest.Combine($"{name}.yml"), recipes);
                        break;

                    case ".idn":
                        var masters = LoadBne2ListFile(finfo.FullName);
                        File.WriteAllLines(dest.Combine($"{name}.idn"), masters.Select(x => x.ToString()));
                        break;
                }
            }

            return true;
        }

        private Recipes LoadBne2SettingFile(string path)
        {
            var lines = File.ReadAllLines(path, Util.Shift_JIS).Select(x => x.RegexReplace("//.*$", ""));

            var converters = Util.CsvRead<IniConverter>(lines, true);
            var recipes = converters.Select(x => x.ConvertRecipe()).ToList();

            return new Recipes(recipes);
        }

        private Masters LoadBne2ListFile(string path)
        {
            var lines = File.ReadAllLines(path, Util.Shift_JIS).Select(x => x.Trim());

            var masters = lines.Select(x => IdnConvertMaster(x)).ToList();

            return new Masters(masters);
        }

        private Master IdnConvertMaster(string text)
        {
            if (text.Match(@"^([0-9A-Fa-f]+)\s+(.*)", out var hex, out var name))
            {
                if (hex == name)
                    name = "";

                return new Master() { Hex = hex, Name = name };
            }
            else if (text.Match("^([0-9A-Fa-f]{1,4})$", out hex, out _))
            {
                return new Master() { Hex = hex, Name = "" };
            }
            else
            {
                return new Master() { Name = text };
            }
        }
    }
}
