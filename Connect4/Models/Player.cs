using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public abstract class Player
    {
        private string _name;
        protected bool _isActing = false;

        public string Name { get { return _name; } }

        public bool IsActing { get { return _isActing; } }

        public Player(string name)
        {
            _name = name;
        }
        public abstract void Act();
    }
}
