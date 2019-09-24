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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestMink
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FadeOutBtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            da.Duration = TimeSpan.FromMilliseconds(2000);

            MyRect.BeginAnimation(OpacityProperty, da);
        }

        private void FadeInBtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = TimeSpan.FromMilliseconds(2000);

            MyRect.BeginAnimation(OpacityProperty, da);
        }

        private void CornerBtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 15;
            da.AutoReverse = true;
            
        }

        private void NormalBtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = MyRect.Width;
            da.To = 300;
            da.Duration = TimeSpan.FromMilliseconds(2000);
            MyRect.BeginAnimation(WidthProperty, da);
        }

        private void ShortBtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = MyRect.Width;
            da.To = 0;
            da.Duration = TimeSpan.FromMilliseconds(2000);
            MyRect.BeginAnimation(WidthProperty, da);
        }

        private void BigBtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = MyRect.Width;
            da.To = 500;
            da.Duration = TimeSpan.FromMilliseconds(2000);
            MyRect.BeginAnimation(WidthProperty, da);
        }
    }
}
