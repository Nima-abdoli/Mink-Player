using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Microsoft.Win32;

using TagLib;

namespace Mink_Player
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        string FileToshow;

        TagLib.File tFile;


        public Notification(OpenFileDialog openFile)
        {
            InitializeComponent();

            //Set startup location of notification windows
            Left = SystemParameters.WorkArea.Width - (Width + 15);
            Top = SystemParameters.WorkArea.Height - (Height + 15);
            FileToshow = openFile.FileName;
        }

        public Notification()
        {
            InitializeComponent();

            //Set startup location of notification windows
            Left = SystemParameters.WorkArea.Width - (Width + 15);
            Top = SystemParameters.WorkArea.Height - (Height + 15);
        }

        /// <summary>
        /// Mouse down event 
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            tFile = TagLib.File.Create(FileToshow);
            NameTrack_TxtBl.Text = tFile.Tag.Title;
            NameArtist_TxtBl.Text = tFile.Tag.Artists[0];
            BackRound_Image.Source = ImageTaker();
            Cover_Image.Background = new ImageBrush(ImageTaker());
        }

        BitmapImage ImageTaker()
        {
            // Load you image data in MemoryStream
            TagLib.IPicture pic = tFile.Tag.Pictures[0];
            MemoryStream ms = new MemoryStream(pic.Data.Data);
            ms.Seek(0, SeekOrigin.Begin);

            // ImageSource for System.Windows.Controls.Image
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.EndInit();

            //ms.Dispose();

            return bitmap;
        }
    }
}
