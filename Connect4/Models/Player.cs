using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Models
{
    internal abstract class Player
    {
        private string _name;
        private Image _chipImage;

        public string GetName { get { return _name; } }
        public Image GetChipImage { get { return _chipImage; } }

        public Player(string name, Image chipImage)
        {
            _name = name;
            _chipImage = chipImage;
        }
        public abstract void Act();
    }
}
