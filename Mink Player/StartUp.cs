using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Mink_Player
{
    class StartUp
    {

        #region Public Property

        // Make instance from wpf application 
        static public System.Windows.Application MyApp = new System.Windows.Application();

        // Instance from NotifyIcon
        static public NotifyIcon MyNotify = new NotifyIcon();

        // Instance from Contextmenu Windows Form.
        static ContextMenu mContextMenu = new ContextMenu();

        // Instance from File Dialog for open file to add to list.
        static Microsoft.Win32.OpenFileDialog OpenFileDiag = new Microsoft.Win32.OpenFileDialog()
        {
            Multiselect = true
        };

        // Hold Time Notification Will Have To be Open.
        static int NotificationTime = 10;

        // Hold List of All File user Choose to Play
        static List<MusicPath> FileList = new List<MusicPath>();

        // Create static instance from Dashboard windows.
        static Dashboard Dashboard = new Dashboard();

        // Create static instance from Media player element.
        static MediaPlayer MPlayer = new MediaPlayer();

        // Hold the list index of Selected File.
        static int SelectedMusic { get; set; }

        // Hold the status of Media Player.(Play/Pause)
        static public bool PlayerStatus { get; set; }

        // Hold player State.
        static public PlayerState PlayerState = PlayerState.Empty;

        // Create instance from Splash Screen.
        static SplashScreen SplashScreen = new SplashScreen();

        #endregion

        #region Entry Point Main Function
        /// <summary>
        /// Entry point of app
        /// </summary>
        /// <param name="args">Get string from outside</param>
        [STAThread]
        static void Main(string[] args)
        {
            MyApp.Exit += MyApp_Exit; // Exit event of App.

            SplashScreen.Show(); // Show Splash Screen on app Start

            //TrayMaker(); // Call TrayMaker Method To setup TrayIcon.

            MPlayerSetup(); // setup property of media player.

            Dashboard.Hide(); // Hide dashboard so app wont close.

            MyApp.Run(); // Run Application 

        }//End of Main
        #endregion    


    }
}
