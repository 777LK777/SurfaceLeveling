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
    public interface IAltitudinal
    {
        /// <summary>
        /// Высотная отметка
        /// </summary>
        double HeightMark { get; }
    }
}
