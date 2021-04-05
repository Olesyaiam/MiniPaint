using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }

        private int _lastX;
        private int _lastY;

        private int _lastDownX;
        private int _lastDownY;

        private bool _isMouse; // mouse зажат в данный момент или нет
        private readonly ArrayPoints _arrayPoints = new ArrayPoints(2);

        Bitmap _map = new Bitmap(100, 100); // переменная для хранения изображения
        private Graphics _graphics;

        private readonly Pen _pen = new Pen(Color.Black, 3f);
        private Color _lastPenColor = Color.Black;

        private void SetSize() // размер для битмапа
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds; // берет разрешение экрана пользователя
            _map = new Bitmap(rectangle.Width, rectangle.Height); // ширина и высота
            _graphics = Graphics.FromImage(_map);

            // для сглаживания линии (без этого она прирывистая)
            _pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            _pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        public void DrawPoint(int x, int y)
        {
            _graphics.FillEllipse(
                new SolidBrush(_pen.Color),
                x,
                y,
                _pen.Width,
                _pen.Width
                );
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1_MouseDown();
        }

        private void pictureBox1_MouseDown()
        {
            _lastDownX = _lastX;
            _lastDownY = _lastY;
            _isMouse = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1_MouseUp();
        }

        private void pictureBox1_MouseUp()
        {
            if (_lastDownX == _lastX && _lastDownY == _lastY)
            {
                DrawPoint(_lastX, _lastY);
            }

            _isMouse = false;
            _arrayPoints.ResetPoint(); //сбрасывание массива, отпуская кнопку мыши мы ничего не сохраняем

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1_MouseMove(e.X, e.Y);
        }

        private void pictureBox1_MouseMove(int x, int y)
        {
            _lastX = x;
            _lastY = y;

            if (_isMouse == false) // нужно чтобы мы рисовали когда зажата лкм
            {
                return;
            }

            _arrayPoints.SetPoint(x, y); // заполнение массива

            if (_arrayPoints.GetLastIndex() >= 2)
            {
                _graphics.DrawLines(_pen, _arrayPoints.GetPoints()); // метод отрисовки линий
                pictureBox1.Image = _map; //pictureBox присваиваем рисунок
                _arrayPoints.SetPoint(x, y); // точки шли друг за другом неприрывисто можно закомментировать
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            _pen.Color = ((Button)sender).BackColor; // присваиваем цвет кнопки на которую мы нажали
            _lastPenColor = _pen.Color;

            checkBoxLastik.Checked = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = _map;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            _pen.Width = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ReSharper disable once LocalizableElement
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            saveFileDialog1.FileName = "picture";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (_isMouse == false)
            {
                return;
            }

            if (_arrayPoints.GetLastIndex() >= 0)
            {
                pictureBox1.Image = _map; //pictureBox присваиваем рисунок
            }
        }

        private void checkBoxLastik_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLastik.Checked)
            {
                _pen.Color = pictureBox1.BackColor;
            }
            else
            {
                _pen.Color = _lastPenColor;
            }
        }
    }
}
