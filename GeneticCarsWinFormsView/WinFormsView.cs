using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticCarsPresenter;

namespace GeneticCarsWinFormsView
{
    public partial class WinFormsView : Form, IWinFormsView
    {
        public WinFormsView()
        {
            InitializeComponent();
            this.Size = new Size(1000, 560);
            this.pictureBox1.Size = new Size(800, 400);

            this.startButton.Location = new Point(800, 10);
            this.startButton.Size = new Size(180, 40);

            this.populationSizeLabel.Location = new Point(800, 65);
            this.populationSizeUpDown.Location = new Point(920, 60);
            this.populationSizeUpDown.Size = new Size(60, 40);
            this.populationSizeUpDown.Minimum = 2;
            this.populationSizeUpDown.Maximum = 20;
            this.populationSizeUpDown.Value = 4;

            this.eliteClonesLabel.Location = new Point(800, 95);
            this.eliteClonesUpDown.Location = new Point(920, 90);
            this.eliteClonesUpDown.Size = new Size(60, 40);
            this.eliteClonesUpDown.Minimum = 0;
            this.eliteClonesUpDown.Maximum = 10;

            this.crossoverTypeLabel.Location = new Point(800, 125);
            this.crossoverTypeComboBox.Location = new Point(880, 120);
            this.crossoverTypeComboBox.Size = new Size(100, 40);
            this.crossoverTypeComboBox.Items.Add("Single-point");
            this.crossoverTypeComboBox.Items.Add("Two-point");
            this.crossoverTypeComboBox.Items.Add("Three-point");
            this.crossoverTypeComboBox.Items.Add("Uniform");
            this.crossoverTypeComboBox.SelectedIndex = 0;

            this.mutationRateLabel.Location = new Point(800, 155);
            this.mutationRateComboBox.Location = new Point(880, 150);
            this.mutationRateComboBox.Size = new Size(100, 40);
            this.mutationRateComboBox.Items.Add("0%");
            this.mutationRateComboBox.Items.Add("1%");
            this.mutationRateComboBox.Items.Add("5%");
            this.mutationRateComboBox.Items.Add("10%");
            this.mutationRateComboBox.Items.Add("50%");
            this.mutationRateComboBox.Items.Add("100%");
            this.mutationRateComboBox.SelectedIndex = 1;

            this.generationLifeTimeLabel.Location = new Point(800, 185);
            this.generationLifeTimeTextBox.Location = new Point(930, 180);
            this.generationLifeTimeTextBox.Size = new Size(50, 40);
            this.generationLifeTimeTextBox.Text = "20";

            this.currentGenerationTimeLabel.Location = new Point(800, 215);

            this.simulationSpeedLabel.Location = new Point(800, 245);
            this.simulationSpeedComboBox.Location = new Point(900, 240);
            for(int i = 0; i < 5; i++)
            {
                simulationSpeedComboBox.Items.Add((1.0 + (double)i / 4).ToString());
            }
            simulationSpeedComboBox.Items.Add("5");
        }

        public int GetWidth { get { return this.pictureBox1.Width; } }
        public int GetHeight { get { return this.pictureBox1.Height; } }

        private PaintEventArgs paintE;
        public void ShowCircle(float x, float y, float radius, int[] color,
            float onCircleX, float onCircleY)
        {
            paintE.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(color[0],
                color[1], color[2])), x - radius, y - radius, 2.0f * radius, 2.0f * radius);
            paintE.Graphics.DrawLine(new Pen(Color.Black), x, y, 
                x + onCircleX, y - onCircleY);
        }

        public void ShowPolygon(float[][] vertices, int[] color)
        {
            Point[] points = new Point[vertices.GetLength(0)];
            for(int i = 0; i < points.Length; i++) {
                points[i] = new Point((int)vertices[i][0], (int)vertices[i][1]);
            }
            paintE.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(color[0],
                color[1], color[2])), points);
        }

        public event EventHandler<EventArgs> paint;
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            paintE = e;
            paint?.Invoke(this, EventArgs.Empty);
        }

        public float Interval { get; private set; }
        public event EventHandler<EventArgs> step;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Interval = (float)timer1.Interval / 1000.0f;
            step(this, EventArgs.Empty);
            Refresh();
        }

        public event EventHandler<EventArgs> start;
        private void StartButton_Click(object sender, EventArgs e)
        {
            start(this, EventArgs.Empty);
            this.timer1.Enabled = true;
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

        private string previousText = "";
        public double GenerationLifeTime { get; private set; }
        public event EventHandler<EventArgs> generationLifeTimeChanged;
        private void GenerationLifeTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            double newValue = 0;
            if(!double.TryParse(generationLifeTimeTextBox.Text, out newValue))
            {
                generationLifeTimeTextBox.Text = previousText;
            }
            else
            {
                previousText = generationLifeTimeTextBox.Text;
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
    }
}
