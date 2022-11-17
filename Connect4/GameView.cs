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
        private List<Chip> _grid;
        private Label _gameStateText;

        const int ChipWidth = 75;
        const int ChipHeight = 75;
        const int ButtonWidth = 75;
        const int ButtonHeight = 40;
        const string ChipNamePrefix = "chip_";
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

        public void InitializeBoard()
        {
            int x = Settings.Instance.BoardSize[0];
            int y = Settings.Instance.BoardSize[1];

            AddGameStateText();
            AddColumnButtons(x);
            CreateGrid(x, y);
        }

        private void AddGameStateText()
        {
            _gameStateText = new Label()
            {
                Name = "txt_GameState",
                Text = "Player turn",
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
            _grid = new List<Chip>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Chip b = new Chip()
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

        public Chip GetChipOnGrid(int column, int row)
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

    public class Chip : Button
    {
        // Custom Button class for auto scalable images
        private Image _imgChipPlayer = Image.FromFile(@"Resources\chip_blue.png");
        private Image _imgChipComputer = Image.FromFile(@"Resources\chip_red.png");

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

        public void SetChipColor(string color)
        {
            // Fills chips color
            switch (color)
            {
                case "blue":
                    this.Image = _imgChipPlayer;
                    break;
                case "red":
                    this.Image = _imgChipComputer;
                    break;
                default:
                    break;
            }
        }
    }
}
