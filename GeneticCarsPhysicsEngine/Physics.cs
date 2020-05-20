using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
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
        /// Массив объектов, составляющих поверхность.
        /// </summary>
        private List<Body> ground = new List<Body>();
        /// <summary>
        /// Массив координат вершин поверхности.
        /// </summary>
        public List<Vector2> groundVertices { get; private set; } = new List<Vector2>();
        /// <summary>
        /// Координата левого нижней вершины поверхности.
        /// </summary>
        public Vector2 lowerLeftVertex { get; } = new Vector2(-80, -2);
        /// <summary>
        /// Координата правой нижней вершины поверхности.
        /// </summary>
        public Vector2 lowerRightVertex { get; private set; }

        /// <summary>
        /// Массив машинок.
        /// </summary>
        public List<Car> Cars { get; private set; }

        /// <summary>
        /// Стартовая координата машинок.
        /// </summary>
        private readonly Vector2 carStartPosition = new Vector2(20, 50);

        /// <summary>
        /// Конструктор, принимающий значение гравитации.
        /// </summary>
        /// <param name="xGravity"> Значение проекции вектора гравитации 
        /// на ось Х. </param>
        /// <param name="yGravity"> Значение проекции вектора гравитации
        /// на ось Y. </param>
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
        public void AddGroundVertices(Vector2[] vertices)
        {
            // Разбиваем полигон земли на четырехугольники, 
            // т.к. нужны выпуклые многоугольники.
            for(int i = 1; i < vertices.Length; i++)
            {
                groundVertices.Add(vertices[i]);
                Vector2[] v = new Vector2[4]{
                    new Vector2(vertices[i - 1].X, lowerLeftVertex.Y),
                    vertices[i - 1],
                    vertices[i],
                    new Vector2(vertices[i].X, lowerLeftVertex.Y) };
                ground.Add(ObjectFactory.AddPolygon(world, new Vertices(v), new Vector2(0, 0),
                    true));
                ground[i - 1].CollisionCategories = (Category)(1);
            }
            lowerRightVertex = new Vector2(groundVertices[groundVertices.Count - 1].X,
                lowerLeftVertex.Y);
        }

        /// <summary>
        /// Удаляет информацию о поверхности.
        /// </summary>
        public void SetNewGround()
        {
            ground = new List<Body>();
            groundVertices = new List<Vector2>();
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
        public void AddCar(Vector2[] vertices, float speed, float fuel, float firstWheelRadius, 
            float secondWheelRadius, Vector2 firstWheelPosition,
            Vector2 secondWheelPosition, int collisionCategory)
        {
            Vector2[] vert = new Vector2[vertices.GetLength(0)];
            for(int i = 0; i < vert.Length; i++) {
                vert[i] = new Vector2(vertices[i].X, vertices[i].Y);
            }
            Cars.Add(ObjectFactory.CreateCar(world, new Vertices(vert), carStartPosition, 
                speed, fuel, firstWheelRadius, secondWheelRadius, 
                new Vector2(firstWheelPosition.X, firstWheelPosition.Y),
                new Vector2(secondWheelPosition.X, secondWheelPosition.Y), 
                collisionCategory));
        }

        /// <summary>
        /// Удаляет все машинки.
        /// </summary>
        public void RemoveCars()
        {
            while(Cars.Count > 0)
            {
                world.RemoveBody(Cars[0].CarBody);
                world.RemoveBody(Cars[0].FirstWheel);
                world.RemoveBody(Cars[0].SecondWheel);
                Cars.Remove(Cars[0]);
            }
        }

        /// <summary>
        /// Заставляет машинку двигаться вперед(вправо).
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        public void GoForward(int index, float delTime)
        {
            Cars[index].GoForward(delTime);
        }


        /// <summary>
        /// Возвращает массив цветов машинок.
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        /// <returns> Цвет корпуса и цвета двух колес в формате RGB. </returns>
        public Color[] GetCarColors(int index)
        {
            Color[] result = new Color[3];
            result[0] = (Cars[index].CarBody.UserData as ObjectInfo).ObjectColor;
            result[1] = (Cars[index].FirstWheel.UserData as ObjectInfo).ObjectColor;
            result[2] = (Cars[index].SecondWheel.UserData as ObjectInfo).ObjectColor;
            return result;
        }

        /// <summary>
        /// Возвращает массив заполненостей баков машинок.
        /// </summary>
        /// <returns></returns>
        public float[] GetCarFuels()
        {
            float[] result = new float[Cars.Count];
            for(int i = 0; i < Cars.Count; ++i)
            {
                result[i] = Cars[i].Fuel / Cars[i].MaxFuel;
            }
            return result;
        }

        /// <summary>
        /// Получение центра корпуса машинки.
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        /// <returns> Возвращает координату центра машинки. </returns>
        public Vector2 GetCarCenter(int index)
        {
            return new Vector2(Cars[index].CarBody.Position.X,
                Cars[index].CarBody.Position.Y);
        }

        /// <summary>
        /// Получение массива координта вершин корпуса машинки.
        /// </summary>
        /// <param name="index"> Индекс машинки. </param>
        /// <returns> Возвращает массив координат. </returns>
        public Vector2[] GetCarBodyCoordinates(int index)
        {
            Vertices vert = (Cars[index].CarBody.UserData as ObjectInfo).vertices;
            Transform t;
            Cars[index].CarBody.GetTransform(out t);
            Vector2 position = Cars[index].CarBody.Position;

            Vector2[] result = new Vector2[vert.Count];
            for(int i = 0; i < vert.Count; i++)
            {
                Vector2 vec = new Vector2();
                vec.X = position.X + vert[i].X * t.q.c - vert[i].Y * t.q.s;
                vec.Y = position.Y + vert[i].X * t.q.s + vert[i].Y * t.q.c;
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
        public ObjectInfo[] GetCarWheelsCoordinates(int index)
        {
            ObjectInfo[] result = new ObjectInfo[2];
            result[0] = (ObjectInfo)Cars[index].FirstWheel.UserData;
            result[1] = (ObjectInfo)Cars[index].SecondWheel.UserData;
            Transform t;
            Cars[index].FirstWheel.GetTransform(out t);
            result[0].CircleCenter = new Vector2(Cars[index].FirstWheel.Position.X,
                Cars[index].FirstWheel.Position.Y);
            result[0].CircleAngle = new Vector2(t.q.c, t.q.s);

            Cars[index].SecondWheel.GetTransform(out t);
            result[1].CircleCenter = new Vector2(Cars[index].SecondWheel.Position.X,
                Cars[index].SecondWheel.Position.Y);
            result[1].CircleAngle = new Vector2(t.q.c, t.q.s);
            return result;
        }
    }
}
