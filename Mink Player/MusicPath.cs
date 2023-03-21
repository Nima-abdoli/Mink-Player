using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mink_Player
{
    public class MusicPath
    {
        public string Path { get; set; } // path property for MusicPath Class

    }// End of MusicPath Class

    /// <summary>
    /// Show Player state of player.
    /// </summary>
    public enum PlayerState { Play, Pause, Empty };
     
    /// <summary>
    /// Show Player Mute state of player.
    /// </summary>
    public enum PlayerMute { Mute,Unmute};

    
}
