using System.Collections.ObjectModel;

namespace BerorinApp
{
    public class SettingPoco
    {
        public string Binary { get; set; }

        public ObservableCollection<string> RecipeFolders { get; set; } = new();
    }
}
