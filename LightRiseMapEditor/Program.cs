#define MAP_EDITOR
using System;

namespace LightRise.MapEditor {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        public static MainThread MainThread { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (MainThread = new MainThread())
                MainThread.Run();
        }
    }
#endif
}
