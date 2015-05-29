using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetVOD
{
    public static class CommandHelper 
    {

        public static void Excute(string message,IPlayer player)
        {
            var cmd = message.Substring(0, 7);
            switch (cmd)
            {
                case NetCommand.PLAY:
                    player.Play(Convert.ToInt32(message.Substring(7)));
                    break;
                case NetCommand.NEXT:
                    player.Next();
                    break;
                case NetCommand.PREV:
                    player.Prev();
                    break;
                case NetCommand.PLAYLISTADD:
                   var value = message.Substring(7);
                   player.AddToPlayList(Convert.ToInt32(value));
                    break;
                case NetCommand.REFRESH_DATA:
                    player.RefreshData();
                    break;
                default:
                    break;
            }
        }
    }
}
