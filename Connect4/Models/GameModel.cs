using Connect4.Properties;
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
        private GameView _gameView;
        private List <Chip> _buttons;
        private BoardSlot[,] _board;
        private GameState _currentGameState;

        private enum GameState
        {
            PlayerTurn,
            ComputerTurn,
            PlayerWin,
            ComputerWin
        }
        protected enum BoardSlot
        {
            Empty,
            PlayerBlue,
            ComputerRed
        }

        public List <Chip> Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }

        public GameModel(GameView view)
        {
            _gameView = view;
            int[] boardSize = Settings.Instance.BoardSize;
            _board = new BoardSlot[boardSize[0],boardSize[1]];
            //_buttons = _view.AddColumns(settings.BoardSize[0]);

            ResetGame();
        }

        public void ResetGame()
        {
            //_board = new BoardSlot[,];
            _currentGameState = GameState.PlayerTurn;
            _gameView.InitializeBoard();
        }

        public void DropChipAtColumn(int col)
        {
            // Drops chip at column
            // Change boardslot from lowest EMPTY at column
            // Push update to View

            // Find lowest EMPTY
            List<BoardSlot> slotColumn = new List<BoardSlot>();

            // TODO: Figure this out !!
            int num = _board.GetLength(col);

            for (int i = _board.GetLength(col); i > 0; i--)
            {
                if (_board[col, i] == BoardSlot.Empty)
                {
                    // Switch slot state and break iteration
                    _board[col, i] = BoardSlot.PlayerBlue;
                    break;
                }
            }

        }
    }
}
