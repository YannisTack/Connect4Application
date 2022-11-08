using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4.Models
{
    internal class GameModel
    {
        private GameView _view;
        private List <AutoSizeButton> _buttons;

        public List <AutoSizeButton> Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }
        public void ChangeButtonText(string txt)
        {
            Console.WriteLine("Button press - GameModel");
        }

        public GameModel(GameView view, Settings settings)
        {
            _view = view;
            _buttons = _view.AddColumns(settings.GetBoardSize[0]);
        }
    }
}
