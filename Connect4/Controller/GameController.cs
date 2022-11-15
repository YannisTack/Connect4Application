using Connect4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4.Controller
{
    internal class GameController
    {
        private GameView _gameView;
        private GameModel _gameModel;
        private List<MyButton> _buttons;

        public enum ButtonListenerTypes
        {
            NewGame,
            ColumnButton
        }

        public GameController(GameView gameView, List<MyButton> buttons, ToolStripItem newGameButton)
        {
            _gameView = gameView;
            _buttons = buttons;
            newGameButton.Click += newGame_Click;
        }

        public void AddButtonListener(Button btn, ButtonListenerTypes type)
        {
            switch (type)
            {
                case ButtonListenerTypes.NewGame:
                    btn.Click += newGame_Click;
                    break;
                case ButtonListenerTypes.ColumnButton:
                    btn.Click += button_Click;
                    break;
                default:
                    break;
            }
        }

        public void NewGame()
        {
            _gameModel = new GameModel(_gameView);
        }

        public void ChangeValue()
        {
            Console.WriteLine("Button press - Controller");
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            this.NewGame();
        }

        private void button_Click(object sender, EventArgs e)
        {
            // Drop chip at column depending on button name
            MyButton b = (MyButton)sender;
            _gameModel.DropChipAtColumn(b.Id);
        }
    }
}
