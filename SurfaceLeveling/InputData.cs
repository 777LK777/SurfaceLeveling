using GeometryLib;
using SurfaceLeveling.Elementary;
using SurfaceLeveling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling
{
    public class InputData
    {
        readonly Rapper _rapper;
        readonly IEnumerable<IVertex> _vertices;
        readonly IAngle _directionalAngle;
        private double _geodesicGradient;

        /// <summary>
        /// Рэппер
        /// </summary>
        public Rapper Rapper { get => _rapper; }

        /// <summary>
        /// Вершины
        /// </summary>
        public IEnumerable<IVertex> Vertices { get => _vertices; }

        /// <summary>
        /// Дирекционный угол
        /// </summary>
        public IAngle DirectionalAngle { get => _directionalAngle; }

        /// <summary>
        /// Геодезический уклон
        /// </summary>
        public double GeodesicGradient { get => _geodesicGradient; }
    }
}
