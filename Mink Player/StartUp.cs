using System;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
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

        // Imstance from Contextmenu Windows Form.
        static ContextMenu mContextMenu = new ContextMenu();

        // Instance from Notification Windows.
        public Notification Notification;

        // Instance from File Dialog for open file to add to list.
        static Microsoft.Win32.OpenFileDialog OpenFileDiag = new Microsoft.Win32.OpenFileDialog();

        // Instance from MainDashboard windows.
        static MainDashboard MainDashboard = new MainDashboard();

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

<<<<<<< d255d48714d3c7928e8fc62c94e2a3a9c18bab8d
            //MainDashboard.Show();
=======
            MainDashboard.Show();
>>>>>>> .net framework back again

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

            MenuItem ShowItem = new MenuItem("Show");
            ShowItem.Click += ShowItem_Click;

            MenuItem NotificationItem = new MenuItem("Show Notification");
            NotificationItem.Click += NotificationItem_Click;

            mContextMenu.MenuItems.Add(ShowItem);
            mContextMenu.MenuItems.Add(NotificationItem);
            mContextMenu.MenuItems.Add(ExitItem);
        }

        static void TrayMaker()
        {
<<<<<<< d255d48714d3c7928e8fc62c94e2a3a9c18bab8d
            MyNotify.Icon = Properties.Resources.Tray_Play;
=======
            MyNotify.Icon = Properties.Resources.M_Logo;
>>>>>>> .net framework back again
            MyNotify.Visible = true;

            ContexSetup();

            MyNotify.ContextMenu = mContextMenu;
        }

        static private void NotificationItem_Click(object sender, EventArgs e)
        {
            new Notification(OpenFileDiag).Show();
        }

        /// <summary>
        /// Click Event For ShowItem
        /// </summary>
        static private void ShowItem_Click(object sender, EventArgs e)
        {
            OpenFileDiag.ShowDialog();
<<<<<<< d255d48714d3c7928e8fc62c94e2a3a9c18bab8d

=======
>>>>>>> .net framework back again
        }

        /// <summary>
        /// Click Event for ExitItem
        /// </summary>
        static private void ExitItem_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
