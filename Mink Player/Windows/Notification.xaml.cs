using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
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
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {

        #region Property of Class

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

        // Hold Time That Windows will be open
        int TimeToStay;

        #endregion

        #region Constructors for Notification Windows

        public Notification(string FilePath, int TimeLeft)
        {
            InitializeComponent();

            //Set startup location of notification windows
            Left = SystemParameters.WorkArea.Width - (Width + 15);
            Top = SystemParameters.WorkArea.Height - (Height + 15);

            TrackPath = FilePath; // Path to Music file.

            TimeToStay = TimeLeft; // Time Left To This Windows Closing.
        }

        public Notification()
        {
            InitializeComponent();

            //Set startup location of notification windows
            Left = SystemParameters.WorkArea.Width - (Width + 15);
            Top = SystemParameters.WorkArea.Height - (Height + 15);
        }
        #endregion

        #region Load Event of Notification Window 
        /// <summary>
        /// Load Event of Notification Window 
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FadeInAnimation(); // Call to This Window Open With Animation To be Smother.

            FileTag = TagLib.File.Create(TrackPath); // Set and Make tag From File path that give to it. 

            //TimeToStay = 8; // Time Left To This Windows Closing.(Better to Handle from Constructor)

            Timer(); // Call This Method To Handle Time Left For Closing this Window.

            CoverExtractor(); // Call To Extract Cover From File.

            ArtistExtractor(); // Call To Extract Artist Name from File.

            TitleExtractor(); // Call To Extract title of Song From File.

        }//End of Window Loaded Event
        #endregion

        #region MouseDown Event Notification window 
        /// <summary>
        /// Mouse Down event for Notification Windows.
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Check if notification are closing don't set time again
                if (TimeToStay >= 2)
                {
                    TimeToStay = 2; // Set timetostay equal to 2, so Timer Handle closing Animation and closing 
                }
            }
        }// End of Windows_MouseDown
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

        #region Fade In Animation Method
        /// <summary>
        /// Fade In Animation Method. Fade in windows when started.
        /// </summary>
        void FadeInAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromMilliseconds(900);

            this.BeginAnimation(OpacityProperty, da);

        }// end of FadeInAnimation()
        #endregion

        #region Fade Out Animation Method
        /// <summary>
        /// Fade In Animation Method. Fade in windows when started.
        /// </summary>
        void FadeOutAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromMilliseconds(900);

            this.BeginAnimation(OpacityProperty, da);

        }// end of FadeOutAnimation()
        #endregion

        #region Timer Method
        /// <summary>
        /// Timer method Will handle Time left to Close Notification Windows. 
        /// </summary>
        void Timer()
        {

            DTimer.Interval = TimeSpan.FromSeconds(1);
            DTimer.IsEnabled = true;
            DTimer.Tick += DTimer_Tick;
            DTimer.Start();

        }// End of Timer Method
        #endregion

        #region Tick Event For DTimer
        /// <summary>
        /// Tick Event For DTimer. Event fire Every 1 Second
        /// </summary>
        private void DTimer_Tick(object sender, EventArgs e)
        {
            // Decrease this variable with each tick(1 Second) to show how much time left for Closing 
            TimeToStay--;

            // This Check So there will be Time For Animation to Complete.
            if (TimeToStay == 1)
            {
                FadeOutAnimation();
            }
            // Check to Close App in Certain Time.
            else if (TimeToStay == 0)
            {
                GC.Collect();
                this.Close();
            }

        }// End of Dtimer_Tick Event
        #endregion

        #region Btn1 Click Event 
        /// <summary>
        /// click event of Btn1, Previous Button
        /// </summary>
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            StartUp.LastSong(); // call this function to play Previous song

        }// End of Btn1_Click
        #endregion

        #region Btn2 Click Event
        /// <summary>
        /// Click event of Button 2. Play/Pause Button
        /// </summary>
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            StartUp.PauseMusic();
        }// End Of Btn2_click event
        #endregion

        #region Btn3 Click Event
        /// <summary>
        /// Click event of Btn3, Next Button
        /// </summary>
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            StartUp.NextSong(); //call this function to play next song

        }// End of Btn3_Click
        #endregion

    }
}
