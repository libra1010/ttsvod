using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace VODClient
{
    public class CommandHelper
    {
        public static void Excute(string message, IClientPlayer player)
        {
            var cmd = message.Substring(0, 7);
            switch (cmd)
            {
                case NetCommand.LIST:
                    var data = message.Substring(7);
                    var list = JsonConvert.DeserializeObject<IList<Song>>(data);
                    player.BindList(list);
                    break;
                case NetCommand.PLAYLIST:
                    var playlist = JsonConvert.DeserializeObject<IList<Song>>(message.Substring(7));
                    player.BindPlayList(playlist);
                    break;
                case NetCommand.REFRESH_DATA:
                    player.SetImportEnable();
                    break;
                case NetCommand.CLEAR_PLAYLIST:
                    player.ClearPlayList();
                    break;
                default:
                    break;
            }
        }
    }
}
