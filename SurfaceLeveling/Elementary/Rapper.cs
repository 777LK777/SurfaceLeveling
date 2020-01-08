using SurfaceLeveling.Interfaces;

namespace SurfaceLeveling.Elementary
{
    //Используется для входных данных

    public class Rapper
    {
        /// <summary>
        /// Отсчёт по рейке, установленной на репере
        /// </summary>
        public double RailCountdown { get; set; }

        /// <summary>
        /// Отметка исходного репера
        /// </summary>
        public double HeightMark { get; set; }

        internal GeoRapper ToGeoRapper()
        {
            return new GeoRapper();
        }
    }
}