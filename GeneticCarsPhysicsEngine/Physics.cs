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
    public class Physics
    {
        /// <summary>
        /// Мир физической симуляции.
        /// </summary>
        private readonly World world;

        /// <summary>
        /// Массив объектов, составляющих "землю" и координаты вершин поверхности.
        /// </summary>
        private List<Body> ground = new List<Body>();
        private float[][] groundVertices;

        /// <summary>
        /// Массив машинок.
        /// </summary>
        private List<Car> Cars;
        public int CarsCount { get { if(Cars == null) return 0; return Cars.Count; } }

        /// <summary>
        /// Стартовая координата машинок.
        /// </summary>
        private readonly Vector2 carStartPosition = new Vector2(20, 50);

        /// <summary>
        /// Возвращает true, если задана "земля".
        /// </summary>
        public bool HasGround { get; private set; } = false;

        public Physics(float xGravity, float yGravity)
        {
            world = new World(new Vector2(xGravity, yGravity));
            Cars = new List<Car>();
        }

        /// <summary>
        /// Задает поверхность мира.
        /// </summary>
        /// <param name="vertices"> Массив координат вершин поверхности
        /// в порядке возрастания координаты X. </param>
        /// <param name="lowBound"> Координата нижней границы земли. </param>
        public void SetGround(float[][] vertices, float lowBound)
        {
            // Делаем массив с длиной на 2 больше для вершин, 
            // описывающих нижнюю грань земли.
            groundVertices = new float[vertices.GetLength(0) + 2][];
            groundVertices[0] = new float[2] { vertices[0][0], lowBound };
            groundVertices[1] = new float[2] { vertices[0][0], vertices[0][1] };
            // Разбиваем полигон земли на четырехугольники, 
            // т.к. нужны выпуклые многоугольники.
            for(int i = 1; i < vertices.GetLength(0); i++)
            {
                groundVertices[i + 1] = new float[2] { vertices[i][0], vertices[i][1] };
                Vector2[] v = new Vector2[4]{
                    new Vector2(vertices[i - 1][0], lowBound),
                    new Vector2(vertices[i - 1][0], vertices[i - 1][1]),
                    new Vector2(vertices[i][0], vertices[i][1]),
                    new Vector2(vertices[i][0], lowBound) };

                Vertices vert = new Vertices(v);
                ground.Add(ObjectFactory.AddPolygon(world, vert, new Vector2(0, 0),
                    true));
                ground[i - 1].CollisionCategories = (Category)(1);
            }
            groundVertices[groundVertices.GetLength(0) - 1] =
                new float[2] { vertices[vertices.GetLength(0) - 1][0], lowBound };

            HasGround = true;
        }

        /// <summary>
        /// Производит шаг симуляции.
        /// </summary>
        /// <param name="dt"> Количество времени, прошедшее с прошлого шага. </param>
        public void Step(float dt)
        {
            world.Step(dt);
        }

        /// <summary>
        /// Добавляет машинку с задаными параметрами.
        /// </summary>
        /// <param name="vertices"> Вершины корпуса машинки. </param>
        /// <param name="speed"> Скорость машинки. </param>
        /// <param name="firstWheelRadius"> Радиус первого колеса. </param>
        /// <param name="secondWheelRadius"> Радиус второго колеса. </param>
        /// <param name="firstWheelPosition"> Координата первого колеса. </param>
        /// <param name="secondWheelPosition"> Координата второго колеса. </param>
        public void AddCar(float[][] vertices, float speed, float firstWheelRadius, 
            float secondWheelRadius, float[] firstWheelPosition, 
            float[] secondWheelPosition, int collisionCategory)
        {
            Vector2[] vert = new Vector2[vertices.GetLength(0)];
            for(int i = 0; i < vert.Length; i++) {
                vert[i] = new Vector2(vertices[i][0], vertices[i][1]);
            }
            Cars.Add(ObjectFactory.CreateCar(world, new Vertices(vert), carStartPosition, 
                speed, firstWheelRadius, secondWheelRadius, 
                new Vector2(firstWheelPosition[0], firstWheelPosition[1]),
                new Vector2(secondWheelPosition[0], secondWheelPosition[1]), 
                collisionCategory));
        }

        public void RemoveCar(int index)
        {
            world.RemoveBody(Cars[index].CarBody);
            world.RemoveBody(Cars[index].FirstWheel);
            world.RemoveBody(Cars[index].SecondWheel);
            Cars.Remove(Cars[index]);
        }

        //public void AddRandomCar()
        //{
        //    Cars.Add(ObjectFactory.CreateRandomCar(world));
        //}

        //public void AddGoodCar(float radius)
        //{
        //    Cars.Add(ObjectFactory.CreateGoodCar(world, radius));
        //}

        /// <summary>
        /// Заставляет машинку двигаться вперед(вправо).
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        public void GoForward(int index)
        {
            Cars[index].GoForward();
        }

        /// <summary>
        /// Получение вершин поверхности земли.
        /// </summary>
        /// <returns> Возвращает массив координта вершин земли. </returns>
        public float[][] GetGround()
        {
            return groundVertices;
        }

        /// <summary>
        /// Получение цвета машинки.
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        /// <returns> Цвет корпуса и цвета двух колес в формате RGB. </returns>
        public int[][] GetCarColors(int index)
        {
            int[][] result = new int[3][];
            result[0] = (Cars[index].CarBody.UserData as ObjectInfo).Color;
            result[1] = (Cars[index].FirstWheel.UserData as ObjectInfo).Color;
            result[2] = (Cars[index].SecondWheel.UserData as ObjectInfo).Color;
            return result;
        }

        /// <summary>
        /// Получение центра корпуса машинки.
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        /// <returns> Возвращает координату центра машинки. </returns>
        public float[] GetCarCenter(int index)
        {
            return new float[2] { Cars[index].CarBody.Position.X,
                Cars[index].CarBody.Position.Y };
        }

        /// <summary>
        /// Получение массива координта вершин корпуса машинки.
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        /// <returns> Возвращает массив координат. </returns>
        public float[][] GetCarBodyCoordinates(int index)
        {
            Vertices vert = (Cars[index].CarBody.UserData as ObjectInfo).vertices;
            Transform t;
            Cars[index].CarBody.GetTransform(out t);
            Vector2 position = Cars[index].CarBody.Position;

            float[][] result = new float[vert.Count][];
            for(int i = 0; i < vert.Count; i++)
            {
                float[] vec = new float[2];
                vec[0] = position.X + vert[i].X * t.q.c - vert[i].Y * t.q.s;
                vec[1] = position.Y + vert[i].X * t.q.s + vert[i].Y * t.q.c;
                result[i] = vec;
            }
            return result;
        }

        // positionX, positionY, radius, Transform.cos, Transform.Sin
        /// <summary>
        /// Получение данных о положении колес.
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        /// <returns> Возвращает массив, состоящий из координаты центра колеса, 
        /// радиуса, косинуса и синуса поворота. </returns>
        public float[][] GetCarWheelsCoordinates(int index)
        {
            float[][] result = new float[2][];
            Transform t;
            Cars[index].FirstWheel.GetTransform(out t);
            result[0] = new float[]{Cars[index].FirstWheel.Position.X,
                Cars[index].FirstWheel.Position.Y,
            (Cars[index].FirstWheel.UserData as ObjectInfo).Radius,
            t.q.c, t.q.s};

            Cars[index].SecondWheel.GetTransform(out t);
            result[1] = new float[]{Cars[index].SecondWheel.Position.X,
                Cars[index].SecondWheel.Position.Y,
            (Cars[index].SecondWheel.UserData as ObjectInfo).Radius,
            t.q.c, t.q.s};
            return result;
        }
    }
}
