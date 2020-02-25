using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticCarsPhysicsEngine;
using GeneticCarsGeneticAlgorithm;

namespace GeneticCarsPresenter
{
    public class Presenter
    {
        private Physics physics;

        private GeneticAlgorithm algorithm;

        private IWinFormsView view;

        private const float PixelPerMeter = 7.0f;

        public Presenter(IWinFormsView view)
        {
            this.view = view;
            view.start += new EventHandler<EventArgs>(OnStartSimulation);
            view.step += new EventHandler<EventArgs>(OnStep);
            view.paint += new EventHandler<EventArgs>(OnPaint);
            view.populationSizeChanged += 
                new EventHandler<EventArgs>(OnPopulationSizeChanged);
            view.eliteClonesChanged += 
                new EventHandler<EventArgs>(OnEliteClonesChanged);
            view.crossoverTypeChanged += 
                new EventHandler<EventArgs>(OnCrossoverTypeChanged);
            view.mutationRateChanged += 
                new EventHandler<EventArgs>(OnMutationRateChanged);
            view.generationLifeTimeChanged +=
                new EventHandler<EventArgs>(OnGenerationLifeTimeChanged);

            algorithm = new GeneticAlgorithm(4, 96);
        }

        private const float minCarSize = 6.0f;
        private const float maxCarSize = 10.0f;
        private const int vertexNumber = 6;
        private const float minWheelRadius = 1.0f;
        private const float maxWheelRadius = 2.0f;
        private const float minCarSpeed = 10.0f;
        private const float maxCarSpeed = 20.0f;
        /// <summary>
        /// Переводит гены особи в ее физический образ.
        /// Геном:
        /// 8 бит - размар машинки
        /// 6 * 8 бит - угол каждой из 6 вершин корпуса машинки
        /// 8 бит - радиус первого колеса
        /// 8 бит - радиус второго колеса
        /// 8 бит - угол первого колеса
        /// 8 бит - кгол второго колеса
        /// 8 бит - скорость машинки (угловая скорость колес)
        /// </summary>
        private void ConvertGenesToCars()
        {
            byte[][] genes = algorithm.GetPopulationInfo();
            
            for(int i = 0; i < genes.GetLength(0); i++)
            {
                // Размер машинки.
                float carSize = minCarSize + (maxCarSize - minCarSize) * 
                    ((float)genes[i][0] / 256.0f);
                // Значения углов вершин.
                List<float> angles = new List<float>(vertexNumber);
                for(int j = 0; j < vertexNumber; j++)
                {
                    angles.Add(2.0f * (float)Math.PI * ((float)genes[i][j + 1] / 256.0f));
                }
                angles.Sort();

                // Радиусы колес.
                float firstWheelRadius = minWheelRadius +
                    (maxWheelRadius - minWheelRadius) * ((float)genes[i][7] / 256.0f);
                float secondWheelRadius = minWheelRadius + 
                    (maxWheelRadius - minWheelRadius) * ((float)genes[i][8] / 256.0f);

                // Значения углов колес.
                float firstWheelAngle = 2.0f * (float)Math.PI * 
                    ((float)genes[i][9] / 256.0f);
                float secondWheelAngle = 2.0f * (float)Math.PI * 
                    ((float)genes[i][10] / 256.0f);

                // Скорость машинки.
                float speed = minCarSpeed + (maxCarSpeed - minCarSpeed) *
                    ((float)genes[i][11] / 256.0f);

                // Вычисляем координаты вершин.
                float[][] vertices = new float[vertexNumber][];
                for(int j = 0; j < vertexNumber; j++)
                {
                    vertices[j] = new float[2]{carSize * (float)Math.Cos(angles[j]),
                        carSize * (float)Math.Sin(angles[j])};
                }

                // Вычисляем координаты колес.
                float[] firstWheelPosition = new float[2]{
                    carSize * (float)Math.Cos(firstWheelAngle),
                    carSize * (float)Math.Sin(firstWheelAngle)};
                float[] secondWheelPosition = new float[2]{
                    carSize * (float)Math.Cos(secondWheelAngle),
                    carSize * (float)Math.Sin(secondWheelAngle)};

                // Передвигаем центры колес на ребра корпуса машинки.
                float[] firstWheelPositionOnCar = new float[2];
                float[] secondWheelPositionOnCar = new float[2];
                // Определяем, между какими соседними углами вершин корпуса
                // находятся колеса.
                for(int j = 1; j <= angles.Count; j++) {
                    float firstAngle = angles[j - 1];
                    float secondAngle = angles[j % angles.Count];
                    float circle = (float)Math.PI * 2;
                    // Если первый угол - угол с максимальным значением,
                    // а второй угол - угол с минимальным значением,
                    // то прибавляем ко второму углу период.
                    if(secondAngle < firstAngle)
                        secondAngle += circle;
                    // Если угол колеса лежит между соседними углами вершин.
                    if((firstAngle <= firstWheelAngle && 
                        firstWheelAngle <= secondAngle) ||
                        (firstAngle - circle <= firstWheelAngle &&
                        firstWheelAngle <= secondAngle - circle)) {
                        // Вычисляем позицию колеса на корпусе как точку 
                        // пересечения прямой, включающей центр машинки и 
                        // координату колеса, и прямой, включающей соседние вершины.
                        firstWheelPositionOnCar =
                            GetIntersectionPoint(
                                new float[2] { 0, 0 }, firstWheelPosition,
                                vertices[j - 1], vertices[j % angles.Count]);
                        // Если пересечение прямых за пределами машинки.
                        if(Math.Pow(firstWheelPositionOnCar[0], 2) +
                            Math.Pow(firstWheelPositionOnCar[1], 2) >=
                            Math.Pow(carSize, 2))
                        {
                            // Перемещаем колесо к ближайшей по углу вершине.
                            if(Math.Abs(firstWheelAngle - firstAngle) <
                                Math.Abs(firstWheelAngle - secondAngle)){
                                firstWheelPositionOnCar = vertices[j - 1];
                            }
                            else {
                                firstWheelPositionOnCar = vertices[j % angles.Count];
                            }
                        }
                    }
                    // Аналогично для второго колеса.
                    if((firstAngle <= secondWheelAngle &&
                        secondWheelAngle <= secondAngle) ||
                        (firstAngle - circle <= secondWheelAngle && 
                        secondWheelAngle <= secondAngle - circle))
                    {
                        secondWheelPositionOnCar =
                            GetIntersectionPoint(
                                new float[2] { 0, 0 }, secondWheelPosition,
                                vertices[j - 1], vertices[j % angles.Count]);
                        if(Math.Pow(secondWheelPositionOnCar[0], 2) +
                            Math.Pow(secondWheelPositionOnCar[1], 2) >=
                            Math.Pow(carSize, 2))
                        {
                            if(Math.Abs(secondWheelAngle - firstAngle) <
                                Math.Abs(secondWheelAngle - secondAngle))
                            {
                                secondWheelPositionOnCar = vertices[j - 1];
                            }
                            else
                            {
                                secondWheelPositionOnCar = vertices[j % angles.Count];
                            }
                        }
                    }
                }

                // Добавляем машинку с расшифрованными параметрами.
                physics.AddCar(vertices, speed, firstWheelRadius, secondWheelRadius,
                    firstWheelPositionOnCar, secondWheelPositionOnCar, i + 1);
            }
        }

        /// <summary>
        /// Вычисляет точку пересечения двух прямых.
        /// </summary>
        /// <param name="a"> Две координаты первой точки первой прямой. </param>
        /// <param name="b"> Две координаты второй точки первой прямой. </param>
        /// <param name="c"> Две координаты первой точки второй прямой. </param>
        /// <param name="d"> Две координаты второй точки второй прямой. </param>
        /// <returns> Возвращает массив из 2 чисел - координат точки пересечения.
        /// </returns>
        private float[] GetIntersectionPoint(float[] a, float[] b, 
            float[] c, float[] d)
        {
            // Вычисляем коэффициенты общих уравнений прямых.
            float firstA = a[1] - b[1];
            float firstB = b[0] - a[0];
            float firstC = -firstA * a[0] - firstB * a[1];

            float secondA = c[1] - d[1];
            float secondB = d[0] - c[0];
            float secondC = -secondA * c[0] - secondB * c[1];

            // Получаем точку пересечения.
            float zn = firstA * secondB - firstB * secondA;
            float xInter = -(firstC * secondB - firstB * secondC) / zn;
            float yInter = -(firstA * secondC - firstC * secondA) / zn;
            return new float[2] { xInter, yInter };
        }

        private void OnStartSimulation(object sender, EventArgs e)
        {
            // Задаем гравитацию и землю.
            physics = new Physics(0, -20.0f);
            SetRandomGround((float)Math.PI / 8, 20, -50, 20, 1000, 5, 45);
            // Переводим гены в машинки.
            ConvertGenesToCars();
        }

        /// <summary>
        /// Создает случайную поверхность для земли.
        /// </summary>
        /// <param name="maxAngle"> Максимальный угол нового ребра относительно 
        /// горизонта. </param>
        /// <param name="maxEdgeLength"> Максимальная длина ребра. </param>
        /// <param name="beginX"> Координата X левой вершины. </param>
        /// <param name="beginY"> Координата Y левой вершины. </param>
        /// <param name="endX"> Координата X правой вершины. </param>
        /// <param name="lowerBound"> Минимально значение координта Y для вершины. </param>
        /// <param name="upperBound"> Максимальное значение координта Y для вершины. 
        /// </param>
        private void SetRandomGround(float maxAngle, float 
            maxEdgeLength, float beginX, float beginY, float endX, 
            float lowerBound, float upperBound)
        {
            Random rnd = new Random();
            List<float[]> groundPoints = new List<float[]>();
            groundPoints.Add(new float[2] { beginX, beginY });
            while(groundPoints[groundPoints.Count - 1][0] < endX)
            {
                float angle = ((float)rnd.NextDouble() * 2.0f - 1.0f) * maxAngle;
                float edgeLength = (float)rnd.NextDouble() * 
                    Math.Max(maxEdgeLength, 0.1f);
                // Проеверяем, не выходит ли новое ребро за пределы.
                if(groundPoints[groundPoints.Count - 1][1] +
                    edgeLength * (float)Math.Sin(angle) > upperBound)
                {
                    edgeLength *= (upperBound - groundPoints[groundPoints.Count - 1][1]) /
                        (edgeLength * (float)Math.Sin(angle));
                }
                if(groundPoints[groundPoints.Count - 1][1] +
                   edgeLength * (float)Math.Sin(angle) < lowerBound)
                {
                    edgeLength *= (groundPoints[groundPoints.Count - 1][1] - lowerBound) /
                        (edgeLength * (float)Math.Sin(angle));
                }

                groundPoints.Add(new float[2]{
                    groundPoints[groundPoints.Count - 1][0] +
                    edgeLength * (float)Math.Cos(angle),
                    groundPoints[groundPoints.Count - 1][1] + 
                    edgeLength * (float)Math.Sin(angle)});
            }
            float[][] ground = new float[groundPoints.Count][];
            for(int i = 0; i < groundPoints.Count; i++)
            {
                ground[i] = new float[2] { groundPoints[i][0], groundPoints[i][1] };
            }
            physics.SetGround(ground, -10);
        }

        private void OnPaint(object sender, EventArgs e)
        {
            if(physics == null)
                return;

            // Найдем машинку с максимальной координатой X, чтобы отрисовывать
            // все остальное относительно нее.
            float maxX = 0;
            float maxY = 0;
            for(int i = 0; i < physics.CarsCount; i++)
            {
                float[] center = physics.GetCarCenter(i);
                if(maxX < center[0])
                {
                    maxX = center[0];
                    maxY = center[1];
                }
            }
            // Переводим в координаты формы.
            maxX = maxX * PixelPerMeter - view.GetWidth / 2;
            maxY = maxY * PixelPerMeter - view.GetHeight / 2;
            
            for(int i = 0; i < physics.CarsCount; i++)
            {
                // Получаем цвета машинки.
                int[][] colors = physics.GetCarColors(i);
                // Получаем координаты вершин корпуса машинки.
                float[][] verts = physics.GetCarBodyCoordinates(i);
                // Переводим в координаты формы.
                float[][] vertices = new float[verts.GetLength(0)][];
                for(int j = 0; j < verts.GetLength(0); j++)
                {
                    vertices[j] = new float[2]{verts[j][0] * PixelPerMeter - maxX,
                    verts[j][1] * PixelPerMeter}; // -maxY
                    vertices[j][1] = view.GetHeight - vertices[j][1];
                }
                // Выведем корпус.
                view.ShowPolygon(vertices, colors[0]);

                // Получим информацию о колесах.
                float[][] wheels = physics.GetCarWheelsCoordinates(i);
                // Position.x, Position.y, radius, cos, sin
                float[] first = new float[5];
                float[] second = new float[5];
                for(int j = 0; j < 3; j++)
                {
                    first[j] = wheels[0][j] * PixelPerMeter;
                    second[j] = wheels[1][j] * PixelPerMeter;
                }
                // Выведем первое колесо.
                view.ShowCircle(first[0] - maxX,
                    //view.GetHeight - (first[1] - maxY), first[2],
                    view.GetHeight - (first[1]), first[2],
                    colors[1], first[2] * wheels[0][3], first[2] * wheels[0][4]);
                // Выведем второе колесо.
                view.ShowCircle(second[0] - maxX,
                    //view.GetHeight - (second[1] - maxY), second[2], 
                    view.GetHeight - (second[1]), second[2], 
                    colors[2], second[2] * wheels[1][3], second[2] * wheels[1][4]);
            }

            // Выведем землю.
            if(physics.HasGround)
            {
                float[][] ground = physics.GetGround();
                float[][] points = new float[ground.GetLength(0)][];
                for(int i = 0; i < ground.GetLength(0); i++)
                {
                    points[i] = new float[2]{ground[i][0] * PixelPerMeter - maxX,
                        //view.GetHeight - (ground[i][1] * PixelPerMeter - maxY) };
                        view.GetHeight - (ground[i][1] * PixelPerMeter) };
                }
                view.ShowPolygon(points, new int[3] { 0, 0, 0 });
            }
        }

        private double currentGenerationTime = 0;
        private double generationLifeTime = 20;
        private void OnStep(object sender, EventArgs e)
        {
            // Прибавляем время, прошедшее с прошлого шага.
            currentGenerationTime += view.Interval;
            Console.WriteLine(currentGenerationTime);
            if(currentGenerationTime < generationLifeTime)
            {
                // Производим шаг симуляции физики.
                physics.Step(view.Interval);
                // Двигаем машинки вперед.
                for(int i = 0; i < physics.CarsCount; i++)
                {
                    physics.GoForward(i);
                }
            }
            // Если время жизни поколения истекло.
            else
            {
                // Получаем результаты машинок как координаты X центра корпуса.
                double[] carResults = new double[physics.CarsCount];
                for(int i = 0; i < physics.CarsCount; i++)
                {
                    float[] result = physics.GetCarCenter(i);
                    carResults[i] = result[0];
                }
                // Кладем результаты в алгоритм.
                algorithm.SetFitnessFunctionResults(carResults);
                // Генерируем новое поколение.
                algorithm.GenerateNextGeneration();
                // Удаляем все машинки старого поколения.
                while(physics.CarsCount > 0)
                {
                    physics.RemoveCar(0);
                }
                // Добавляем машинки нового поколения.
                ConvertGenesToCars();
                currentGenerationTime = 0;
            }
            view.SetCurrentGenerationTime(currentGenerationTime);
        }

        private void OnGenerationLifeTimeChanged(object sender, EventArgs e)
        {
            generationLifeTime = view.GenerationLifeTime;
        }

        private void OnPopulationSizeChanged(object sender, EventArgs e)
        {
            algorithm.PopulationSize = view.PopulationSize;
        }

        private void OnEliteClonesChanged(object sender, EventArgs e)
        {
            algorithm.EliteClones = view.EliteClones;
        }

        private void OnCrossoverTypeChanged(object sender, EventArgs e)
        {
            algorithm.SetCrossover(view.CrossoverType);
        }

        private void OnMutationRateChanged(object sender, EventArgs e)
        {
            algorithm.MutationRate = (double)view.MutationRate / 100;
        }
    }
}
