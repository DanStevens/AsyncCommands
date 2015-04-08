using System.ComponentModel;
using System.Runtime.CompilerServices;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        Url = "http://www.example.com/";
        CountUrlBytesCommand = AsyncCommand.Create(() => MyService.DownloadAndCountBytesAsync(Url));
    }

    private string _url;
    public string Url
    {
        get { return _url; }
        set
        {
            _url = value;
            OnPropertyChanged();
        }
    }

    public IAsyncCommand CountUrlBytesCommand { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
}