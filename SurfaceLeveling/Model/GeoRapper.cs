using SurfaceLeveling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Model
{
    internal class GeoRapper : IAltitudinal
    {
        /// <summary>
        /// Отметка горизонта нивелира.Исчисляется в метрах
        /// </summary>
        public double RapperHorizon
        {
            get
            {
                return RailCountdown + SourceHeightMark;
            }
        }

        /// <summary>
        /// Отсчет по рейке, установленной на репере. Исчисляется в метрах
        /// </summary>
        public double RailCountdown { get { return railCountdown; } }

        /// <summary>
        /// Высотная отметка исходного репера. Исчисляется в метрах
        /// </summary>
        public double SourceHeightMark { get { return sourceHeightMark; } }

        private readonly double railCountdown;
        private readonly double sourceHeightMark;

        /// <summary>
        /// Геодезический репер
        /// </summary>
        /// <param name="RailCountdown">Отсчет по рейке, установленной на репере. Исчисляется в метрах</param>
        /// <param name="SourceHeightMark">Высотная отметка исходного репера. Исчисляется в метрах</param>
        public GeoRapper(double RailCountdown, double SourceHeightMark)
        {
            MyObserver.AddAct("Создание репера...");
            railCountdown = RailCountdown;
            sourceHeightMark = SourceHeightMark;
        }
    }
}
