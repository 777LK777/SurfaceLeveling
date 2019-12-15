using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Interfaces
{
    /// <summary>
    /// Предоставляет сведения (координаты) о местоположении
    /// </summary>
    public interface IPositionable
    {

        /// <summary>
        /// Координата X
        /// </summary>
        double CoordinateX { get; }

        /// <summary>
        /// Координата Y
        /// </summary>
        double CoordinateY { get; }

    }
}
