using Ereoz.Abstractions.Logging;
using Ereoz.Abstractions.Serialization;
using Ereoz.DataStorage;
using System;

namespace Ereoz.WindowManagement
{
    /// <summary>
    /// Represents the saved state (position and dimensions) of the window.
    /// This class is responsible for loading and saving window location data.
    /// </summary>
    [Serializable]
    public class SettingsBase : StoredState
    {
        /// <inheritdoc/>
        public SettingsBase() { }

        /// <inheritdoc/>
        public SettingsBase(string file) : base(file, null) { }

        /// <inheritdoc/>
        public SettingsBase(string file, ILogger logger) : base(file, logger) { }

        /// <inheritdoc/>
        public SettingsBase(IStringSerializer serializer) : base(serializer) { }

        /// <inheritdoc/>
        public SettingsBase(IBinarySerializer serializer) : base(serializer) { }

        /// <inheritdoc/>
        public SettingsBase(IStringSerializer serializer, string fileName) : base(serializer, fileName) { }

        /// <inheritdoc/>
        public SettingsBase(IBinarySerializer serializer, string fileName) : base(serializer, fileName) { }

        /// <inheritdoc/>
        public SettingsBase(IStringSerializer serializer, string fileName, ILogger logger) : base(serializer, fileName, logger) { }

        /// <inheritdoc/>
        public SettingsBase(IBinarySerializer serializer, string fileName, ILogger logger) : base(serializer, fileName, logger) { }

        /// <summary>
        /// Horizontal position (X-coordinate) of the window's left edge.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Vertical position (Y-coordinate) of the window's top edge.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Width of the window.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of the window.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Indicating whether the window is maximized.
        /// </summary>
        public bool IsMaximized { get; set; }

        /// <inheritdoc/>
        protected override void LoadFail()
        {
            Left = 100;
            Top = 100;
            Width = 800;
            Height = 450;
        }
    }
}
