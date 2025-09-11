using Ereoz.Serialization.Json;
using System;
using System.Windows;

namespace Ereoz.WindowManagement
{
    /// <summary>
    /// Manages the state of the window.
    /// </summary>
    public class WindowStateManager
    {
        private readonly Window _window;
        private readonly WindowLocation _location;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowStateManager"/> class with a specified window and <see cref="WindowLocation"/>.
        /// If <see cref="WindowLocation"/> is null, then <see cref="WindowLocation"/> will be created with the <see cref="SimpleJson"/> serializer.
        /// </summary>
        /// <param name="window">The window whose state will be managed.</param>
        /// <param name="location">Implementation of the saved state (position and dimensions) of the window.</param>
        public WindowStateManager(Window window, WindowLocation location = null)
        {
            _window = window;
            _location = location ?? new WindowLocation(new SimpleJson());

            _window.Left = _location.Left;
            _window.Top = _location.Top;
            _window.Width = _location.Width;
            _window.Height = _location.Height;

            _window.Loaded += Window_Loaded;
            _window.Closed += Window_Closed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!CheckWindowVisibilityOnAtLeastOneScreens())
            {
                _window.Top = 100;
                _window.Left = 100;
            }

            if (_location.IsMaximized)
                _window.WindowState = WindowState.Maximized;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_window.WindowState != WindowState.Maximized)
            {
                _location.Left = _window.Left;
                _location.Top = _window.Top;
                _location.Width = _window.Width;
                _location.Height = _window.Height;
            }

            _location.IsMaximized = _window.WindowState == WindowState.Maximized ? true : false;
            _location.SaveState();
        }

        private bool CheckWindowVisibilityOnAtLeastOneScreens()
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
                (int)_window.Left,
                (int)_window.Top,
                (int)_window.Width,
                (int)_window.Height);

            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (screen.Bounds.IntersectsWith(rectangle))
                    return true;
            }
            return false;
        }
    }
}
