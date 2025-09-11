using Ereoz.WindowManagement.Tests.Sources;
using System.IO;
using System.Windows.Threading;

namespace Ereoz.WindowManagement.Tests
{
    [Collection("Sequential")]
    public class WindowManagementTest : IClassFixture<MainWindowFixture>
    {
        private readonly MainWindowFixture _fixture;
        private readonly string _fileName = "WindowLocation.json";

        private readonly string firstState = "{\r\n  \"Left\": 200,\r\n  \"Top\": 150,\r\n  \"Width\": 640,\r\n  \"Height\": 480,\r\n  \"IsMaximized\": false\r\n}";
        private readonly string secondState = "{\r\n  \"Left\": 250,\r\n  \"Top\": 100,\r\n  \"Width\": 320,\r\n  \"Height\": 240,\r\n  \"IsMaximized\": false\r\n}";

        public WindowManagementTest(MainWindowFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void SaveAndLoadWindowState_ShouldCorrectState()
        {
            if (File.Exists(_fileName))
                File.Delete(_fileName);

            File.WriteAllText(_fileName, firstState);

            _fixture.ShowWindow();

            _fixture.Window.Dispatcher.Invoke(DispatcherPriority.Render, new Action(() =>
            {
                Assert.Equal(200, _fixture.Window.Left);
                Assert.Equal(150, _fixture.Window.Top);
                Assert.Equal(640, _fixture.Window.Width);
                Assert.Equal(480, _fixture.Window.Height);

                _fixture.Window.Left = 250;
                _fixture.Window.Top = 100;
                _fixture.Window.Width = 320;
                _fixture.Window.Height = 240;

                _fixture.Window.Close();
            }));

            Assert.Equal(secondState, File.ReadAllText(_fileName));

            File.Delete(_fileName);
        }
    }
}