using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Interfaces
{
    /// <summary>
    /// Предоставляет высотную отметку
    /// </summary>
    public interface IHeight
    {
        /// <summary>
        /// Высотная отметка
        /// </summary>
        double CoordinateZ { get; }
    }
}
