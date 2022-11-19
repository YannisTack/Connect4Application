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
        public HumanPlayer(string name) : base(name)
        {
            
        }

        public override void Act()
        {
            // Enable user to perform an action
            _isActing = true;

            throw new NotImplementedException();
        }
    }
}
