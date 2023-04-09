using System;
using System.Collections.Generic;
using System.Threading;
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

        // Instance from File Dialog for open file to add to list.
        static public Microsoft.Win32.OpenFileDialog OpenFileDiag = new Microsoft.Win32.OpenFileDialog()
        {
            Multiselect = true // User can Select More than one File.
        };

        // Hold Time Notification Will Have To be Open.
        static int NotificationTime = 10;

        // Hold List of All File user Choose to Play
        static List<MusicPath> FileList = new List<MusicPath>();

        // Create static instance from Dashboard windows.
        static public Windows.Dashboard Dashboard = new Windows.Dashboard();

        // Create static instance from Media player element.
        static public MediaPlayer MPlayer = new MediaPlayer();

        // Hold the list index of Selected File.
        static int SelectedMusic { get; set; }

        // Hold player State.
        static public PlayerState PlayerState = PlayerState.Empty;

        // Hold Player Mute/unmute State
        static public PlayerMute PlyerMutestate = PlayerMute.Unmute;

        // Create instance from Splash Screen.
        static Windows.SplashScreen SplashScreen = new Windows.SplashScreen();

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

            //TrayMaker(); // Call TrayMaker Method To setup TrayIcon. it will be called from splashscreen

            MPlayerSetup(); // setup property of media player.

            Dashboard.Hide(); // Hide dashboard so app wont close.

            MyApp.Run(); // Run Application 
            
        }//End of Main
        #endregion    

        #region Tray Maker Method
        /// <summary>
        /// This Function Will Setup TrayIcon.
        /// </summary>
        static public void TrayMaker()
        {
            MyNotify.Icon = Properties.Resources.mTrayEmpty;
            MyNotify.Visible = true;
            MyNotify.MouseClick += MyNotify_MouseClick;

        }// End Of TrayMaker
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

        #region MyNotify MouseClick Event
        /// <summary>
        /// Mouse Click Event for MyNotify
        /// </summary>
        private static void MyNotify_MouseClick(object sender, MouseEventArgs e)
        {
            //ToDo : Statment Will Be Deleted if or Change in case of opning by both buttons.
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                ContextMenuCalling();
            }

        }// End of MyNotify_MouseClick
        #endregion

        #region Add To List Method
        /// <summary>
        /// Add user file to list 
        /// </summary>
        static public void AddToList()
        {

            // Adding user selected file to list 
            foreach (string mPath in OpenFileDiag.FileNames)
            {
                FileList.Add(new MusicPath() { Path = mPath }); // add file to list
            }

            // Check if list is empty set selected index to zero
            if (PlayerState == PlayerState.Empty)
            {
                SelectedMusic = 0;  // set selected index in first time

                SetMuisc(); // set Music For the First time

            }

        }// End of AddtoList
        #endregion

        #region Play Music Function
        /// <summary>
        /// Play selected Music. 
        /// </summary>
        static public void PlayMusic()
        {
            // check state of player to not be in play state.
            if (PlayerState == PlayerState.Empty || PlayerState == PlayerState.Pause)
            {
                SetState(PlayerState.Play); // set state of player to play so means player playing music.

                MPlayer.Play(); // music player play music.

                new Windows.Notification(FileList[SelectedMusic].Path, NotificationTime).Show(); // show music notification 
            
            }// end of if

        }// End of PlayMuic
        #endregion

        #region Set Music Function
        /// <summary>
        /// Set Music from selected path to player.
        /// </summary>
        static public void SetMuisc()
        {
            MPlayer.Open(new Uri(FileList[SelectedMusic].Path)); // open selected music to music player

            PlayMusic(); // call this to play music

        }// end of SetMusic Function 
        #endregion

        #region Pause Music Function 
        /// <summary>
        /// Pause Selected Music 
        /// </summary>
        static public void PauseMusic()
        {
            if (PlayerState == PlayerState.Play)
            {
                MPlayer.Pause(); // Pause Music
                SetState(PlayerState.Pause);
            }
        }// End of PauseMuic
        #endregion

        #region  Setup Media player
        /// <summary>
        /// This Function will Setup media player property. 
        /// </summary>
        static void MPlayerSetup()
        {
            MPlayer.MediaEnded += MPlayer_MediaEnded; // Media Ended Event

            //TODO: user last choose.
            MPlayer.Volume = 100; // set volume to max(100)

        }// End of MPLayerSetup
        #endregion

        #region MediaEnded event for Media Player
        /// <summary>
        /// fire when media reach to it's end.
        /// </summary>
        static private void MPlayer_MediaEnded(object sender, EventArgs e)
        {
            // TODO: User Choose in Setting
            //Check if list reach to end don't go to start of list when media end
            if (SelectedMusic + 1 < FileList.Count)
            {
                NextInList(); // call to set selected file to next in list

                SetState(PlayerState.Pause); // set state to pause temporarily to play function work.

                SetMuisc(); // set selected file.
            }
            else
            {
               // NextInList(); // select first music of list when list reach to end

                SetState(PlayerState.Pause); // Set player state to Pause 

                MPlayer.Stop(); // stop the player so music will go to top, and it will start from last music played.
            }

        }// End of MPlayer_MediaEnded
        #endregion

        #region Next Song Function
        /// <summary>
        /// Play next song in the list
        /// </summary>
        static public void NextSong()
        {
            //Check list to not be empty
            if (FileList.Count != 0)
            {
                NextInList(); // call to set selected file to next in list

                SetState(PlayerState.Pause); // set state to pause temporarily to play function work.

                SetMuisc(); // set selected file.
            }
           
        } // End of NextSong Function
        #endregion

        #region Last Song Function
        /// <summary>
        /// Play last Song in List.
        /// </summary>
        static public void LastSong()
        {
            // Check list to not be empty
            if (FileList.Count != 0)
            {
                LastInList(); // call to set selected file to next in list

                SetState(PlayerState.Pause); // set state to pause temporarily to play function work.

                SetMuisc(); // play selected file.
            }

        } // end of LastSong 
        #endregion

        #region  Next in List Function
        /// <summary>
        /// Set the selected file to next in the list if it exist.
        /// </summary>
        static void NextInList()
        {
            // Check for list reach to end or not. if reach to end it will go to index 1 of list
            if (SelectedMusic + 1 < FileList.Count) SelectedMusic++;

            else SelectedMusic = 0;

        } // End of NextInList
        #endregion

        #region Last in List Function
        /// <summary>
        /// Set the selected file to last in list if it exist.
        /// </summary>
        static void LastInList()
        {
            // check if list reach to beginning or not. if reach to beginning of list it will set to end of the list.
            if (SelectedMusic > 0)
            {
                SelectedMusic--; // go back by one in the list.
            }
            else
            {
                SelectedMusic = FileList.Count - 1; // go to last music if list reach to first one.
            }

        } // end of LastInLIst
        #endregion

        #region Set State Function
        /// <summary>
        /// Set state and tray icon of player.
        /// </summary>
        /// <param name="state"> get the state that player should be.</param>
        static void SetState(PlayerState state)
        {
            PlayerState = state; // set player state

            // set tray icon by player state.
            switch (PlayerState)
            {
                case PlayerState.Play:
                    MyNotify.Icon = Properties.Resources.mTrayPlay;
                    break;

                case PlayerState.Pause:
                    MyNotify.Icon = Properties.Resources.mTrayPause;
                    break;

                case PlayerState.Empty:
                    MyNotify.Icon = Properties.Resources.mTrayEmpty;
                    break;

            }// end of switch

        }// End of SetState Function
        #endregion

        #region Select Music Function
        /// <summary>
        /// Select Music from ContextMenu
        /// </summary>
        public static void SelectMusic()
        {            
            OpenFileDiag.ShowDialog(); // Show OpenDialog To Choose music File

            if (OpenFileDiag.FileName != "")
            {
                AddToList(); // add selected music to list
            }

        }// End of SelectMItem_Click Event
        #endregion

        #region Context Menu Calling Function
        /// <summary>
        /// this function call contextmenu and handle 
        /// </summary>
        static void ContextMenuCalling()
        {
            // check if list is empty or not.
            if (FileList.Count > 0)
            {

                Windows.ContextMenu ContexMenu = new Windows.ContextMenu(FileList[SelectedMusic].Path, Cursor.Position);

                ContexMenu.Show(); // Show Context Menu
            }
            else
            {
                Windows.ContextMenu ContexMenu = new Windows.ContextMenu(null, Cursor.Position);
                ContexMenu.Show(); // Show Context Menu
            }

        }// End of ContextMenuCalling fiction
        #endregion

        #region Muting Player 
        /// <summary>
        /// Mute and unmute player
        /// </summary>
        static public void MutePlayer()
        {

            if (StartUp.PlyerMutestate == PlayerMute.Mute)
            {
                PlyerMutestate = PlayerMute.Unmute;
                MPlayer.IsMuted = false;
            }
            else if (StartUp.PlyerMutestate == PlayerMute.Unmute)
            {
                PlyerMutestate = PlayerMute.Mute;
                MPlayer.IsMuted = true;
            }

        }// End Of MutePLayer Function
        #endregion

    }// End of StartUp class
}// End of Mink_Player NameSpace
