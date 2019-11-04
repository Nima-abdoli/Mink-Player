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
