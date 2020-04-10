using FarseerPhysics.Common;
using Microsoft.Xna.Framework;

namespace GeneticCarsPhysicsEngine
{
    public enum ObjectType { Circle, Polygon };
    /// <summary>
    /// Информация об объекте.
    /// </summary>
    public class ObjectInfo
    {
        public readonly ObjectType Type;
        public readonly Vertices vertices;
        public readonly float Radius;
        public readonly Color ObjectColor;

        public Vector2 CircleCenter { get; set; }

        public Vector2 CircleAngle { get; set; }

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
