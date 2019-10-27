using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Mink_Player_Core
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {

        #region private Property

        // set time that splash screen will be open
        int TimeLeft = 5;

        // animation for move element in top(x) axis.
        DoubleAnimation TopAnimation = new DoubleAnimation();

        // animation for move element in left(y) axis.
        DoubleAnimation Leftanimation = new DoubleAnimation();

        // animation for change size of element.
        DoubleAnimation ImageSizeAnimation = new DoubleAnimation();

        // Timer to count time in second.
        DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1), // timer will fire event every second
            IsEnabled = true // timer will be enable and will work
        };

        #endregion

        #region splash screen windows constructor
        public SplashScreen()
        {
            InitializeComponent();

            TimeLeft = RanadomTime(); // set time left

            Timer.Tick += Timer_Tick; // make tick event for timer

        }// End of SplashScreen Constructor
        #endregion

        #region Timer Tick Event
        /// <summary>
        /// Tick event of Timer
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            
            if (TimeLeft == 1) // check if timer reach to 1 second, start animations.
            {
                ClearBackground(); // call this method to clear all element in splash screen except app image

                ToTrayAnimation(); // run animation. and app image (mink head) goes to tray bar.
            }
            else if (TimeLeft == 0) // check for timeleft when it's reach to 0, clear every thing 
            {
                StartUp.TrayMaker(); // setup tray icon after splash screen closed.

                // disable timer. timer tick will continue ticking if timer not disabled.
                Timer.Stop();
                Timer.IsEnabled = false;

                this.Close(); // close splash window.

                GC.Collect(); // call garbage collector to release memory.
            }

            // check to make sure timer don't go more than 0 and splash screen be closed.
            if (TimeLeft >= 0)
            {
                TimeLeft--; // reduce timeleft to be sure splash screen show for just moment
            }

        }// End of Timer_tick Event
        #endregion

        #region To Tray Animation Function
        /// <summary>
        /// Move splash screen to tray notification.
        /// </summary>
        void ToTrayAnimation()
        {
            
            // move window in top(x)axis. go to bottom of screen
            TopAnimation.From = this.Top;
            TopAnimation.To = SystemParameters.WorkArea.Height - 30;
            TopAnimation.Duration = TimeSpan.FromMilliseconds(800);

            // move window in left(y)axis. go to Right of screen
            Leftanimation.From = Left;
            // Leftanimation.To = SystemParameters.WorkArea.Width - 400;
            Leftanimation.To = 960;
            Leftanimation.Duration = TimeSpan.FromMilliseconds(800);

            // shrink app image width. 
            ImageSizeAnimation.From = AppImage.Width;
            ImageSizeAnimation.To = 10;
            ImageSizeAnimation.Duration = TimeSpan.FromMilliseconds(1000);

            // animation starts here.
            this.BeginAnimation(TopProperty, TopAnimation);
            this.BeginAnimation(LeftProperty, Leftanimation);
            AppImage.BeginAnimation(WidthProperty, ImageSizeAnimation);
        }// End of ToTrayAnimation method
        #endregion

        #region Clear background
        /// <summary>
        /// Clear all element in splash screen except app image.
        /// </summary>
        void ClearBackground()
        {
            BackGrid.Background = null; // clear color of splash screen background 

            AppVerTxtBl.Visibility = Visibility.Collapsed; // collapse version number textblock 

            AppName_TxtBl.Visibility = Visibility.Collapsed; // collapse app name text block

            Progressgrid.Visibility = Visibility.Collapsed; // collapse progress grid. it will clear child of it to.
        }// End of Clear Background method
        #endregion

        #region Random Time
        /// <summary>
        /// Make Random time for splash screen time left
        /// </summary>
        /// <returns>Return random int 2 - 5</returns>
        int RanadomTime()
        {
            return new Random().Next(2, 5); // make random number between 2 - 5. and return it to caller

        }// End of RandomTime Method
        #endregion

    }// End of SplashScreen Class
}// End of Mink_player_Core NameSpace
