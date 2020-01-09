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
    internal class Row<T>
        where T : IVertex
    {
        IEnumerable<T> _elements;

    }
}
