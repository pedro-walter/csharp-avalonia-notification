using System.Windows.Input;
using Avalonia.Controls;
using ReactiveUI;
using CrossPlatformNotification.Services;

namespace CrossPlatformNotification.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NotificationService _notificationService;

    public MainWindowViewModel(NotificationService notificationService)
    {
        _notificationService = notificationService;
        ShowNotificationCommand = ReactiveCommand.Create(ShowNotification);
    }


    public ICommand ShowNotificationCommand { get; }

    private void ShowNotification()
    {
        var message = _notificationService.GetCurrentTimeMessage();
        _notificationService.ShowNotification("Notificação", message);
    }
}