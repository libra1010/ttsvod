using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public enum PlayState
    {
        None,
        Playing,
        Stopped,
        Paused
    }




    public class Song
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 演唱者
        /// </summary>
        public string Singer { get; set; }

    }

    public class Song2 : Song
    {
        public int OrderId { get; set; }


    }
}
