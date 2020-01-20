using SurfaceLeveling.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Frame
{
    public class Row<T>
        where T : class, IPositionable
    {
        // Node - узел координатной сетки
        readonly T[] _elements;

        /// <summary>
        /// Длина массива элементов
        /// </summary>
        readonly int _length;
        readonly double _step;

        public int Length
        {
            get { return _length; }
        }

        /// <summary>
        /// Шаг элементов в последовательности
        /// </summary>
        public double Step { get => _step; }

        internal Row(IEnumerable<T> elements)
        {
            // По умолчанию принимается условие, что все элементы располагаются на одной линии
            // Далее определяется какой координатной оси параллельна линия

            if (elements.Count() < 2)
                throw new ArgumentException("Количество элементов в последовательности должно быть больше 1");

            _length = elements.Count();
            _elements = elements.ToArray();
            _step = SetStep(_elements);

            



            //// TODO: Переопределить ToString()
            //if (((coords.Max() - coords.Min()) / coords.Count - 1) != coords.Average())
            //    throw new InvalidOperationException($"Не равный шаг в последовательности элементов для {this.ToString()}");            
        }

        public T this[int index]
        {
            get
            {
                return _elements[index];
            }
        }

        double CalculateStep(IEnumerable<double> coordinates)
        {
            return (coordinates.Max() - coordinates.Min()) / (coordinates.Count() - 1);
        }

        private double SetStep(IEnumerable<T> elements)
        {
            double[] nodesXCoords = elements.
                Where(el => el.IsNode).
                Select(el => el.CoordinateX).
                Distinct().
                ToArray();

            double[] nodesYCoords = elements.
                Where(el => el.IsNode).
                Select(el => el.CoordinateY).
                Distinct().
                ToArray();

            List<double> steps = new List<double>();

            if ((nodesXCoords.Length - 1) > 0)
                steps.Add(CalculateStep(nodesXCoords));

            if ((nodesYCoords.Length - 1) > 0)
                steps.Add(CalculateStep(nodesYCoords));

            return steps.Distinct().Single();
        }
    }
}
