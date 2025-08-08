using System;

namespace CrossPlatformNotification.Services;

public class NotificationService
{
    public void ShowNotification(string title, string message)
    {
        if (OperatingSystem.IsWindows())
        {
            ShowWindowsNotification(title, message);
        }
        else if (OperatingSystem.IsLinux())
        {
            ShowLinuxNotification(title, message);
        }
        else if (OperatingSystem.IsMacOS())
        {
            ShowMacOSNotification(title, message);
        }
    }

    private void ShowWindowsNotification(string title, string message)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "powershell",
            Arguments = $"-Command \"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null; [Windows.Data.Xml.Dom.XmlDocument, Windows.Data.Xml.Dom.XmlDocument, ContentType = WindowsRuntime] | Out-Null; $template = [Windows.UI.Notifications.ToastTemplateType]::ToastText02; $toastXml = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent($template); $toastXml.GetElementsByTagName('text')[0].AppendChild($toastXml.CreateTextNode('{title}')) | Out-Null; $toastXml.GetElementsByTagName('text')[1].AppendChild($toastXml.CreateTextNode('{message}')) | Out-Null; $toast = [Windows.UI.Notifications.ToastNotification]::new($toastXml); [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('CrossPlatformNotification').Show($toast);\"",
            UseShellExecute = false,
            CreateNoWindow = true
        });
    }

    private void ShowLinuxNotification(string title, string message)
    {
        System.Diagnostics.Process.Start("notify-send", $"\"{title}\" \"{message}\"");
    }

    private void ShowMacOSNotification(string title, string message)
    {
        System.Diagnostics.Process.Start("osascript", $"-e \"display notification \\\"{message}\\\" with title \\\"{title}\\\"\"");
    }

    public string GetCurrentTimeMessage()
    {
        return $"Eu sou uma notificação feita as {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
    }
}