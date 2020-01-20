using SurfaceLeveling.Elementary;
using SurfaceLeveling.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Shapes
{
    internal class Square : IEnumerable
    {
        public static IList<Square> SquaresFactory(IEnumerable<SquareVertex> vertices)
        {
            //foreach (double Y in GridCoordsY)
            //{
            //    if ((Y + GridSpacing) > GridCoordsY.Max()) break;
            //    else
            //    {
            //        List<double> Xs = _vertices.
            //            Where(vs => vs.CoordinateY == Y).
            //            Select(vs => vs.X).
            //            OrderBy(X => X).
            //            ToList();

            //        foreach (double X in Xs)
            //        {
            //            if ((X + GridSpacing) > Xs.Max()) break;
            //            else
            //            {
            //                Squares.Add(new Square(_vertices.
            //                    Where(vs => (vs.Y >= Y) && (vs.Y <= (Y + GridSpacing))).
            //                    Where(vs => (vs.X >= X) && (vs.X <= (X + GridSpacing))), _squares.Count + 1));
            //            }
            //        }
            //    }


                return null;
            
        }


        // !! Ни одна из сторон фигур (Figure) не должна быть больше диагонали квадрата сетки
        // 5. Из точек нужно собрать фигуру (Figure) (для начала можно массив точек ((далее квадрат треугольник и трапеция))

        List<SquareVertex> PointsForFigure;

        readonly private CenterOfGravity Center;

        public Square(SquareVertex[] points, int SerialNo)
        {
            List<SquareVertex> PointsForFigure = new List<SquareVertex>();

            for (int i = 0; i < points.Length; i++)
            {
                PointsForFigure.Add(points[i].Call());
            }

            Center = new CenterOfGravity(PointsForFigure);
        }

        public IPositionable CenterOfGravity
        {
            get => Center;
        }

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public FigureEnum GetEnumerator()
        {
            return new FigureEnum(PointsForFigure.ToArray());
        }
        #endregion

        public override string ToString()
        {
            string str = string.Empty;
            foreach(SquareVertex pt in PointsForFigure.OrderBy(pt => pt.CoordinateY).OrderBy(pt => pt.CoordinateX))
            {
                str += pt.ToString() + "\n";
            }
            str += "--- --- --- ---";
            return str;
        }
    }
}

    class CenterOfGravity : IPositionable
    {
        private readonly double coordX;
        private readonly double coordY;

        public CenterOfGravity(List<SquareVertex> pointsForFigure)
        {
            coordX = pointsForFigure.Sum(pt => pt.CoordinateX) / pointsForFigure.Count();

            coordY = pointsForFigure.Sum(pt => pt.CoordinateY) / pointsForFigure.Count();
        }

        public double CoordinateX => coordX;

        public double CoordinateY => coordY;

    public bool IsNode => throw new NotImplementedException();
}


    #region IEnumerable
    internal class FigureEnum : IEnumerator
    {
        public SquareVertex[] points;
        int position = -1;

        public FigureEnum(SquareVertex[] list)
        {
            points = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < points.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public SquareVertex Current
        {
            get
            {
                try
                {
                    return points[position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }
    #endregion

