using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurfaceLeveling;
using GeometryLib;
using SurfaceLeveling.Elementary;
using SurfaceLeveling.Frame;
using SurfaceLeveling.Interfaces;

namespace AreaTest
{
    class Program
    {
        class Vertex : IVertex
        {
            double _x;
            double _y;
            double _z;

            public double CoordinateZ { get => _z; }

            public double CoordinateX { get => _x; }

            public double CoordinateY { get => _y; }

            public bool IsNode => true;

            public Vertex(double X, double Y, double Z)
            {
                _x = X;
                _y = Y;
                _z = Z;
            }

            public override string ToString()
            {
                return $"X {_x}|Y {_y}|Z {_z}";
            }
        }

        static void Main(string[] args)
        {
            List<Vertex> MyPoints1 = new List<Vertex>
            {
                new Vertex(0, 80, 0.965),
                new Vertex(0, 60, 1.315),
                new Vertex(0, 40, 1.785),
                new Vertex(0, 20, 2.168),
                new Vertex(0, 0, 2.68),

                new Vertex(20, 80, 1.08),
                new Vertex(20, 60, 1.22),
                new Vertex(20, 40, 1.63),
                new Vertex(20, 20, 2.012),
                new Vertex(20, 0, 2.19),

                new Vertex(40, 80, 1.903),
                new Vertex(40, 60, 1.49),
                new Vertex(40, 40, 1.535),
                new Vertex(40, 20, 1.96),
                new Vertex(40, 0, 2.075)                
            };

            List<Vertex> MyPoints2 = new List<Vertex>
            {
                new Vertex(60, 80, 2.415),
                new Vertex(60, 60, 1.71),
                new Vertex(60, 40, 1.845),

                new Vertex(80, 80, 2.863),
                new Vertex(80, 60, 2.419),
                new Vertex(80, 40, 2.117)
            };

            Field<Vertex> field = new Field<Vertex>(MyPoints1.Union(MyPoints2));

            //foreach (var row in field.YRows)
            //{
            //    for (int i = 0; i < row.Length; i++)
            //    {
            //        Console.WriteLine(row[i].ToString());
            //    }
            //    Console.WriteLine("--- --- --- ---");
            //}

            //Playground MyArea = new Playground(MyPoints);

            //MyArea.GeodesicGradient = -0.01;
            //MyArea.DirectAngle = new Angle(Convert.ToInt32(135 + K));
            //MyArea.RapperHeight = (12.5 + K) + (K / 100);
            //MyArea.RapperRailCountdown = 1.567;
            //MyArea.Origin = new Vertex(0, 0);

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
