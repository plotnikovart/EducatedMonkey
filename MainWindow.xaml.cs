using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Media;

namespace EducatedMonkey
{
    public partial class MainWindow : Window
    {
        int n = 10;                                 // количество цифр в таблице умножения
        double diam, space;                         // диаметр и расстояние между кругами
        double x1, x2, y;                           // текущее положение ног
        bool leftfoot = false, rightfoot = false;   // Левая и правая нога

        Drawing draw;
        Coordinates coord;
        SoundPlayer player;

        public MainWindow()
        {
            InitializeComponent();
            Initialization(n);

            player = new SoundPlayer();
            player.Stream = Properties.Resources.Click;
        }

        /// <summary>
        /// События нажатия на изображение увеличения чисел в таблице
        /// </summary>
        private void arrowUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (n == 15)
                return;

            canv.Children.Clear();
            player.Play();
            Initialization(++n);
            label.Content = n.ToString();
        }

        /// <summary>
        /// События нажатия на изображение увеличения чисел в таблице
        /// </summary>
        private void arrowDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (n == 4)
                return;

            canv.Children.Clear();
            player.Play();
            Initialization(--n);
            label.Content = n.ToString();
        }

        /// <summary>
        /// Отрисовка поля
        /// </summary>
        /// <param name="n">Количество цифр</param>
        void Initialization(int n)
        {
            Ellipse e;
            Border bord;
            TextBlock text;
            diam = 2 * canv.Width / (3 * n - 1);
            space = diam / 2;
            for (int i = 0; i < n; i++)
            {
                // Добавление кругов
                e = new Ellipse();
                e.Height = e.Width = diam;
                e.Stroke = Brushes.SaddleBrown;
                e.Fill = Brushes.AliceBlue;
                e.StrokeThickness = 3;
                e.SetValue(Canvas.TopProperty, (canv.Height - diam));
                e.SetValue(Canvas.LeftProperty, (i * (diam + space)));
                canv.Children.Add(e);

                // Добавление текста в круги
                bord = new Border();
                bord.Height = diam;
                bord.Width = diam;
                text = new TextBlock();
                text.Text = (i + 1).ToString();
                text.FontSize = diam * 0.5;
                text.FontFamily = new FontFamily("Rockwell");
                text.Foreground = Brushes.DarkBlue;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.HorizontalAlignment = HorizontalAlignment.Center;
                bord.Child = text;
                bord.SetValue(Canvas.TopProperty, (canv.Height - diam));
                bord.SetValue(Canvas.LeftProperty, (i * (diam + space)));
                canv.Children.Add(bord);
            }

            // Отрисовка поля
            y = canv.Height - diam / 2;
            coord = new Coordinates(canv.Height, y);
            double dim = 0.8 * diam;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    coord.Calc(i * (diam + space) + diam / 2, j * (diam + space) + diam / 2);
                    double xa = coord.GetArms[0];
                    double ya = coord.GetArms[1];

                    // Добавление кругов
                    e = new Ellipse();
                    e.Width = e.Height = dim;
                    e.Fill = Brushes.AliceBlue;
                    e.Stroke = Brushes.DarkGreen;
                    e.StrokeThickness = 3;
                    e.SetValue(Canvas.TopProperty, ya);
                    e.SetValue(Canvas.LeftProperty, xa - dim / 2);
                    canv.Children.Add(e);

                    // Добавление текста в круги
                    bord = new Border();
                    bord.Height = dim;
                    bord.Width = dim;
                    text = new TextBlock();
                    text.Text = ((i + 1) * (j + 1)).ToString();
                    text.FontSize = dim * 0.5;
                    text.FontFamily = new FontFamily("Rockwell");
                    text.Foreground = Brushes.DarkBlue;
                    text.VerticalAlignment = VerticalAlignment.Center;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    bord.Child = text;
                    bord.SetValue(Canvas.TopProperty, ya);
                    bord.SetValue(Canvas.LeftProperty, xa - dim / 2);
                    canv.Children.Add(bord);
                }
            }

            // Начальное положение обезьянки
            x1 = diam / 2;
            x2 = (n - 1) * (diam + space) + diam / 2;
            coord.Calc(x1, x2);
            draw = new Drawing(canv, coord.LenLeg, coord.LenArm, diam / 2);
            draw.Draw(coord.GetCord);
        }

        /// <summary>
        /// Событие нажатия кнопки
        /// </summary>
        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Текущие координаты
            double _x = e.GetPosition(canv).X;
            double _y = e.GetPosition(canv).Y;

            if (Math.Sqrt((_x - x1) * (_x - x1) + (_y - y) * (_y - y)) <= diam / 2)
            {
                leftfoot = true;
            }
            else if (Math.Sqrt((_x - x2) * (_x - x2) + (_y - y) * (_y - y)) <= diam / 2)
            {
                rightfoot = true;
            }
        }

        /// <summary>
        /// Событие движения мыши
        /// </summary>
        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (leftfoot)
            {
                if (e.GetPosition(pole).X >= x2 - diam - space)
                    return;

                x1 = e.GetPosition(pole).X;
                coord.Calc(x1, x2);
                draw.Draw(coord.GetCord);
            }

            if (rightfoot)
            {
                if (x1 >= e.GetPosition(pole).X - diam - space)
                    return;

                x2 = e.GetPosition(pole).X;
                coord.Calc(x1, x2);
                draw.Draw(coord.GetCord);
            }
        }

        /// <summary>
        /// Событие отпуска кнопки
        /// </summary>
        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (leftfoot)
            {
                leftfoot = false;
                double x = e.GetPosition(pole).X;

                if (x >= x2 - diam - space)
                {
                    player.Play();
                    return;
                }

                if (Math.Abs(x % (diam + space) - diam / 2) <= diam / 2)
                {
                    player.Play();
                    int i = (int)(x / (diam + space));
                    x1 = i * (diam + space) + diam / 2;
                    coord.Calc(x1, x2);
                    draw.Draw(coord.GetCord);
                }
            }

            if (rightfoot)
            {
                rightfoot = false;
                double x = e.GetPosition(pole).X;

                if (x1 >= x - diam - space)
                {
                    player.Play();
                    return;
                }

                if (Math.Abs(x % (diam + space) - diam / 2) <= diam / 2)
                {
                    player.Play();
                    int i = (int)(x / (diam + space));
                    x2 = i * (diam + space) + diam / 2;
                    coord.Calc(x1, x2);
                    draw.Draw(coord.GetCord);
                }
            }
        }
    }
}