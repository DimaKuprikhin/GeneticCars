using FarseerPhysics.Common;
using Microsoft.Xna.Framework;

namespace GeneticCarsPhysicsEngine
{
    /// <summary>
    /// Перечисление для типа объекта.
    /// </summary>
    public enum ObjectType { Circle, Polygon };
    /// <summary>
    /// Информация об объекте.
    /// </summary>
    public class ObjectInfo
    {
        /// <summary>
        /// Тип объекта.
        /// </summary>
        public readonly ObjectType Type;
        /// <summary>
        /// Массив координат вершин.
        /// </summary>
        public readonly Vertices vertices;
        /// <summary>
        /// Радиус.
        /// </summary>
        public readonly float Radius;
        /// <summary>
        /// Цвет объекта.
        /// </summary>
        public readonly Color ObjectColor;

        /// <summary>
        /// Координата центра круга.
        /// </summary>
        public Vector2 CircleCenter { get; set; }

        /// <summary>
        /// Значение угла поворота круга.
        /// </summary>
        public Vector2 CircleAngle { get; set; }

        /// <summary>
        /// Конструктор для объекта информации объектов.
        /// </summary>
        /// <param name="vertices"> Массив вершин. </param>
        /// <param name="radius"> Радиус. </param>
        /// <param name="type"> Тип. </param>
        /// <param name="color"> Цвет. </param>
        public ObjectInfo(Vertices vertices, float radius, ObjectType type,
            Color color)
        {
            this.vertices = vertices;
            Radius = radius;
            Type = type;
            ObjectColor = color;
        }
    }
}
