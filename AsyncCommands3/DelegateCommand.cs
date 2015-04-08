using System;
using System.Windows.Input;

public sealed class DelegateCommand : ICommand
{
    private readonly Action _command;

    public DelegateCommand(Action command)
    {
        _command = command;
    }

    public void Execute(object parameter)
    {
        _command();
    }

    bool ICommand.CanExecute(object parameter)
    {
        return true;
    }

    event EventHandler ICommand.CanExecuteChanged
    {
        add { }
        remove { }
    }
}