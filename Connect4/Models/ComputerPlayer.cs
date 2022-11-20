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

        private List<int> GetAvailableColumns(BoardSlot[,] board)
        {
            List<int> columns = new List<int>();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i,0] == BoardSlot.Empty)
                {
                    columns.Add(i);
                }
            }

            return columns;
        }

        public override void Act(BoardSlot[,] board)
        {
            // Sick AI stuff
            Random rnd = new Random();

            List<int> colList = GetAvailableColumns(board);
            int choice = rnd.Next(0, colList.Count - 1);
            GameModel.Instance.DropChipAtColumn(choice);
        }
    }
}
