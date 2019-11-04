using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using TagLib;

namespace Mink_Player.Windows
{
    /// <summary>
    /// Interaction logic for ContextMenu.xaml
    /// </summary>
    public partial class ContextMenu : Window
    {

        #region Private Property

        // string to holds Artist Name.
        string ArtistName { get; set; }

        // String to hold Title of Track.
        string TrackTitle { get; set; }

        // BitmapImage To Holds Cover image of track(Music File) 
        BitmapImage TrackCover { get; set; }

        // String to Hold File Path(Name)
        string TrackPath { get; set; }

        // This Variable will 
        TagLib.File FileTag;

        // Dispatcher Timer 
        DispatcherTimer DTimer = new DispatcherTimer();

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ContextMenu()
        {
            InitializeComponent();

        }// End of Constructor

        /// <summary>
        /// Custom Constructor
        /// </summary>
        /// <param name="FilePath"> get music file Path</param>
        /// <param name="MouseP"> get the mouse location.</param>
        public ContextMenu(string FilePath,System.Drawing.Point MouseP)
        {
            InitializeComponent();

            // set position of window were mouse is.
            Left = MouseP.X - 115; 
            Top = SystemParameters.WorkArea.Height - 275;

            TrackPath = FilePath; // Path to Music file.

            StateIconChanger(); // call to set play/pause button icon.

            MuteIconChanger(); // Call to set Mute/unmute button icon.
 
        }// End of Constructor
        #endregion

        #region ContextMenu Deactivated Event.
        /// <summary>
        /// Fire this event in when window become deactivated. 
        /// </summary>
        private void Window_Deactivated(object sender, EventArgs e)
        {

            this.Hide(); // hide This windows.

            GC.Collect(); // Call to release unused ram.

        }// End of Window_Deactivated Method
        #endregion

        #region Window Loaded Event
        /// <summary>
        /// contextMenu Window Loaded Event
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
 
            Activate(); // Activate this to be on top and Deactivate event don't have problem in closing windows with one click.
            
            Storyboard sb = this.FindResource("SlideUp") as Storyboard; // get slideUp function to fire it in windows Load up.

            sb.Begin(); // Begin Slide Up Function

            // check to be sure file is available if not don't get meta of nothing
            if (TrackPath != null)
            {
                FileTag = TagLib.File.Create(TrackPath); // Set and Make tag From File path that give to it. 

                CoverExtractor(); // Call To Extract Cover From File.

                ArtistExtractor(); // Call To Extract Artist Name from File.

                TitleExtractor(); // Call To Extract title of Song From File.
            }

        }// End of Window_Load Event
        #endregion

        #region App Close Button Click Event
        /// <summary>
        /// Close app(MyApp) 
        /// </summary>
        private void AppClose_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Hide Context Window First Because have Intercept with Window_Deactivated event,
                         // if app ShutDown first there will be conflict with closing shutdown. 

            Application.Current.Shutdown(); // Close the whole app

        }// End of AppClose_Btn Function
        #endregion

        #region Select Music Button Click Event
        /// <summary>
        /// select music from folder with open dialog.
        /// </summary>
        private void SelectMusic_Btn_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Close this windows.

            StartUp.SelectMusic(); // select music from folder by calling this from startup class.

        }// End of SelectMusic_Btn_Click Event
        #endregion 

        #region previous Button Click Event
        /// <summary>
        /// choose Previous music file.
        /// </summary>
        private void Previous_Btn_Click(object sender, RoutedEventArgs e)
        {
            StartUp.LastSong(); // Call this function to choose Previous music.

        }// End of Previous_Btn_Click
        #endregion

        #region Play Button Click Event
        /// <summary>
        /// play/pause music player.
        /// </summary>
        private void Play_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Check if state of player in play, pause it.
            if (StartUp.PlayerState == PlayerState.Play)
            {
                StartUp.PauseMusic(); // pause music
            }
            else if (StartUp.PlayerState == PlayerState.Pause) // Check if state of player in pause, play it.
            {
                StartUp.PlayMusic(); // Play music.
            }

            StateIconChanger(); // call to set play/pause button icon.

        }// End of Play_Btn_Click
        #endregion

        #region Next Button click Event
        /// <summary>
        /// choose next music file.
        /// </summary>
        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {

            StartUp.NextSong(); // Call this function to choose next music.

        }// End of Next_Btn_Click 
        #endregion

        #region State Icon Changer
        /// <summary>
        /// change play/pause button icon by the state of player.
        /// </summary>
        void StateIconChanger()
        {
            if (StartUp.PlayerState == PlayerState.Play)
            {
                Play_Btn.Content = "\uE768"; // Change button icon to play icon
            }
            else if (StartUp.PlayerState == PlayerState.Pause)
            {
                Play_Btn.Content = "\uE769"; // change button icon to pause icon.
            }

        }// End of StateIconChanger Function
        #endregion

        #region File Cover Extractor Method
        /// <summary>
        /// Extract Image Cover From file to show in notification imageview
        /// </summary>
        /// <returns> Return file cover in BitmapImage</returns>
        void CoverExtractor()
        {
            // Check if Image exist or not
            if (FileTag.Tag.Pictures.Length != 0)
            {
                // Load you image data in MemoryStream
                TagLib.IPicture pic = FileTag.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                ms.Seek(0, SeekOrigin.Begin);

                // ImageSource for System.Windows.Controls.Image
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.EndInit();

                // put extracted image in TrackCover Property.
                TrackCover = bitmap;
            }
            else
            {
                // get default Image From Application Resource
                TrackCover = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Cover/Cassette.png", UriKind.RelativeOrAbsolute));
            }

            Cover_Image.Background = new ImageBrush(TrackCover);
            BackRound_Image.Source = TrackCover;

        }// End of CoverExtractor Method
        #endregion

        #region Artist Name Extractor Method
        /// <summary>
        /// Extract Name of Artist from file.
        /// </summary>
        void ArtistExtractor()
        {
            if (FileTag.Tag.Performers.Length != 0)
            {
                NameArtist_TxtBl.Text = FileTag.Tag.Performers[0];
            }
            else
            {
                NameArtist_TxtBl.Text = "";
            }
        } // End of ArtistExtractor Method
        #endregion

        #region Title Extractor Method
        /// <summary>
        /// Extract Title of Music From File
        /// </summary>
        void TitleExtractor()
        {
            if (FileTag.Tag.Title != null)
            {
                NameTrack_TxtBl.Text = FileTag.Tag.Title;
            }
            else
            {
                NameTrack_TxtBl.Text = System.IO.Path.GetFileName(TrackPath);
            }
        } // End of TitleExtractor Method
        #endregion

        #region Mute Button Click Event
        /// <summary>
        /// click event of Mutebtn in context menu.
        /// </summary>
        private void MuteBtn_Click(object sender, RoutedEventArgs e)
        {
            StartUp.MutePlayer(); // call to mute/unmute player.

            MuteIconChanger(); // Call to set Mute/unmute button icon.

        }// End of MuteBtn_click Event
        #endregion

        #region Mute Icon Changer
        /// <summary>
        /// set mute icon with state of it from mplayer
        /// </summary>
        private void MuteIconChanger()
        {

            if (StartUp.PlyerMutestate == PlayerMute.Mute)
            {
                MuteBtn.Content = "\uE74F"; // set the mute icon to Mute
            }
            else if (StartUp.PlyerMutestate == PlayerMute.Unmute)
            {
                MuteBtn.Content = "\uE995"; // set the mute icon to UnMute.
            }

        }// End of MUteIconChanger function
        #endregion

    }// End of ContextMenu Class
}// End of Mink_Player NameSpace
