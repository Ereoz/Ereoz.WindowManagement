using System.Windows;
using System.Windows.Threading;

namespace Ereoz.WindowManagement.Tests.Sources
{
    public class MainWindowFixture : IDisposable
    {
        public MainWindow Window { get; private set; }

        private Application _app;
        private Thread _uiThread;
        private readonly AutoResetEvent _windowReady = new AutoResetEvent(false);

        public void ShowWindow()
        {
            _uiThread = new Thread(() =>
            {
                _app = new Application();

                _app.Dispatcher.Invoke(() =>
                {
                    Window = new MainWindow();

                    new WindowStateManager(Window);

                    Window.Closed += (s, e) => Window.Dispatcher.InvokeShutdown();
                    Window.Show();
                    _windowReady.Set();
                });

                Dispatcher.Run();
            });

            _uiThread.SetApartmentState(ApartmentState.STA);
            _uiThread.Start();
            _windowReady.WaitOne();
        }

        public void Dispose()
        {
            try
            {
                Window.Dispatcher.Invoke(() => Window.Close());
                _uiThread.Join();
                Window.Dispatcher.InvokeShutdown();
            }
            catch { }
        }
    }
}
