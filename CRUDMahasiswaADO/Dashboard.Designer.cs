namespace CRUDMahasiswaADO
{
    partial class Dashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTanggalMasuk = new System.Windows.Forms.DateTimePicker();
            this.loadDataChart = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ChartsArea = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbTipe = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ChartsArea)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rekap Data Mahasiswa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tahun Masuk";
            // 
            // dtpTanggalMasuk
            // 
            this.dtpTanggalMasuk.Location = new System.Drawing.Point(142, 48);
            this.dtpTanggalMasuk.Name = "dtpTanggalMasuk";
            this.dtpTanggalMasuk.Size = new System.Drawing.Size(200, 26);
            this.dtpTanggalMasuk.TabIndex = 2;
            // 
            // loadDataChart
            // 
            this.loadDataChart.Location = new System.Drawing.Point(367, 51);
            this.loadDataChart.Name = "loadDataChart";
            this.loadDataChart.Size = new System.Drawing.Size(75, 23);
            this.loadDataChart.TabIndex = 3;
            this.loadDataChart.Text = "Load";
            this.loadDataChart.UseVisualStyleBackColor = true;
            this.loadDataChart.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(460, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Reset";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // ChartsArea
            // 
            chartArea2.Name = "ChartArea1";
            this.ChartsArea.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.ChartsArea.Legends.Add(legend2);
            this.ChartsArea.Location = new System.Drawing.Point(16, 92);
            this.ChartsArea.Name = "ChartsArea";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ChartsArea.Series.Add(series2);
            this.ChartsArea.Size = new System.Drawing.Size(440, 200);
            this.ChartsArea.TabIndex = 5;
            this.ChartsArea.Text = "chart1";
            // 
            // cmbTipe
            // 
            this.cmbTipe.FormattingEnabled = true;
            this.cmbTipe.Location = new System.Drawing.Point(616, 52);
            this.cmbTipe.Name = "cmbTipe";
            this.cmbTipe.Size = new System.Drawing.Size(121, 28);
            this.cmbTipe.TabIndex = 6;
            this.cmbTipe.SelectedIndexChanged += new System.EventHandler(this.cmbTipe_SelectedValueChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(543, 258);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(170, 34);
            this.button3.TabIndex = 7;
            this.button3.Text = "Data Mahasiswa";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btn_Data_Mhs_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cmbTipe);
            this.Controls.Add(this.ChartsArea);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.loadDataChart);
            this.Controls.Add(this.dtpTanggalMasuk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Dashboard";
            this.Text = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.ChartsArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTanggalMasuk;
        private System.Windows.Forms.Button loadDataChart;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartsArea;
        private System.Windows.Forms.ComboBox cmbTipe;
        private System.Windows.Forms.Button button3;
    }
}