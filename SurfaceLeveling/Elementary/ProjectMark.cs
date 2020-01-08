using GeometryLib;
using SurfaceLeveling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Elementary
{
    internal class ProjectMark
    {        
        /// <summary>
        /// Вычисляет проектные отметки вершин квадратов
        /// </summary>
        /// <param name="playground">Поле с вершинами квадратов</param>
        public static void SetProjectMarks(Playground playground)
        {
            playground.
                Vertices.
                Where(vx => vx.IsOrigin).
                Single().
                ProjectMark = new ProjectMark(playground.GeodesicGradient,
                playground.DirectionalAngle,
                playground.Origin,
                playground.CenterOfGravity);

            playground.
                Vertices.
                Where(vx => !vx.IsOrigin).
                ToList().
                ForEach(vx => vx.ProjectMark = new ProjectMark(playground.GeodesicGradient,
                playground.DirectionalAngle,
                vx,
                playground.Origin));

            playground.
                Vertices.
                ForEach(vx =>
                {
                    vx.ProjectMark._deltaX = vx.CoordinateX - playground.Origin.CoordinateX;
                    vx.ProjectMark._deltaY = vx.CoordinateY - playground.Origin.CoordinateY;
                });
        }

        double _deltaX;
        double _deltaY;
        readonly double _excessX;
        readonly double _excessY;
        readonly double _projectHeight;
        readonly SquareVertex _vertex;

        /// <summary>
        /// Проектная отметка
        /// </summary>
        /// <param name="GeodesicGradient">Уклон</param>
        /// <param name="DirectionalAngle">Дирекционный угол</param>
        /// <param name="Vertex">Текущая вершина</param>
        /// <param name="RelativeVertex">Вершина, относительно которой вычисляется текущая</param>
        private ProjectMark(double GeodesicGradient, IAngle DirectionalAngle, IVertex Vertex, IVertex RelativeVertex)
        {
            _excessX = GeodesicGradient * DirectionalAngle.Cos * (Vertex.CoordinateX - RelativeVertex.CoordinateX);
            _excessY = GeodesicGradient * DirectionalAngle.Sin * (Vertex.CoordinateY - RelativeVertex.CoordinateY);
            _projectHeight = RelativeVertex.CoordinateZ + _excessX + _excessY;
        }

        /// <summary>
        /// Разность между X-координатами начала отсчета и соответствующей вершины
        /// </summary>
        public double DeltaX
        {
            get { return _deltaX; }
            set { _deltaX = value; }
        }

        /// <summary>
        /// Разность между Y-координатами начала отсчета и соответствующей вершины
        /// </summary>
        public double DeltaY
        {
            get { return _deltaY; }
            set { _deltaY = value; }
        }

        /// <summary>
        /// Превышение по оси X
        /// </summary>
        public double ExcessX { get => _excessX; }

        /// <summary>
        /// Превышение по оси Y
        /// </summary>
        public double ExcessY { get => _excessY; }

        /// <summary>
        /// Проектная отметка вершины
        /// </summary>
        public double ProjectHeight { get => _projectHeight; }

        public IVertex Origin
        {
            get { return _vertex; }
        }
    }
}
