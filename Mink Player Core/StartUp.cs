using System;
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
        static Microsoft.Win32.OpenFileDialog OpenFileDiag = new Microsoft.Win32.OpenFileDialog();

        // Instance from Notification Windows.
        //public Notification Notification;

        // Hold Time Notification Will Have To be Open.
        static int NotificationTime = 10;

        #endregion


        #region Entry Point

        /// <summary>
        /// Entry point of app
        /// </summary>
        /// <param name="args">Get string from outside</param>
        [STAThread]
        static void Main(string[] args)
        {

            TrayMaker();

            // Run application 
            MyApp.Run();

        }//End of Main

        #endregion


        /// <summary>
        /// Setup up Context menu item
        /// </summary>
        static void ContexSetup()
        {
            MenuItem ExitItem = new MenuItem("Exit");
            ExitItem.Click += ExitItem_Click;

            MenuItem ShowItem = new MenuItem("Open");
            ShowItem.Click += ShowItem_Click;

            MenuItem NotificationItem = new MenuItem("Show Notification");
            NotificationItem.Click += NotificationItem_Click;

            mContextMenu.MenuItems.Add(ShowItem);
            mContextMenu.MenuItems.Add(NotificationItem);
            mContextMenu.MenuItems.Add(ExitItem);
        }


        static void TrayMaker()
        {
            MyNotify.Icon = Properties.Resources.Tray_Play;
            MyNotify.Visible = true;

            ContexSetup();

            MyNotify.ContextMenu = mContextMenu;
        }


        private static void NotificationItem_Click(object sender, EventArgs e)
        {
            new Notification(OpenFileDiag.FileName, NotificationTime).Show();
        }

        private static void ShowItem_Click(object sender, EventArgs e)
        {
            OpenFileDiag.ShowDialog();
        }

        private static void ExitItem_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
