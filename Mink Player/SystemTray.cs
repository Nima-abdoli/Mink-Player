using System;

using System.Windows.Forms;
using System.Drawing;

namespace Mink_Player 
{
    public class SystemTray
    {
        public NotifyIcon MyNotify = new NotifyIcon();
        ContextMenu mContextMenu = new ContextMenu();

        /// <summary>
        /// class constructor
        /// </summary>
        public SystemTray()
        {
            // Setup notify Icon
            MyNotify.Icon = Properties.Resources.NotifyIcon;
            MyNotify.Visible = true;
            
            MyNotify.ContextMenu = mContextMenu;

            // Call this function to make contextmenu
            ContexSetup();
            
        }


        /// <summary>
        /// Setup up Context menu item
        /// </summary>
        void ContexSetup()
        {
            MenuItem ExitItem = new MenuItem("Exit");
            ExitItem.Click += ExitItem_Click;

            MenuItem ShowItem = new MenuItem("Show");
            ShowItem.Click += ShowItem_Click;

            MenuItem NotificationItem = new MenuItem("Show Notification");
            NotificationItem.Click += NotificationItem_Click;

            mContextMenu.MenuItems.Add(ShowItem);
            mContextMenu.MenuItems.Add(NotificationItem);
            mContextMenu.MenuItems.Add(ExitItem);
        }

        private void NotificationItem_Click(object sender, EventArgs e)
        {
            new Notification().Show();
        }

        /// <summary>
        /// Click Event For ShowItem
        /// </summary>
        private void ShowItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Show Click");
        }

        /// <summary>
        /// Click Event for ExitItem
        /// </summary>
        private void ExitItem_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
