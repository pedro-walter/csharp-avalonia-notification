using System;
using System.Windows.Input;
using Avalonia.Controls;
using CrossPlatformNotification.Services;

namespace CrossPlatformNotification.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NotificationService _notificationService;

    public MainWindowViewModel(NotificationService notificationService)
    {
        _notificationService = notificationService;
        ShowNotificationCommand = new RelayCommand(ShowNotification);
    }

    public MainWindowViewModel() : this(new NotificationService())
    {
    }

    public ICommand ShowNotificationCommand { get; }

    private void ShowNotification()
    {
        var message = _notificationService.GetCurrentTimeMessage();
        _notificationService.ShowNotification("Notificação", message);
    }
}

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object? parameter) => _execute();

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}