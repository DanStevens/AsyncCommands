using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public sealed class CountUrlBytesViewModel
{
    public CountUrlBytesViewModel(MainWindowViewModel parent, string url, IAsyncCommand command)
    {
        LoadingMessage = "Loading (" + url + ")...";
        Command = command;
        RemoveCommand = new DelegateCommand(() => parent.Operations.Remove(this));
    }

    public string LoadingMessage { get; private set; }

    public IAsyncCommand Command { get; private set; }

    public ICommand RemoveCommand { get; private set; }
}

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        Url = "http://www.example.com/";
        Operations = new ObservableCollection<CountUrlBytesViewModel>();
        CountUrlBytesCommand = new DelegateCommand(() =>
        {
            var countBytes = AsyncCommand.Create(token => MyService.DownloadAndCountBytesAsync(Url, token));
            countBytes.Execute(null);
            Operations.Add(new CountUrlBytesViewModel(this, Url, countBytes));
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

    public ObservableCollection<CountUrlBytesViewModel> Operations { get; private set; }

    public ICommand CountUrlBytesCommand { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
}