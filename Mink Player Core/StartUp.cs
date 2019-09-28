using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Forms;


namespace Mink_Player_Core
{
    class StartUp
    {

        #region Public Property

        // Make instance from wpf application 
        static public System.Windows.Application MyApp = new System.Windows.Application();

        // Instance from NotifyIcon
        static public NotifyIcon MyNotify = new NotifyIcon();

        // Imstance from Contextmenu Windows Form.
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

        static string s;

        static Dashboard Dashboard = new Dashboard();

        #endregion


        #region Entry Point
        /// <summary>
        /// Entry point of app
        /// </summary>
        /// <param name="args">Get string from outside</param>
        [STAThread]
        static void Main(string[] args)
        {
            // Call TrayMaker Method To setup TrayIcon.
            TrayMaker();

            s = "";

            Dashboard.Hide();

            // Run application 
            MyApp.Run();

        }//End of Main
        #endregion


        #region Contex Menu Setup Method
        /// <summary>
        /// Setup up Context menu item
        /// </summary>
        static void ContexSetup()
        {
            MenuItem ExitItem = new MenuItem("Exit");
            ExitItem.Click += ExitItem_Click;

            MenuItem OpenItem = new MenuItem("Open");
            OpenItem.Click += OpenItem_Click;

            MenuItem NotificationItem = new MenuItem("Show Notification");
            NotificationItem.Click += NotificationItem_Click;

            mContextMenu.MenuItems.Add(OpenItem);
            mContextMenu.MenuItems.Add(NotificationItem);
            mContextMenu.MenuItems.Add(ExitItem);
        }// End OF ContexSetup
        #endregion


        #region Tray Maker Method
        /// <summary>
        /// This Function Will Setup TrayIcon.
        /// </summary>
        static void TrayMaker()
        {
            MyNotify.Icon = Properties.Resources.Tray_Play;
            MyNotify.Visible = true;

            // Call To Setup Contex Menu For TrayIcon.
            ContexSetup();

            MyNotify.ContextMenu = mContextMenu;
        }// End Of TrayMaker
        #endregion


        #region NotificationItem Click Event
        // this Will Be Deleted !!!!!!!!
        private static void NotificationItem_Click(object sender, EventArgs e)
        {
            new Notification(OpenFileDiag.FileName, NotificationTime).Show();
        }
        #endregion


        #region OpenItem Click Event
        /// <summary>
        /// click Event For OpenItem In ContextMenu
        /// </summary>
        private static void OpenItem_Click(object sender, EventArgs e)
        {
            OpenFileDiag.ShowDialog(); // Show OpenDialog To Choose music File

            AddToList();
            
        }// End of OpenItem_Click Event
        #endregion


        #region ExitItem click Event
        /// <summary>
        /// click Event For ExitItem In ContextMenu
        /// </summary>
        private static void ExitItem_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown(); // Close(Shutdown) This(MyApp) application
        }// End of ExitItem Event
        #endregion


        static void AddToList()
        {
            
            foreach (string mPath in OpenFileDiag.FileNames)
            {
                FileList.Add(new MusicPath() { Path = mPath });
                s += mPath + "\n";
            }

            //System.Windows.MessageBox.Show(s);
        }

        static void PlayMusic()
        {

        }
    }
}
