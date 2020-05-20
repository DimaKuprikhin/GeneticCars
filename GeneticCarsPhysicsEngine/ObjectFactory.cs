using System;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace GeneticCarsPhysicsEngine
{
    static class ObjectFactory
    {
        /// <summary>
        /// Значение упругости объектов.
        /// </summary>
        private const float standartRestitution = 0.4f;
        /// <summary>
        /// Значение коэффициента трения объектов.
        /// </summary>
        private const float standartFriction = 0.4f;

        /// <summary>
        /// Генератор случайных чисел.
        /// </summary>
        private static Random rnd = new Random();

        /// <summary>
        /// Создает объект случайного цвета.
        /// </summary>
        /// <returns> Возвращает объект цвета со случайными значениями каналов.
        /// </returns>
        private static Color RandomColor()
        {
            return new Color( rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
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
                isGround ? new Color(0, 0, 0) : RandomColor() ));
            body.Restitution = restitution;
            body.Friction = friction;
            return body;
        }


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
            float speed, float fuel, float firstWheelRadius, float secondWheelRadius,
            Vector2 firstWheelPosition, Vector2 secondWheelPosition, int collisionCategory)
        {
            Car car = new Car(world, speed, fuel);
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
    }
}
