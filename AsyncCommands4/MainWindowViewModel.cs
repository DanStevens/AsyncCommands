using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        Url = "http://www.example.com/";
        CountUrlBytesCommand = AsyncCommand.Create(token => MyService.DownloadAndCountBytesAsync(Url, token));
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