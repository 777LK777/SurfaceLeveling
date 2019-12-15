using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Interfaces
{
    /// <summary>
    /// Предоставляет данные о местоположении и высотной отметке
    /// </summary>
    public interface IVertex : IAltitudinal, IPositionable { }
}
