using Ereoz.Abstractions.Logging;
using Ereoz.Abstractions.Serialization;
using Ereoz.DataStorage;
using System;
using System.ComponentModel;

namespace Ereoz.WindowManagement
{
    /// <summary>
    /// Represents the saved state (position and dimensions) of the window.
    /// This class is responsible for loading and saving window location data.
    /// </summary>
    [Serializable]
    public class WindowLocation : StoredState
    {
        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public WindowLocation() { }

        /// <inheritdoc/>
        public WindowLocation(IStringSerializer serializer) : base(serializer) { }

        /// <inheritdoc/>
        public WindowLocation(IBinarySerializer serializer) : base(serializer) { }

        /// <inheritdoc/>
        public WindowLocation(IStringSerializer serializer, string fileName) : base(serializer, fileName) { }

        /// <inheritdoc/>
        public WindowLocation(IBinarySerializer serializer, string fileName) : base(serializer, fileName) { }

        /// <inheritdoc/>
        public WindowLocation(IStringSerializer serializer, string fileName, ILogger logger) : base(serializer, fileName, logger) { }

        /// <inheritdoc/>
        public WindowLocation(IBinarySerializer serializer, string fileName, ILogger logger) : base(serializer, fileName, logger) { }

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
