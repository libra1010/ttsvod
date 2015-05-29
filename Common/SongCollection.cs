using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SongCollection:List<Song>
    {
        public event EventHandler<EventArgs> CollectionChanged;


        private IPlayer _player;

        public SongCollection(IPlayer player)
        {
            this._player = player;
           
        }

        private int _index = -1;

        new public void Add(Song item)
        {
            base.Add(item);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, EventArgs.Empty);
            }
            if (this.Count == 1)
            {
                _player.Next();
            }
        }

        public Song Next()
        {
            if (this.Count == 0) 
            {
                return null;
            }
            var position = _index + 1;
            if (position < this.Count)
            {
                _index = position;
                return this[_index];
            }
            _index = 0;
            return this[0];
        }

        public Song Prev()
        {
            if (this.Count == 0)
            {
                return null;
            }
            if (_index > 0)
            {
                _index = _index - 1;
                return this[_index];
            }
            else
            {
                _index = this.Count - 1;
                return this[this.Count - 1];
            }
        }

        public Song GetById(int id)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ID == id)
                {
                    this._index = i;
                    return this[i];
                }   
            }
            return null;
        }

        public void RemoveById(int id)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ID == id)
                {
                    this.RemoveAt(i);
                    break;
                }
            }
        }

    }
}
