using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class NetCommand
    {
        public const string PLAY = "CMD0001";

        public const string STOP = "CMD0002";

        public const string PAUSH = "CMD0003";

        public const string NEXT = "CMD0004";

        public const string PREV = "CMD0005";

        public const string LIST = "CMD0006";

        public const string PLAYLIST = "CMD0007";

        public const string SEARCH = "CMD0009";

        public const string PLAYLISTADD = "CMD0010";

        public const string REFRESH_DATA = "CMD0011";

        public const string CLEAR_PLAYLIST = "CMD0012";

    }

    public interface IPlayer
    {
        void Next();
        void Prev();

        void Play(int id);

        void AddToPlayList(int id);

        void RefreshData();

        void ClearPlayList();

        void RemovePlayList(int id);

    }

}
