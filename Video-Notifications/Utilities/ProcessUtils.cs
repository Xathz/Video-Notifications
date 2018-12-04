using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VideoNotifications.Utilities {

    internal static class ProcessUtils {

        /// <summary>
        /// Starts a program, file, or opens a URL.
        /// </summary>
        /// <param name="path">Path to the file or URL to start.</param>
        public static void Start(string path) {
            try {
                Process.Start(path);
            } catch (Exception ex) {
                LoggingManager.Log.Error(ex, $"Failed to start/open '{path}'.");
            }
        }

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window.
        /// Keyboard input is directed to the window, and various visual cues are changed for the user.
        /// The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx</remarks>
        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(int hWnd);

    }

}
