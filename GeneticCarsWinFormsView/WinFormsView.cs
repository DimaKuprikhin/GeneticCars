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
        public WinFormsView()
        {
            InitializeComponent();
            this.Size = new Size(1000, 700);
            this.pictureBox1.Size = new Size(800, 400);

            this.graphPictureBox.Size = new Size(400, 200);
            this.graphPictureBox.Location = new Point(20, 420);

            this.bestReusltEverLabel.Location = new Point(420, 420);
            this.label1.Location = new Point(420, 610);

            this.createGroundButton.Location = new Point(800, 10);
            this.createGroundButton.Size = new Size(180, 40);

            this.startButton.Location = new Point(800, 60);
            this.startButton.Size = new Size(180, 40);

            this.populationSizeLabel.Location = new Point(800, 115);
            this.populationSizeUpDown.Location = new Point(920, 110);
            this.populationSizeUpDown.Size = new Size(60, 40);
            this.populationSizeUpDown.Minimum = 2;
            this.populationSizeUpDown.Maximum = 50;
            this.populationSizeUpDown.Value = 50;

            this.eliteClonesLabel.Location = new Point(800, 145);
            this.eliteClonesUpDown.Location = new Point(920, 140);
            this.eliteClonesUpDown.Size = new Size(60, 40);
            this.eliteClonesUpDown.Minimum = 0;
            this.eliteClonesUpDown.Maximum = 10;

            this.crossoverTypeLabel.Location = new Point(800, 175);
            this.crossoverTypeComboBox.Location = new Point(880, 170);
            this.crossoverTypeComboBox.Size = new Size(100, 40);
            this.crossoverTypeComboBox.Items.Add("Single-point");
            this.crossoverTypeComboBox.Items.Add("Two-point");
            this.crossoverTypeComboBox.Items.Add("Three-point");
            this.crossoverTypeComboBox.Items.Add("Uniform");
            this.crossoverTypeComboBox.SelectedIndex = 0;

            this.mutationRateLabel.Location = new Point(800, 205);
            this.mutationRateComboBox.Location = new Point(880, 200);
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

            this.generationLifeTimeLabel.Location = new Point(800, 235);
            this.setLifeTimeLabel.Location = new Point(800, 265);
            this.generationLifeTimeTextBox.Location = new Point(870, 260);
            this.setLifeTimeButton.Location = new Point(930, 260);
            this.setLifeTimeButton.Size = new Size(40, 20);
            this.generationLifeTimeTextBox.Size = new Size(50, 40);
            this.generationLifeTimeTextBox.Text = "20";

            this.currentGenerationTimeLabel.Location = new Point(800, 295);

            this.simulationSpeedLabel.Location = new Point(800, 325);
            this.simulationSpeedComboBox.Location = new Point(900, 320);
            for(int i = 0; i < 5; i++)
            {
                simulationSpeedComboBox.Items.Add((1.0 + (double)i / 4).ToString());
            }
            simulationSpeedComboBox.Items.Add("5");

            this.fuelPerSquareMeterLabel.Location = new Point(800, 355);
            this.setFuelPerSquareMeterLabel.Location = new Point(800, 385);
            this.setFuelPerSquareMeterTextBox.Location = new Point(890, 380);
            this.setFuelPerSquareMeterTextBox.Size = new Size(40, 40);
            this.setFuelPerSquareMeterButton.Location = new Point(930, 380);
            this.setFuelPerSquareMeterButton.Size = new Size(40, 20);

            this.hideShowButton.Location = new Point(600, 420);
            this.hideShowButton.Size = new Size(180, 40);

            this.pauseButton.Location = new Point(800, 420);
            this.pauseButton.Size = new Size(180, 40);

            this.populationSaveButton.Location = new Point(600, 470);
            this.populationSaveButton.Size = new Size(180, 40);

            this.populationLoadButton.Location = new Point(800, 470);
            this.populationLoadButton.Size = new Size(180, 40);

            this.finishSimulationButton.Location = new Point(600, 520);
            this.finishSimulationButton.Size = new Size(180, 40);
        }

        public int GetWidth { get { return this.pictureBox1.Width; } }
        public int GetHeight { get { return this.pictureBox1.Height; } }

        private PaintEventArgs paintE;
        public void ShowCircle(Vector2 center, float radius, GeneticCarsPhysicsEngine.Color color,
            Vector2 pointOnCircle)
        {
            paintE.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.FromArgb(color.R,
                color.G, color.B)), center.X - radius, center.Y - radius,
                2.0f * radius, 2.0f * radius);
            paintE.Graphics.DrawLine(new Pen(System.Drawing.Color.Black), center.X, center.Y, 
                center.X + pointOnCircle.X, center.Y - pointOnCircle.Y);
        }

        public void ShowPolygon(Vector2[] vertices, GeneticCarsPhysicsEngine.Color color,
            int alpha)
        {
            Point[] points = new Point[vertices.GetLength(0)];
            for(int i = 0; i < points.Length; i++) {
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

        public void ShowLine(Vector2 begin, Vector2 end, GeneticCarsPhysicsEngine.Color color)
        {
            paintE.Graphics.DrawLine(new Pen(System.Drawing.Color.FromArgb(
                color.R, color.G, color.B)), begin.X, begin.Y, end.X, end.Y);
        }

        private PaintEventArgs graphPaintE;
        public void ShowGraph(List<float> bestResults, List<float> averageResults)
        {
            float bestResultEver = bestResults[0];
            for(int i = 1; i < bestResults.Count; ++i)
                bestResultEver = Math.Max(bestResultEver, bestResults[i]);
            bestReusltEverLabel.Text = $"{(int)bestResultEver} meters";
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

        public void ShowMessage(string text, string title)
        {
            MessageBox.Show(text, title);
        }

        public event EventHandler<EventArgs> paint;
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            paintE = e;
            paint?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> graphPaint;
        private void GraphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            graphPaintE = e;
            graphPaint?.Invoke(this, EventArgs.Empty);
        }


        public float Interval { get; private set; }
        public event EventHandler<EventArgs> step;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Interval = (float)timer1.Interval / 1000.0f;
            step(this, EventArgs.Empty);
            Refresh();
        }

        public event EventHandler<EventArgs> createPopulation;
        private void StartButton_Click(object sender, EventArgs e)
        {
            createPopulation(this, EventArgs.Empty);
            IsPaused = false;
            this.timer1.Enabled = true;
        }

        public event EventHandler<EventArgs> createGround;
        private void CreateGroundButton_Click(object sender, EventArgs e)
        {
            createGround?.Invoke(this, EventArgs.Empty);
            Refresh();
        }

        public event EventHandler<EventArgs> finishSimulation;
        private void FinishSimulationButton_Click(object sender, EventArgs e)
        {
            finishSimulation?.Invoke(this, EventArgs.Empty);
            timer1.Enabled = false;
            SetCurrentGenerationTime(0);
            Refresh();
        }

        public int PopulationSize { get { return (int)populationSizeUpDown.Value; } }
        public event EventHandler<EventArgs> populationSizeChanged;
        private void PopulationSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            populationSizeChanged?.Invoke(this, EventArgs.Empty);
            eliteClonesUpDown.Value = Math.Min(EliteClones, PopulationSize);
            EliteClonesUpDown_ValueChanged(this, EventArgs.Empty);
        }

        public int EliteClones { get { return (int)eliteClonesUpDown.Value; } }
        public event EventHandler<EventArgs> eliteClonesChanged;
        private void EliteClonesUpDown_ValueChanged(object sender, EventArgs e)
        {
            eliteClonesUpDown.Value = Math.Min(EliteClones, PopulationSize);
            eliteClonesChanged?.Invoke(this, EventArgs.Empty);
        }

        public int CrossoverType
        {
            get
            {
                string item = (string)crossoverTypeComboBox.SelectedItem;
                switch(item)
                {
                    case "Single-point":
                        return 1;
                    case "Two-point":
                        return 2;
                    case "Three-point":
                        return 3;
                    case "Uniform":
                        return 4;
                    default:
                        return 1;
                }
            }
        }
        public event EventHandler<EventArgs> crossoverTypeChanged;
        private void CrossoverTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            crossoverTypeChanged?.Invoke(this, EventArgs.Empty);
        }

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
        public event EventHandler<EventArgs> mutationRateChanged;
        private void MutationRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mutationRateChanged?.Invoke(this, EventArgs.Empty);
        }

        public double GenerationLifeTime { get; private set; }
        public event EventHandler<EventArgs> generationLifeTimeChanged;
        private void GenerationLifeTimeTextBox_TextChanged(object sender, EventArgs e) { }

        private void SetLifeTimeButton_Click(object sender, EventArgs e)
        {
            double newValue = 0;
            if(!double.TryParse(generationLifeTimeTextBox.Text, out newValue))
            {
                generationLifeTimeTextBox.Text = "";
            }
            else
            {
                generationLifeTimeLabel.Text = $"Generation life time: {newValue:F2} sec";
                GenerationLifeTime = newValue;
                generationLifeTimeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SetCurrentGenerationTime(double value)
        {
            this.currentGenerationTimeLabel.Text = $"Current generation time: {value:F2}";
        }

        public float SimulationSpeed
        {
            get { return float.Parse((string)simulationSpeedComboBox.SelectedItem); }
        }
        public event EventHandler<EventArgs> simulationSpeedChanged;
        private void SimulationSpeedComboBox_SelectedIndexChanged(object sender,
            EventArgs e)
        {
            simulationSpeedChanged(this, EventArgs.Empty);
        }

        public float FuelPerSquareMeter { get; private set; }
        public event EventHandler<EventArgs> fuelPerSquareMeterSet;

        private void SetFuelPerSquareMeterButton_Click(object sender, EventArgs e)
        {
            float newValue = 0;
            if(!float.TryParse(setFuelPerSquareMeterTextBox.Text, out newValue))
            {
                setFuelPerSquareMeterTextBox.Text = "";
            }
            else
            {
                fuelPerSquareMeterLabel.Text = $"Fuel per square meter: {newValue:F2}";
                FuelPerSquareMeter = newValue;
                fuelPerSquareMeterSet?.Invoke(this, EventArgs.Empty);
            }
            
        }

        public event EventHandler<EventArgs> populatioinSaveButtonClick;
        private void PopulationSaveButton_Click(object sender, EventArgs e)
        {
            populatioinSaveButtonClick?.Invoke(this, EventArgs.Empty);
        }

        public void SavePopulation(int populationSize, int bytesPerIndivid, byte[][] genes)
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
                writer.Close();
            }
        }

        public event EventHandler<EventArgs> populatioinLoadButtonClick;
        private void PopulationLoadButton_Click(object sender, EventArgs e)
        {
            populatioinLoadButtonClick?.Invoke(this, EventArgs.Empty);
            Refresh();
            IsPaused = false;
            timer1.Enabled = true;
        }

        public byte[][] LoadPopulation(ref int populationSize, ref int bytesPerIndivid)
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
                reader.Close();
                return genes;
            }
            throw new Exception();
        }

        public bool IsPaused { get; private set; } = true;
        private void PauseButton_Click(object sender, EventArgs e)
        {
            IsPaused = !IsPaused;
        }

        public bool IsHide { get; private set; } = false;
        private void HideShowButton_Click(object sender, EventArgs e)
        {
            IsHide = !IsHide;
            Refresh();
        }
    }
}
