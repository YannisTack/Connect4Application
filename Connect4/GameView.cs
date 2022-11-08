using Connect4.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class GameView : Form
    {
        private Connect4Controller _controller;

        public GameView()
        {
            InitializeComponent();
            _controller = new Connect4Controller(this);
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            _controller.NewGame();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button press - GameView");
            _controller.ChangeValue();
        }

        public List<AutoSizeButton> AddColumns(int count)
        {
            List<AutoSizeButton> list = new List<AutoSizeButton>();
            string s = Environment.CurrentDirectory;
            for (int i = 0; i < count; i++)
            {
                AutoSizeButton b = new AutoSizeButton()
                {
                    Name = "btn_" + i,
                    Text = "MyButton",
                    Size = new Size(75, 75),
                    Location = new Point((20 + 75 * i), 120),
                    Image = Image.FromFile(@"Resources\chip_blue.png"),
                    BackColor = Color.Transparent,
                    ForeColor = Color.Transparent,
                    FlatStyle = FlatStyle.Flat,
                };
                b.Click += button_Click;
                list.Add(b);
                Controls.Add(b);
            }
            
            return list;
        }
    }
    public class AutoSizeButton : Button
    {

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
    }
}
