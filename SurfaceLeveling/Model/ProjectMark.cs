using Degree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Model
{
    class ProjectMark
    {        
        private SquareVertex Vertex;

        /// <summary>
        /// Проектная отметка
        /// </summary>
        /// <param name="GeodesicGradient">Уклон</param>
        /// <param name="DirectionalAngle">Дирекционный угол</param>
        /// <param name="Vertex">Текущая вершина</param>
        /// <param name="RelativeVertex">Вершина, относительно которой вычисляется текущая</param>
        public ProjectMark(double GeodesicGradient, IAngle DirectionalAngle, SquareVertex Vertex, SquareVertex RelativeVertex)
        {
            this.Vertex = Vertex;
            X_AxialExcess = GeodesicGradient * DirectionalAngle.Cos * (Vertex.X - RelativeVertex.X);
            Y_AxialExcess = GeodesicGradient * DirectionalAngle.Sin * (Vertex.X - RelativeVertex.Y);
            ProjectHeight = RelativeVertex.
        }

        /// <summary>
        /// Разность между X-координатами начала отсчета и соответствующей вершины
        /// </summary>
        public double Dxh { get => Vertex.X; }

        /// <summary>
        /// Разность между Y-координатами начала отсчета и соответствующей вершины
        /// </summary>
        public double Dyh { get => Vertex.Y; }

        /// <summary>
        /// Превышение по оси X
        /// </summary>
        public double X_AxialExcess { get; }

        /// <summary>
        /// Превышение по оси Y
        /// </summary>
        public double Y_AxialExcess { get; }

        /// <summary>
        /// Проектная отметка вершины
        /// </summary>
        public double ProjectHeight { get; }
    }
}
