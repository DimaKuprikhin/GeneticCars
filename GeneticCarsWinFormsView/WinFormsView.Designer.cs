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
            this.graphPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bestReusltEverLabel = new System.Windows.Forms.Label();
            this.setLifeTimeLabel = new System.Windows.Forms.Label();
            this.setLifeTimeButton = new System.Windows.Forms.Button();
            this.fuelPerSquareMeterLabel = new System.Windows.Forms.Label();
            this.setFuelPerSquareMeterLabel = new System.Windows.Forms.Label();
            this.setFuelPerSquareMeterTextBox = new System.Windows.Forms.TextBox();
            this.setFuelPerSquareMeterButton = new System.Windows.Forms.Button();
            this.populationSaveButton = new System.Windows.Forms.Button();
            this.populationLoadButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.createGroundButton = new System.Windows.Forms.Button();
            this.hideShowButton = new System.Windows.Forms.Button();
            this.finishSimulationButton = new System.Windows.Forms.Button();
            this.fpsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eliteClonesUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).BeginInit();
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
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(803, 46);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(180, 40);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Создать популяцию";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // populationSizeLabel
            // 
            this.populationSizeLabel.AutoSize = true;
            this.populationSizeLabel.Location = new System.Drawing.Point(806, 111);
            this.populationSizeLabel.Name = "populationSizeLabel";
            this.populationSizeLabel.Size = new System.Drawing.Size(132, 17);
            this.populationSizeLabel.TabIndex = 3;
            this.populationSizeLabel.Text = "Размер популяции";
            // 
            // eliteClonesLabel
            // 
            this.eliteClonesLabel.AutoSize = true;
            this.eliteClonesLabel.Location = new System.Drawing.Point(806, 143);
            this.eliteClonesLabel.Name = "eliteClonesLabel";
            this.eliteClonesLabel.Size = new System.Drawing.Size(111, 17);
            this.eliteClonesLabel.TabIndex = 4;
            this.eliteClonesLabel.Text = "Элитные клоны";
            // 
            // populationSizeUpDown
            // 
            this.populationSizeUpDown.Location = new System.Drawing.Point(936, 109);
            this.populationSizeUpDown.Name = "populationSizeUpDown";
            this.populationSizeUpDown.Size = new System.Drawing.Size(44, 22);
            this.populationSizeUpDown.TabIndex = 6;
            this.populationSizeUpDown.ValueChanged += new System.EventHandler(this.PopulationSizeUpDown_ValueChanged);
            // 
            // eliteClonesUpDown
            // 
            this.eliteClonesUpDown.Location = new System.Drawing.Point(916, 138);
            this.eliteClonesUpDown.Name = "eliteClonesUpDown";
            this.eliteClonesUpDown.Size = new System.Drawing.Size(60, 22);
            this.eliteClonesUpDown.TabIndex = 7;
            this.eliteClonesUpDown.ValueChanged += new System.EventHandler(this.EliteClonesUpDown_ValueChanged);
            // 
            // crossoverTypeLabel
            // 
            this.crossoverTypeLabel.AutoSize = true;
            this.crossoverTypeLabel.Location = new System.Drawing.Point(802, 176);
            this.crossoverTypeLabel.Name = "crossoverTypeLabel";
            this.crossoverTypeLabel.Size = new System.Drawing.Size(125, 17);
            this.crossoverTypeLabel.TabIndex = 8;
            this.crossoverTypeLabel.Text = "Тип скрещивания";
            // 
            // crossoverTypeComboBox
            // 
            this.crossoverTypeComboBox.FormattingEnabled = true;
            this.crossoverTypeComboBox.Location = new System.Drawing.Point(916, 173);
            this.crossoverTypeComboBox.Name = "crossoverTypeComboBox";
            this.crossoverTypeComboBox.Size = new System.Drawing.Size(64, 24);
            this.crossoverTypeComboBox.TabIndex = 10;
            this.crossoverTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.CrossoverTypeComboBox_SelectedIndexChanged);
            // 
            // mutationRateLabel
            // 
            this.mutationRateLabel.AutoSize = true;
            this.mutationRateLabel.Location = new System.Drawing.Point(802, 199);
            this.mutationRateLabel.Name = "mutationRateLabel";
            this.mutationRateLabel.Size = new System.Drawing.Size(152, 17);
            this.mutationRateLabel.TabIndex = 11;
            this.mutationRateLabel.Text = "Вероятность мутации";
            // 
            // mutationRateComboBox
            // 
            this.mutationRateComboBox.FormattingEnabled = true;
            this.mutationRateComboBox.Location = new System.Drawing.Point(936, 196);
            this.mutationRateComboBox.Name = "mutationRateComboBox";
            this.mutationRateComboBox.Size = new System.Drawing.Size(50, 24);
            this.mutationRateComboBox.TabIndex = 12;
            this.mutationRateComboBox.SelectedIndexChanged += new System.EventHandler(this.MutationRateComboBox_SelectedIndexChanged);
            // 
            // generationLifeTimeTextBox
            // 
            this.generationLifeTimeTextBox.Location = new System.Drawing.Point(879, 266);
            this.generationLifeTimeTextBox.Name = "generationLifeTimeTextBox";
            this.generationLifeTimeTextBox.Size = new System.Drawing.Size(30, 22);
            this.generationLifeTimeTextBox.TabIndex = 13;
            this.generationLifeTimeTextBox.TextChanged += new System.EventHandler(this.GenerationLifeTimeTextBox_TextChanged);
            // 
            // generationLifeTimeLabel
            // 
            this.generationLifeTimeLabel.AutoSize = true;
            this.generationLifeTimeLabel.Location = new System.Drawing.Point(802, 232);
            this.generationLifeTimeLabel.Name = "generationLifeTimeLabel";
            this.generationLifeTimeLabel.Size = new System.Drawing.Size(219, 17);
            this.generationLifeTimeLabel.TabIndex = 14;
            this.generationLifeTimeLabel.Text = "Время жизни популяции: 20 сек";
            // 
            // currentGenerationTimeLabel
            // 
            this.currentGenerationTimeLabel.AutoSize = true;
            this.currentGenerationTimeLabel.Location = new System.Drawing.Point(794, 295);
            this.currentGenerationTimeLabel.Name = "currentGenerationTimeLabel";
            this.currentGenerationTimeLabel.Size = new System.Drawing.Size(213, 17);
            this.currentGenerationTimeLabel.TabIndex = 15;
            this.currentGenerationTimeLabel.Text = "Текущее время популяции: 0.0";
            // 
            // simulationSpeedLabel
            // 
            this.simulationSpeedLabel.AutoSize = true;
            this.simulationSpeedLabel.Location = new System.Drawing.Point(793, 335);
            this.simulationSpeedLabel.Name = "simulationSpeedLabel";
            this.simulationSpeedLabel.Size = new System.Drawing.Size(144, 17);
            this.simulationSpeedLabel.TabIndex = 16;
            this.simulationSpeedLabel.Text = "Скорость симуляции";
            // 
            // simulationSpeedComboBox
            // 
            this.simulationSpeedComboBox.FormattingEnabled = true;
            this.simulationSpeedComboBox.Location = new System.Drawing.Point(923, 325);
            this.simulationSpeedComboBox.Name = "simulationSpeedComboBox";
            this.simulationSpeedComboBox.Size = new System.Drawing.Size(60, 24);
            this.simulationSpeedComboBox.TabIndex = 17;
            this.simulationSpeedComboBox.SelectedIndexChanged += new System.EventHandler(this.SimulationSpeedComboBox_SelectedIndexChanged);
            // 
            // graphPictureBox
            // 
            this.graphPictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.graphPictureBox.Location = new System.Drawing.Point(0, 406);
            this.graphPictureBox.Name = "graphPictureBox";
            this.graphPictureBox.Size = new System.Drawing.Size(411, 240);
            this.graphPictureBox.TabIndex = 18;
            this.graphPictureBox.TabStop = false;
            this.graphPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphPictureBox_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(417, 629);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "0 метров";
            // 
            // bestReusltEverLabel
            // 
            this.bestReusltEverLabel.AutoSize = true;
            this.bestReusltEverLabel.Location = new System.Drawing.Point(417, 406);
            this.bestReusltEverLabel.Name = "bestReusltEverLabel";
            this.bestReusltEverLabel.Size = new System.Drawing.Size(67, 17);
            this.bestReusltEverLabel.TabIndex = 20;
            this.bestReusltEverLabel.Text = "0 метров";
            // 
            // setLifeTimeLabel
            // 
            this.setLifeTimeLabel.AutoSize = true;
            this.setLifeTimeLabel.Location = new System.Drawing.Point(793, 266);
            this.setLifeTimeLabel.Name = "setLifeTimeLabel";
            this.setLifeTimeLabel.Size = new System.Drawing.Size(172, 17);
            this.setLifeTimeLabel.TabIndex = 21;
            this.setLifeTimeLabel.Text = "Установить время жизни";
            // 
            // setLifeTimeButton
            // 
            this.setLifeTimeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.setLifeTimeButton.Location = new System.Drawing.Point(904, 266);
            this.setLifeTimeButton.Name = "setLifeTimeButton";
            this.setLifeTimeButton.Size = new System.Drawing.Size(82, 28);
            this.setLifeTimeButton.TabIndex = 22;
            this.setLifeTimeButton.Text = "Установить";
            this.setLifeTimeButton.UseVisualStyleBackColor = true;
            this.setLifeTimeButton.Click += new System.EventHandler(this.SetLifeTimeButton_Click);
            // 
            // fuelPerSquareMeterLabel
            // 
            this.fuelPerSquareMeterLabel.AutoSize = true;
            this.fuelPerSquareMeterLabel.Location = new System.Drawing.Point(798, 352);
            this.fuelPerSquareMeterLabel.Name = "fuelPerSquareMeterLabel";
            this.fuelPerSquareMeterLabel.Size = new System.Drawing.Size(143, 17);
            this.fuelPerSquareMeterLabel.TabIndex = 23;
            this.fuelPerSquareMeterLabel.Text = "Топлива на кв. м: 12";
            // 
            // setFuelPerSquareMeterLabel
            // 
            this.setFuelPerSquareMeterLabel.AutoSize = true;
            this.setFuelPerSquareMeterLabel.Location = new System.Drawing.Point(778, 373);
            this.setFuelPerSquareMeterLabel.Name = "setFuelPerSquareMeterLabel";
            this.setFuelPerSquareMeterLabel.Size = new System.Drawing.Size(189, 17);
            this.setFuelPerSquareMeterLabel.TabIndex = 24;
            this.setFuelPerSquareMeterLabel.Text = "Установить кол-во топлива";
            // 
            // setFuelPerSquareMeterTextBox
            // 
            this.setFuelPerSquareMeterTextBox.Location = new System.Drawing.Point(900, 373);
            this.setFuelPerSquareMeterTextBox.Name = "setFuelPerSquareMeterTextBox";
            this.setFuelPerSquareMeterTextBox.Size = new System.Drawing.Size(43, 22);
            this.setFuelPerSquareMeterTextBox.TabIndex = 25;
            // 
            // setFuelPerSquareMeterButton
            // 
            this.setFuelPerSquareMeterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.setFuelPerSquareMeterButton.Location = new System.Drawing.Point(949, 372);
            this.setFuelPerSquareMeterButton.Name = "setFuelPerSquareMeterButton";
            this.setFuelPerSquareMeterButton.Size = new System.Drawing.Size(34, 23);
            this.setFuelPerSquareMeterButton.TabIndex = 26;
            this.setFuelPerSquareMeterButton.Text = "Установить";
            this.setFuelPerSquareMeterButton.UseVisualStyleBackColor = true;
            this.setFuelPerSquareMeterButton.Click += new System.EventHandler(this.SetFuelPerSquareMeterButton_Click);
            // 
            // populationSaveButton
            // 
            this.populationSaveButton.Location = new System.Drawing.Point(613, 471);
            this.populationSaveButton.Name = "populationSaveButton";
            this.populationSaveButton.Size = new System.Drawing.Size(179, 47);
            this.populationSaveButton.TabIndex = 27;
            this.populationSaveButton.Text = "Сохранить популяцию";
            this.populationSaveButton.UseVisualStyleBackColor = true;
            this.populationSaveButton.Click += new System.EventHandler(this.PopulationSaveButton_Click);
            // 
            // populationLoadButton
            // 
            this.populationLoadButton.Location = new System.Drawing.Point(796, 474);
            this.populationLoadButton.Name = "populationLoadButton";
            this.populationLoadButton.Size = new System.Drawing.Size(183, 44);
            this.populationLoadButton.TabIndex = 28;
            this.populationLoadButton.Text = "Загрузить популяцию";
            this.populationLoadButton.UseVisualStyleBackColor = true;
            this.populationLoadButton.Click += new System.EventHandler(this.PopulationLoadButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(798, 414);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(178, 40);
            this.pauseButton.TabIndex = 29;
            this.pauseButton.Text = "Пауза";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // createGroundButton
            // 
            this.createGroundButton.Location = new System.Drawing.Point(802, 0);
            this.createGroundButton.Name = "createGroundButton";
            this.createGroundButton.Size = new System.Drawing.Size(177, 40);
            this.createGroundButton.TabIndex = 30;
            this.createGroundButton.Text = "Создать поверхность";
            this.createGroundButton.UseVisualStyleBackColor = true;
            this.createGroundButton.Click += new System.EventHandler(this.CreateGroundButton_Click);
            // 
            // hideShowButton
            // 
            this.hideShowButton.Location = new System.Drawing.Point(613, 415);
            this.hideShowButton.Name = "hideShowButton";
            this.hideShowButton.Size = new System.Drawing.Size(179, 39);
            this.hideShowButton.TabIndex = 31;
            this.hideShowButton.Text = "Скрыть/Показать";
            this.hideShowButton.UseVisualStyleBackColor = true;
            this.hideShowButton.Click += new System.EventHandler(this.HideShowButton_Click);
            // 
            // finishSimulationButton
            // 
            this.finishSimulationButton.Location = new System.Drawing.Point(613, 524);
            this.finishSimulationButton.Name = "finishSimulationButton";
            this.finishSimulationButton.Size = new System.Drawing.Size(179, 46);
            this.finishSimulationButton.TabIndex = 32;
            this.finishSimulationButton.Text = "Завершить популяцию";
            this.finishSimulationButton.UseVisualStyleBackColor = true;
            this.finishSimulationButton.Click += new System.EventHandler(this.FinishSimulationButton_Click);
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Location = new System.Drawing.Point(12, 12);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(0, 17);
            this.fpsLabel.TabIndex = 33;
            // 
            // WinFormsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.fpsLabel);
            this.Controls.Add(this.finishSimulationButton);
            this.Controls.Add(this.hideShowButton);
            this.Controls.Add(this.createGroundButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.populationLoadButton);
            this.Controls.Add(this.populationSaveButton);
            this.Controls.Add(this.setFuelPerSquareMeterButton);
            this.Controls.Add(this.setFuelPerSquareMeterTextBox);
            this.Controls.Add(this.setFuelPerSquareMeterLabel);
            this.Controls.Add(this.fuelPerSquareMeterLabel);
            this.Controls.Add(this.setLifeTimeButton);
            this.Controls.Add(this.setLifeTimeLabel);
            this.Controls.Add(this.bestReusltEverLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graphPictureBox);
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
            ((System.ComponentModel.ISupportInitialize)(this.graphPictureBox)).EndInit();
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
        private System.Windows.Forms.PictureBox graphPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label bestReusltEverLabel;
        private System.Windows.Forms.Label setLifeTimeLabel;
        private System.Windows.Forms.Button setLifeTimeButton;
        private System.Windows.Forms.Label fuelPerSquareMeterLabel;
        private System.Windows.Forms.Label setFuelPerSquareMeterLabel;
        private System.Windows.Forms.TextBox setFuelPerSquareMeterTextBox;
        private System.Windows.Forms.Button setFuelPerSquareMeterButton;
        private System.Windows.Forms.Button populationSaveButton;
        private System.Windows.Forms.Button populationLoadButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button createGroundButton;
        private System.Windows.Forms.Button hideShowButton;
        private System.Windows.Forms.Button finishSimulationButton;
        private System.Windows.Forms.Label fpsLabel;
    }
}