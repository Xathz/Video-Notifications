namespace VideoNotifications.Settings {

    public class FormState {

        /// <summary>
        /// Position X.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Position Y.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Window width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Window height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Checks if the form position is already set.
        /// </summary>
        public bool PositionSet() => (X == 0 & Y == 0) ? false : true;

    }

}
