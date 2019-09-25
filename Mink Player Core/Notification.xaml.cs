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

using Microsoft.Win32;
using TagLib;

namespace Mink_Player_Core
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

        #endregion


        #region Constructors for Notification Windows
        public Notification(string FilePath)
        {
            InitializeComponent();

            //Set startup location of notification windows
            Left = SystemParameters.WorkArea.Width - (Width + 15);
            Top = SystemParameters.WorkArea.Height - (Height + 15);

            TrackPath = FilePath;
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
            FadeInAnimation();
            FileTag = TagLib.File.Create(TrackPath);

            CoverExtractor();
            ArtistExtractor();
            TitleExtractor();
        }//End of Window Loaded Event
        #endregion


        #region Notification window MouseDown Event
        /// <summary>
        /// Mouse Down event for Notification Windows.
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
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
                // get defult Image From Application Resource
                TrackCover = new BitmapImage(new Uri("pack://application:,,,/Resources/Image/Cover/Cassette.png", UriKind.RelativeOrAbsolute));
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


    } // End of Notification Class
} //End of NameSpcae
