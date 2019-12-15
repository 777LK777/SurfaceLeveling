using SurfaceLeveling.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Model
{
    public class SquareVertex : IEquatable<SquareVertex>, IVertex
    {
        /// <summary>
        /// Абсолютная отметка вершины
        /// </summary>
        AbsoluteMark absoluteMark;

        /// <summary>
        /// Абсолютная отметка вершины
        /// </summary>
        public double AbsoluteMark { get => absoluteMark.markVertex; }

        /// <summary>
        /// Проектная отметка вершины
        /// </summary>
        ProjectMark projectMark;

        IVertex vertex;

        // TODO: Можно определить свои классы исключений
        /// <summary>
        /// Рабочая отметка вершины
        /// </summary>
        public double WorkingMark
        {
            get
            {
                try
                {
                    return projectMark.ProjectHeight - absoluteMark.markVertex;
                }
                catch
                {
                    throw new InvalidOperationException("Не определены абсолютная и проектная отметки");
                }
            }
        }

        /// <summary>
        /// Количество вызовов точки для создания квадратов
        /// </summary>
        internal int CountCalls { get; private set; }

        /// <summary>
        /// Флаг начала координат
        /// </summary>
        internal bool IsOrigin { get; set; }

        /// <summary>
        /// X-координата положения точки
        /// </summary>
        public double CoordinateX
        {
            get
            {
                return vertex.CoordinateX;
            }
        }

        /// <summary>
        /// Y-координата положения точки
        /// </summary>
        public double CoordinateY
        {
            get
            {
                return vertex.CoordinateY;
            }
        }        
        
        /// <summary>
        /// Отсчет по рейке
        /// </summary>
        public double HeightMark { get; }

        /// <summary>
        /// Вершина квадрата
        /// </summary>
        /// <param name="Vertex">Вершина с координатами и высотой</param>
        /// <param name="SerialNo">Порядковый номер. Правильный отсчет: слева - направо, сверху - вниз</param>
        public SquareVertex(IVertex Vertex)
        {
            vertex = Vertex;            

            if (vertex.CoordinateX == 0 && vertex.CoordinateY == 0) IsOrigin = true;
            else IsOrigin = false;
        }        

        /// <summary>
        /// Использование вершины для инициализации квадрата
        /// </summary>
        public SquareVertex Call()
        {
            CountCalls++;
            return this;
        }

        public void SetAbsoluteMark(Playground field)
        {
            absoluteMark = new AbsoluteMark(this, field.rapper);
        }

        public void SetProjectMark(Playground field)
        {
            projectMark = new ProjectMark(field.GeodesicGradient, field.directionalAngle, this, IsOrigin ? field : this);
        }

        #region IEquatable
        public bool Equals(SquareVertex other)
        {
            if (other == null)
                return false;

            if (CoordinateX == other.CoordinateX && CoordinateY == other.CoordinateY)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            SquareVertex ptObj = obj as SquareVertex;
            if (ptObj == null)
                return false;
            else
                return Equals(ptObj);
        }

        public override int GetHashCode()
        {
            // TODO: Нуждается в проверке!
            // переделать для 0 и 1
            return Math.Pow(CoordinateX, CoordinateY).GetHashCode();
        }
        #endregion

        #region Перегруженные операторы
        // TODO: Протестить!
        public static bool operator ==(SquareVertex First, SquareVertex Second)
        {
            if (((object)First) == null || ((object)Second) == null)
                return object.Equals(First, Second);

            return First.Equals(Second);
        }

        public static bool operator !=(SquareVertex First, SquareVertex Second)
        {
            if (((object)First) == null || ((object)Second) == null)
                return !object.Equals(First, Second);

            return !First.Equals(Second);
        }
        #endregion

        /// <summary>
        /// Возвращает строку представляющую собой сведения об объекте
        /// </summary>
        /// <returns>Возрат номера точки с координатами</returns>
        public override string ToString()
        {
            return $"Point:(X{CoordinateX}m, Y{CoordinateY}m";
        }
    }

}