using SurfaceLeveling.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Elementary
{
    internal class SquareVertex : IEquatable<SquareVertex>, IVertex
    {
        AbsoluteMark _absoluteMark;
        ProjectMark _projectMark;

        WorkingMark _workingMark;
        IVertex _vertex;

        public static IList<SquareVertex> VerticesFactory(IEnumerable<IVertex> vertices)
        {
            List<SquareVertex> _vertices = new List<SquareVertex>();

            foreach (IVertex vertex in vertices.Distinct())
            {
                _vertices.Add(new SquareVertex(vertex));
            }            

            return _vertices;
        }        

        /// <summary>
        /// Абсолютная отметка вершины
        /// </summary>
        public AbsoluteMark AbsoluteMark
        {
            get
            {
                return _absoluteMark;
            }
            set
            {
                _absoluteMark = value;
            }
        }

        /// <summary>
        /// Проектная отметка вершины
        /// </summary>
        public ProjectMark ProjectMark
        {
            get { return _projectMark; }
            internal set { _projectMark = value; }
        }
        

        // TODO: ТЕХДОЛГ Можно определить свои классы исключений
        /// <summary>
        /// Рабочая отметка вершины
        /// </summary>
        public double WorkingMark
        {
            get { return _workingMark.WorkingHeight; }
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
                return _vertex.CoordinateX;
            }
        }

        /// <summary>
        /// Y-координата положения точки
        /// </summary>
        public double CoordinateY
        {
            get
            {
                return _vertex.CoordinateY;
            }
        }        
        
        /// <summary>
        /// Отсчет по рейке
        /// </summary>
        public double CoordinateZ { get; }

        public bool IsNode => _vertex.IsNode;

        /// <summary>
        /// Вершина квадрата
        /// </summary>
        /// <param name="Vertex">Вершина с координатами и высотой</param>
        /// <param name="SerialNo">Порядковый номер. Правильный отсчет: слева - направо, сверху - вниз</param>
        public SquareVertex(IVertex Vertex)
        {
            _vertex = Vertex;            

            if (_vertex.CoordinateX == 0 && _vertex.CoordinateY == 0) IsOrigin = true;
            else IsOrigin = false;
        }        

        /// <summary>
        /// Использование вершины для инициализации квадрата
        /// </summary>
        internal SquareVertex Call()
        {
            CountCalls++;
            return this;
        }

        //TODO: ПЕРЕДЕЛАТЬ это! У WorkingMark должен быть закрытый конструктор
        internal void SetWorkingMark()
        {
            _workingMark = new WorkingMark(this);
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