using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRUDMahasiswaADO
{
    public partial class Dashboard : Form 
    {
        DAL dbLogic = new DAL(); //  Inisialisasi logika Data Access Layer (DAL) untuk interaksi database
        bool isInitializing = true; //  Flag untuk mencegah event handler berjalan saat form atau komponen sedang dimuat
        DataTable dt;
        int button = 0;
        public Dashboard()
        {
            InitializeComponent();

            dtpTanggalMasuk.MinDate = new DateTime(2000, 1, 1); //  Menentukan batas minimal tahun (1 Januari 2000) pada kontrol DateTimePicker
            dtpTanggalMasuk.Format = DateTimePickerFormat.Custom;
            dtpTanggalMasuk.CustomFormat = "yyyy"; // Mengubah format tampilan DateTimePicker agar hanya memunculkan format tahun saja
            dtpTanggalMasuk.ShowUpDown = true;
            dtpTanggalMasuk.MaxDate = DateTime.Now;

            cmbTipe.DropDownStyle = ComboBoxStyle.DropDownList;
            var items = new List<KeyValuePair<string, SeriesChartType>> // Membuat daftar jenis grafik menggunakan KeyValuePair untuk diikat (bind) ke ComboBox
            {
                new KeyValuePair<string, SeriesChartType>("Kolom", SeriesChartType.Column),
                new KeyValuePair<string, SeriesChartType>("Pie", SeriesChartType.Pie),
            };

            isInitializing = true;

            //  Mengatur sumber data ComboBox dan menentukan properti yang ditampilkan ke pengguna
            cmbTipe.DataSource = items;
            cmbTipe.DisplayMember = "Key";
            cmbTipe.ValueMember = "Value";
            cmbTipe.SelectedIndex = 0;

            isInitializing = false;
            loadDataChart();
        }

        public void DataChart()
        {
            // Membersihkan sisa elemen grafik sebelumnya untuk mencegah penumpukan visual saat reload
            ChartsArea.Series.Clear();
            ChartsArea.Titles.Clear();
            ChartsArea.Legends.Clear();
            ChartsArea.ChartAreas.Clear();

            // Mengonfigurasi area utama grafik termasuk judul sumbu X dan Y, serta latar belakang transparan
            ChartsArea ca = new ChartsArea("MainArea");
            ca.AxisX.Title = "Program Studi";
            ca.AxisY.Title = "Jumlah Mahasiswa";
            ca.BackColor = Color.Transparent;
            ChartsArea.ChartAreas.Add(ca);
            try
            {
                // Mengambil data grafik berdasarkan filter tahun jika tombol filter (button == 1) aktif
                if (button == 1)
                {
                    dt = dbLogic.GetDataChartByTahun(dtpTanggalMasuk.Value);
                }
                else {
                {
                        dt = dbLogic.GetAllDataChart();
                }

                SeriesChartType tipe = (SeriesChartType)cmbTipe.SelectedValue;
                if (tipe == SeriesChartType.Column)
                {
                    Series s = new Series("Mahasiswa");
                    s.ChartType = SeriesChartType.Column;
                    foreach (DataRow row in dt.Rows)
                    {
                        string prodi = row["NamaProdi"].ToString();
                        int jumlah = Convert.ToInt32((long)row["JmlhMhs"]); // Mengonversi tipe data kembalian database (long) ke tipe data integer yang didukung grafik
                            s.Points.AddXY(prodi, jumlah);
                    }
                    ChartsArea.Series.Add(s);
                }
                else
                {
                     Series s = new Series("Jumlah Mahasiswa");
                     s.ChartType = tipe;


                     foreach (DataRow row in dt.Rows)
                     {
                         string prodi = row["NamaProdi"].ToString();
                         int jumlah = Convert.ToInt32((long)row["JmlhMhs"]);
                         s.Points.AddXY(prodi, jumlah); // Memetakan nama program studi (sumbu X) dan jumlah mahasiswa (sumbu Y) ke dalam titik grafik
                        }
                     ChartsArea.Series.Add(s);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Gagal Load Data: " + ex.Message);
            }

            // Menambahkan konfigurasi judul utama grafik dengan font Arial berukuran 14pt tebal
            Tittle tittle = new Tittle("Jumlah Mahasiswa per Program Studi", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.DarkBlue);
            ChartsArea.Titles.Add(tittle);
            Legend legend = new Legend("MainLegend");
            ChartsArea.Legend.Add(legend);
        }
        private void cmbTipe_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isInitializing)
                return;
            if (button == 1)
            {

            }
            else
            {
                loadDataChart();
            }

        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            button = 1;
            loadDataChart();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            button = 0;
            loadDataChart();
        }

        private void btn_Data_Mhs_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
