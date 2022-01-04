using System.IO;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace MovieMatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Browser/Trailer autoplay
            var settings = new CefSettings();
            settings.CefCommandLineArgs["autoplay-policy"] = "no-user-gesture-required";
            settings.PersistSessionCookies = true;
            settings.CachePath = Path.GetFullPath(Directory.CreateDirectory("CefBrowserCache").ToString());
            Cef.Initialize(settings, true, browserProcessHandler: null);

            var appWindow = new MainWindow();
            appWindow.Show();
            base.OnStartup(e);
        }
    }
}