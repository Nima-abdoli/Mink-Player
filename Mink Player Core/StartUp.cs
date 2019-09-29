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

        static int SelectedMusic { get; set; } 
        

        #endregion


        #region Entry Point Main Function
        /// <summary>
        /// Entry point of app
        /// </summary>
        /// <param name="args">Get string from outside</param>
        [STAThread]
        static void Main(string[] args)
        {
            MyApp.Exit += MyApp_Exit;

            // Call TrayMaker Method To setup TrayIcon.
            TrayMaker();

            MPlayerSetup();

            // Hide dashboard so app wont close.
            Dashboard.Hide();

            // Run application 
            MyApp.Run();

        }//End of Main
        #endregion


        #region Application Exit Event.
        /// <summary>
        /// this event will fire when app will be exited.
        /// </summary>
        private static void MyApp_Exit(object sender, ExitEventArgs e)
        {
            MyNotify.Dispose(); // Will Dispose trayIcon so icon will remove from tray menu.

        }// End of MyApp_Exit
        #endregion


        #region Context Menu Setup Method
        /// <summary>
        /// Setup up Context menu item
        /// </summary>
        static void ContexSetup()
        {
            // Create Menu item for Dashboard
            MenuItem DashboardItem = new MenuItem("Dashboard");
            DashboardItem.Click += DashboardItem_Click;

            // Create menu Item for Select Music
            MenuItem SelectMItem = new MenuItem("Select Music");
            SelectMItem.Click += SelectMItem_Click;

            // Create Menu item for Next Music
            MenuItem NextMItem = new MenuItem("Next Music");
            NextMItem.Click += NextMItem_Click;

            //TO Delete
            MenuItem NotificationItem = new MenuItem("Show Notification");
            NotificationItem.Click += NotificationItem_Click;

            // Create menu Item for Exit app
            MenuItem ExitItem = new MenuItem("Exit");
            ExitItem.Click += ExitItem_Click;

            // created Menu Items added to Contextmenu 
            mContextMenu.MenuItems.Add(DashboardItem);
            mContextMenu.MenuItems.Add(SelectMItem);
            mContextMenu.MenuItems.Add(NextMItem);
            mContextMenu.MenuItems.Add(NotificationItem);
            mContextMenu.MenuItems.Add(ExitItem);
            
        }// End OF ContexSetup
        #endregion


        #region 
        private static void NextMItem_Click(object sender, EventArgs e)
        {
            
        }
        #endregion


        #region DashBoard Item Click Event
        /// <summary>
        /// Click Event for Dashboard item.
        /// </summary>
        private static void DashboardItem_Click(object sender, EventArgs e)
        {
            Dashboard.Show(); // Show DashBoard Windows.

        }// End of DashboardItem_Click
        #endregion


        #region Tray Maker Method
        /// <summary>
        /// This Function Will Setup TrayIcon.
        /// </summary>
        static void TrayMaker()
        {
            MyNotify.Icon = Properties.Resources.Tray_Play;
            MyNotify.Visible = true;

            // Call To Setup Context Menu For TrayIcon.
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


        #region Select Music Click Event
        /// <summary>
        /// click Event For SelectMuic Item In ContextMenu
        /// </summary>
        private static void SelectMItem_Click(object sender, EventArgs e)
        {
            OpenFileDiag.ShowDialog(); // Show OpenDialog To Choose music File

            AddToList();

        }// End of SelectMItem_Click Event
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
            }

            SelectedMusic = 0;

            PlayMusic();

        }

        static void PlayMusic()
        {
            MPlayer.Open(new Uri(FileList[SelectedMusic].Path));
            MPlayer.Play();
            new Notification(FileList[SelectedMusic].Path, NotificationTime).Show();
        }

        static void MPlayerSetup()
        {
            MPlayer.MediaEnded += MPlayer_MediaEnded;
            MPlayer.Volume = 100;
        }

        static private void MPlayer_MediaEnded(object sender, EventArgs e)
        {
            SelectedMusic++;
            PlayMusic();
        }
    }
}
