using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Connect4.Models.GameModel;

namespace Connect4.Models
{
    internal class ComputerPlayer : Player
    {
        public ComputerPlayer(string name) : base(name)
        {
        }

        private List<int[]> GetAvailableSlots(BoardSlot[,] board)
        {
            List<int[]> slots = new List<int[]>();

            for (int col = 0; col < board.GetLength(0); col++)
            {
                if (board[col, 0] == BoardSlot.Empty)
                {
                    // Find lowest EMPTY
                    for (int row = board.GetLength(1) - 1; row >= 0; row--)
                    {
                        if (board[col, row] == BoardSlot.Empty)
                        {
                            slots.Add(new int[] { col, row });
                            break;
                        }
                    }
                }
            }

            return slots;
        }

        private bool IsPossibleWin(int XPos, int YPos, BoardSlot validator)
        {
            int numberOfConnected = 1;
            BoardSlot[,] board = GameModel.Instance.Board;

            // Is chip at border?
            bool isAtLeftBorder = (XPos == 0);
            bool isAtRightBorder = (XPos == board.GetLength(0) - 1);
            bool isAtBottomBorder = (YPos == board.GetLength(1) - 1);
            bool isAtTopBorder = (YPos == 0);

            #region Check Diagonal 1 (\)
            if (!isAtLeftBorder && !isAtTopBorder)
            {
                // Left
                for (int i = 1; XPos - i >= 0 && YPos - i >= 0; i++)
                {
                    if (board[XPos - i, YPos - i] == validator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (!isAtRightBorder && !isAtBottomBorder)
            {
                // Right
                for (int i = 1; XPos + i < board.GetLength(0) && YPos + i < board.GetLength(1); i++)
                {
                    if (board[XPos + i, YPos + i] == validator)
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
                for (int i = 1; XPos - i >= 0 && YPos + i < board.GetLength(1); i++)
                {
                    if (board[XPos - i, YPos + i] == validator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }
            }
            if (!isAtRightBorder && !isAtTopBorder)
            {
                // Right
                for (int i = 1; XPos + i < board.GetLength(0) && YPos - i >= 0; i++)
                {
                    if (board[XPos + i, YPos - i] == validator)
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
                for (int i = 1; XPos - i >= 0; i++)
                {
                    if (board[XPos - i, YPos] == validator)
                    {
                        numberOfConnected++;
                    }
                    else break;
                }

            }
            if (!isAtRightBorder)
            {
                // Right
                for (int i = 1; XPos + i < board.GetLength(0); i++)
                {
                    if (board[XPos + i, YPos] == validator)
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
                for (int i = 1; YPos + i < board.GetLength(1); i++)
                {
                    if (board[XPos, YPos + i] == validator)
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

        public override void Act(BoardSlot[,] board)
        {
            // Sick AI stuff
            Random rnd = new Random();
            int choice = -1;

            List<int[]> slotList = GetAvailableSlots(board);

            // Check for winnable move
            foreach (int[] slot in slotList)
            {
                // Can I win ?
                if (IsPossibleWin(slot[0], slot[1], BoardSlot.Player2))
                {
                    choice = slot[0];
                    break;
                }
                // Can the other player win ?
                else if (IsPossibleWin(slot[0], slot[1], BoardSlot.Player1))
                {
                    choice = slot[0];
                    break;
                }
            }

            // If no choice made on ground of above reasons, then pick random
            if (choice == -1)
            {
                choice = rnd.Next(0, slotList.Count - 1);
            }
            
            GameModel.Instance.DropChipAtColumn(choice);
        }

    }
}
