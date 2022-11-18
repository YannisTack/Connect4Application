using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Models
{
    internal class HumanPlayer : Player
    {
        public HumanPlayer(string name, Image chipImage) : base(name, chipImage)
        {
            
        }

        public override void Act()
        {
            throw new NotImplementedException();
        }
    }
}
