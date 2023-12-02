using System.ComponentModel;

namespace BerorinApp
{
    public class PartsViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DataPart Data { get; set; }
    }
}
