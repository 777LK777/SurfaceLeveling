using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryLib;
using SurfaceLeveling.Elementary;
using SurfaceLeveling.Interfaces;
using SurfaceLeveling.Shapes;

namespace SurfaceLeveling
{
    public class Playground
    {
        // TODO: Необходимо исключить возможность добавить точки на одну и ту же координату       

        // Внутрянка:
        /// <summary>
        /// Данные рэппера
        /// </summary>
        readonly GeoRapper _rapper;
        /// <summary>
        /// Дирекционный угол
        /// </summary>
        readonly IAngle _directionalAngle;
        /// <summary>
        /// Геодезический уклон
        /// </summary>
        readonly double _geodesicGradient;
        /// <summary>
        /// Центр тяжести площадки
        /// </summary>
        readonly PlaygroundCenterOfGravity _centerOfGravity;

        /// <summary>
        /// Список точек поля
        /// </summary>
        readonly List<SquareVertex> _vertices;
        /// <summary>
        /// Список квадратов поля
        /// </summary>
        readonly List<Square> _squares;
        /// <summary>
        /// Список точек нулевых работ
        /// </summary>
        readonly List<PointOfZeroWork> _zeroWorks;


        #region ctor
        // Массив точек:
        public Playground(InputData inputData)
        {
            _rapper = inputData.Rapper.ToGeoRapper();
            _directionalAngle = inputData.DirectionalAngle;
            _geodesicGradient = inputData.GeodesicGradient;

            _vertices = SquareVertex.VerticesFactory(inputData.Vertices).ToList();
            AbsoluteMark.SetAbsoluteMarks(_vertices, _rapper);

            _squares = Square.SquaresFactory(_vertices).ToList();

            _centerOfGravity = new PlaygroundCenterOfGravity(_squares);

            ProjectMark.SetProjectMarks(this);
            WorkingMark.SetWorkingMarks(_vertices);

            _zeroWorks = PointOfZeroWork.FactoryMethod(_vertices).ToList();
        }
        #endregion

      

        #region Свойства 

        #region Рельсы
        /// <summary>
        /// Отсортированный список X-координат
        /// </summary>
        IEnumerable<double> GridCoordsX
        {
            get => _vertices.                
                Select(pt => pt.CoordinateX).
                Distinct().
                OrderBy(X => X).
                ToList();
        }

        /// <summary>
        /// Отсортированный список Y-координат
        /// </summary>
        IEnumerable<double> GridCoordsY
        {
            get => _vertices.
                Select(pt => pt.CoordinateY).
                Distinct().
                OrderBy(Y => Y);
        }
        #endregion

        /// <summary>
        /// Рэпер площадки
        /// </summary>
        public GeoRapper Rapper { get => _rapper; }

        /// <summary>
        /// Геодезический уклон
        /// </summary>
        public double GeodesicGradient { get => _geodesicGradient; }

        /// <summary>
        /// Дирекционный угол
        /// </summary>
        public IAngle DirectionalAngle { get => _directionalAngle; }

        /// <summary>
        /// Вершины квадратов
        /// </summary>
        internal List<SquareVertex> Vertices { get => _vertices; }

        /// <summary>
        /// XY и высотное-положения центра тяжести площадки
        /// </summary>
        public IVertex CenterOfGravity
        {
            get => _centerOfGravity;
        }

        
        /// <summary>
        /// Возвращает точку отсчета для площадки
        /// </summary>
        public IVertex Origin
        {
            get
            {
                try
                {
                    return _vertices.Where(vx => vx.IsOrigin == true).Single();
                }
                catch
                {
                    // TODO: В конструкторе предусмотреть обнуление свойства IsOrigin у точек
                    throw new InvalidOperationException("В поле нет или несколько точек отсчета");
                }                
            }            
        }        
        #endregion
              


        
    }
}
