using System.Globalization;
using System.Reflection.Metadata;

namespace game_Live
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private bool[,] field;
        private int rows;
        private int cols;
        private int delimeter_resolution = 2;
        public Form1()
        {
            InitializeComponent();
        }
        private void StartGame()
        {
            if (timer1.Enabled) { return; }
            numResolution.Enabled = false;
            numDensity.Enabled = false;

            resolution = (int)numResolution.Value;
            rows = pictureBox1.Height / resolution;
            cols = pictureBox1.Width / resolution;
            field = new bool[cols, rows];

            Random rnd = new Random();
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = rnd.Next((int)numDensity.Value) == 0;
                }
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
            //graphics.FillRectangle(Brushes.Crimson, 0, 0, resolution, resolution);
        }

        private void NextGeneration()
        {
            graphics.Clear(Color.Black);
            var newField = new bool[cols, rows];
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var neighboursCount = CountNeighbours(i, j);
                    var hasLife = field[i, j];
                    if (!hasLife && neighboursCount == 3) { newField[i, j] = true; }
                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3)) { newField[i, j] = false; }
                    else { newField[i, j] = field[i, j]; }
                    if (hasLife)
                    {
                        graphics.FillRectangle(Brushes.Crimson, i * resolution, j * resolution, resolution - delimeter_resolution, resolution - delimeter_resolution);
                    }
                }
            }
            field = newField;
            pictureBox1.Refresh();
        }
        private int CountNeighbours(int i, int j)//счётчик соседей
        {
            int count = 0;
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    var col = (i + x + cols) % cols;
                    var row = (j + y + rows) % rows;
                    var isSelfCheking = col == i && row == j;
                    var haslife = field[col, row];
                    if (haslife && !isSelfCheking) { count++; }
                }
            }
            return count;
        }

        private void StopGame()
        {
            if (!timer1.Enabled) return;
            timer1.Stop();
            numResolution.Enabled = true;
            numDensity.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e) { NextGeneration(); }
        private void btnStart_Click(object sender, EventArgs e) { StartGame(); }
        private void btnStop_Click(object sender, EventArgs e) { StopGame(); }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //var mouseX = e.Location.X;
            //var mouseY = e.Location.Y;
            //Text = mouseX + " " + mouseY;
            //Text  = Size.Width.ToString()+" "+Size.Height.ToString();

            //if (( Size.Height> e.Location.Y|| e.Location.Y > 0) || (Size.Width > e.Location.X || e.Location.X > 0)){Text = "норма";}else{Text = "хуета";}
            //if (e.X > Size.Width || e.X<0 || (e.Y > ClientSize.Height || e.Y<0)) { Text = "не норма"; }
            int xx = e.X;
            int yy = e.Y;
            int size_X = ClientSize.Width;
            int size_Y = ClientSize.Height;
            if ((e.X > ClientSize.Width - 155 || e.X < 0) || (e.Y > ClientSize.Height - 10 || e.Y < 0)) { Text = "не норма"; }
            else
            {
                Text = "норма";

                if (!timer1.Enabled) return;
                if (e.Button == MouseButtons.Left)
                {
                    var x = e.X / resolution;
                    var y = e.Y / resolution;
                    field[x, y] = true;
                }
                if (e.Button == MouseButtons.Right)
                {
                    var x = e.Location.X / resolution;
                    var y = e.Location.Y / resolution;
                    field[x, y] = false;
                }


            }
            ////if(Size.Height<e.Location.Y||e.Location.Y==0)
            //if (!timer1.Enabled) return;
            //if (e.Button == MouseButtons.Left)
            //{
            //    var x = e.Location.X / resolution;
            //    var y = e.Location.Y / resolution;
            //    field[x, y] = true;
            //}
            //if (e.Button == MouseButtons.Right)
            //{
            //    var x = e.Location.X / resolution;
            //    var y = e.Location.Y / resolution;
            //    field[x, y] = false;
            //}
        }

        //private void mouse_move(object sender, MouseEventArgs e)
        //{
        //    //Point mouseXY = e.GetPosition(this);

        //    int mouseX = e.X;
        //    int mouseY = e.Y;
        //    string x = Form1.MousePosition.X.ToString();//text = "X:" + mouseX + " " + "Y:" + mouseY;
        //    string y = Form1.MousePosition.Y.ToString();//text = "X:" + mouseX + " " + "Y:" + mouseY;
        //  Text = x + " " + y;
        //    //mousePath.AddLine(mouseX, mouseY, mouseX, mouseY);
        //}

    }
}