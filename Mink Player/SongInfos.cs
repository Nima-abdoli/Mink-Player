using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Mink_Player
{
    internal class SongInfos
    {
        //private string filepath { get; set; }

        private TagLib.File FileTag;

        public SongInfos(String Filepath)
        {
            FileTag = TagLib.File.Create(Filepath);

            GetArtist();
        }

        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Year { get; set; }
        public string Track { get; set; }
        public string Genre { get; set; }
        public string AlbumArtist { get; set; }
        public int Length { get; set; }
        public BitmapImage cover { get; set; }

        private void GetArtist()
        {
            if (FileTag.Tag.Performers.Length != 0)
            {
                Artist = FileTag.Tag.Performers[0];
            }
            else
            {
                Artist = string.Empty;
            }
        }
    }
}
