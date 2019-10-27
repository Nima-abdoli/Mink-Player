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
<<<<<<< d255d48714d3c7928e8fc62c94e2a3a9c18bab8d
=======
using System.Windows.Threading;
>>>>>>> .net framework back again

namespace Mink_Player
{
    /// <summary>
    /// Interaction logic for MainDashboard.xaml
    /// </summary>
<<<<<<< d255d48714d3c7928e8fc62c94e2a3a9c18bab8d
    public partial class MainDashboard : Window
    {
        public MainDashboard()
        {
            InitializeComponent();
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
>>>>>>> .net framework back again
        }
    }
}
