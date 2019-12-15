using SurfaceLeveling.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Model
{
    internal class Square : IEnumerable
    {
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
            foreach(SquareVertex pt in PointsForFigure.OrderBy(pt => pt.Y).OrderBy(pt => pt.X))
            {
                str += pt.ToString() + "\n";
            }
            str += "--- --- --- ---";
            return str;
        }
    }

    class CenterOfGravity : IPositionable
    {
        private readonly double coordX;
        private readonly double coordY;

        public CenterOfGravity(List<SquareVertex> pointsForFigure)
        {
            coordX = pointsForFigure.Sum(pt => pt.X) / pointsForFigure.Count();

            coordY = pointsForFigure.Sum(pt => pt.Y) / pointsForFigure.Count();
        }

        public double CoordinateX => coordX;

        public double CoordinateY => coordY;
    }


    #region IEnumerable
    public class FigureEnum : IEnumerator
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
}
