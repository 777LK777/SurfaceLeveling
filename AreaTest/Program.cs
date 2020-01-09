using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurfaceLeveling;
using GeometryLib;
using SurfaceLeveling.Elementary;

namespace AreaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // № зачетной книжки
            double K = 101;

            for(char a = 'а'; a <= 'я'; a++)
            {
                Console.WriteLine(a);
            }
            
            

            //SquareVertex v = new SquareVertex(new Vertex(), 0);
            
            //List<SquareVertex> MyPoints = new List<SquareVertex>
            //{
            //    new SquareVertex(0, 80, 0.965),
            //    new SquareVertex(0, 60, 1.315),
            //    new SquareVertex(0, 40, 1.785),
            //    new SquareVertex(0, 20, 2.168),
            //    new SquareVertex(0, 0, 2.68),

            //    new SquareVertex(20, 80, 1.08),
            //    new SquareVertex(20, 60, 1.22),
            //    new SquareVertex(20, 40, 1.63),
            //    new SquareVertex(20, 20, 2.012),
            //    new SquareVertex(20, 0, 2.19),

            //    new SquareVertex(40, 80, 1.903),
            //    new SquareVertex(40, 60, 1.49),
            //    new SquareVertex(40, 40, 1.535),
            //    new SquareVertex(40, 20, 1.96),
            //    new SquareVertex(40, 0, 2.075),

            //    new SquareVertex(60, 80, 2.415),
            //    new SquareVertex(60, 60, 1.71),
            //    new SquareVertex(60, 40, 1.845),

            //    new SquareVertex(80, 80, 2.863),
            //    new SquareVertex(80, 60, 2.419),
            //    new SquareVertex(80, 40, 2.117)
            //};

            //Playground MyArea = new Playground(MyPoints);

            //MyArea.GeodesicGradient = -0.01;
            //MyArea.DirectAngle = new Angle(Convert.ToInt32(135 + K));
            //MyArea.RapperHeight = (12.5 + K) + (K / 100);
            //MyArea.RapperRailCountdown = 1.567;
            //MyArea.Origin = new SquareVertex(0, 0);

            //MyArea.CalculationAbsoluteMarks();
            //MyArea.GatherSquares();
            //MyArea.HeightCenterOfGravity();
            //Console.WriteLine("Положение центра тяжести:");
            //MyArea.CenterOfGravity.ToString();
            //MyArea.CalculationProjectMarks();
            //MyArea.CalculationWorkingMarks();



            Console.ReadKey();            
        }       
    }
}
