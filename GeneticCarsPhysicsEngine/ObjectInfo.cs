using FarseerPhysics.Common;

namespace GeneticCarsPhysicsEngine
{
    enum ObjectType { Circle, Polygon };
    /// <summary>
    /// Информация об объекте.
    /// </summary>
    class ObjectInfo
    {
        public readonly ObjectType Type;
        public readonly Vertices vertices;
        public readonly float Radius;
        public readonly int[] Color;

        public ObjectInfo(Vertices vertices, float radius, ObjectType type,
            int[] color)
        {
            this.vertices = vertices;
            Radius = radius;
            Type = type;
            Color = color;
        }
    }
}
