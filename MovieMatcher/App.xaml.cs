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
            Cef.Initialize(settings, true, browserProcessHandler: null);

            var appWindow = new Login();
            appWindow.Show();
            base.OnStartup(e);
        }
    }
}