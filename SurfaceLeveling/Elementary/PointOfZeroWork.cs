using SurfaceLeveling.Frame;
using SurfaceLeveling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Elementary
{
    internal class PointOfZeroWork : IPositionable
    {        
        /// <summary>
        /// Возвращает список точек нулевых работ
        /// </summary>
        /// <param name="vertices">Список вершин квадратов поля</param>
        /// <returns>Список точек нулевых работ</returns>
        internal static IList<PointOfZeroWork> FactoryMethod(Field<SquareVertex> field)
        {
            List<PointOfZeroWork> points = new List<PointOfZeroWork>();

            // TODO: ТЕХДОЛГ Переписать на асинхрон!!!
            // Строки по X и строки по Y должны обходиться одновременно

            foreach(var row in field.XRows)
            {
                for(int i = 1; i< row.Length; i++)
                {
                    points.Add(new PointOfZeroWork(row[i - 1], row[i], field.Step));
                }
            }

            foreach(var row in field.YRows)
            {
                for(int i = 1; i< row.Length; i++)
                {
                    points.Add(new PointOfZeroWork(row[i - 1], row[i], field.Step));
                }
            }

            return points;
        }

        readonly double _firstWorkMark;
        readonly double _secondWorkMark;
        readonly double _distanceBeetwPoints;

        double _sumOfWorkMarks;
        double _distanceToFirstWorkMark;
        double _distanceToSecondWorkMark;

        public double DistanceBeetwenVertices { get => _distanceBeetwPoints; }
        public double SumWorkMarks { get => _sumOfWorkMarks; }        

        private PointOfZeroWork(SquareVertex firstVertex, SquareVertex secondVertex, double distanceBeetwPoints)
        {

            _firstWorkMark = Math.Abs(firstVertex.WorkingMark);
            _secondWorkMark = Math.Abs(secondVertex.WorkingMark);
            _distanceBeetwPoints = distanceBeetwPoints;

            SetSumWorkMarks(_firstWorkMark, _secondWorkMark);
            SetDistance(_firstWorkMark, out _distanceToFirstWorkMark);
            SetDistance(_secondWorkMark, out _distanceToSecondWorkMark);            
        }

        private void SetSumWorkMarks(double firstWorkMark, double secondWorkMark)
        {
            _sumOfWorkMarks = firstWorkMark + secondWorkMark;
        }

        private void SetDistance(double workMark, out double distanceToWorkMark)
        {
            distanceToWorkMark = (workMark * DistanceBeetwenVertices) / SumWorkMarks;
        }

        public double CoordinateX
        {
            get
            {


                return 0;
            }
        }

        public double CoordinateY
        {
            get
            {


                return 0;
            }
        }

        bool IPositionable.IsNode => false;
    }
}
