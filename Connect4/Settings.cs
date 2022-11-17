using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    internal class Settings
    {
        private Dictionary<GridSize, int[]> _boardSize;
        private GridSize _gridSize = GridSize.Medium;

        public static Settings Instance;

        public enum GridSize
        {
            Small,
            Medium,
            Large
        }

        public void SetGridSize(GridSize size) { _gridSize = size; }

        public int[] BoardSize
        {
            get { return _boardSize[_gridSize]; }
        }

        public Settings()
        {
            Instance = this;
            InitBoardSizes();
            _gridSize = GridSize.Medium;
        }
        public Settings(GridSize gridSize)
        {
            InitBoardSizes();
            _gridSize = gridSize;
        }

        private void InitBoardSizes()
        {
            _boardSize = new Dictionary<GridSize, int[]>();
            _boardSize.Add(GridSize.Small, new int[] { 3, 3 });
            _boardSize.Add(GridSize.Medium, new int[] { 7, 6 });
            _boardSize.Add(GridSize.Large, new int[] { 15, 15 });
        }
    }
}
