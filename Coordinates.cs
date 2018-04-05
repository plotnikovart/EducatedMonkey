using System;

namespace EducatedMonkey
{
    class Coordinates
    {
        double Height;                          // высота холста
        double leg = 850;                       // длина ног
        double arm = 440;                       // длина рук
        double angle = 45 * (Math.PI / 180);    // угол между ногой и плечом
        // H - голова
        // A - ладони
        // B - левый локоть
        // C - правый локоть
        double x1, x2, y, xh, yh, xa, ya, xb, yb, xc, yc;

        public Coordinates(double Height, double y)
        {
            this.Height = Height;
            this.y = y;
        }

        /// <summary>
        /// Подсчет координат основных частей тела
        /// </summary>
        /// <param name="x1">Координата по x левой ноги</param>
        /// <param name="x2">Координата по x правой ноги</param>
        public void Calc(double x1, double x2)
        {
            xh = xa = (x1 + x2) / 2;
            double m = (x2 - x1) / 2;
            double h = Math.Sqrt(leg * leg - m * m);
            yh = Height - h;
            double z = (arm / leg) * (h * Math.Cos(angle) - m * Math.Sin(angle));
            ya = yh + 2 * z;
            xb = xh - Math.Sqrt(arm * arm - z * z);
            yb = yh + z;
            xc = xh + Math.Sqrt(arm * arm - z * z);
            yc = yh + z;

            this.x1 = x1;
            this.x2 = x2;
        }

        /// <summary>
        /// Получение координат всех конечностей
        /// </summary>
        public double[] GetCord
        {
            get
            {
                double[] m = { x1, x2, y, xh, yh, xa, ya, xb, yb, xc, yc };
                return m;
            }
        }

        /// <summary>
        /// Получение координат ладоней
        /// </summary>
        public double[] GetArms
        {
            get
            {
                double[] m = { xa, ya };
                return m;
            }
        }

        /// <summary>
        /// Получение длины ноги
        /// </summary>
        public double LenLeg
        {
            get
            {
                return Math.Sqrt((xh - x1) * (xh - x1) + (yh - y) * (yh - y));
            }
        }

        /// <summary>
        /// Получение длины руки
        /// </summary>
        public double LenArm
        {
            get
            {
                return arm;
            }
        }
    }
}