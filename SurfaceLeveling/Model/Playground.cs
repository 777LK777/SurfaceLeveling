using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Degree;
using SurfaceLeveling.Interfaces;
using SurfaceLeveling.Model;

namespace SurfaceLeveling
{
    #region Project
    // ULTRON   
    #endregion


    public class Playground
    {
        // TODO: Необходимо исключить возможность добавить точки на одну и ту же координату       

        // Внутрянка:
        internal readonly GeoRapper rapper;
        internal readonly IAngle directionalAngle;


        /// <summary>
        /// Список точек поля
        /// </summary>
        List<SquareVertex> Vertecies;

        
        

        /// <summary>
        /// Список квадратов поля
        /// </summary>
        List<Square> Squares;        

        #region Свойства:        
        /// <summary>
        /// Геодезический уклон
        /// </summary>
        public double GeodesicGradient { get; set; }

        #region Рельсы
        /// <summary>
        /// Отсортированный список X-координат
        /// </summary>
        IEnumerable<double> GridCoordsX
        {
            get => Vertecies.                
                Select(pt => pt.X).
                Distinct().
                OrderBy(X => X).
                ToList();
        }

        /// <summary>
        /// Отсортированный список Y-координат
        /// </summary>
        IEnumerable<double> GridCoordsY
        {
            get => Vertecies.
                Select(pt => pt.Y).
                Distinct().
                OrderBy(Y => Y);
        }
        #endregion


        /// <summary>
        /// Шаг сетки поля
        /// </summary>
        double GridSpacing
        {
            get
            {
                try
                {
                    return StepsGridSpacing().
                        DefaultIfEmpty(-1000000).
                        SingleOrDefault();
                }
                catch
                {
                    throw new InvalidOperationException("В сетке квадратов разный шаг, измените входные даннные и перезапустите программу");
                }                    
            }
        }

        /// <summary>
        /// XY и высотное-положения центра тяжести площадки
        /// </summary>
        public PointP CenterOfGravity
        {
            get
            {
                double SummXi = 0;
                foreach(double center in Squares.
                    Select(figure => figure.CenterOfGravity.y).
                    OrderBy(y => y))
                {
                    SummXi += Squares.                        
                        Where(f => f.CenterOfGravity.y == center).
                        Count() * center;
                }

                double SummYi = 0;
                foreach(double center in Squares.
                    Select(f => f.CenterOfGravity.X).
                    OrderBy(x => x))
                {
                    SummYi += Squares.
                        Where(f => f.CenterOfGravity.X == center).
                        Count() * center;
                };

                return new PointP((SummXi / Squares.Count), (SummYi / Squares.Count));
            }
        }

        class FieldCenterOfGravity : IVertex
        {
            public FieldCenterOfGravity(Playground field)
            {
                double SummXi = 0;
                foreach (double center in field.Squares.
                    Select(figure => figure.CenterOfGravity.y).
                    OrderBy(y => y))
                {
                    SummXi += field.Squares.
                        Where(f => f.CenterOfGravity.y == center).
                        Count() * center;
                }

                double SummYi = 0;
                foreach (double center in field.Squares.
                    Select(f => f.CenterOfGravity.X).
                    OrderBy(x => x))
                {
                    SummYi += field.Squares.
                        Where(f => f.CenterOfGravity.X == center).
                        Count() * center;
                };

                //
                List<double> amount = new List<double>();   // Список сумм

                // Вычисление сумм вершин:
                foreach (int count in field.Vertecies.
                    Select(pt => pt.CountCalls).
                    Distinct().
                    OrderBy(count => count))
                {
                    amount.Add(field.Vertecies.
                        Where(pt => pt.CountCalls == count).
                        Sum(pt => pt.AbsoluteMark));
                }

                double numerator = 0;   // числитель

                for (int i = 1; i <= 4; i++)
                {
                    numerator += i * amount[i - 1];
                }
                
                //

                new FieldCenterOfGravity(X: SummXi / field.Squares.Count, Y: SummYi / field.Squares.Count, numerator / (field.Squares.Count() * 4));
            }

            internal FieldCenterOfGravity(double X, double Y, double Z) : base(X, Y, Z) { }
        }

        /// <summary>
        /// Возвращает точку отсчета для площадки
        /// </summary>
        public SquareVertex Origin
        {
            get
            {
                try
                {
                    return Vertecies.Where(vx => vx.IsOrigin == true).Single();
                }
                catch
                {
                    // TODO: В конструкторе предусмотреть обнуление свойства IsOrigin у точек
                    throw new InvalidOperationException("В поле нет или несколько точек отсчета");
                }                
            }            
        }        
        #endregion

        #region Конструкторы:
        // Массив точек:
        public Playground(IAltitudinal Rapper, IEnumerable<IPositionable> Points, IAngle DirectionalAngle)
        {
            rapper = new GeoRapper(Rapper.RailCountdown, Rapper.ReferenceMark);

            directionalAngle = DirectionalAngle;

            Vertecies = new List<SquareVertex>();

            foreach (Vertex vertex in Points.OrderByDescending(pt => pt.Y).OrderBy(pt => pt.X))
            {                
                Vertecies.Add(new SquareVertex(vertex, Vertecies.Count + 1));                
            }

            Vertecies.ForEach(v => v.SetAbsoluteMark(this));           

            Squares = new List<Square>();

            foreach (double Y in GridCoordsY)
            {
                if ((Y + GridSpacing) > GridCoordsY.Max()) break;
                else
                {
                    List<double> Xs = Vertecies.
                        Where(vs => vs.Y == Y).
                        Select(vs => vs.X).
                        OrderBy(X => X).
                        ToList();

                    foreach (double X in Xs)
                    {
                        if ((X + GridSpacing) > Xs.Max()) break;
                        else
                        {
                            Squares.Add(new Square(Vertecies.
                                Where(vs => (vs.Y >= Y) && (vs.Y <= (Y + GridSpacing))).
                                Where(vs => (vs.X >= X) && (vs.X <= (X + GridSpacing))), Squares.Count + 1));                            
                        }
                    }                    
                }
            }

            Vertecies.ForEach(v => v.SetProjectMark(this));






        }        
        
        #endregion

        #region Методы:       

        /// <summary>
        /// Вычисляет размеры шагов между точками сетки поля
        /// </summary>
        /// <returns>Возвращает список шагов</returns>
        List<double> StepsGridSpacing()
        {
            // TODO: Добавить исключение для ситуации, 
            // в которой появляется шаг сетки,
            // который не равен существующему
            
            List<double> Steps = new List<double>();

            for(int i = 1; i < GridCoordsX.Count(); i++)
                Steps.Add(GridCoordsX.ElementAt(i) - GridCoordsX.ElementAt(i - 1));

            for (int i = 1; i < GridCoordsY.Count(); i++)
                Steps.Add(GridCoordsY.ElementAt(i) - GridCoordsY.ElementAt(i - 1));

            return Steps.
                Select(step => step).
                Distinct().
                ToList();            
        }

        /// <summary>
        /// Вычисляет высоту центра тяжести
        /// </summary>
        /// <returns>Возвращает значение высоты центра тяжести</returns>
        public double HeightCenterOfGravity()
        {
            Console.WriteLine("Вычисляю высотную отметку центра тяжести");
            // TODO: Нужна осуществить проверку значений (см. стр. 10)
            List<double> amount = new List<double>();   // Список сумм

            // Вычисление сумм вершин:
            foreach(int count in Vertecies.
                Select(pt => pt.CountCalls).
                Distinct().
                OrderBy(count => count))
            {
                amount.Add(Vertecies.
                    Where(pt => pt.CountCalls == count).
                    Sum(pt => pt.AbsoluteMark));
            }

            double numerator = 0;   // числитель

            for(int i = 1; i <= 4; i++)
            {
                numerator += i * amount[i - 1];
            }
            return numerator / (Squares.Count() * 4);   // 4 - константа

            Console.WriteLine("Высота центра тяжести равна: " + (numerator / (Squares.Count() * 4)));
            
        }

        
        #endregion

        


        //public static void Test()
        //{
        //    FieldTest.Run();
        //}



        ///// <summary>
        ///// Тестовый класс
        ///// </summary>
        //private class FieldTest
        //{
        //    private static List<SquareVertex> AddLine(double startX, double Y, double increment, double CountSegments)
        //    {
        //        List<SquareVertex> points = new List<SquareVertex>();
        //        for (int i = 0; i <= CountSegments; i++)
        //        {
        //            points.Add(new SquareVertex(startX + (i * increment), Y));
        //        }
        //        return points;
        //    }

        //    private static void Next()
        //    {
        //        Console.WriteLine("Для продолжения нажмите любую клавишу...");
        //        Console.ReadKey();
        //        Console.Clear();
        //    }

        //    public static void Run()
        //    {
        //        #region то что уже работает
        //        List<SquareVertex> GridPoints = new List<SquareVertex>();
        //        List<SquareVertex> AdditionalPoints = new List<SquareVertex>();

        //        for (int Y = 0; Y <= 8; Y += 2)
        //        {
        //            GridPoints.AddRange(AddLine(0, Y, 2, 2));
        //        }
        //        for (int Y = 4; Y <= 8; Y += 2)
        //        {
        //            GridPoints.AddRange(AddLine(6, Y, 2, 1));
        //        }
        //        Console.WriteLine("В сетку будут добавлены следующие точки");
        //        foreach (var pt in GridPoints)
        //        {
        //            Console.WriteLine(pt.ToString());
        //        }
        //        Next();

        //        Field MyArea = new Field(GridPoints);
        //        Console.WriteLine($"Шаг сетки: {MyArea.GridSpacing}");
        //        Next();

        //        Console.WriteLine("Будут добавлены следующие дополнительные точки");
        //        for (int Y = 0; Y <= 4; Y += 2)
        //        {
        //            AdditionalPoints.AddRange(AddLine(1, Y, 2, 1));
        //        }
        //        for (int Y = 1; Y <= 3; Y += 2)
        //        {
        //            AdditionalPoints.AddRange(AddLine(0, Y, 2, 2));
        //        }
        //        foreach (var pt in AdditionalPoints)
        //        {
        //            MyArea.AddPoint(pt);
        //            Console.WriteLine(pt.ToString());
        //        }
        //        Next();

        //        Console.WriteLine("Проверка на принадлежность сетке");
        //        foreach (var pt in MyArea.Vertexes.
        //            Select(pt => pt).
        //            OrderBy(pt => pt.Y).
        //            OrderBy(pt => pt.X))
        //        {
        //            Console.WriteLine(pt.ToString() + " |\tIs for grid: " + pt.IsForGrid);
        //        }
        //        Next();
        //        #endregion

        //        foreach (var Y in MyArea.GridCoordsY)
        //        {
        //            Console.WriteLine($" Для линии Y{Y}:");

        //            List<SquareVertex> points = MyArea.
        //                GetRowPoints(Y).
        //                OrderBy(pt => pt.X).
        //                OrderBy(pt => pt.Y).
        //                ToList();

        //            foreach (var pt in points)
        //            {
        //                Console.WriteLine(pt.ToString());
        //            }
        //            Console.WriteLine("--- --- --- ---");
        //            Next();
        //        }

        //        Console.WriteLine("Собираю фигуры");
        //        MyArea.GatherSquares();
        //        Console.WriteLine("Сбор завершен");
        //        Next();

        //        foreach (Square f in MyArea.Squares)
        //        {
        //            Console.WriteLine("Получившиеся фигуры:");
        //            Console.WriteLine(f.ToString());
        //            Next();
        //        }                

        //        foreach (var cnt in MyArea.Vertexes.
        //            Select(pt => pt.CountCalls).
        //            Distinct().
        //            OrderBy(cnt => cnt)
        //            )
        //        {
        //            Console.WriteLine("Точки по категориям");
        //            Console.WriteLine($"Вызваны были {cnt} раз");

        //            foreach (var pt in MyArea.Vertexes.
        //                Where(pt => pt.CountCalls == cnt).
        //                Select(pt => pt).
        //                OrderBy(pt => pt.Y).
        //                OrderBy(pt => pt.X))
        //            {                         
        //                Console.WriteLine(pt.ToString());
        //            }
        //            Next();
        //        }

        //    }
        //}
    }
}
