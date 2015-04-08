using System.ComponentModel;
using System.Runtime.CompilerServices;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        Url = "http://www.example.com/";
        CountUrlBytesCommand = new AsyncCommand(async () =>
        {
            ByteCount = await MyService.DownloadAndCountBytesAsync(Url);
        });
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

    private int _byteCount;
    public int ByteCount
    {
        get { return _byteCount; }
        private set
        {
            _byteCount = value; 
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
}