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
    public class GameModel
    {
        private GameView _gameView;
        private BoardSlot[,] _board;
        private GameState _currentGameState;
        private List <Player> _players;

        // Singleton var
        private static GameModel _instance = null;

        public static GameModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameModel();
                }
                return _instance;
            }
        }

        public BoardSlot[,] Board
        {
            get { return _board; }
        }

        public enum GameState
        {
            Player1Turn,
            Player2Turn,
            Player1Win,
            Player2Win
        }
        public enum BoardSlot
        {
            Empty,
            Player1,
            Player2
        }

        private void NextPlayerTurn()
        {
            if (_currentGameState == GameState.Player1Turn)
            {
                _currentGameState = GameState.Player2Turn;
            }
            else 
            {  
                _currentGameState = GameState.Player1Turn;
            }

            _gameView.UpdateLabelView(GetCurrentPlayer(), false);

            if (GetCurrentPlayer().GetType() == typeof(ComputerPlayer))
            {
                GetCurrentPlayer().Act(_board);
            }
            else
            {
                GetCurrentPlayer().Act();
            }
        }

        private bool CheckForWin(int lastChipXPos, int lastChipYPos)
        {
            int numberOfConnected = 1;
            BoardSlot slotValidator = _board[lastChipXPos, lastChipYPos];

            // Is chip at border?
            bool isAtLeftBorder = (lastChipXPos == 0);
            bool isAtRightBorder = (lastChipXPos == _board.GetLength(0) -1);
            bool isAtBottomBorder = (lastChipYPos == _board.GetLength(1) -1);
            bool isAtTopBorder = (lastChipYPos == 0);

            #region Check Diagonal 1 (\)
            if (!isAtLeftBorder && !isAtTopBorder)
            {
                // Left
                for (int i = 1; lastChipXPos - i >= 0 && lastChipYPos - i >= 0; i++)
                {
                    if (_board[lastChipXPos - i, lastChipYPos - i] == slotValidator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (!isAtRightBorder && !isAtBottomBorder)
            {
                // Right
                for (int i = 1; lastChipXPos + i < _board.GetLength(0) && lastChipYPos + i < _board.GetLength(1); i++)
                {
                    if (_board[lastChipXPos + i, lastChipYPos + i] == slotValidator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (numberOfConnected >= 4) { return true; }
            #endregion

            #region Check Diagonal 2 (/)
            numberOfConnected = 1;
            if (!isAtLeftBorder && !isAtBottomBorder)
            {
                // Left
                for (int i = 1; lastChipXPos - i >= 0 && lastChipYPos + i < _board.GetLength(1); i++)
                {
                    if (_board[lastChipXPos - i, lastChipYPos + i] == slotValidator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (!isAtRightBorder && !isAtTopBorder)
            {
                // Right
                for (int i = 1; lastChipXPos + i < _board.GetLength(0) && lastChipYPos - i >= 0; i++)
                {
                    if (_board[lastChipXPos + i, lastChipYPos - i] == slotValidator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (numberOfConnected >= 4) { return true; }
            #endregion

            #region Check Horizontal (-)
            numberOfConnected = 1;
            if (!isAtLeftBorder)
            {
                // Left
                for (int i = 1; lastChipXPos - i >= 0; i++)
                {
                    if (_board[lastChipXPos - i, lastChipYPos] == slotValidator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
                
            }
            if (!isAtRightBorder)
            {
                // Right
                for (int i = 1; lastChipXPos + i < _board.GetLength(0); i++)
                {
                    if (_board[lastChipXPos + i, lastChipYPos] == slotValidator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (numberOfConnected >= 4) { return true; }
            #endregion

            #region Check Vertical (|)
            numberOfConnected = 1;
            if (!isAtBottomBorder)
            {
                for (int i = 1; lastChipYPos + i < _board.GetLength(1); i++)
                {
                    if (_board[lastChipXPos, lastChipYPos + i] == slotValidator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (numberOfConnected >= 4) { return true; }
            #endregion

            return false;
        }

        // Constructor
        private GameModel()
        {
            _gameView = (GameView)Form.ActiveForm;
        }

        private Player GetCurrentPlayer()
        {
            if (_currentGameState == GameState.Player1Turn || _currentGameState == GameState.Player1Win)
            {
                return _players[0];
            }
            else
            {
                return _players[1];
            }
        }

        public void ResetGame()
        {
            // Init board
            int[] boardSize = Settings.Instance.BoardSize;
            _board = new BoardSlot[boardSize[0], boardSize[1]];

            // Init players
            _players = new List<Player>();
            _players.Add(new HumanPlayer("Player1"));
            _players.Add(new ComputerPlayer("Computer"));

            // Init gamestate
            _currentGameState = GameState.Player1Turn;

            // Init UI
            _gameView.ClearView();
            _gameView.InitializeBoard();
            _gameView.UpdateLabelView(GetCurrentPlayer(), false);
        }

        public void DropChipAtColumn(int col)
        {
            // Drops chip at column
            // Change boardslot from lowest EMPTY at column
            // Push update to View
            GetCurrentPlayer().EndAct();

            // Find lowest EMPTY
            for (int row = _board.GetLength(1) -1; row >= 0; row--)
            {
                if (_board[col, row] == BoardSlot.Empty)
                {
                    // Switch slot state and break iteration
                    _board[col, row] = BoardSlot.Player1;

                    // Set chip color depending on turn
                    if (_currentGameState == GameState.Player1Turn)
                    {
                        _board[col, row] = BoardSlot.Player1;
                    }
                    else
                    {
                        _board[col, row] = BoardSlot.Player2;
                    }

                    // Update grid UI
                    _gameView.UpdateGridView(_board);

                    // Check for win and change game state
                    if (CheckForWin(col, row))
                    {
                        // Update gamestate UI -> push winner + win = true
                        _gameView.UpdateLabelView(GetCurrentPlayer(), true);
                        _gameView.DisableAllButtons();
                    }
                    else 
                    {
                        NextPlayerTurn();
                        
                    }

                    break;
                }
            }

        }
    }
}
