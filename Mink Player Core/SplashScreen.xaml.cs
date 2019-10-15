using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mink_Player_Core
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        int TimeLeft = 5;

        DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
            IsEnabled = true
        };


        public SplashScreen()
        {
            InitializeComponent();
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TimeLeft == 0)
            {
                this.Close();
            }
            else
            {
                TimeLeft--;
            }
        }
    }
}
