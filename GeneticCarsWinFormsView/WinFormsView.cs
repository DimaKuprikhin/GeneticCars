using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticCarsPresenter;
using Microsoft.Xna.Framework;
using GeneticCarsPhysicsEngine;

namespace GeneticCarsWinFormsView
{
    public partial class WinFormsView : Form, IWinFormsView
    {
        /// <summary>
        /// Устанавливает положения и размеры всех контролеров на форме.
        /// </summary>
        public WinFormsView()
        {
            InitializeComponent();
            this.Size = new Size(1100, 700);
            this.pictureBox1.Size = new Size(800, 420);
            this.pauseMessageLabel.Location = new Point(275, 200);

            this.graphPictureBox.Size = new Size(400, 200);
            this.graphPictureBox.Location = new Point(20, 420);

            this.bestReusltEverLabel.Location = new Point(420, 420);
            this.label1.Location = new Point(420, 610);
            this.distanceLabel.Location = new Point(420, 520);
            this.populationsLabel.Location = new Point(200, 620);

            this.createGroundButton.Location = new Point(820, 10);
            this.createGroundButton.Size = new Size(240, 40);

            this.startButton.Location = new Point(820, 50);
            this.startButton.Size = new Size(240, 40);

            this.populationSizeLabel.Location = new Point(820, 105);
            this.populationSizeUpDown.Location = new Point(1000, 100);
            this.populationSizeUpDown.Size = new Size(60, 40);
            this.populationSizeUpDown.Minimum = 2;
            this.populationSizeUpDown.Maximum = 30;
            this.populationSizeUpDown.Value = 20;

            this.eliteClonesLabel.Location = new Point(820, 135);
            this.eliteClonesUpDown.Location = new Point(1000, 130);
            this.eliteClonesUpDown.Size = new Size(60, 40);
            this.eliteClonesUpDown.Minimum = 0;
            this.eliteClonesUpDown.Maximum = 20;

            this.crossoverTypeLabel.Location = new Point(820, 165);
            this.crossoverTypeComboBox.Location = new Point(960, 160);
            this.crossoverTypeComboBox.Size = new Size(100, 40);
            this.crossoverTypeComboBox.Items.Add("Одноточечное");
            this.crossoverTypeComboBox.Items.Add("Двухточечное");
            this.crossoverTypeComboBox.Items.Add("Трехточечное");
            this.crossoverTypeComboBox.Items.Add("Погенное");
            this.crossoverTypeComboBox.SelectedIndex = 0;

            this.mutationRateLabel.Location = new Point(820, 195);
            this.mutationRateComboBox.Location = new Point(960, 190);
            this.mutationRateComboBox.Size = new Size(100, 40);
            this.mutationRateComboBox.Items.Add("0%");
            this.mutationRateComboBox.Items.Add("1%");
            this.mutationRateComboBox.Items.Add("2%");
            this.mutationRateComboBox.Items.Add("3%");
            this.mutationRateComboBox.Items.Add("5%");
            this.mutationRateComboBox.Items.Add("10%");
            this.mutationRateComboBox.Items.Add("50%");
            this.mutationRateComboBox.Items.Add("100%");
            this.mutationRateComboBox.SelectedIndex = 1;

            this.generationLifeTimeLabel.Location = new Point(820, 225);
            this.setLifeTimeLabel.Location = new Point(820, 255);
            this.generationLifeTimeTextBox.Location = new Point(960, 250);
            this.setLifeTimeButton.Location = new Point(990, 249);
            this.setLifeTimeButton.Size = new Size(70, 22);
            this.generationLifeTimeTextBox.Size = new Size(30, 40);
            this.generationLifeTimeTextBox.Text = "20";

            this.currentGenerationTimeLabel.Location = new Point(820, 285);
            this.totalSimulationTimeLabel.Location = new Point(820, 315);

            //this.simulationSpeedLabel.Location = new Point(820, 345);
            //this.simulationSpeedTrackBar.Location = new Point(950, 340);
            //this.simulationSpeedTrackBar.Minimum = 1;
            //this.simulationSpeedTrackBar.Maximum = 5;

            this.fuelPerSquareMeterLabel.Location = new Point(820, 345);
            this.setFuelPerSquareMeterLabel.Location = new Point(820, 375);
            this.setFuelPerSquareMeterTextBox.Location = new Point(965, 370);
            this.setFuelPerSquareMeterTextBox.Size = new Size(30, 40);
            this.setFuelPerSquareMeterTextBox.Text = "12";
            this.setFuelPerSquareMeterButton.Location = new Point(995, 369);
            this.setFuelPerSquareMeterButton.Size = new Size(65, 22);

            this.showInfoButton.Location = new Point(820, 400);
            this.showInfoButton.Size = new Size(240, 40);

            this.hideShowButton.Location = new Point(820, 440);
            this.hideShowButton.Size = new Size(240, 40);

            this.pauseButton.Location = new Point(820, 480);
            this.pauseButton.Size = new Size(240, 40);

            this.populationSaveButton.Location = new Point(820, 520);
            this.populationSaveButton.Size = new Size(240, 40);

            this.populationLoadButton.Location = new Point(820, 560);
            this.populationLoadButton.Size = new Size(240, 40);

            this.finishSimulationButton.Location = new Point(820, 600);
            this.finishSimulationButton.Size = new Size(240, 40);
        }

        /// <summary>
        /// Возвращает ширину PictureBox для отрисовки симуляции алгоритма.
        /// </summary>
        public int GetWidth { get { return this.pictureBox1.Width; } }
        /// <summary>
        /// Возвращает высоту PictureBox для отрисовки симуляции алгоритма.
        /// </summary>
        public int GetHeight { get { return this.pictureBox1.Height; } }

        /// <summary>
        /// PaintEventArgs окна для отрисовки симуляции алгоритма.
        /// </summary>
        private PaintEventArgs paintE;
        /// <summary>
        /// Рисует круг.
        /// </summary>
        /// <param name="center"> Координата центра. </param>
        /// <param name="radius"> Радиус. </param>
        /// <param name="color"> Цвет. </param>
        /// <param name="pointOnCircle"> Координата точки на круге для 
        /// отрисовки линии, соединяющей центр и эту точку, для наглядности
        /// движения колес. </param>
        public void ShowCircle(Vector2 center, float radius, GeneticCarsPhysicsEngine.Color color,
            Vector2 pointOnCircle)
        {
            paintE.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.FromArgb(color.R,
                color.G, color.B)), center.X - radius, center.Y - radius,
                2.0f * radius, 2.0f * radius);
            paintE.Graphics.DrawLine(new Pen(System.Drawing.Color.Black), center.X, center.Y, 
                center.X + pointOnCircle.X, center.Y - pointOnCircle.Y);
        }

        /// <summary>
        /// Отрисовывает многоугольник.
        /// </summary>
        /// <param name="vertices"> Массив координат вершин. </param>
        /// <param name="color"> Цвет. </param>
        /// <param name="alpha"> Прозрачность. </param>
        public void ShowPolygon(Vector2[] vertices, GeneticCarsPhysicsEngine.Color color,
            int alpha)
        {
            Point[] points = new Point[vertices.Length];
            for(int i = 0; i < points.Length; i++)
            {
                points[i] = new Point((int)vertices[i].X, (int)vertices[i].Y);
            }
            for(int i = 0; i < points.Length; ++i)
            {
                paintE.Graphics.DrawLine(new Pen(System.Drawing.Color.Black, 2),
                    points[i].X, points[i].Y, points[(i + 1) % points.Length].X,
                    points[(i + 1) % points.Length].Y);
            }
            paintE.Graphics.FillPolygon(new SolidBrush(System.Drawing.Color.FromArgb(
                alpha, color.R, color.G, color.B)), points);
        }

        /// <summary>
        /// TextureBrush для отрисовки текстуры поверхности.
        /// </summary>
        TextureBrush tBrush = new TextureBrush(new Bitmap("grass.jpg"));
        /// <summary>
        /// Отрисовывает поверхность.
        /// </summary>
        /// <param name="vertices"> Массив координат вершин. </param>
        /// <param name="delta"> Сдвиг по горизонтали. </param>
        public void ShowGround(Vector2[] vertices, int delta)
        {
            Point[] points = new Point[vertices.Length];
            for(int i = 0; i < points.Length; i++)
            {
                points[i] = new Point((int)vertices[i].X, (int)vertices[i].Y);
            }
            for(int i = 0; i < points.Length; ++i)
            {
                paintE.Graphics.DrawLine(new Pen(System.Drawing.Color.Black, 4),
                    points[i].X, points[i].Y, points[(i + 1) % points.Length].X,
                    points[(i + 1) % points.Length].Y);
            }
            tBrush.TranslateTransform(-delta, 0);
            paintE.Graphics.FillPolygon(tBrush, points);
            tBrush.TranslateTransform(delta, 0);
        }

        /// <summary>
        /// Отрисовка линии.
        /// </summary>
        /// <param name="begin"> Координата начала. </param>
        /// <param name="end"> Координата конца. </param>
        /// <param name="color"> Цвет. </param>
        public void ShowLine(Vector2 begin, Vector2 end, GeneticCarsPhysicsEngine.Color color)
        {
            paintE.Graphics.DrawLine(new Pen(System.Drawing.Color.FromArgb(
                color.R, color.G, color.B)), begin.X, begin.Y, end.X, end.Y);
        }

        /// <summary>
        /// PaintEventArgs окна для отрисовки графиков результатов популяции.
        /// </summary>
        private PaintEventArgs graphPaintE;
        /// <summary>
        /// Отрисовка графиков.
        /// </summary>
        /// <param name="bestResults"> Массив лучших результатов. </param>
        /// <param name="averageResults"> Массив средних результатов. </param>
        public void ShowGraph(List<int> bestResults, List<int> averageResults)
        {
            for(int i = 0; i < 5; ++i)
                graphPaintE.Graphics.DrawLine(
                    new Pen(new SolidBrush(System.Drawing.Color.FromArgb(64, 0, 0, 0))),
                    0, graphPictureBox.Height * i / 4,
                    graphPictureBox.Width, graphPictureBox.Height * i / 4);

            float bestResultEver = bestResults[0];
            for(int i = 1; i < bestResults.Count; ++i)
                bestResultEver = Math.Max(bestResultEver, bestResults[i]);
            bestReusltEverLabel.Text = $"{(int)bestResultEver} метров";
            float count = bestResults.Count - 1;
            for(int i = 1; i < bestResults.Count; ++i)
            {
                graphPaintE.Graphics.DrawLine(
                    new Pen(new SolidBrush(System.Drawing.Color.Blue)),
                    graphPictureBox.Width * (i - 1) / count,
                    graphPictureBox.Height - 
                    graphPictureBox.Height * bestResults[i - 1] / bestResultEver,
                    graphPictureBox.Width * i / count,
                    graphPictureBox.Height - 
                    graphPictureBox.Height * bestResults[i] / bestResultEver);
            }
            for(int i = 1; i < averageResults.Count; ++i)
            {
                graphPaintE.Graphics.DrawLine(
                    new Pen(new SolidBrush(System.Drawing.Color.Green)),
                    graphPictureBox.Width * (i - 1) / count,
                    graphPictureBox.Height -
                    graphPictureBox.Height * averageResults[i - 1] / bestResultEver,
                    graphPictureBox.Width * i / count,
                    graphPictureBox.Height -
                    graphPictureBox.Height * averageResults[i] / bestResultEver);
            }
        }

        /// <summary>
        /// Показывает окно с сообщением.
        /// </summary>
        /// <param name="text"> Текст. </param>
        /// <param name="title"> Загаловок. </param>
        public void ShowMessage(string text, string title)
        {
            MessageBox.Show(text, title);
        }

        /// <summary>
        /// Событие отрисовки нового кадра симуляции.
        /// </summary>
        public event EventHandler<EventArgs> paint;
        /// <summary>
        /// Обработчик события PictureBox для отрисовки симуляции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            paintE = e;
            paint?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Событие отрисовки графика результатов популяции.
        /// </summary>
        public event EventHandler<EventArgs> graphPaint;
        /// <summary>
        /// Обработчик события PictureBox для отрисовки графика результатов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            graphPaintE = e;
            graphPaint?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Время в секундах с прошлого кадра.
        /// </summary>
        public float Interval { get; private set; }
        /// <summary>
        /// Событие обработки симуляции нового шага симуляции.
        /// </summary>
        public event EventHandler<EventArgs> step;
        /// <summary>
        /// Объект DateTime, хранящий время последнего обработанного шага 
        /// симуляции.
        /// </summary>
        private DateTime lastDateTime = DateTime.Now;
        /// <summary>
        /// Обработка события тика таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Interval = (float)(DateTime.Now - lastDateTime).TotalSeconds;
            lastDateTime = DateTime.Now;
            //fpsLabel.Text = $"{(1 / Interval):F2}";
            if(!IsPaused)
                step(this, EventArgs.Empty);
            Refresh();
        }

        /// <summary>
        /// Событие создания новой популяции.
        /// </summary>
        public event EventHandler<EventArgs> createPopulation;
        /// <summary>
        /// Обработка события нажатия на кнопку создания новой популяции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            createPopulation(this, EventArgs.Empty);
            IsPaused = false;
            lastDateTime = DateTime.Now;
            this.timer1.Enabled = true;
        }

        /// <summary>
        /// Событие создание новой поверхности.
        /// </summary>
        public event EventHandler<EventArgs> createGround;
        /// <summary>
        /// Обработка события нажатия на кнопку создания новой поверхности.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateGroundButton_Click(object sender, EventArgs e)
        {
            createGround?.Invoke(this, EventArgs.Empty);
            Refresh();
        }

        /// <summary>
        /// Событие завершения симуляции.
        /// </summary>
        public event EventHandler<EventArgs> finishSimulation;
        /// <summary>
        /// Обработка события нажатия на кнопку завершения симуляции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishSimulationButton_Click(object sender, EventArgs e)
        {
            finishSimulation?.Invoke(this, EventArgs.Empty);
            timer1.Enabled = false;
            SetCurrentGenerationTime(0);
            Refresh();
        }

        /// <summary>
        /// Возвращает установленный размер популяции.
        /// </summary>
        public int PopulationSize { get { return (int)populationSizeUpDown.Value; } }
        /// <summary>
        /// Событие изменения размера популяции.
        /// </summary>
        public event EventHandler<EventArgs> populationSizeChanged;
        /// <summary>
        /// Обработка события изменения пользователем размера популяции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulationSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            populationSizeChanged?.Invoke(this, EventArgs.Empty);
            eliteClonesUpDown.Value = Math.Min(EliteClones, PopulationSize);
            EliteClonesUpDown_ValueChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Возвращает количество элитных особей.
        /// </summary>
        public int EliteClones { get { return (int)eliteClonesUpDown.Value; } }
        /// <summary>
        /// Событие изменения количества элитных особей.
        /// </summary>
        public event EventHandler<EventArgs> eliteClonesChanged;
        /// <summary>
        /// Обработка события изменения пользователем количества элитных особей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EliteClonesUpDown_ValueChanged(object sender, EventArgs e)
        {
            eliteClonesUpDown.Value = Math.Min(EliteClones, PopulationSize);
            eliteClonesChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Возвращает тип скрещивания.
        /// </summary>
        public int CrossoverType
        {
            get
            {
                string item = (string)crossoverTypeComboBox.SelectedItem;
                switch(item)
                {
                    case "Одноточечное":
                        return 1;
                    case "Двухточечное":
                        return 2;
                    case "Трехточечное":
                        return 3;
                    case "Погенное":
                        return 4;
                    default:
                        return 1;
                }
            }
        }
        /// <summary>
        /// Событие изменения типа скрещивания.
        /// </summary>
        public event EventHandler<EventArgs> crossoverTypeChanged;
        /// <summary>
        /// Обработка события изменения пользователем типа скрещивания.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrossoverTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            crossoverTypeChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Возвращает вероятность мутации в процентах.
        /// </summary>
        public int MutationRate
        {
            get
            {
                string value = (string)mutationRateComboBox.SelectedItem;
                value = value.Remove(value.Length - 1, 1);
                int result = int.Parse(value);
                return result;
            }
        }
        /// <summary>
        /// Событие изменения вероятности мутации.
        /// </summary>
        public event EventHandler<EventArgs> mutationRateChanged;
        /// <summary>
        /// Обработка события изменения пользователем вероятности мутации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MutationRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mutationRateChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Возвращает время жизни поколения.
        /// </summary>
        public double GenerationLifeTime { get; private set; }
        /// <summary>
        /// Событие изменения времени жизни поколения.
        /// </summary>
        public event EventHandler<EventArgs> generationLifeTimeChanged;

        /// <summary>
        /// Обработка события нажатия на кнопку установки времени жизни 
        /// поколения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetLifeTimeButton_Click(object sender, EventArgs e)
        {
            double newValue = 0;
            if(!double.TryParse(generationLifeTimeTextBox.Text, out newValue))
            {
                generationLifeTimeTextBox.Text = "";
            }
            else
            {
                generationLifeTimeLabel.Text = $"Время жизни популяции: {newValue:F2} сек";
                GenerationLifeTime = newValue;
                generationLifeTimeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Установка текста текущего времени поколения.
        /// </summary>
        /// <param name="value"> Время в секундах. </param>
        public void SetCurrentGenerationTime(double value)
        {
            this.currentGenerationTimeLabel.Text = $"Текущее время популяции: {value:F2}";
        }

        /// <summary>
        /// Установка текста общего времени жизни популяции.
        /// </summary>
        /// <param name="value"></param>
        public void SetTotalSimulationTime(double value)
        {
            this.totalSimulationTimeLabel.Text = $"Общее время симуляции: {value:F2}";
        }

        /// <summary>
        /// Возвращает количество топлива на квадратный метр.
        /// </summary>
        public float FuelPerSquareMeter { get; private set; }
        /// <summary>
        /// Событие изменения количества топлива на квадратный метр.
        /// </summary>
        public event EventHandler<EventArgs> fuelPerSquareMeterSet;

        /// <summary>
        /// Обработка события нажатия на кнопку установки нового значения 
        /// количества топлива на квадратный метр.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFuelPerSquareMeterButton_Click(object sender, EventArgs e)
        {
            float newValue = 0;
            if(!float.TryParse(setFuelPerSquareMeterTextBox.Text, out newValue))
            {
                setFuelPerSquareMeterTextBox.Text = "";
            }
            else
            {
                fuelPerSquareMeterLabel.Text = $"Топлива на кв. м: {newValue:F2}";
                FuelPerSquareMeter = newValue;
                fuelPerSquareMeterSet?.Invoke(this, EventArgs.Empty);
            }
            
        }

        /// <summary>
        /// Событие сохранения популяции.
        /// </summary>
        public event EventHandler<EventArgs> populatioinSaveButtonClick;
        /// <summary>
        /// Обработка события нажатия на кнопку сохранения популяции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulationSaveButton_Click(object sender, EventArgs e)
        {
            populatioinSaveButtonClick?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Сохраняет популяцию в txt файл.
        /// </summary>
        /// <param name="populationSize"> Размер популяции. </param>
        /// <param name="bytesPerIndivid"> Количество байтов, кодирующих особь.
        /// </param>
        /// <param name="genes"> Массив массивов генов каждой особи. </param>
        /// <param name="averageResults"> Массив средних результатов поколений.
        /// </param>
        /// <param name="bestResults"> Массив лучщих результатов поколений. 
        /// </param>
        /// <param name="simulationTime"> Общее время жизни популяции. </param>
        public void SavePopulation(int populationSize, int bytesPerIndivid, byte[][] genes,
            List<int> averageResults, List<int> bestResults, double simulationTime)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "txt files (*.txt)|*.txt";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(dialog.FileName);
                writer.WriteLine($"{populationSize},{bytesPerIndivid}");
                for(int i = 0; i < populationSize; ++i)
                {
                    writer.WriteLine(string.Join(",", genes[i]));
                }
                writer.WriteLine(bestResults.Count);
                writer.WriteLine(string.Join(",", averageResults));
                writer.WriteLine(string.Join(",", bestResults));
                writer.WriteLine(simulationTime);
                writer.Close();
            }
        }

        /// <summary>
        /// Событие загрузки популяции.
        /// </summary>
        public event EventHandler<EventArgs> populatioinLoadButtonClick;
        /// <summary>
        /// Обработка события нажатия на кнопку загрузки популяции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulationLoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                populatioinLoadButtonClick?.Invoke(this, EventArgs.Empty);
                Refresh();
                IsPaused = false;
                timer1.Enabled = true;
            }
            catch { }
        }

        /// <summary>
        /// Загружает информацию о популяции из txt файла.
        /// </summary>
        /// <param name="populationSize"> Размер популяции. </param>
        /// <param name="bytesPerIndivid"> Количество байтов, кодирующих особь.
        /// </param>
        /// <param name="averageResults"> Массив средних результатов поколений.
        /// </param>
        /// <param name="bestResults"> Массив лучщих результатов поколений. 
        /// </param>
        /// <param name="simulationTime"> Общее время жизни популяции. </param>
        /// <returns></returns>
        public byte[][] LoadPopulation(ref int populationSize, ref int bytesPerIndivid,
            ref List<int> averageResults, ref List<int> bestResults, 
            ref double simulationTime)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "txt files (*.txt)|*.txt";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(dialog.FileName);
                string[] firstLine = reader.ReadLine().Split(',');
                populationSize = int.Parse(firstLine[0]);
                bytesPerIndivid = int.Parse(firstLine[1]);
                if(populationSize < 1 || bytesPerIndivid < 1)
                    throw new Exception();
                populationSizeUpDown.Value = populationSize;
                byte[][] genes = new byte[populationSize][];
                for(int i = 0; i < populationSize; ++i)
                {
                    genes[i] = new byte[bytesPerIndivid];
                    string[] nextGenes = reader.ReadLine().Split(',');
                    if(nextGenes.Length != bytesPerIndivid)
                        throw new Exception();
                    for(int j = 0; j < bytesPerIndivid; ++j)
                    {
                        genes[i][j] = byte.Parse(nextGenes[j]);
                    }
                }
                int resultsSize = int.Parse(reader.ReadLine());
                string[] results = reader.ReadLine().Split(',');
                if(resultsSize != results.Length)
                    throw new Exception();
                for(int i = 0; i < results.Length; ++i)
                    averageResults.Add(int.Parse(results[i]));
                results = reader.ReadLine().Split(',');
                if(resultsSize != results.Length)
                    throw new Exception();
                for(int i = 0; i < results.Length; ++i)
                    bestResults.Add(int.Parse(results[i]));
                simulationTime = double.Parse(reader.ReadLine());
                reader.Close();
                return genes;
            }
            throw new Exception();
        }

        /// <summary>
        /// Возвращает true, если симуляции приостановлена, иначе false.
        /// </summary>
        public bool IsPaused { get; private set; } = true;
        /// <summary>
        /// Обработка события нажатия на кнопку паузы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseButton_Click(object sender, EventArgs e)
        {
            IsPaused = !IsPaused;
        }

        /// <summary>
        /// Возвращает true, если скрыта отрисовка симуляции, иначе false.
        /// </summary>
        public bool IsHide { get; private set; } = false;
        /// <summary>
        /// Обработка события нажатия на кнопку скрытия отрисовки сумуляции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideShowButton_Click(object sender, EventArgs e)
        {
            IsHide = !IsHide;
            Refresh();
        }

        /// <summary>
        /// Показывает сообщение пользователю.
        /// </summary>
        /// <param name="text"> Текст. </param>
        public void ShowPauseMessage(string text)
        {
            pauseMessageLabel.Text = text;
        }

        /// <summary>
        /// Показывает справку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowInfoButton_Click(object sender, EventArgs e)
        {
            ShowMessage(
                "Программа предназначена для воспроизведения процесса симуляции " + 
                "работы генетического алгоритма." + Environment.NewLine + 
                Environment.NewLine +
                "Программа производит поиск оптимальных параметров «машинки» " +
                "для езды по конкретной трассе. Поиск производится на основе " +
                "генетического алгоритма. Программа визуализирует процесс поиска " +
                "оптимального решения и позволяет задавать различные параметры " +
                "генетического алгоритма, влияющие на такой поиск." + 
                Environment.NewLine + Environment.NewLine + 
                "Перед началом симуляции, Вы можете сгенерировать случайную трассу, " +
                "на которой будет производиться симуляция." + 
                Environment.NewLine + Environment.NewLine + 
                "Нажатие на кнопку \"Создать популяцию\" сгенерирует случайную " +
                "первое поколение и начнет симуляцию." + 
                Environment.NewLine + Environment.NewLine + 
                "Во время симуляции у Вас есть возможность настройки количества " +
                "особей в популяции, количества элитных клонов, то есть количество " +
                "лучших особей, которые без изменений перейдут в следующее поколение." +
                Environment.NewLine + Environment.NewLine + 
                "Общее время популяции отображает суммарное количество времени жизни " +
                "всех поколений этой популяции." + 
                Environment.NewLine + Environment.NewLine + 
                "Количество топлива на квадратный метр задает количество топлива, " +
                "хранящееся в единице площади корпуса машинки. Топливо расходуется " +
                "при езде, и, когда топливо кончается, колеса машинки останавливаются. " +
                "Прозрачность корпуса машинки отражает количество оставшегося топлива. " +
                "Через каждое фиксированое расстояние на карте есть \"заправки\" " +
                "(красные вертикальные линии), проезжая через которые, машинка " +
                "полностью пополняет свой запас топлива." + 
                Environment.NewLine + Environment.NewLine + 
                "Синим на графике показаны результаты лучших особей на каждом поколении, " +
                "а зеленым - средний резельтат особей поколения." + 
                Environment.NewLine + Environment.NewLine + 
                "При сохранении сохраняются текущие особи популяции, общее время " +
                "существования этой популяции и результаты каждого поколения.",
                "Справка");
        }
    }
}