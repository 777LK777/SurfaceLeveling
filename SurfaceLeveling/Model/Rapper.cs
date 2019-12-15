using SurfaceLeveling.Interfaces;

namespace SurfaceLeveling
{
    public class Rapper : IAltitudinal
    {

        /// <summary>
        /// Отсчёт по рейке, установленной на репере
        /// </summary>
        public double RailCountdown { get; set; }

        /// <summary>
        /// Отметка исходного репера
        /// </summary>
        public double HeightMark { get; set; }

    }
}