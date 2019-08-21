using System;
using System.Windows;
using System.Windows.Forms;

namespace Mink_Player
{

    class StartUp
    {

        // Make instance from wpf application 
        static public System.Windows.Application MyApp = new System.Windows.Application();

        #region Entry Point
        /// <summary>
        /// Entry point of app
        /// </summary>
        /// <param name="args">Get string from outside</param>
        [STAThread]
        static void Main(string[] args)
        {
           
            // make new instance from this class
            new SystemTray();

            // Run my instanced application 
            MyApp.Run();
        }//End of Main
        #endregion

    }
}
