using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Drawing;

namespace Mink_Player 
{
    public class SystemTray
    {
        

        Notification Notification;

        Microsoft.Win32.OpenFileDialog OpenFile = new Microsoft.Win32.OpenFileDialog();

        /// <summary>
        /// class constructor
        /// </summary>
        public SystemTray()
        {
            // Setup notify Icon
            

            OpenFile.ShowDialog();
            Notification = new Notification(OpenFile);
            
        }


        

        
    }
}
