using Avalonia.Controls;
using CrossPlatformNotification.ViewModels;
using System;
using Avalonia.Platform;

namespace CrossPlatformNotification.Views;

public partial class MainWindow : Window
{
    private TrayIcon? _trayIcon;

    public MainWindow()
    {
        InitializeComponent();
        InitializeTrayIcon();
        
        Closing += (s, e) =>
        {
            e.Cancel = true;
            Hide();
        };
    }

    private void InitializeTrayIcon()
    {
        _trayIcon = new TrayIcon
        {
            ToolTipText = "Cross Platform Notification"
        };

        _trayIcon.Clicked += (s, e) => ShowWindow();

        var menu = new NativeMenu();
        
        var showItem = new NativeMenuItem("Exibir Janela");
        showItem.Click += (s, e) => ShowWindow();
        
        var exitItem = new NativeMenuItem("Fechar");
        exitItem.Click += (s, e) => Environment.Exit(0);
        
        menu.Add(showItem);
        menu.Add(new NativeMenuItemSeparator());
        menu.Add(exitItem);
        
        _trayIcon.Menu = menu;
    }

    private void ShowWindow()
    {
        Show();
        WindowState = WindowState.Normal;
        Activate();
    }

    protected override void OnClosed(EventArgs e)
    {
        _trayIcon?.Dispose();
        base.OnClosed(e);
    }
}