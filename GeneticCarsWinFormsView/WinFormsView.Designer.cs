namespace GeneticCarsWinFormsView
{
    partial class WinFormsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startButton = new System.Windows.Forms.Button();
            this.populationSizeLabel = new System.Windows.Forms.Label();
            this.eliteClonesLabel = new System.Windows.Forms.Label();
            this.populationSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.eliteClonesUpDown = new System.Windows.Forms.NumericUpDown();
            this.crossoverTypeLabel = new System.Windows.Forms.Label();
            this.crossoverTypeComboBox = new System.Windows.Forms.ComboBox();
            this.mutationRateLabel = new System.Windows.Forms.Label();
            this.mutationRateComboBox = new System.Windows.Forms.ComboBox();
            this.generationLifeTimeTextBox = new System.Windows.Forms.TextBox();
            this.generationLifeTimeLabel = new System.Windows.Forms.Label();
            this.currentGenerationTimeLabel = new System.Windows.Forms.Label();
            this.simulationSpeedLabel = new System.Windows.Forms.Label();
            this.simulationSpeedComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eliteClonesUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(800, 0);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(180, 40);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Simulation";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // populationSizeLabel
            // 
            this.populationSizeLabel.AutoSize = true;
            this.populationSizeLabel.Location = new System.Drawing.Point(806, 48);
            this.populationSizeLabel.Name = "populationSizeLabel";
            this.populationSizeLabel.Size = new System.Drawing.Size(104, 17);
            this.populationSizeLabel.TabIndex = 3;
            this.populationSizeLabel.Text = "Population size";
            // 
            // eliteClonesLabel
            // 
            this.eliteClonesLabel.AutoSize = true;
            this.eliteClonesLabel.Location = new System.Drawing.Point(806, 80);
            this.eliteClonesLabel.Name = "eliteClonesLabel";
            this.eliteClonesLabel.Size = new System.Drawing.Size(80, 17);
            this.eliteClonesLabel.TabIndex = 4;
            this.eliteClonesLabel.Text = "Elite clones";
            // 
            // populationSizeUpDown
            // 
            this.populationSizeUpDown.Location = new System.Drawing.Point(920, 48);
            this.populationSizeUpDown.Name = "populationSizeUpDown";
            this.populationSizeUpDown.Size = new System.Drawing.Size(53, 22);
            this.populationSizeUpDown.TabIndex = 6;
            this.populationSizeUpDown.ValueChanged += new System.EventHandler(this.PopulationSizeUpDown_ValueChanged);
            // 
            // eliteClonesUpDown
            // 
            this.eliteClonesUpDown.Location = new System.Drawing.Point(920, 80);
            this.eliteClonesUpDown.Name = "eliteClonesUpDown";
            this.eliteClonesUpDown.Size = new System.Drawing.Size(60, 22);
            this.eliteClonesUpDown.TabIndex = 7;
            this.eliteClonesUpDown.ValueChanged += new System.EventHandler(this.EliteClonesUpDown_ValueChanged);
            // 
            // crossoverTypeLabel
            // 
            this.crossoverTypeLabel.AutoSize = true;
            this.crossoverTypeLabel.Location = new System.Drawing.Point(807, 116);
            this.crossoverTypeLabel.Name = "crossoverTypeLabel";
            this.crossoverTypeLabel.Size = new System.Drawing.Size(103, 17);
            this.crossoverTypeLabel.TabIndex = 8;
            this.crossoverTypeLabel.Text = "Crossover type";
            // 
            // crossoverTypeComboBox
            // 
            this.crossoverTypeComboBox.FormattingEnabled = true;
            this.crossoverTypeComboBox.Location = new System.Drawing.Point(916, 116);
            this.crossoverTypeComboBox.Name = "crossoverTypeComboBox";
            this.crossoverTypeComboBox.Size = new System.Drawing.Size(64, 24);
            this.crossoverTypeComboBox.TabIndex = 10;
            this.crossoverTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.CrossoverTypeComboBox_SelectedIndexChanged);
            // 
            // mutationRateLabel
            // 
            this.mutationRateLabel.AutoSize = true;
            this.mutationRateLabel.Location = new System.Drawing.Point(806, 144);
            this.mutationRateLabel.Name = "mutationRateLabel";
            this.mutationRateLabel.Size = new System.Drawing.Size(91, 17);
            this.mutationRateLabel.TabIndex = 11;
            this.mutationRateLabel.Text = "Mutation rate";
            // 
            // mutationRateComboBox
            // 
            this.mutationRateComboBox.FormattingEnabled = true;
            this.mutationRateComboBox.Location = new System.Drawing.Point(903, 146);
            this.mutationRateComboBox.Name = "mutationRateComboBox";
            this.mutationRateComboBox.Size = new System.Drawing.Size(70, 24);
            this.mutationRateComboBox.TabIndex = 12;
            this.mutationRateComboBox.SelectedIndexChanged += new System.EventHandler(this.MutationRateComboBox_SelectedIndexChanged);
            // 
            // generationLifeTimeTextBox
            // 
            this.generationLifeTimeTextBox.Location = new System.Drawing.Point(913, 193);
            this.generationLifeTimeTextBox.Name = "generationLifeTimeTextBox";
            this.generationLifeTimeTextBox.Size = new System.Drawing.Size(67, 22);
            this.generationLifeTimeTextBox.TabIndex = 13;
            this.generationLifeTimeTextBox.TextChanged += new System.EventHandler(this.GenerationLifeTimeTextBox_TextChanged);
            // 
            // generationLifeTimeLabel
            // 
            this.generationLifeTimeLabel.AutoSize = true;
            this.generationLifeTimeLabel.Location = new System.Drawing.Point(797, 173);
            this.generationLifeTimeLabel.Name = "generationLifeTimeLabel";
            this.generationLifeTimeLabel.Size = new System.Drawing.Size(167, 17);
            this.generationLifeTimeLabel.TabIndex = 14;
            this.generationLifeTimeLabel.Text = "Generation life time (sec)";
            // 
            // currentGenerationTimeLabel
            // 
            this.currentGenerationTimeLabel.AutoSize = true;
            this.currentGenerationTimeLabel.Location = new System.Drawing.Point(806, 210);
            this.currentGenerationTimeLabel.Name = "currentGenerationTimeLabel";
            this.currentGenerationTimeLabel.Size = new System.Drawing.Size(185, 17);
            this.currentGenerationTimeLabel.TabIndex = 15;
            this.currentGenerationTimeLabel.Text = "Current generation time: 0.0";
            // 
            // simulationSpeedLabel
            // 
            this.simulationSpeedLabel.AutoSize = true;
            this.simulationSpeedLabel.Location = new System.Drawing.Point(807, 244);
            this.simulationSpeedLabel.Name = "simulationSpeedLabel";
            this.simulationSpeedLabel.Size = new System.Drawing.Size(116, 17);
            this.simulationSpeedLabel.TabIndex = 16;
            this.simulationSpeedLabel.Text = "Simulation speed";
            // 
            // simulationSpeedComboBox
            // 
            this.simulationSpeedComboBox.FormattingEnabled = true;
            this.simulationSpeedComboBox.Location = new System.Drawing.Point(920, 244);
            this.simulationSpeedComboBox.Name = "simulationSpeedComboBox";
            this.simulationSpeedComboBox.Size = new System.Drawing.Size(60, 24);
            this.simulationSpeedComboBox.TabIndex = 17;
            this.simulationSpeedComboBox.SelectedIndexChanged += new System.EventHandler(this.SimulationSpeedComboBox_SelectedIndexChanged);
            // 
            // WinFormsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 513);
            this.Controls.Add(this.simulationSpeedComboBox);
            this.Controls.Add(this.simulationSpeedLabel);
            this.Controls.Add(this.currentGenerationTimeLabel);
            this.Controls.Add(this.generationLifeTimeLabel);
            this.Controls.Add(this.generationLifeTimeTextBox);
            this.Controls.Add(this.mutationRateComboBox);
            this.Controls.Add(this.mutationRateLabel);
            this.Controls.Add(this.crossoverTypeComboBox);
            this.Controls.Add(this.crossoverTypeLabel);
            this.Controls.Add(this.eliteClonesUpDown);
            this.Controls.Add(this.populationSizeUpDown);
            this.Controls.Add(this.eliteClonesLabel);
            this.Controls.Add(this.populationSizeLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "WinFormsView";
            this.Text = "Genetic Cars";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationSizeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eliteClonesUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label populationSizeLabel;
        private System.Windows.Forms.Label eliteClonesLabel;
        private System.Windows.Forms.NumericUpDown populationSizeUpDown;
        private System.Windows.Forms.NumericUpDown eliteClonesUpDown;
        private System.Windows.Forms.Label crossoverTypeLabel;
        private System.Windows.Forms.ComboBox crossoverTypeComboBox;
        private System.Windows.Forms.Label mutationRateLabel;
        private System.Windows.Forms.ComboBox mutationRateComboBox;
        private System.Windows.Forms.TextBox generationLifeTimeTextBox;
        private System.Windows.Forms.Label generationLifeTimeLabel;
        private System.Windows.Forms.Label currentGenerationTimeLabel;
        private System.Windows.Forms.Label simulationSpeedLabel;
        private System.Windows.Forms.ComboBox simulationSpeedComboBox;
    }
}