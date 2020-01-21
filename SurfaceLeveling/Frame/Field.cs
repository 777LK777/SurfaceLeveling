using SurfaceLeveling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Frame
{
    /// <summary>
    /// Класс для ориентирования по полю точек
    /// </summary>
    /// <typeparam name="T">Тип с координатами X и Y</typeparam>
    public class Field<T>
        where T : class, IPositionable
    {
        readonly Row<T>[] _xRows;
        readonly Row<T>[] _yRows;
        readonly double _step;

        public IList<Row<T>> XRows
        {
            get { return _xRows; }
        }

        public IList<Row<T>> YRows
        {
            get { return _yRows; }
        }

        /// <summary>
        /// Шаг координатной сетки
        /// </summary>
        public double Step { get => _step; }

        public Field(IEnumerable<T> elements)
        {
            /*
             Линия проводится даже через одну точку
             Линия добавляется в строку, если проходит более чем через один узел
            */

            //Линии параллельные осям
            #region 1. Y-строки
            List<double> lines = elements.
                Where(node => node.IsNode).
                Select(coordsX => coordsX.CoordinateX).
                Distinct().
                OrderBy(y => y).
                ToList();            

            List<Row<T>> yRows = new List<Row<T>>();

            for(int i = 0; i < lines.Count; i++)
            {
                List<T> temp = elements.
                    Where(e => e.CoordinateY == lines[i]).
                    ToList();

                if (temp.Count() > 1)
                {
                    yRows.Add(new Row<T>(temp));
                }

                else continue;
            }

            _yRows = yRows.ToArray();
            #endregion

            #region 2. X-строки
            lines = elements.
                Where(node => node.IsNode).
                Select(crdsY => crdsY.CoordinateY).
                Distinct().
                OrderBy(x => x).
                ToList();

            List<Row<T>> xRows = new List<Row<T>>();

            for(int i = 0; i < lines.Count; i++)
            {
                //TODO: ТЕХДОЛГ наладить работу Distinct()
                List<T> temp = elements.
                    Where(e => e.CoordinateX == lines[i]).
                    ToList();

                if (temp.Count() > 1)
                {
                    xRows.Add(new Row<T>(temp));
                }

                else continue;
            }

            _xRows = xRows.ToArray();

            //TODO: ТЕХДОЛГ шаг должен быть равным во всех направлениях
            // Необходимо предотвратить возможность создания каркаса Frame с разным шагом узлов

            IEnumerable<double> steps = _yRows.
                Select(r => r.Step).
                Union(_xRows.
                Select(r => r.Step));

            if (steps.Count() == 1)
            {
                _step = steps.Single();
            }
            else
                Console.WriteLine("Нарушен шаг сетки поля (неравный шаг, проверьте входные данные!)");


            #endregion







        }
    }
}