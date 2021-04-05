using System.Drawing;

namespace Paint
{
    // класс, который хранит точки
    public class ArrayPoints
    {
        private int index; // номер текущей точки в массиве
        private Point[] points; // массив точек

        public ArrayPoints(int size)
        {
            //MessageBox.Show(size.ToString());

            if (size <= 0) // чтобы нельзя было инициализировать массив с отрицательным числом
            {
                size = 2;
            }

            points = new Point[size]; // инициализация массива
        }

        public void SetPoint(int x, int y)
        {
            if (index >= points.Length) // выход за границы
            {
                index = 0;
            }

            points[index] = new Point(x, y); // будем присваивать новую точку
            index++;

        }

        // метод сбрасывания точек (например когда отпустили
        // клик мышки и предыдущая точка не сохранялась)
        public void ResetPoint()
        {
            index = 0;
        }

        // метод получения индекса последней точки
        public int GetLastIndex()
        {
            return index;
        }

        // метод возращает массив наших точек для отрисования рисунка

        public Point[] GetPoints()
        {
            return points;
        }

    }
}
