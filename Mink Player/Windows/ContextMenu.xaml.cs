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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mink_Player.Windows
{
    /// <summary>
    /// Interaction logic for ContextMenu.xaml
    /// </summary>
    public partial class ContextMenu : Window
    {
        public ContextMenu()
        {
            InitializeComponent();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
            Activate();
            
            Storyboard sb = this.FindResource("SlideUp") as Storyboard;
            sb.Begin();

        }
    }
}
