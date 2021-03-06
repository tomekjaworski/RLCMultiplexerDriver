﻿namespace MultiplexerGUI
{
    partial class MainForm
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
            System.Windows.Forms.Label label149;
            System.Windows.Forms.Label label150;
            System.Windows.Forms.Label label151;
            System.Windows.Forms.Label label152;
            System.Windows.Forms.Label label153;
            System.Windows.Forms.Label label147;
            System.Windows.Forms.Label label154;
            System.Windows.Forms.Label label155;
            System.Windows.Forms.Label label160;
            System.Windows.Forms.Label label161;
            System.Windows.Forms.Label label162;
            System.Windows.Forms.Label label163;
            System.Windows.Forms.Label label164;
            System.Windows.Forms.Label label165;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            this.btnConnectToMultiplexer = new System.Windows.Forms.Button();
            this.btnShowRelayDriverWindow = new System.Windows.Forms.Button();
            this.btnRLCConnect = new System.Windows.Forms.Button();
            this.btnSendParametersToRLC = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbImpedance = new System.Windows.Forms.RadioButton();
            this.rbSerialCapacitance = new System.Windows.Forms.RadioButton();
            this.rbParallelCapacitance = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnShowSocketDriverWindow = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchForMultiplexer = new System.Windows.Forms.Button();
            this.tbMultiplexerAddress = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRLCAddress = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.edtAverage = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label148 = new System.Windows.Forms.Label();
            this.btnDoOneMeasurement = new System.Windows.Forms.Button();
            this.edtTriggerDelay = new System.Windows.Forms.NumericUpDown();
            this.edtMeasurementFrequency = new System.Windows.Forms.NumericUpDown();
            this.edtVoltage = new System.Windows.Forms.NumericUpDown();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblNumberOfMeasurements = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCommenceMeasurements = new System.Windows.Forms.Button();
            this.btnSaveMeasurements = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblCurrentCapacity = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tmrMultKeepAlive = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStartStop = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.edtNumberOfMeasurementsPerPair = new System.Windows.Forms.NumericUpDown();
            this.edtNumberOfElectrodes = new System.Windows.Forms.NumericUpDown();
            label149 = new System.Windows.Forms.Label();
            label150 = new System.Windows.Forms.Label();
            label151 = new System.Windows.Forms.Label();
            label152 = new System.Windows.Forms.Label();
            label153 = new System.Windows.Forms.Label();
            label147 = new System.Windows.Forms.Label();
            label154 = new System.Windows.Forms.Label();
            label155 = new System.Windows.Forms.Label();
            label160 = new System.Windows.Forms.Label();
            label161 = new System.Windows.Forms.Label();
            label162 = new System.Windows.Forms.Label();
            label163 = new System.Windows.Forms.Label();
            label164 = new System.Windows.Forms.Label();
            label165 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtAverage)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtTriggerDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMeasurementFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtVoltage)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNumberOfMeasurementsPerPair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNumberOfElectrodes)).BeginInit();
            this.SuspendLayout();
            // 
            // label149
            // 
            label149.AutoSize = true;
            label149.Location = new System.Drawing.Point(12, 9);
            label149.Name = "label149";
            label149.Size = new System.Drawing.Size(68, 13);
            label149.TabIndex = 422;
            label149.Text = "Typ pomiaru:";
            // 
            // label150
            // 
            label150.AutoSize = true;
            label150.Location = new System.Drawing.Point(191, 9);
            label150.Name = "label150";
            label150.Size = new System.Drawing.Size(122, 13);
            label150.TabIndex = 423;
            label150.Text = "Napięcie pomiarowe [V]:";
            // 
            // label151
            // 
            label151.AutoSize = true;
            label151.Location = new System.Drawing.Point(319, 9);
            label151.Name = "label151";
            label151.Size = new System.Drawing.Size(150, 13);
            label151.TabIndex = 424;
            label151.Text = "Częstotliwość pomiarowa [Hz]:";
            // 
            // label152
            // 
            label152.AutoSize = true;
            label152.Location = new System.Drawing.Point(475, 9);
            label152.Name = "label152";
            label152.Size = new System.Drawing.Size(117, 13);
            label152.TabIndex = 427;
            label152.Text = "Opóźnienie pomiaru [s]:";
            // 
            // label153
            // 
            label153.AutoSize = true;
            label153.Location = new System.Drawing.Point(8, 8);
            label153.Name = "label153";
            label153.Size = new System.Drawing.Size(42, 13);
            label153.TabIndex = 434;
            label153.Text = "Pomiar:";
            // 
            // label147
            // 
            label147.AutoSize = true;
            label147.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label147.Location = new System.Drawing.Point(7, 7);
            label147.Name = "label147";
            label147.Size = new System.Drawing.Size(128, 23);
            label147.TabIndex = 0;
            label147.Text = "Multiplekser";
            // 
            // label154
            // 
            label154.AutoSize = true;
            label154.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            label154.Location = new System.Drawing.Point(7, 8);
            label154.Name = "label154";
            label154.Size = new System.Drawing.Size(394, 23);
            label154.TabIndex = 0;
            label154.Text = "Mostek pomiarowy RLC (Agilent E4980)";
            // 
            // label155
            // 
            label155.AutoSize = true;
            label155.Location = new System.Drawing.Point(598, 9);
            label155.Name = "label155";
            label155.Size = new System.Drawing.Size(108, 13);
            label155.TabIndex = 435;
            label155.Text = "Usrednienie [probek]:";
            // 
            // label160
            // 
            label160.AutoSize = true;
            label160.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            label160.ForeColor = System.Drawing.Color.White;
            label160.Location = new System.Drawing.Point(13, 11);
            label160.Name = "label160";
            label160.Size = new System.Drawing.Size(321, 13);
            label160.TabIndex = 0;
            label160.Text = "Zautomatyzowany pomiar dla czujnika N-elektrodowego";
            // 
            // label161
            // 
            label161.AutoSize = true;
            label161.Location = new System.Drawing.Point(12, 320);
            label161.Name = "label161";
            label161.Size = new System.Drawing.Size(82, 13);
            label161.TabIndex = 448;
            label161.Text = "Liczba elektrod:";
            // 
            // label162
            // 
            label162.AutoSize = true;
            label162.Location = new System.Drawing.Point(192, 320);
            label162.Name = "label162";
            label162.Size = new System.Drawing.Size(94, 13);
            label162.TabIndex = 449;
            label162.Text = "Liczba kombinacji:";
            // 
            // label163
            // 
            label163.AutoSize = true;
            label163.Location = new System.Drawing.Point(12, 345);
            label163.Name = "label163";
            label163.Size = new System.Drawing.Size(150, 13);
            label163.TabIndex = 454;
            label163.Text = "Postęp procesu pomiarowego:";
            // 
            // label164
            // 
            label164.AutoSize = true;
            label164.Location = new System.Drawing.Point(335, 320);
            label164.Name = "label164";
            label164.Size = new System.Drawing.Size(89, 13);
            label164.TabIndex = 456;
            label164.Text = "Liczba pomiarów:";
            // 
            // label165
            // 
            label165.AutoSize = true;
            label165.Location = new System.Drawing.Point(221, 345);
            label165.Name = "label165";
            label165.Size = new System.Drawing.Size(99, 13);
            label165.TabIndex = 457;
            label165.Text = "Zmierzona wartość:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(12, 390);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(77, 13);
            label5.TabIndex = 460;
            label5.Text = "Plik wyjściowy:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(472, 434);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(116, 13);
            label6.TabIndex = 463;
            label6.Text = "Interwał pomiarowy [s]:";
            // 
            // btnConnectToMultiplexer
            // 
            this.btnConnectToMultiplexer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnConnectToMultiplexer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnConnectToMultiplexer.Location = new System.Drawing.Point(546, 7);
            this.btnConnectToMultiplexer.Name = "btnConnectToMultiplexer";
            this.btnConnectToMultiplexer.Size = new System.Drawing.Size(88, 50);
            this.btnConnectToMultiplexer.TabIndex = 414;
            this.btnConnectToMultiplexer.Text = "Połącz (1)";
            this.btnConnectToMultiplexer.UseVisualStyleBackColor = true;
            this.btnConnectToMultiplexer.Click += new System.EventHandler(this.btnConnectToMultiplexer_Click);
            // 
            // btnShowRelayDriverWindow
            // 
            this.btnShowRelayDriverWindow.Location = new System.Drawing.Point(741, 7);
            this.btnShowRelayDriverWindow.Name = "btnShowRelayDriverWindow";
            this.btnShowRelayDriverWindow.Size = new System.Drawing.Size(95, 50);
            this.btnShowRelayDriverWindow.TabIndex = 415;
            this.btnShowRelayDriverWindow.Text = "Indywidualne sterowanie przekaźnikami";
            this.btnShowRelayDriverWindow.UseVisualStyleBackColor = true;
            this.btnShowRelayDriverWindow.Click += new System.EventHandler(this.btnShowRelayDriverWindow_Click);
            // 
            // btnRLCConnect
            // 
            this.btnRLCConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnRLCConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnRLCConnect.Location = new System.Drawing.Point(720, 7);
            this.btnRLCConnect.Name = "btnRLCConnect";
            this.btnRLCConnect.Size = new System.Drawing.Size(116, 49);
            this.btnRLCConnect.TabIndex = 431;
            this.btnRLCConnect.Text = "Połącz (2)";
            this.btnRLCConnect.UseVisualStyleBackColor = true;
            this.btnRLCConnect.Click += new System.EventHandler(this.btnRLCConnect_Click);
            // 
            // btnSendParametersToRLC
            // 
            this.btnSendParametersToRLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSendParametersToRLC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSendParametersToRLC.Location = new System.Drawing.Point(703, 21);
            this.btnSendParametersToRLC.Name = "btnSendParametersToRLC";
            this.btnSendParametersToRLC.Size = new System.Drawing.Size(129, 25);
            this.btnSendParametersToRLC.TabIndex = 432;
            this.btnSendParametersToRLC.Text = "Wyślij parametry (3)";
            this.btnSendParametersToRLC.UseVisualStyleBackColor = true;
            this.btnSendParametersToRLC.Click += new System.EventHandler(this.btnSendParametersToRLC_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbImpedance);
            this.panel1.Controls.Add(this.rbSerialCapacitance);
            this.panel1.Controls.Add(this.rbParallelCapacitance);
            this.panel1.Location = new System.Drawing.Point(10, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 72);
            this.panel1.TabIndex = 433;
            // 
            // rbImpedance
            // 
            this.rbImpedance.AutoSize = true;
            this.rbImpedance.Location = new System.Drawing.Point(6, 44);
            this.rbImpedance.Name = "rbImpedance";
            this.rbImpedance.Size = new System.Drawing.Size(107, 17);
            this.rbImpedance.TabIndex = 422;
            this.rbImpedance.Text = "Impedancja (R-X)";
            this.rbImpedance.UseVisualStyleBackColor = true;
            // 
            // rbSerialCapacitance
            // 
            this.rbSerialCapacitance.AutoSize = true;
            this.rbSerialCapacitance.Location = new System.Drawing.Point(6, 4);
            this.rbSerialCapacitance.Name = "rbSerialCapacitance";
            this.rbSerialCapacitance.Size = new System.Drawing.Size(163, 17);
            this.rbSerialCapacitance.TabIndex = 420;
            this.rbSerialCapacitance.Text = "Pojemność szeregowo (Cs-D)";
            this.rbSerialCapacitance.UseVisualStyleBackColor = true;
            // 
            // rbParallelCapacitance
            // 
            this.rbParallelCapacitance.AutoSize = true;
            this.rbParallelCapacitance.Location = new System.Drawing.Point(6, 24);
            this.rbParallelCapacitance.Name = "rbParallelCapacitance";
            this.rbParallelCapacitance.Size = new System.Drawing.Size(164, 17);
            this.rbParallelCapacitance.TabIndex = 421;
            this.rbParallelCapacitance.TabStop = true;
            this.rbParallelCapacitance.Text = "Pojemność równolegle (Cp-D)";
            this.rbParallelCapacitance.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.btnShowSocketDriverWindow);
            this.panel2.Controls.Add(this.lblStatus);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnSearchForMultiplexer);
            this.panel2.Controls.Add(label147);
            this.panel2.Controls.Add(this.btnConnectToMultiplexer);
            this.panel2.Controls.Add(this.tbMultiplexerAddress);
            this.panel2.Controls.Add(this.btnShowRelayDriverWindow);
            this.panel2.Location = new System.Drawing.Point(12, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(843, 65);
            this.panel2.TabIndex = 435;
            // 
            // btnShowSocketDriverWindow
            // 
            this.btnShowSocketDriverWindow.Location = new System.Drawing.Point(640, 7);
            this.btnShowSocketDriverWindow.Name = "btnShowSocketDriverWindow";
            this.btnShowSocketDriverWindow.Size = new System.Drawing.Size(95, 50);
            this.btnShowSocketDriverWindow.TabIndex = 462;
            this.btnShowSocketDriverWindow.Text = "Indywidualne sterowanie gniazdami";
            this.btnShowSocketDriverWindow.UseVisualStyleBackColor = true;
            this.btnShowSocketDriverWindow.Click += new System.EventHandler(this.btnShowSocketDriverWindow_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStatus.Location = new System.Drawing.Point(254, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(85, 13);
            this.lblStatus.TabIndex = 463;
            this.lblStatus.Text = "Niepołączony";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(207, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 462;
            this.label3.Text = "Status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 461;
            this.label2.Text = "Brak adresu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(145, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 460;
            this.label1.Text = "Adres/nazwa hosta:";
            // 
            // btnSearchForMultiplexer
            // 
            this.btnSearchForMultiplexer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSearchForMultiplexer.Location = new System.Drawing.Point(452, 7);
            this.btnSearchForMultiplexer.Name = "btnSearchForMultiplexer";
            this.btnSearchForMultiplexer.Size = new System.Drawing.Size(88, 50);
            this.btnSearchForMultiplexer.TabIndex = 459;
            this.btnSearchForMultiplexer.Text = "Wykryj";
            this.btnSearchForMultiplexer.UseVisualStyleBackColor = true;
            this.btnSearchForMultiplexer.Click += new System.EventHandler(this.btnSearchForMultiplexer_Click);
            // 
            // tbMultiplexerAddress
            // 
            this.tbMultiplexerAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MultiplexerGUI.Properties.Settings.Default, "ip_raspberrypi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbMultiplexerAddress.Location = new System.Drawing.Point(253, 12);
            this.tbMultiplexerAddress.Name = "tbMultiplexerAddress";
            this.tbMultiplexerAddress.Size = new System.Drawing.Size(100, 20);
            this.tbMultiplexerAddress.TabIndex = 412;
            this.tbMultiplexerAddress.Text = global::MultiplexerGUI.Properties.Settings.Default.ip_raspberrypi;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.YellowGreen;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(label154);
            this.panel3.Controls.Add(this.btnRLCConnect);
            this.panel3.Controls.Add(this.tbRLCAddress);
            this.panel3.Location = new System.Drawing.Point(12, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(843, 71);
            this.panel3.TabIndex = 436;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(145, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 461;
            this.label4.Text = "Adres/nazwa hosta:";
            // 
            // tbRLCAddress
            // 
            this.tbRLCAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MultiplexerGUI.Properties.Settings.Default, "ip_agilent", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbRLCAddress.Location = new System.Drawing.Point(253, 36);
            this.tbRLCAddress.Name = "tbRLCAddress";
            this.tbRLCAddress.Size = new System.Drawing.Size(157, 20);
            this.tbRLCAddress.TabIndex = 416;
            this.tbRLCAddress.Text = global::MultiplexerGUI.Properties.Settings.Default.ip_agilent;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.edtAverage);
            this.panel4.Controls.Add(label155);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(label149);
            this.panel4.Controls.Add(this.btnSendParametersToRLC);
            this.panel4.Controls.Add(label150);
            this.panel4.Controls.Add(this.edtTriggerDelay);
            this.panel4.Controls.Add(label151);
            this.panel4.Controls.Add(this.edtMeasurementFrequency);
            this.panel4.Controls.Add(label152);
            this.panel4.Controls.Add(this.edtVoltage);
            this.panel4.Location = new System.Drawing.Point(12, 153);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(843, 118);
            this.panel4.TabIndex = 437;
            // 
            // edtAverage
            // 
            this.edtAverage.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MultiplexerGUI.Properties.Settings.Default, "average", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtAverage.Location = new System.Drawing.Point(601, 25);
            this.edtAverage.Name = "edtAverage";
            this.edtAverage.Size = new System.Drawing.Size(86, 20);
            this.edtAverage.TabIndex = 436;
            this.edtAverage.Value = global::MultiplexerGUI.Properties.Settings.Default.average;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Gainsboro;
            this.panel5.Controls.Add(label153);
            this.panel5.Controls.Add(this.label148);
            this.panel5.Controls.Add(this.btnDoOneMeasurement);
            this.panel5.Location = new System.Drawing.Point(194, 55);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(638, 30);
            this.panel5.TabIndex = 434;
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label148.Location = new System.Drawing.Point(56, 8);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(56, 13);
            this.label148.TabIndex = 439;
            this.label148.Text = "overload";
            // 
            // btnDoOneMeasurement
            // 
            this.btnDoOneMeasurement.Location = new System.Drawing.Point(530, 3);
            this.btnDoOneMeasurement.Name = "btnDoOneMeasurement";
            this.btnDoOneMeasurement.Size = new System.Drawing.Size(105, 23);
            this.btnDoOneMeasurement.TabIndex = 440;
            this.btnDoOneMeasurement.Text = "Wykonaj 1 pomiar";
            this.btnDoOneMeasurement.UseVisualStyleBackColor = true;
            this.btnDoOneMeasurement.Click += new System.EventHandler(this.btnDoOneMeasurement_Click);
            // 
            // edtTriggerDelay
            // 
            this.edtTriggerDelay.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MultiplexerGUI.Properties.Settings.Default, "delay", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtTriggerDelay.DecimalPlaces = 3;
            this.edtTriggerDelay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edtTriggerDelay.Location = new System.Drawing.Point(478, 25);
            this.edtTriggerDelay.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.edtTriggerDelay.Name = "edtTriggerDelay";
            this.edtTriggerDelay.Size = new System.Drawing.Size(96, 20);
            this.edtTriggerDelay.TabIndex = 430;
            this.edtTriggerDelay.Value = global::MultiplexerGUI.Properties.Settings.Default.delay;
            // 
            // edtMeasurementFrequency
            // 
            this.edtMeasurementFrequency.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MultiplexerGUI.Properties.Settings.Default, "freq", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtMeasurementFrequency.Location = new System.Drawing.Point(322, 25);
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
            this.edtMeasurementFrequency.Value = global::MultiplexerGUI.Properties.Settings.Default.freq;
            // 
            // edtVoltage
            // 
            this.edtVoltage.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MultiplexerGUI.Properties.Settings.Default, "voltage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtVoltage.DecimalPlaces = 3;
            this.edtVoltage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.edtVoltage.Location = new System.Drawing.Point(194, 25);
            this.edtVoltage.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.edtVoltage.Name = "edtVoltage";
            this.edtVoltage.Size = new System.Drawing.Size(96, 20);
            this.edtVoltage.TabIndex = 428;
            this.edtVoltage.Value = global::MultiplexerGUI.Properties.Settings.Default.voltage;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Navy;
            this.panel6.Controls.Add(label160);
            this.panel6.Location = new System.Drawing.Point(12, 277);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(843, 35);
            this.panel6.TabIndex = 446;
            // 
            // lblNumberOfMeasurements
            // 
            this.lblNumberOfMeasurements.AutoSize = true;
            this.lblNumberOfMeasurements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNumberOfMeasurements.Location = new System.Drawing.Point(292, 320);
            this.lblNumberOfMeasurements.Name = "lblNumberOfMeasurements";
            this.lblNumberOfMeasurements.Size = new System.Drawing.Size(14, 13);
            this.lblNumberOfMeasurements.TabIndex = 450;
            this.lblNumberOfMeasurements.Text = "0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 361);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(658, 23);
            this.progressBar1.TabIndex = 451;
            // 
            // btnCommenceMeasurements
            // 
            this.btnCommenceMeasurements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCommenceMeasurements.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnCommenceMeasurements.Location = new System.Drawing.Point(739, 345);
            this.btnCommenceMeasurements.Name = "btnCommenceMeasurements";
            this.btnCommenceMeasurements.Size = new System.Drawing.Size(116, 23);
            this.btnCommenceMeasurements.TabIndex = 452;
            this.btnCommenceMeasurements.Text = "Pomiar (5)";
            this.btnCommenceMeasurements.UseVisualStyleBackColor = true;
            this.btnCommenceMeasurements.Click += new System.EventHandler(this.btnCommenceMeasurements_Click);
            // 
            // btnSaveMeasurements
            // 
            this.btnSaveMeasurements.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSaveMeasurements.Location = new System.Drawing.Point(739, 320);
            this.btnSaveMeasurements.Name = "btnSaveMeasurements";
            this.btnSaveMeasurements.Size = new System.Drawing.Size(116, 23);
            this.btnSaveMeasurements.TabIndex = 453;
            this.btnSaveMeasurements.Text = "Wybierz plik... (4)";
            this.btnSaveMeasurements.UseVisualStyleBackColor = true;
            this.btnSaveMeasurements.Click += new System.EventHandler(this.btnSaveMeasurements_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Zapisz dane pomiarowe...";
            // 
            // lblCurrentCapacity
            // 
            this.lblCurrentCapacity.AutoSize = true;
            this.lblCurrentCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCurrentCapacity.ForeColor = System.Drawing.Color.Green;
            this.lblCurrentCapacity.Location = new System.Drawing.Point(319, 345);
            this.lblCurrentCapacity.Name = "lblCurrentCapacity";
            this.lblCurrentCapacity.Size = new System.Drawing.Size(21, 13);
            this.lblCurrentCapacity.TabIndex = 458;
            this.lblCurrentCapacity.Text = "0F";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "*-list.txt";
            this.openFileDialog1.Filter = "Pliki pojemności własnych (*-list.txt)|*-list.txt|Wszystkie pliki (*.*)|*.txt";
            this.openFileDialog1.Title = "Wybierz plik pojemności własnych...";
            // 
            // tmrMultKeepAlive
            // 
            this.tmrMultKeepAlive.Interval = 1000;
            this.tmrMultKeepAlive.Tick += new System.EventHandler(this.tmrMultKeepAlive_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnStartStop.Location = new System.Drawing.Point(739, 429);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(116, 23);
            this.btnStartStop.TabIndex = 462;
            this.btnStartStop.Text = "button1";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MultiplexerGUI.Properties.Settings.Default, "interval", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDown1.Location = new System.Drawing.Point(594, 432);
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
            this.numericUpDown1.TabIndex = 461;
            this.numericUpDown1.Value = global::MultiplexerGUI.Properties.Settings.Default.interval;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::MultiplexerGUI.Properties.Settings.Default, "meas_file_name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(15, 406);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(658, 20);
            this.textBox1.TabIndex = 459;
            this.textBox1.Text = global::MultiplexerGUI.Properties.Settings.Default.meas_file_name;
            // 
            // edtNumberOfMeasurementsPerPair
            // 
            this.edtNumberOfMeasurementsPerPair.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MultiplexerGUI.Properties.Settings.Default, "measurements_per_pair", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtNumberOfMeasurementsPerPair.Location = new System.Drawing.Point(430, 320);
            this.edtNumberOfMeasurementsPerPair.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.edtNumberOfMeasurementsPerPair.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtNumberOfMeasurementsPerPair.Name = "edtNumberOfMeasurementsPerPair";
            this.edtNumberOfMeasurementsPerPair.Size = new System.Drawing.Size(58, 20);
            this.edtNumberOfMeasurementsPerPair.TabIndex = 455;
            this.edtNumberOfMeasurementsPerPair.Value = global::MultiplexerGUI.Properties.Settings.Default.measurements_per_pair;
            // 
            // edtNumberOfElectrodes
            // 
            this.edtNumberOfElectrodes.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MultiplexerGUI.Properties.Settings.Default, "electrodes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.edtNumberOfElectrodes.Location = new System.Drawing.Point(100, 318);
            this.edtNumberOfElectrodes.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.edtNumberOfElectrodes.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.edtNumberOfElectrodes.Name = "edtNumberOfElectrodes";
            this.edtNumberOfElectrodes.Size = new System.Drawing.Size(86, 20);
            this.edtNumberOfElectrodes.TabIndex = 447;
            this.edtNumberOfElectrodes.Value = global::MultiplexerGUI.Properties.Settings.Default.electrodes;
            this.edtNumberOfElectrodes.ValueChanged += new System.EventHandler(this.edtNumberOfElectrodes_ValueChanged);
            this.edtNumberOfElectrodes.BindingContextChanged += new System.EventHandler(this.edtNumberOfElectrodes_BindingContextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(867, 510);
            this.Controls.Add(label6);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblCurrentCapacity);
            this.Controls.Add(label165);
            this.Controls.Add(label164);
            this.Controls.Add(this.edtNumberOfMeasurementsPerPair);
            this.Controls.Add(label163);
            this.Controls.Add(this.btnSaveMeasurements);
            this.Controls.Add(this.btnCommenceMeasurements);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblNumberOfMeasurements);
            this.Controls.Add(label162);
            this.Controls.Add(label161);
            this.Controls.Add(this.edtNumberOfElectrodes);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Pomiary pojemności";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtAverage)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtTriggerDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtMeasurementFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtVoltage)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNumberOfMeasurementsPerPair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtNumberOfElectrodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnectToMultiplexer;
        private System.Windows.Forms.TextBox tbMultiplexerAddress;
        private System.Windows.Forms.Button btnShowRelayDriverWindow;
        private System.Windows.Forms.TextBox tbRLCAddress;
        private System.Windows.Forms.RadioButton rbSerialCapacitance;
        private System.Windows.Forms.RadioButton rbParallelCapacitance;
        private System.Windows.Forms.NumericUpDown edtVoltage;
        private System.Windows.Forms.NumericUpDown edtMeasurementFrequency;
        private System.Windows.Forms.NumericUpDown edtTriggerDelay;
        private System.Windows.Forms.Button btnRLCConnect;
        private System.Windows.Forms.Button btnSendParametersToRLC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.Button btnDoOneMeasurement;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.NumericUpDown edtAverage;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown edtNumberOfElectrodes;
        private System.Windows.Forms.Label lblNumberOfMeasurements;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCommenceMeasurements;
        private System.Windows.Forms.Button btnSaveMeasurements;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.NumericUpDown edtNumberOfMeasurementsPerPair;
        private System.Windows.Forms.Label lblCurrentCapacity;
        private System.Windows.Forms.Button btnSearchForMultiplexer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnShowSocketDriverWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrMultKeepAlive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbImpedance;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}