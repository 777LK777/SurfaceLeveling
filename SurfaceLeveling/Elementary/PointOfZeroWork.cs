using SurfaceLeveling.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Elementary
{
    internal class PointOfZeroWork
    {        
        /// <summary>
        /// Возвращает список точек нулевых работ
        /// </summary>
        /// <param name="vertices">Список вершин квадратов поля</param>
        /// <returns>Список точек нулевых работ</returns>
        internal static IList<PointOfZeroWork> FactoryMethod(IEnumerable<SquareVertex> vertices)
        {
            List<PointOfZeroWork> points = new List<PointOfZeroWork>();
            Field<SquareVertex> field = new Field<SquareVertex>(vertices);

            // TODO: ТЕХДОЛГ Переписать на асинхрон!!!
            // Строки по X и строки по Y должны обходиться одновременно

            foreach(var row in field.XRows)
            {
                for(int i = 1; i< row.Length; i++)
                {
                    points.Add(new PointOfZeroWork(row[i - 1].WorkingMark, row[i].WorkingMark, field.Step));
                }
            }



            return null;
        }

        readonly double _firstWorkMark;
        readonly double _secondWorkMark;
        readonly double _distanceBeetwPoints;

        double _sumOfWorkMarks;
        double _distanceToFirstWorkMark;
        double _distanceToSecondWorkMark;

        public double DistanceBeetwenVertices { get => _distanceBeetwPoints; }
        public double SumWorkMarks { get => _sumOfWorkMarks; }

        private PointOfZeroWork(WorkingMark firstVertex, WorkingMark secondVertex, double distanceBeetwPoints)
        {
            _firstWorkMark = Math.Abs(firstVertex.WorkingHeight);
            _secondWorkMark = Math.Abs(secondVertex.WorkingHeight);
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
    }
}
