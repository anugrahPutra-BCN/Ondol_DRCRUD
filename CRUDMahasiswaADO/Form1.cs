using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace CRUDMahasiswaADO
{
    public partial class FormMahasiswa : Form
    {
        private BindingSource bindingSource = new BindingSource(); // Sumber data untuk DataGridView, memudahkan binding data
        private DataTable dtMahasiswa = new DataTable(); // Tabel data untuk menyimpan hasil query Mahasiswa
        private readonly SqlConnection conn; // Objek koneksi ke SQL Server, dipakai untuk eksekusi query
        private readonly string connectionString =
            @"Data Source=DESKTOP-98D81B1\ANUGRAH;Initial Catalog=DBAkademikADO;Integrated Security=True";
        // Membuka koneksi ke database, tampilkan pesan berhasil/gagal
        private void SimpanLog(string pesan)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO LogError
                    VALUES (GETDATE(), @Pesan)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Pesan", pesan);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FormMahasiswa()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void ConnectDatabase()
        // Membuka koneksi ke database, tampilkan pesan berhasil/gagal
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                MessageBox.Show("Koneksi berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }
        }

        // ================================
        // CONNECT TEST
        // ===============================
        private void btnConnect_Click(object sender, EventArgs e)
        // Event tombol Connect, membuka koneksi ke database
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Koneksi berhasil!");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        // Event tombol Load, menampilkan data Mahasiswa ke DataGridView
        {
            LoadData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        // Event tombol Insert, menambahkan data Mahasiswa baru ke database
        {
            // 1. Bungkus seluruh proses database di dalam blok try
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertMahasiswa", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@JenisKelamin", cmbJK.Text);
                    cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value.Date);
                    cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                    cmd.Parameters.AddWithValue("@KodeProdi", txtKodeProdi.Text);
                    cmd.Parameters.AddWithValue("@TanggalDaftar", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    LoadData();
                    ClearForm();

                    // Opsional: Beri tahu user kalau data berhasil masuk
                    MessageBox.Show("Data mahasiswa berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // 2. Tangkap error spesifik database (seperti Primary Key kembar tadi)
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("SQL Error : " + ex.Message);
            }
            // 3. Tangkap error umum lainnya jika ada (misal koneksi jaringan putus, dll)
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("General Error : " + ex.Message);
            }

        }
        private void BtnUpdate_Click(object sender, EventArgs e)
        // Event tombol Update, mengubah data Mahasiswa berdasarkan NIM
        {
                SqlCommand cmd = new SqlCommand("sp_UpdateMahasiswa", conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@JenisKelamin", cmbJK.Text);
                    cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value.Date);
                    cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                    cmd.Parameters.AddWithValue("@KodeProdi", txtKodeProdi.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                LoadData();
            }
        }

        private void btnDelete_Click_Click(object sender, EventArgs e)
        // Event tombol Delete, menghapus data Mahasiswa berdasarkan NIM
        {
            using (SqlCommand cmd = new SqlCommand("sp_DeleteMahasiswa", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@NIM", SqlDbType.Char, 11).Value = txtNIM.Text;

                    conn.Open();
                    int rowsAffectedd = cmd.ExecuteNonQuery();
                    
                    if (rowsAffectedd < 0)
                    {
                        MessageBox.Show("Data berhasil dihapus");
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukan");
                    }
                conn.Close();
                LoadData();
            }
        }

        private void btnResetData_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                            IF OBJECT_ID('dbo.Mahasiswa_Backup') IS NOT NULL
                            BEGIN
                                DELETE FROM dbo.Mahasiswa;
                                INSERT INTO dbo.Mahasiswa
                                SELECT * FROM dbo.Mahasiswa_Backup
                            END";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();

                }
                MessageBox.Show("Semua data berhasil direset");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reset Gagal: " + ex.Message);
            }
        }

        private void btnTestInjection_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    using (SqlConnection conn =
                    new SqlConnection(connectionString))
                    {
                        string query =
                        "UPDATE Mahasiswa SET Nama='" +
                        txtNama.Text +
                        "' WHERE NIM='" +
                        txtNIM.Text + "'";

                        SqlCommand cmd =
                        new SqlCommand(query, conn);

                        conn.Open();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Update berhasil");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        // Event klik baris DataGridView, mengisi form dengan data baris terpilih
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtNIM.Text = row.Cells["NIM"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                cmbJK.Text = row.Cells["JenisKelamin"].Value.ToString();
                dtpTanggalLahir.Value = Convert.ToDateTime(row.Cells["TanggalLahir"].Value);
                txtAlamat.Text = row.Cells["Alamat"].Value.ToString();
                txtKodeProdi.Text = row.Cells["KodeProdi"].Value.ToString();
            }
        }

        private void ClearForm()
        // Membersihkan semua input form dan mengembalikan fokus ke txtNIM
        {
            txtNIM.Clear();
            txtNama.Clear();
            cmbJK.SelectedIndex = -1;
            dtpTanggalLahir.Value = DateTime.Now;
            txtAlamat.Clear();
            txtKodeProdi.Clear();
            txtNIM.Focus();
        }

        private void FormMahasiswa_Load_1(object sender, EventArgs e)
        {
            //comboBox JK manual
            cmbJK.DataSource = new string[] { "L", "P" };

            // setting Grid
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // BindingNavigator
            bindingNavigator1.BindingSource = bindingSource;

            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetMahasiswa", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dtMahasiswa = new DataTable();
                        da.Fill(dtMahasiswa);

                        bindingSource.DataSource = dtMahasiswa;
                        dataGridView1.DataSource = bindingSource;

                       // BindingControls();
                    }
                }
            }
            HitungTotal();
        }

        private void BindingControls()
        {
            txtNIM.DataBindings.Clear();
            txtNama.DataBindings.Clear();
            cmbJK.DataBindings.Clear();
            dtpTanggalLahir.DataBindings.Clear();
            txtAlamat.DataBindings.Clear();
            txtKodeProdi.DataBindings.Clear();

            txtNIM.DataBindings.Add("Text", bindingSource, "NIM");
            txtNama.DataBindings.Add("Text", bindingSource, "Nama");
            cmbJK.DataBindings.Add("Text", bindingSource, "JenisKelamin");
            dtpTanggalLahir.DataBindings.Add("Value", bindingSource, "TanggalLahir");
            txtAlamat.DataBindings.Add("Text", bindingSource, "Alamat");
            txtKodeProdi.DataBindings.Add("Text", bindingSource, "KodeProdi");
        }

        private void HitungTotal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CountMahasiswa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter outputParam = new SqlParameter("@Total", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        lblTotal.Text = "Total Mahasiswa: " + outputParam.Value.ToString();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghitung total: " + ex.Message);
            }
        }
    }
}