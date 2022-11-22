using Connect4.Controller;
using Connect4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class GameView : Form
    {
        private GameController _controller;
        private List<MyButton> _buttons = new List<MyButton>();
        private List<ChipSlot> _grid;
        private Label _gameStateText;

        const int ChipWidth = 75;
        const int ChipHeight = 75;
        const int ButtonWidth = 75;
        const int ButtonHeight = 40;
        const string ChipNamePrefix = "chipSlot_";
        const float LabelFontSize = 20;
        private Point TopLeftCornerBoard = new Point(20, 100);

        // Menu items
        private ToolStripItem _btnNewGame;

        public GameView()
        {
            InitializeComponent();
            CollectMenuItems();

            try
            {
                Settings.Instance = new Settings();
                _controller = new GameController(this, _buttons, _btnNewGame);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void CollectMenuItems()
        {
            // Collect and store all menu items to push to controller
            ToolStripItemCollection menuItems = menuStrip1.Items; 

            foreach (ToolStripItem item in menuItems)
            {
                if (item.Name == "btnNewGame")
                {
                    _btnNewGame = item;
                }
            }
        }

        private void AddGameStateText()
        {
            _gameStateText = new Label()
            {
                Name = "txt_GameState",
                Text = "Player's turn",
                Font = new Font(FontFamily.GenericSerif, LabelFontSize),
                AutoSize = true,
                Location = new Point(50, 50)
            };

            Controls.Add(_gameStateText);
        }

        private void AddColumnButtons(int count)
        {
            // Adds the buttons which will be used to drop chips

            _buttons = new List<MyButton>();

            for (int i = 0; i < count; i++)
            {
                MyButton b = new MyButton()
                {
                    Name = "btn_" + i,
                    Id = i,
                    Text = "V",
                    Size = new Size(ButtonWidth, ButtonHeight),
                    Location = new Point((TopLeftCornerBoard.X + ButtonWidth * i), TopLeftCornerBoard.Y)
                };
                _controller.AddButtonListener(b, GameController.ButtonListenerTypes.ColumnButton);
                _buttons.Add(b);
                Controls.Add(b);
            }
        }

        private void CreateGrid(int columns, int rows)
        {
            // Create grid where the chips will fall
            _grid = new List<ChipSlot>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    ChipSlot b = new ChipSlot(j, i)
                    {
                        Name = ChipNamePrefix + j + i,
                        Size = new Size(ChipWidth, ChipHeight),
                        Location = new Point((TopLeftCornerBoard.X + 75 * j), ((TopLeftCornerBoard.Y + ButtonHeight) + ChipHeight * i)),
                        BackColor = Color.Transparent,
                        ForeColor = Color.Transparent,
                        FlatStyle = FlatStyle.Flat
                    };

                    // Disable hover color effect
                    b.FlatAppearance.MouseOverBackColor = b.BackColor;
                    b.BackColorChanged += (s, e) => {
                        b.FlatAppearance.MouseOverBackColor = b.BackColor;
                    };

                    // Store and add to view controls
                    _grid.Add(b);
                    Controls.Add(b);
                }
            }
        }

        public void InitializeBoard()
        {
            int x = Settings.Instance.BoardSize[0];
            int y = Settings.Instance.BoardSize[1];

            AddColumnButtons(x);
            CreateGrid(x, y);
        }

        public void ClearView()
        {
            // Clear label
            if (_gameStateText != null)
            {
                _gameStateText.Dispose();
                _gameStateText = null;
            }

            // Clear column buttons
            if (_buttons != null && _buttons.Count > 0)
            {
                foreach (MyButton button in _buttons)
                {
                    button.Dispose();
                    _buttons = null;
                }
            }

            // Clear grid
            if (_grid != null && _grid.Count > 0)
            {
                foreach (ChipSlot chipSlot in _grid)
                {
                    chipSlot.Dispose();
                    _grid = null;
                }
            }
        }

        public void UpdateView(Player player, GameModel.BoardSlot[,] board)
        {
            // Update label
            _gameStateText.Text = player.Name + "'s turn";

            // Update grid
            for (int i = 0; i < _grid.Count; i++)
            {
                GameModel.BoardSlot tempSlot = board[_grid[i].XPos, _grid[i].YPos];

                // Check for upper level slot filled
                if (tempSlot != GameModel.BoardSlot.Empty && _grid[i].YPos == 0)
                {
                    _buttons[i].Enabled = false;
                }

                // Set color for filled slots
                if (tempSlot == GameModel.BoardSlot.Player1)
                {
                    _grid[i].SetChipColor(ChipSlot.Color.Blue);
                    
                }
                else if (tempSlot == GameModel.BoardSlot.Player2)
                {
                    _grid[i].SetChipColor(ChipSlot.Color.Red);
                }
            }
        }

        public void UpdateLabelView(Player player, bool isWinner)
        {
            // Update gamestate label
            if (_gameStateText is null)
            {
                AddGameStateText();
            }
            if (!isWinner)
            {
                _gameStateText.Text = player.Name + "'s turn";
            }
            else
            {
                _gameStateText.Text = player.Name + " wins !!";
            }
        }

        public void UpdateGridView(GameModel.BoardSlot[,] board)
        {
            // Update grid
            for (int i = 0; i < _grid.Count; i++)
            {
                GameModel.BoardSlot tempSlot = board[_grid[i].XPos, _grid[i].YPos];

                // Check for upper level slot filled
                if (tempSlot != GameModel.BoardSlot.Empty && _grid[i].YPos == 0)
                {
                    _buttons[i].Enabled = false;
                }

                // Set color for filled slots
                if (tempSlot == GameModel.BoardSlot.Player1)
                {
                    _grid[i].SetChipColor(ChipSlot.Color.Blue);

                }
                else if (tempSlot == GameModel.BoardSlot.Player2)
                {
                    _grid[i].SetChipColor(ChipSlot.Color.Red);
                }
            }
        }

        public void DisableAllButtons()
        {
            foreach (MyButton button in _buttons)
            {
                button.Enabled = false;
            }
        }

        public ChipSlot GetChipOnGrid(int column, int row)
        {
            return _grid.Find(x => x.Name == ChipNamePrefix + column + row);
        }

        public MyButton GetColumnButton(int column)
        {
            return _buttons.Find(x => x.Id == column);
        }

    }
    public class MyButton : Button
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }

    public class ChipSlot : Button
    {
        // Custom Button class for auto scalable images

        private int _xPos;
        private int _yPos;
        private Image _imgChipPlayer1 = Image.FromFile(@"Resources\chip_blue.png");
        private Image _imgChipPlayer2 = Image.FromFile(@"Resources\chip_red.png");

        public int XPos { get { return _xPos; } }
        public int YPos { get { return _yPos; } }

        public enum Color
        {
            Blue,
            Red
        }

        public new Image Image
        {
            get { return base.Image; }
            set
            {
                Image newImage = new Bitmap(Width -10, Height - 10);
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.DrawImage(value, 0, 0, Width -10, Height -10);
                }
                base.Image = newImage;
            }
        }

        public ChipSlot(int xPos, int yPos) : base()
        {
            _xPos = xPos;
            _yPos = yPos;
        }

        public void SetChipColor(Color color)
        {
            // Fills chips color
            switch (color)
            {
                case Color.Blue:
                    this.Image = _imgChipPlayer1;
                    break;
                case Color.Red:
                    this.Image = _imgChipPlayer2;
                    break;
                default:
                    break;
            }
        }
    }
}
