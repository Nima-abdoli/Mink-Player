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

namespace Mink_Player.Windows
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        #region Constructor 

        /// <summary>
        /// Defult Constructor.
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();
        }// End of DashBoard

        #endregion

        #region Title Grid MouseDown Event
        /// <summary>
        /// Title grid mouse Down Event To move Windows
        /// </summary>
        private void Title_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // Check if Left Mouse button Down
            {
                DragMove(); // Move Windows
            }

        }// End of Title_Grid_MouseDown
        #endregion

        #region Close Window Button Click Event
        /// <summary>
        /// Close windows button click Event to close Windows
        /// </summary>
        private void CloseWindow_btn_Click(object sender, RoutedEventArgs e)
        {

            Hide(); // Hide Window From User.(Work And Present in Background)

        }// End of CloseWindow_Btn_Click
        #endregion

        #region Minimize Button Click Event
        /// <summary>
        /// minimize dashbourd window.
        /// </summary>
        private void Minimize_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // set Window state to minimize.

        }// End of Minimize_Btn_Click
        #endregion
    }
}
