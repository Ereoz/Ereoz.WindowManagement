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
        private readonly SettingsBase _settingsBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowStateManager"/> class with a specified window and <see cref="SettingsBase"/>.
        /// If <see cref="SettingsBase"/> is null, then <see cref="SettingsBase"/> will be created with the <see cref="SimpleJson"/> serializer
        /// with file name corresponding to the name of the transmitted window.
        /// </summary>
        /// <param name="window">The window whose state will be managed.</param>
        /// <param name="settingsBase">Implementation of the saved state (position and dimensions) of the window.</param>
        public WindowStateManager(Window window, SettingsBase settingsBase = null)
        {
            _window = window;
            _settingsBase = settingsBase ?? new SettingsBase(new SimpleJson(), window.GetType().Name + ".json");
            _settingsBase.LoadState();

            _window.Left = _settingsBase.Left;
            _window.Top = _settingsBase.Top;
            _window.Width = _settingsBase.Width;
            _window.Height = _settingsBase.Height;

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

            if (_settingsBase.IsMaximized)
                _window.WindowState = WindowState.Maximized;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_window.WindowState != WindowState.Maximized)
            {
                _settingsBase.Left = _window.Left;
                _settingsBase.Top = _window.Top;
                _settingsBase.Width = _window.Width;
                _settingsBase.Height = _window.Height;
            }

            _settingsBase.IsMaximized = _window.WindowState == WindowState.Maximized ? true : false;
            _settingsBase.SaveState();
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
