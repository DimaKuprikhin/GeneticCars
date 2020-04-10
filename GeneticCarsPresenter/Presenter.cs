using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using GeneticCarsPhysicsEngine;
using GeneticCarsGeneticAlgorithm;
using Microsoft.Xna.Framework;

namespace GeneticCarsPresenter
{
    public class Presenter
    {
        private Physics physics;

        private GeneticAlgorithm algorithm;

        private IWinFormsView view;

        private const float PixelPerMeter = 7.0f;

        private float simulationSpeed = 1.0f;

        /// <summary> Массив лучших результатов поколений. </summary>
        private List<float> bestResults = new List<float>();

        /// <summary> Массив средних резьльтатов поколений. </summary>
        private List<float> averageResults = new List<float>();

        private float fuelPerSquareMeter = 12;

        public Presenter(IWinFormsView view)
        {
            this.view = view;
            view.createPopulation += new EventHandler<EventArgs>(OnCreatePopulation);
            view.createGround += new EventHandler<EventArgs>(OnCreateGround);
            view.finishSimulation += new EventHandler<EventArgs>(OnFinishSimulation);
            view.step += new EventHandler<EventArgs>(OnStep);
            view.paint += new EventHandler<EventArgs>(OnPaint);
            view.graphPaint += new EventHandler<EventArgs>(OnGraphPaint);
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
            view.simulationSpeedChanged += 
                new EventHandler<EventArgs>(OnSimulationSpeedChanged);
            view.fuelPerSquareMeterSet +=
                new EventHandler<EventArgs>(OnFuelPerSquareMeterSet);
            view.populatioinSaveButtonClick +=
                new EventHandler<EventArgs>(OnSavePopulation);
            view.populatioinLoadButtonClick +=
                new EventHandler<EventArgs>(OnLoadPopulation);
            //algorithm = new GeneticAlgorithm(view.PopulationSize, 96);
            OnCreateGround(this, EventArgs.Empty);
        }

        private const float minCarSize = 6.0f;
        private const float maxCarSize = 10.0f;
        private const int vertexNumber = 6;
        private const float minWheelRadius = 1.0f;
        private const float maxWheelRadius = 2.0f;
        private const float minCarSpeed = 10.0f;
        private const float maxCarSpeed = 50.0f;
        /// <summary>
        /// Переводит гены особи в ее физический образ.
        /// Геном:
        /// 8 бит - размар машинки
        /// 6 * 8 бит - угол каждой из 6 вершин корпуса машинки
        /// 8 бит - радиус первого колеса
        /// 8 бит - радиус второго колеса
        /// 8 бит - угол первого колеса
        /// 8 бит - угол второго колеса
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
                Vector2[] vertices = new Vector2[vertexNumber];
                for(int j = 0; j < vertexNumber; j++)
                {
                    vertices[j] = new Vector2(carSize * (float)Math.Cos(angles[j]),
                        carSize * (float)Math.Sin(angles[j]));
                }

                // Вычисляем координаты колес.
                Vector2 firstWheelPosition = new Vector2(
                    carSize * (float)Math.Cos(firstWheelAngle),
                    carSize * (float)Math.Sin(firstWheelAngle));
                Vector2 secondWheelPosition = new Vector2(
                    carSize * (float)Math.Cos(secondWheelAngle),
                    carSize * (float)Math.Sin(secondWheelAngle));

                // Передвигаем центры колес на ребра корпуса машинки.
                Vector2 firstWheelPositionOnCar = new Vector2();
                Vector2 secondWheelPositionOnCar = new Vector2();
                // Определяем, между какими соседними углами вершин корпуса
                // находятся колеса.
                for(int j = 1; j <= angles.Count; j++)
                {
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
                        firstWheelAngle <= secondAngle - circle))
                    {
                        // Вычисляем позицию колеса на корпусе как точку 
                        // пересечения прямой, включающей центр машинки и 
                        // координату колеса, и прямой, включающей соседние вершины.
                        firstWheelPositionOnCar =
                            GetIntersectionPoint(
                                new Vector2(0, 0), firstWheelPosition,
                                vertices[j - 1], vertices[j % angles.Count]);
                        // Если пересечение прямых за пределами машинки.
                        if(Math.Pow(firstWheelPositionOnCar.X, 2) +
                            Math.Pow(firstWheelPositionOnCar.Y, 2) >=
                            Math.Pow(carSize, 2))
                        {
                            // Перемещаем колесо к ближайшей по углу вершине.
                            if(Math.Abs(firstWheelAngle - firstAngle) <
                                Math.Abs(firstWheelAngle - secondAngle))
                            {
                                firstWheelPositionOnCar = vertices[j - 1];
                            }
                            else
                            {
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
                                new Vector2(0, 0), secondWheelPosition,
                                vertices[j - 1], vertices[j % angles.Count]);
                        if(Math.Pow(secondWheelPositionOnCar.X, 2) +
                            Math.Pow(secondWheelPositionOnCar.Y, 2) >=
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

                float square = 0;
                for(int j = 2; j < vertices.Length; ++j)
                {
                    square += 0.5f * Math.Abs(
                        (vertices[j - 1].X - vertices[0].X) * 
                        (vertices[j].Y - vertices[0].Y) - 
                        (vertices[j - 1].Y - vertices[0].Y) *
                        (vertices[j].X - vertices[0].X));
                }

                // Добавляем машинку с расшифрованными параметрами.
                physics.AddCar(vertices, speed, fuelPerSquareMeter * square, 
                    firstWheelRadius, secondWheelRadius,
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
        private Vector2 GetIntersectionPoint(Vector2 a, Vector2 b, 
            Vector2 c, Vector2 d)
        {
            // Вычисляем коэффициенты общих уравнений прямых.
            float firstA = a.Y - b.Y;
            float firstB = b.X - a.X;
            float firstC = -firstA * a.X - firstB * a.Y;

            float secondA = c.Y - d.Y;
            float secondB = d.X - c.X;
            float secondC = -secondA * c.X - secondB * c.Y;

            // Получаем точку пересечения.
            float zn = firstA * secondB - firstB * secondA;
            float xInter = -(firstC * secondB - firstB * secondC) / zn;
            float yInter = -(firstA * secondC - firstC * secondA) / zn;
            return new Vector2(xInter, yInter);
        }

        private void OnCreatePopulation(object sender, EventArgs e)
        {
            if(algorithm != null)
            {
                view.ShowMessage("Невозможно создать новую популяцию, пока не " +
                    "завершена текущая симуляция. Завершите текущюю, а затем " +
                    "сгенерируйте новую, или загрузите сохраненную популяцию.", "");
                return;
            }
            algorithm = new GeneticAlgorithm(view.PopulationSize, 96);
            SetAlgorithmSettings();
            // Переводим гены в машинки.
            ConvertGenesToCars();

            bestResults = new List<float>();
            averageResults = new List<float>();
            bestResults.Add(0);
            averageResults.Add(0);
        }

        private void OnCreateGround(object sender, EventArgs e)
        {
            if(algorithm != null)
            {
                view.ShowMessage("Невозможно сгенерировать новую поверхность во время " +
                    "симуляции. Сохраните популяцию и завершите текущюю симуляцию, а " +
                    "затем сгенерируйте новую поверхность и загрузите сохраненную " +
                    "популяцию.", "");
                return;
            }
            // Задаем гравитацию и землю.
            physics = new Physics(0, -30.0f);
            SetRandomGround((float)Math.PI / 5, 20, -80, 20, 30000, 5, 45);
        }

        private void OnFinishSimulation(object sender, EventArgs e)
        {
            algorithm = null;
            physics.RemoveCars();
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
            List<Vector2> groundPoints = new List<Vector2>();
            groundPoints.Add(new Vector2(beginX, beginY));
            while(groundPoints[groundPoints.Count - 1].X < endX)
            {
                float angle = ((float)rnd.NextDouble() * 2.0f - 1.0f) * maxAngle;
                float edgeLength = (float)Math.Max(rnd.NextDouble(), 0.01) * 
                    Math.Max(maxEdgeLength, 0.1f);
                // Проеверяем, не выходит ли новое ребро за пределы.
                if(groundPoints[groundPoints.Count - 1].Y +
                    edgeLength * (float)Math.Sin(angle) > upperBound)
                {
                    //edgeLength *= 
                    //(upperBound - groundPoints[groundPoints.Count - 1][1]) /
                    //    (edgeLength * (float)Math.Sin(angle));
                    angle *= (-1);
                }
                if(groundPoints[groundPoints.Count - 1].Y +
                   edgeLength * (float)Math.Sin(angle) < lowerBound)
                {
                    //edgeLength *=
                    //    (groundPoints[groundPoints.Count - 1][1] - lowerBound) /
                    //    (edgeLength * (float)Math.Sin(angle));
                    angle *= (-1);
                }

                groundPoints.Add(new Vector2(
                    groundPoints[groundPoints.Count - 1].X +
                    edgeLength * (float)Math.Cos(angle),
                    groundPoints[groundPoints.Count - 1].Y + 
                    edgeLength * (float)Math.Sin(angle)));
            }
            Vector2[] ground = new Vector2[groundPoints.Count];
            for(int i = 0; i < groundPoints.Count; i++)
            {
                ground[i] = new Vector2(groundPoints[i].X, groundPoints[i].Y);
            }
            physics.SetGround(ground, -10);
        }

        private void OnPaint(object sender, EventArgs e)
        {
            if(view.IsHide)
                return;

            // Найдем машинку с максимальной координатой X, чтобы отрисовывать
            // все остальное относительно нее.
            float maxX = 20;
            float maxY = 0;
            for(int i = 0; i < physics.CarsCount; i++)
            {
                Vector2 center = physics.GetCarCenter(i);
                if(physics.Cars[i].FuelRefillCount < (int)center.X / 500)
                {
                    physics.Cars[i].RefillFuel();
                }
                if(maxX < center.X)
                {
                    maxX = center.X;
                    maxY = center.Y;
                }
            }
            Console.WriteLine($"{maxX}");
            // Переводим в координаты формы.
            maxX = maxX * PixelPerMeter - view.GetWidth / 2;
            maxY = maxY * PixelPerMeter - view.GetHeight / 2;

            float[] fuels = physics.GetCarFuels();

            for(int i = 0; i < physics.CarsCount; i++)
            {
                // Получаем цвета машинки.
                Color[] colors = physics.GetCarColors(i);
                // Получаем координаты вершин корпуса машинки.
                Vector2[] verts = physics.GetCarBodyCoordinates(i);
                // Переводим в координаты формы.
                Vector2[] vertices = new Vector2[verts.Length];
                for(int j = 0; j < verts.Length; j++)
                {
                    vertices[j] = new Vector2(verts[j].X * PixelPerMeter - maxX,
                    verts[j].Y * PixelPerMeter); // -maxY
                    vertices[j].Y = view.GetHeight - vertices[j].Y;
                }
                // Выведем корпус.
                view.ShowPolygon(vertices, colors[0], (int)(255 * fuels[i]));

                // Получим информацию о колесах.
                ObjectInfo[] wheels = physics.GetCarWheelsCoordinates(i);
                // Position.x, Position.y, radius, cos, sin
                Vector2 firstCenter = wheels[0].CircleCenter;
                firstCenter *= PixelPerMeter;
                firstCenter.X -= maxX;
                firstCenter.Y = view.GetHeight - firstCenter.Y;

                Vector2 secondCenter = wheels[1].CircleCenter;
                secondCenter *= PixelPerMeter;
                secondCenter.X -= maxX;
                secondCenter.Y = view.GetHeight - secondCenter.Y;

                float firstRadius = wheels[0].Radius * PixelPerMeter;
                float secondRadius = wheels[1].Radius * PixelPerMeter;
                float firstCos = wheels[0].CircleAngle.X;
                float firstSin = wheels[0].CircleAngle.Y;
                float secondCos = wheels[1].CircleAngle.X;
                float secondSin = wheels[1].CircleAngle.Y;

                // Выведем первое колесо.
                view.ShowCircle(firstCenter, firstRadius,
                    colors[1],
                    new Vector2(firstRadius * firstCos, firstRadius * firstSin));
                // Выведем второе колесо.
                view.ShowCircle(secondCenter, secondRadius,
                    colors[2], new Vector2(secondRadius * secondCos,
                    secondRadius * secondSin));
            }

            for(int i = (int)maxX / 3500 * 3500,
                j = 0; j < 2; ++j, i += 500 * (int)PixelPerMeter)
            {
                if(i == 0)
                    continue;
                view.ShowLine(new Vector2(i - maxX, 0),
                    new Vector2(i - maxX, 1000),
                    new Color(255, 0, 0));
            }
            Vector2[] ground = physics.GetGround();
            Vector2[] points = new Vector2[ground.Length];
            for(int i = 0; i < ground.Length; i++)
            {
                points[i] = new Vector2(ground[i].X * PixelPerMeter - maxX,
                    //view.GetHeight - (ground[i][1] * PixelPerMeter - maxY) };
                    view.GetHeight - (ground[i].Y * PixelPerMeter));
            }
            view.ShowPolygon(points, new Color(0, 0, 0), 255);
        }

        private void OnGraphPaint(object sender, EventArgs e)
        {
            if(bestResults.Count > 0)
                view.ShowGraph(bestResults, averageResults);
        }

        private double currentGenerationTime = 0;
        private double generationLifeTime = 20;
        private void OnStep(object sender, EventArgs e)
        {
            if(view.IsPaused)
                return;
            // Прибавляем время, прошедшее с прошлого шага.
            double delTime = view.Interval * simulationSpeed;
            currentGenerationTime += delTime;
            //Console.WriteLine(currentGenerationTime);
            if(currentGenerationTime < generationLifeTime)
            {
                // Производим шаг симуляции физики.
                int iterations = (int)simulationSpeed + 1;
                float time = view.Interval * simulationSpeed;
                for(int i = 0; i < iterations; i++)
                {
                    physics.Step(time / iterations);
                }
                // Двигаем машинки вперед.
                for(int i = 0; i < physics.CarsCount; i++)
                {
                    physics.GoForward(i, (float)delTime);
                }
            }
            // Если время жизни поколения истекло.
            else
            {
                // Получаем результаты машинок как координаты X центра корпуса.
                double[] carResults = new double[physics.CarsCount];
                double bestResult = -1000;
                double averageResult = 0;
                for(int i = 0; i < physics.CarsCount; i++)
                {
                    Vector2 result = physics.GetCarCenter(i);
                    carResults[i] = result.X;
                    bestResult = Math.Max(bestResult, carResults[i]);
                    averageResult += carResults[i];
                }
                averageResult /= carResults.Length;
                bestResults.Add((float)bestResult);
                averageResults.Add((float)averageResult);
                // Кладем результаты в алгоритм.
                algorithm.SetFitnessFunctionResults(carResults);
                // Генерируем новое поколение.
                algorithm.GenerateNextGeneration();
                // Удаляем все машинки старого поколения.
                physics.RemoveCars();
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
            if(algorithm != null)
                algorithm.PopulationSize = view.PopulationSize;
        }

        private void OnEliteClonesChanged(object sender, EventArgs e)
        {
            if(algorithm != null)
                algorithm.EliteClones = view.EliteClones;
        }

        private void OnCrossoverTypeChanged(object sender, EventArgs e)
        {
            if(algorithm != null)
                algorithm.SetCrossover(view.CrossoverType);
        }

        private void OnMutationRateChanged(object sender, EventArgs e)
        {
            if(algorithm != null)
                algorithm.MutationRate = (double)view.MutationRate / 100;
        }

        private void OnSimulationSpeedChanged(object sender, EventArgs e)
        {
            simulationSpeed = view.SimulationSpeed;
        }

        private void OnFuelPerSquareMeterSet(object sender, EventArgs e)
        {
            fuelPerSquareMeter = view.FuelPerSquareMeter;
        }

        private void OnSavePopulation(object sender, EventArgs e)
        {
            if(algorithm == null)
            {
                view.ShowMessage("Нет популяции для сохранения.", "");
                return;
            }
            int populationSize = algorithm.PopulationSize;
            int bytesPerIndivid = algorithm.GeneSize / 8;
            byte[][] genes = algorithm.GetPopulationInfo();
            try
            {
                view.SavePopulation(populationSize, bytesPerIndivid, genes);
            }
            catch
            {
                view.ShowMessage("Не удалось сохранить информацию о популяции.", 
                    "Ошибка сохранения в файл.");
            }
        }

        private void OnLoadPopulation(object sender, EventArgs e)
        {
            if(algorithm != null)
            {
                view.ShowMessage("Невозможно загрузить популяцию во время " +
                    "симуляции. Завершите текущюю симуляцию, а затем загрузите" +
                    "сохраненнцю популяцию.", "");
                return;
            }
            int populationSize = 0, bytesPerIndivid = 0;
            try
            {
                byte[][] genes = view.LoadPopulation(ref populationSize, ref bytesPerIndivid);
                if(bytesPerIndivid != 12)
                    throw new Exception();
                algorithm = new GeneticAlgorithm(populationSize, bytesPerIndivid * 8,
                    genes);
                SetAlgorithmSettings();
                ConvertGenesToCars();

                bestResults = new List<float>();
                averageResults = new List<float>();
                bestResults.Add(0);
                averageResults.Add(0);
            }
            catch
            {
                view.ShowMessage("Не удалось загрузить корректную информацию о популяции.", 
                    "Ошибка загрузки из файла.");
            }
        }

        private void SetAlgorithmSettings()
        {
            algorithm.EliteClones = view.EliteClones;
            algorithm.SetCrossover(view.CrossoverType);
            algorithm.MutationRate = (double)view.MutationRate / 100.0;
        }
    }
}
