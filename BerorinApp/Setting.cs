namespace BerorinApp
{
    public static class Setting
    {
        public static List<SettingPoco> History { get; private set; }

        public static SettingPoco Current { get; private set; } = new();

        private static readonly string settingYaml = Util.BaseDir.Combine("Setting.yml");

        static Setting()
        {
            Load();
        }

        public static void Save()
        {
            Util.YamlSave(settingYaml, History);
        }

        public static void Load()
        {
            History = Util.YamlLoadOrNew<List<SettingPoco>>(settingYaml);
        }

        public static void ChangeCurrent(string binary)
        {
            var sameInHistory = History.FirstOrDefault(x => x.Binary == binary);

            if (sameInHistory != null)
            {
                History.Remove(sameInHistory);
                History.Insert(0, sameInHistory);
                Save();
            }
            else
            {
                History.Insert(0, new SettingPoco() { Binary = binary });
                Save();
            }

            Current = History[0];
            Current.RecipeFolders.CollectionChanged += (s, e) => Save();
        }
    }
}
