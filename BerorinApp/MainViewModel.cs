using System.ComponentModel;

namespace BerorinApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public List<(string, List<DataPart>)> Data { get; private set; }

        public MainViewModel()
        {
            Setting.Current.RecipeFolders.CollectionChanged += (s, e) => GetRecipeList();
            GetRecipeList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void GetRecipeList()
        {
            Data = Setting.Current.RecipeFolders.Select(x => new DirectoryInfo(x)).Where(x => x.Exists)
                    .Select(x => (x.Name, x.GetFiles("*.yml").Select(x => new DataPart(x)).ToList())).ToList();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Data)));
        }
    }
}
