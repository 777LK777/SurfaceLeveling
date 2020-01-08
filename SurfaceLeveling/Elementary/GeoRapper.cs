using SurfaceLeveling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Elementary
{
    //TODO: Изменить на internal
    public class GeoRapper : Rapper
    {
        readonly double rapperHorizon;

        /// <summary>
        /// Отметка горизонта нивелира.Исчисляется в метрах
        /// </summary>
        public double RapperHorizon
        {
            get => rapperHorizon;
        }

        /// <summary>
        /// Геодезический репер
        /// </summary>
        /// <param name="RailCountdown">Отсчет по рейке, установленной на репере. Исчисляется в метрах</param>
        /// <param name="SourceHeightMark">Высотная отметка исходного репера. Исчисляется в метрах</param>
        public GeoRapper()
        {
            rapperHorizon = HeightMark + RailCountdown;
        }
    }
}
