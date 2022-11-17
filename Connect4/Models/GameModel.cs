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

        public enum GameState
        {
            PlayerTurn,
            ComputerTurn,
            PlayerWin,
            ComputerWin
        }
        public enum BoardSlot
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

        private void NextPlayerTurn()
        {
            if (_currentGameState == GameState.PlayerTurn)
            {
                _currentGameState = GameState.ComputerTurn;
            }
            else {  _currentGameState = GameState.PlayerTurn; }
        }

        private bool CheckForWin()
        {
            // Did current player win ?

            return false;
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
            for (int i = _board.GetLength(1) -1; i >= 0; i--)
            {
                if (_board[col, i] == BoardSlot.Empty)
                {
                    // Switch slot state and break iteration
                    _board[col, i] = BoardSlot.PlayerBlue;

                    // Set chip color depending on turn
                    if (_currentGameState == GameState.PlayerTurn)
                    {
                        _board[col, i] = BoardSlot.PlayerBlue;
                        _gameView.GetChipOnGrid(col, i).SetChipColor("blue");
                    }
                    else
                    {
                        _board[col, i] = BoardSlot.ComputerRed;
                        _gameView.GetChipOnGrid(col, i).SetChipColor("red");
                    }

                    // Disable column button if at top
                    if (i == 0) { _gameView.GetColumnButton(col).Enabled = false; }

                    // Check for win and change game state
                    // If win -> current player wins !
                    // If not win -> other player turn !
                    if (CheckForWin())
                    {

                    }
                    else { NextPlayerTurn(); }
                    
                    
                    break;
                }
            }

        }
    }
}
