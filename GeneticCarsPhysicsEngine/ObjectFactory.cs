using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace GeneticCarsPhysicsEngine
{
    static class ObjectFactory
    {
        private const float standartRestitution = 0.4f;
        private const float standartFriction = 0.4f;

        private static Random rnd = new Random();

        private static int[] RandomColor()
        {
            return new int[3] { rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256) };
        }

        /// <summary>
        /// Создает описанный многоугольник.
        /// </summary>
        /// <param name="world"> Физическая модель, в которую добавляется объект. </param>
        /// <param name="vertices"> Координаты вершин. </param>
        /// <param name="position"> Позиция в мире. </param>
        /// <param name="isGround"> Является ли объект землей. </param>
        /// <param name="restitution"> Restitution. </param>
        /// <param name="friction"> Коэффициент трения. </param>
        /// <returns> Возвращает ссылку на объект. </returns>
        public static Body AddPolygon(World world, Vertices vertices, Vector2 position, bool isGround = false, float restitution = standartRestitution,
            float friction = standartFriction)
        {
            Body body = BodyFactory.CreatePolygon(world, vertices, 1,
                position, 0, isGround ? BodyType.Static : BodyType.Dynamic,
                new ObjectInfo(vertices, 0, ObjectType.Polygon, 
                isGround ? new int[3] { 0, 0, 0 } : RandomColor() ));
            body.Restitution = restitution;
            body.Friction = friction;
            return body;
        }

        /// <summary>
        /// Создает случайный объект в форме многоугольника.
        /// </summary>
        /// <param name="world"> Физическая модель, в которую добавляется объект.
        /// </param>
        /// <param name="verticesCount"> Количество вершин многоугольника. </param>
        /// <param name="radius"> Радиус многоугольника. </param>
        /// <param name="position"> Позиция в мире. </param>
        /// <returns> Возвращает ссылку на объект. </returns>
        //public static Body AddRandomPolygon(World world, int verticesCount,
        //    float radius, Vector2 position)
        //{
        //    Vector2[] vertices = new Vector2[verticesCount];
        //    List<double> angles = new List<double>();
        //    for(int i = 0; i < verticesCount; i++)
        //    {
        //        angles.Add(Common.Rnd.NextDouble() * 2 * Math.PI);
        //    }
        //    angles.Sort();
        //    for(int i = 0; i < verticesCount; i++)
        //    {
        //        vertices[i] = new Vector2((float)Math.Cos(angles[i]) * radius,
        //            (float)Math.Sin(angles[i]) * radius);
        //    }

        //    Body body = BodyFactory.CreatePolygon(world, new Vertices(vertices),
        //        1, position, 0, BodyType.Dynamic, new ObjectInfo(new Vertices(vertices),
        //        0, ObjectType.Polygon, Common.RandomColor()));
        //    body.Restitution = standartRestitution;
        //    body.Friction = standartFriction;

        //    return body;
        //}

        /// <summary>
        /// Создает объект с формой окружности.
        /// </summary>
        /// <param name="world"> Физическая модель, в которую добавляется модель. </param>
        /// <param name="position"> Позиция в мире. </param>
        /// <param name="radius"> Радиус. </param>
        /// <returns> Возвращает ссылку на объект. </returns>
        public static Body AddCircle(World world, Vector2 position, float radius,
            float restitution = standartRestitution, float friction = standartFriction)
        {
            Body body = BodyFactory.CreateCircle(world, radius, 1, position, BodyType.Dynamic,
                new ObjectInfo(new Vertices(), radius, ObjectType.Circle, RandomColor()));
            body.Restitution = restitution;
            body.Friction = friction;
            return body;
        }

        /// <summary>
        /// Создает машину с заданными параметрами.
        /// </summary>
        /// <param name="world"> Мир, в котором создается машина. </param>
        /// <param name="carVertices"> Набор вершин корпуса машины. </param>
        /// <param name="position"> Позиция машины. </param>
        /// <param name="speed"> Скорость машины. </param>
        /// <param name="firstWheelRadius"> Радиус первого колеса. </param>
        /// <param name="secondWheelRadius"> Радиус второго колеса. </param>
        /// <param name="firstWheelPosition"> Позиция первого колеса относительно
        /// центра корпуса машины. </param>
        /// <param name="secondWheelPosition">Позиция второго колеса относительно
        /// центра корпуса машины. </param>
        /// <param name="collisionCategory"> Категория, к которой относиться машина. 
        /// </param>
        /// <returns> Возвращает ссылку на машину. </returns>
        public static Car CreateCar(World world, Vertices carVertices, Vector2 position,
            float speed, float firstWheelRadius, float secondWheelRadius,
            Vector2 firstWheelPosition, Vector2 secondWheelPosition, int collisionCategory)
        {
            Car car = new Car(world, speed);
            car.CarBody = ObjectFactory.AddPolygon(world, carVertices, position);
            car.FirstWheel = ObjectFactory.AddCircle(world, position,
                firstWheelRadius);
            car.SecondWheel = ObjectFactory.AddCircle(world, position,
                secondWheelRadius);
            JointFactory.CreateRevoluteJoint(world, car.CarBody, car.FirstWheel,
                firstWheelPosition, new Vector2(0, 0));
            JointFactory.CreateRevoluteJoint(world, car.CarBody, car.SecondWheel,
                secondWheelPosition, new Vector2(0, 0));

            // Задаем категории частям машинки.
            car.CarBody.CollisionCategories = (Category)(1 << collisionCategory);
            car.FirstWheel.CollisionCategories = (Category)(1 << collisionCategory);
            car.SecondWheel.CollisionCategories = (Category)(1 << collisionCategory);
            // Устанавливаем, что сталкиваемя только с землей.
            car.CarBody.CollidesWith = Category.Cat1;
            car.FirstWheel.CollidesWith = Category.Cat1;
            car.SecondWheel.CollidesWith = Category.Cat1;

            return car;
        }

        /// <summary>
        /// Создает машину со случайной формой корпуса.
        /// </summary>
        /// <param name="world"> Физическая модель, в которую добавляется объект. </param>
        /// <returns> Возвращает ссылку на машину. </returns>
        //public static Car CreateRandomCar(World world)
        //{
        //    Car car = new Car(world, 100.0f);
        //    car.CarBody = AddRandomPolygon(world, 6, 60, new Vector2(400, 100));
        //    Vertices vert = (car.CarBody.UserData as ObjectInfo).vertices;
        //    int vertexIndex = Common.Rnd.Next(vert.Count);
        //    Vector2 firstWheelPos = vert[vertexIndex];
        //    Vector2 secondWheelPos = vert[(vertexIndex + 1) % vert.Count];

        //    car.FirstWheel = AddCircle(world, new Vector2(0, 100), 20);
        //    car.SecondWheel = AddCircle(world, new Vector2(0, 100), 20);
        //    JointFactory.CreateRevoluteJoint(world, car.CarBody, car.FirstWheel,
        //        firstWheelPos, new Vector2(0, 0));
        //    JointFactory.CreateRevoluteJoint(world, car.CarBody, car.SecondWheel,
        //        secondWheelPos, new Vector2(0, 0));
        //    return car;
        //}

        /// <summary>
        /// Создает машину "хорошой" формы.
        /// </summary>
        /// <param name="world"> Мир, в который добавляется объект. </param>
        /// <returns> Возвращает ссылку на машину. </returns>
        //public static Car CreateGoodCar(World world, float radius)
        //{
        //    Car car = new Car(world, 20.0f);
        //    car.CarBody = AddPolygon(world,
        //        new Vertices(new Vector2[]{
        //            new Vector2(-radius, 0),
        //            new Vector2(-radius * 0.95f, radius * 0.3f),
        //            new Vector2(-radius * 0.5f, radius * 0.3f),
        //            new Vector2(-radius * 0.35f, radius * 0.6f),
        //            new Vector2(radius * 0.35f, radius * 0.6f),
        //            new Vector2(radius * 0.5f, radius * 0.3f),
        //            new Vector2(radius * 0.95f, radius * 0.3f),
        //            new Vector2(radius, 0)}),
        //        new Vector2(10, 50), Common.RandomColor());

        //    car.FirstWheel = AddCircle(world, new Vector2(10, 50), radius * 0.2f);
        //    car.SecondWheel = AddCircle(world, new Vector2(10, 50), radius * 0.2f);
        //    JointFactory.CreateRevoluteJoint(world, car.CarBody, car.FirstWheel,
        //        new Vector2(-radius * 0.8f, 0), new Vector2(0, 0));
        //    JointFactory.CreateRevoluteJoint(world, car.CarBody, car.SecondWheel,
        //        new Vector2(radius * 0.8f, 0), new Vector2(0, 0));
        //    return car;
        //}
    }
}
