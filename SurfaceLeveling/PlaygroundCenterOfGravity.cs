using SurfaceLeveling.Interfaces;
using SurfaceLeveling.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling
{
    internal class PlaygroundCenterOfGravity : IVertex
    {
        readonly double _projectHeightMark;
        readonly double _projectXCoord;
        readonly double _projectYCoord;

        #region ctor
        public PlaygroundCenterOfGravity(IEnumerable<Square> squares)
        {
            //double SummXi = 0;
            //foreach (double center in squares.
            //    Select(figure => figure.CenterOfGravity.y).
            //    OrderBy(y => y))
            //{
            //    SummXi += field.Squares.
            //        Where(f => f.CenterOfGravity.y == center).
            //        Count() * center;
            //}

            //double SummYi = 0;
            //foreach (double center in field.Squares.
            //    Select(f => f.CenterOfGravity.X).
            //    OrderBy(x => x))
            //{
            //    SummYi += field.Squares.
            //        Where(f => f.CenterOfGravity.X == center).
            //        Count() * center;
            //};

            ////
            //List<double> amount = new List<double>();   // Список сумм

            //// Вычисление сумм вершин:
            //foreach (int count in field._verticies.
            //    Select(pt => pt.CountCalls).
            //    Distinct().
            //    OrderBy(count => count))
            //{
            //    amount.Add(field._verticies.
            //        Where(pt => pt.CountCalls == count).
            //        Sum(pt => pt.AbsoluteMark));
            //}

            //double numerator = 0;   // числитель

            //for (int i = 1; i <= 4; i++)
            //{
            //    numerator += i * amount[i - 1];
            //}

            ////

            //new PlaygroundCenterOfGravity(X: SummXi / field.Squares.Count, Y: SummYi / field.Squares.Count, numerator / (field.Squares.Count() * 4));


            ////get
            ////{
            ////    double SummXi = 0;
            ////    foreach (double center in _squares.
            ////        Select(figure => figure.CenterOfGravity.y).
            ////        OrderBy(y => y))
            ////    {
            ////        SummXi += _squares.
            ////            Where(f => f.CenterOfGravity.y == center).
            ////            Count() * center;
            ////    }

            ////    double SummYi = 0;
            ////    foreach (double center in _squares.
            ////        Select(f => f.CenterOfGravity.X).
            ////        OrderBy(x => x))
            ////    {
            ////        SummYi += _squares.
            ////            Where(f => f.CenterOfGravity.X == center).
            ////            Count() * center;
            ////    };

            ////    return new PointP((SummXi / Squares.Count), (SummYi / Squares.Count));
            ////}

            //Console.WriteLine("Вычисляю высотную отметку центра тяжести");
            //// TODO: Нужна осуществить проверку значений (см. стр. 10)
            //List<double> amount = new List<double>();   // Список сумм

            //// Вычисление сумм вершин:
            //foreach (int count in _vertices.
            //    Select(pt => pt.CountCalls).
            //    Distinct().
            //    OrderBy(count => count))
            //{
            //    amount.Add(_vertices.
            //        Where(pt => pt.CountCalls == count).
            //        Sum(pt => pt.AbsoluteMark));
            //}

            //double numerator = 0;   // числитель

            //for (int i = 1; i <= 4; i++)
            //{
            //    numerator += i * amount[i - 1];
            //}
            //return numerator / (Squares.Count() * 4);   // 4 - константа

            //Console.WriteLine("Высота центра тяжести равна: " + (numerator / (Squares.Count() * 4)));

        }
        #endregion

        /// <summary>
        /// Проектная отметка центра тяжести площадки
        /// </summary>
        public double CoordinateZ { get => _projectHeightMark; }
        /// <summary>
        /// X-координата центра тяжести площадки
        /// </summary>
        public double CoordinateX { get => _projectXCoord; }
        /// <summary>
        /// Y-координата центра тяжести площадки
        /// </summary>
        public double CoordinateY { get => _projectYCoord; }

        public bool IsNode => false;
    }
}
