namespace PomiarWilgotnosci
{
    partial class Form1
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
            if (disposing && (components != null))
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
            System.Windows.Forms.Label label154;
            System.Windows.Forms.Label label155;
            System.Windows.Forms.Label label153;
            System.Windows.Forms.Label label150;
            System.Windows.Forms.Label label151;
            System.Windows.Forms.Label label152;
            System.Windows.Forms.Label label1;
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRLCConnect = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label148 = new System.Windows.Forms.Label();
            this.btnDoOneMeasurement = new System.Windows.Forms.Button();
            this.btnSendParametersToRLC = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.edtAverage = new System.Windows.Forms.NumericUpDown();
            this.edtTriggerDelay = new System.Windows.Forms.NumericUpDown();
            this.edtMeasurementFrequency = new System.Windows.Forms.NumericUpDown();
            this.edtVoltage = new System.Windows.Forms.NumericUpDown();
            this.tbRLCAddress = new System.Windows.Forms.TextBox();
            label154 = new System.Windows.Forms.Label();
            label155 = new System.Windows.Forms.Label();
            label153 = new System.Windows.Forms.Label();
            label150 = new System.Windows.Forms.Label();
            label151 = new System.Windows.Forms.Label();
            label152 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTriggerDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMeasurementFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtVoltage)).BeginInit();
            this.SuspendLayout();
            // 
            // label154
            // 
            label154.AutoSize = true;
            label154.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            label154.Location = new System.Drawing.Point(14, 17);
            label154.Name = "label154";
            label154.Size = new System.Drawing.Size(394, 23);
            label154.TabIndex = 0;
            label154.Text = "Mostek pomiarowy RLC (Agilent E4980)";
            // 
            // label155
            // 
            label155.AutoSize = true;
            label155.Location = new System.Drawing.Point(413, 6);
            label155.Name = "label155";
            label155.Size = new System.Drawing.Size(108, 13);
            label155.TabIndex = 435;
            label155.Text = "Usrednienie [probek]:";
            // 
            // label153
            // 
            label153.AutoSize = true;
            label153.Location = new System.Drawing.Point(8, 8);
            label153.Name = "label153";
            label153.Size = new System.Drawing.Size(65, 13);
            label153.TabIndex = 434;
            label153.Text = "Impedancja:";
            // 
            // label150
            // 
            label150.AutoSize = true;
            label150.Location = new System.Drawing.Point(6, 6);
            label150.Name = "label150";
            label150.Size = new System.Drawing.Size(122, 13);
            label150.TabIndex = 423;
            label150.Text = "Napięcie pomiarowe [V]:";
            // 
            // label151
            // 
            label151.AutoSize = true;
            label151.Location = new System.Drawing.Point(134, 6);
            label151.Name = "label151";
            label151.Size = new System.Drawing.Size(150, 13);
            label151.TabIndex = 424;
            label151.Text = "Częstotliwość pomiarowa [Hz]:";
            // 
            // label152
            // 
            label152.AutoSize = true;
            label152.Location = new System.Drawing.Point(290, 6);
            label152.Name = "label152";
            label152.Size = new System.Drawing.Size(117, 13);
            label152.TabIndex = 427;
            label152.Text = "Opóźnienie pomiaru [s]:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(72, 230);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(102, 13);
            label1.TabIndex = 441;
            label1.Text = "Interwał pomiarowy:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.YellowGreen;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(label154);
            this.panel3.Controls.Add(this.btnRLCConnect);
            this.panel3.Controls.Add(this.tbRLCAddress);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(654, 100);
            this.panel3.TabIndex = 437;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(48, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 461;
            this.label4.Text = "Adres/nazwa hosta:";
            // 
            // btnRLCConnect
            // 
            this.btnRLCConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnRLCConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnRLCConnect.Location = new System.Drawing.Point(531, 19);
            this.btnRLCConnect.Name = "btnRLCConnect";
            this.btnRLCConnect.Size = new System.Drawing.Size(116, 25);
            this.btnRLCConnect.TabIndex = 431;
            this.btnRLCConnect.Text = "Połącz (1)";
            this.btnRLCConnect.UseVisualStyleBackColor = true;
            this.btnRLCConnect.Click += new System.EventHandler(this.btnRLCConnect_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.edtAverage);
            this.panel4.Controls.Add(label155);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.btnSendParametersToRLC);
            this.panel4.Controls.Add(label150);
            this.panel4.Controls.Add(this.edtTriggerDelay);
            this.panel4.Controls.Add(label151);
            this.panel4.Controls.Add(this.edtMeasurementFrequency);
            this.panel4.Controls.Add(label152);
            this.panel4.Controls.Add(this.edtVoltage);
            this.panel4.Location = new System.Drawing.Point(12, 118);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(654, 85);
            this.panel4.TabIndex = 438;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.Controls.Add(label153);
            this.panel5.Controls.Add(this.label148);
            this.panel5.Controls.Add(this.btnDoOneMeasurement);
            this.panel5.Location = new System.Drawing.Point(9, 47);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(638, 30);
            this.panel5.TabIndex = 434;
            // 
            // label148
            // 
            this.label148.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label148.Location = new System.Drawing.Point(79, 8);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(433, 13);
            this.label148.TabIndex = 439;
            this.label148.Text = "overload";
            // 
            // btnDoOneMeasurement
            // 
            this.btnDoOneMeasurement.Location = new System.Drawing.Point(530, 4);
            this.btnDoOneMeasurement.Name = "btnDoOneMeasurement";
            this.btnDoOneMeasurement.Size = new System.Drawing.Size(105, 23);
            this.btnDoOneMeasurement.TabIndex = 440;
            this.btnDoOneMeasurement.Text = "Wykonaj 1 pomiar";
            this.btnDoOneMeasurement.UseVisualStyleBackColor = true;
            this.btnDoOneMeasurement.Click += new System.EventHandler(this.btnDoOneMeasurement_Click);
            // 
            // btnSendParametersToRLC
            // 
            this.btnSendParametersToRLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSendParametersToRLC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSendParametersToRLC.Location = new System.Drawing.Point(518, 18);
            this.btnSendParametersToRLC.Name = "btnSendParametersToRLC";
            this.btnSendParametersToRLC.Size = new System.Drawing.Size(129, 25);
            this.btnSendParametersToRLC.TabIndex = 432;
            this.btnSendParametersToRLC.Text = "Wyślij parametry (2)";
            this.btnSendParametersToRLC.UseVisualStyleBackColor = true;
            this.btnSendParametersToRLC.Click += new System.EventHandler(this.btnSendParametersToRLC_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnStartStop.Location = new System.Drawing.Point(281, 225);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnStartStop.TabIndex = 440;
            this.btnStartStop.Text = "button1";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 442;
            this.label2.Text = "Nazwa pliku:";
            // 
            // tbFileName
            // 
            this.tbFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PomiarWilgotnosci.Properties.Settings.Default, "meas_file_name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbFileName.Location = new System.Drawing.Point(180, 264);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(176, 20);
            this.tbFileName.TabIndex = 443;
            this.tbFileName.Text = global::PomiarWilgotnosci.Properties.Settings.Default.meas_file_name;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PomiarWilgotnosci.Properties.Settings.Default, "seconds", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown1.Location = new System.Drawing.Point(180, 228);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(79, 20);
            this.numericUpDown1.TabIndex = 439;
            this.numericUpDown1.Value = global::PomiarWilgotnosci.Properties.Settings.Default.seconds;
            // 
            // edtAverage
            // 
            this.edtAverage.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PomiarWilgotnosci.Properties.Settings.Default, "average", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtAverage.Location = new System.Drawing.Point(416, 22);
            this.edtAverage.Name = "edtAverage";
            this.edtAverage.Size = new System.Drawing.Size(86, 20);
            this.edtAverage.TabIndex = 436;
            this.edtAverage.Value = global::PomiarWilgotnosci.Properties.Settings.Default.average;
            // 
            // edtTriggerDelay
            // 
            this.edtTriggerDelay.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PomiarWilgotnosci.Properties.Settings.Default, "delay", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtTriggerDelay.DecimalPlaces = 3;
            this.edtTriggerDelay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edtTriggerDelay.Location = new System.Drawing.Point(293, 22);
            this.edtTriggerDelay.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.edtTriggerDelay.Name = "edtTriggerDelay";
            this.edtTriggerDelay.Size = new System.Drawing.Size(96, 20);
            this.edtTriggerDelay.TabIndex = 430;
            this.edtTriggerDelay.Value = global::PomiarWilgotnosci.Properties.Settings.Default.delay;
            // 
            // edtMeasurementFrequency
            // 
            this.edtMeasurementFrequency.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PomiarWilgotnosci.Properties.Settings.Default, "freq", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtMeasurementFrequency.Location = new System.Drawing.Point(137, 22);
            this.edtMeasurementFrequency.Maximum = new decimal(new int[] {
            2000000,
            0,
            0,
            0});
            this.edtMeasurementFrequency.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.edtMeasurementFrequency.Name = "edtMeasurementFrequency";
            this.edtMeasurementFrequency.Size = new System.Drawing.Size(96, 20);
            this.edtMeasurementFrequency.TabIndex = 429;
            this.edtMeasurementFrequency.Value = global::PomiarWilgotnosci.Properties.Settings.Default.freq;
            // 
            // edtVoltage
            // 
            this.edtVoltage.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PomiarWilgotnosci.Properties.Settings.Default, "voltage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtVoltage.DecimalPlaces = 3;
            this.edtVoltage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edtVoltage.Location = new System.Drawing.Point(9, 22);
            this.edtVoltage.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.edtVoltage.Name = "edtVoltage";
            this.edtVoltage.Size = new System.Drawing.Size(96, 20);
            this.edtVoltage.TabIndex = 428;
            this.edtVoltage.Value = global::PomiarWilgotnosci.Properties.Settings.Default.voltage;
            // 
            // tbRLCAddress
            // 
            this.tbRLCAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PomiarWilgotnosci.Properties.Settings.Default, "ip_agilent", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbRLCAddress.Location = new System.Drawing.Point(156, 62);
            this.tbRLCAddress.Name = "tbRLCAddress";
            this.tbRLCAddress.Size = new System.Drawing.Size(157, 20);
            this.tbRLCAddress.TabIndex = 416;
            this.tbRLCAddress.Text = global::PomiarWilgotnosci.Properties.Settings.Default.ip_agilent;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 306);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtTriggerDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMeasurementFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtVoltage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRLCConnect;
        private System.Windows.Forms.TextBox tbRLCAddress;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown edtAverage;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.Button btnDoOneMeasurement;
        private System.Windows.Forms.Button btnSendParametersToRLC;
        private System.Windows.Forms.NumericUpDown edtTriggerDelay;
        private System.Windows.Forms.NumericUpDown edtMeasurementFrequency;
        private System.Windows.Forms.NumericUpDown edtVoltage;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFileName;
    }
}

