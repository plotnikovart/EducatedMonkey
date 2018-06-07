using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EducatedMonkey
{
    class Drawing
    {
        Canvas canv;
        //Line _arm1, _arm2, _arm3, _arm4, _leg1, _leg2;
        Ellipse mark1, mark2, mark3;
        Image arm1, arm3, arm2, arm4, leg1, leg2, head;

        public Drawing(Canvas canv, double leg, double arm, double r)
        {
            this.canv = canv;
            // Инициализация линий
            //InitializeLine(ref _arm1);
            //InitializeLine(ref _arm2);
            //InitializeLine(ref _arm3);
            //InitializeLine(ref _arm4);
            //InitializeLine(ref _leg1);
            //InitializeLine(ref _leg2);

            // Инициализация меток
            InitializeMark(ref mark1, r * 1.4);
            InitializeMark(ref mark2, r * 1.4);
            InitializeMark(ref mark3, r * 1.6);

            // Инициализация изображений
            InitializeImage(ref leg1, "../resources/leg1.png", leg - r * 0.5);
            InitializeImage(ref leg2, "../resources/leg2.png", leg - r * 0.5);
            InitializeImage(ref arm1, "../resources/arm1.png", arm);
            InitializeImage(ref arm2, "../resources/arm2.png", arm);
            InitializeImage(ref arm3, "../resources/arm3.png", arm + 10);
            InitializeImage(ref arm4, "../resources/arm4.png", arm + 10);
            InitializeImage(ref head, "../resources/head.png", arm * 0.7);
        }

        /// <summary>
        /// Метод для рисования обезьянки
        /// </summary>
        /// <param name="m">Массив координат конечностей</param>
        public void Draw(double[] m)
        {
            // Добавление линий
            //AddLine(_leg1, m[0], m[2], m[3], m[4]);
            //AddLine(_leg2, m[1], m[2], m[3], m[4]);
            //AddLine(_arm1, m[7], m[8], m[3], m[4]);
            //AddLine(_arm2, m[9], m[10], m[3], m[4]);
            //AddLine(_arm3, m[7], m[8], m[5], m[6]);
            //AddLine(_arm4, m[9], m[10], m[5], m[6]);

            // Добавление меток
            AddMark(mark1, m[0], m[2]);
            AddMark(mark2, m[1], m[2]);
            AddMark(mark3, m[5], m[6] + mark3.Height / 2);

            // Левая верхняя рука (arm1)
            arm1.SetValue(Canvas.TopProperty, m[4] + 60);
            arm1.SetValue(Canvas.LeftProperty, m[3] - 114);
            arm1.RenderTransformOrigin = new Point(1, 0);
            arm1.RenderTransform = new RotateTransform(180 / Math.PI * Math.Atan((m[3] - m[7]) / (m[8] - m[4])));

            // Правая верхняя рука (arm2)
            arm2.SetValue(Canvas.TopProperty, m[4] + 60);
            arm2.SetValue(Canvas.LeftProperty, m[3]);
            arm2.RenderTransformOrigin = new Point(0, 0);
            arm2.RenderTransform = new RotateTransform(180 / Math.PI * Math.Atan((m[3] - m[9]) / (m[10] - m[4])));

            // Левая нижняя рука (arm3)
            arm3.SetValue(Canvas.TopProperty, m[8] - 10);
            arm3.SetValue(Canvas.LeftProperty, m[7] - 81.5625);
            arm3.RenderTransformOrigin = new Point(1, 0);
            arm3.RenderTransform = new RotateTransform(-180 / Math.PI * Math.Atan((m[5] - m[7]) / (m[6] - m[8])));

            // Правая нижняя рука (arm4)
            arm4.SetValue(Canvas.TopProperty, m[10] - 10);
            arm4.SetValue(Canvas.LeftProperty, m[9]);
            arm4.RenderTransformOrigin = new Point(0, 0);
            arm4.RenderTransform = new RotateTransform(180 / Math.PI * Math.Atan((m[5] - m[9]) / (m[10] - m[6])));

            // Левая нога (leg1)
            leg1.SetValue(Canvas.TopProperty, m[4]);
            leg1.SetValue(Canvas.LeftProperty, m[3] - 231 + 20);
            leg1.RenderTransformOrigin = new Point(1, 0);
            leg1.RenderTransform = new RotateTransform(180 / Math.PI * Math.Atan((m[3] - m[0]) / (m[2] - m[4])));

            // Правая нога (leg2)
            leg2.SetValue(Canvas.TopProperty, m[4]);
            leg2.SetValue(Canvas.LeftProperty, m[3] - 20);
            leg2.RenderTransformOrigin = new Point(0, 0);
            leg2.RenderTransform = new RotateTransform(180 / Math.PI * Math.Atan((m[3] - m[1]) / (m[2] - m[4])));

            // Голова (head)
            head.SetValue(Canvas.TopProperty, m[4] - head.Height / 2 + 25);
            head.SetValue(Canvas.LeftProperty, m[3] - 170.7);
        }

        /// <summary>
        /// Инициализация линий
        /// </summary>
        void InitializeLine(ref Line line)
        {
            line = new Line();
            line.StrokeThickness = 4;
            line.Stroke = Brushes.LightSteelBlue;
            canv.Children.Add(line);
        }

        /// <summary>
        /// Добавление линий
        /// </summary>
        void AddLine(Line line, double x1, double y1, double x2, double y2)
        {
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
        }

        /// <summary>
        /// Инициализация меток
        /// </summary>
        /// <param name="diam">Диаметр эллипса</param>
        void InitializeMark(ref Ellipse e, double diam)
        {
            e = new Ellipse();
            e.Height = e.Width = diam;
            e.Stroke = Brushes.Gray;
            e.StrokeThickness = 3;
            e.Fill = Brushes.LightSkyBlue;
            e.Opacity = 0.5;
            canv.Children.Add(e);
        }

        /// <summary>
        /// Добавление метки
        /// </summary>
        void AddMark(Ellipse e, double x, double y)
        {
            e.SetValue(Canvas.TopProperty, y - e.Height / 2);
            e.SetValue(Canvas.LeftProperty, x - e.Width / 2);
        }

        /// <summary>
        /// Инициализация изображения
        /// </summary>
        /// <param name="image">Картинка</param>
        /// <param name="source">Путь</param>
        /// <param name="len">Высота картинки</param>
        void InitializeImage(ref Image image, string source, double len)
        {
            image = new Image();
            image.Source = new BitmapImage(new Uri(source, UriKind.Relative));
            image.Height = len;
            canv.Children.Add(image);
        }
    }
}