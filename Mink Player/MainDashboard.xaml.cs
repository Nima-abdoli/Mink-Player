using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
<<<<<<< HEAD
<<<<<<< d255d48714d3c7928e8fc62c94e2a3a9c18bab8d
=======
using System.Windows.Threading;
>>>>>>> .net framework back again
=======
using System.Windows.Threading;
>>>>>>> 92f5757a04c5bb11e4dfceb3853e9106d464cb1c

namespace Mink_Player
{
    /// <summary>
    /// Interaction logic for MainDashboard.xaml
    /// </summary>
<<<<<<< HEAD
<<<<<<< d255d48714d3c7928e8fc62c94e2a3a9c18bab8d
=======

>>>>>>> 92f5757a04c5bb11e4dfceb3853e9106d464cb1c
    public partial class MainDashboard : Window
    {

        int TimeLeft = 30;

        DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
            IsEnabled = true
        };

        public MainDashboard()
        {
            InitializeComponent();
<<<<<<< HEAD
=======

    public partial class MainDashboard : Window
    {

        int TimeLeft = 30;

        DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
            IsEnabled = true
        };

        public MainDashboard()
        {
            InitializeComponent();
=======
>>>>>>> 92f5757a04c5bb11e4dfceb3853e9106d464cb1c
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TimeLeft == 0)
            {
                this.Hide();
            }
            else
            {
                TimeLeft--;
            }
<<<<<<< HEAD
>>>>>>> .net framework back again
=======
>>>>>>> 92f5757a04c5bb11e4dfceb3853e9106d464cb1c
        }
    }
}
