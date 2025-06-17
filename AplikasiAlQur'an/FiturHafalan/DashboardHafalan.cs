using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiAlQur_an.FiturHafalan
{
    public partial class DashboardHafalan: Form
    {
        private List<Surah> _allSurahs;
        private readonly HttpClient _httpClient; // instance HttpClient untuk menghindari socket exhaustion
        private List<HafalanData> daftarHafalan = new List<HafalanData>();
        private readonly string _projectRootPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
        private readonly string _pathHafalanFile;

        // URL API 
        private const string ApiSurahUrl = "https://equran.id/api/surat";
        public DashboardHafalan()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _pathHafalanFile = Path.Combine(_projectRootPath, "FiturHafalan", "hafalan.json");
            PastikanFolderHafalanAda();
        }

        private async void DashboardHafalan_Load(object sender, EventArgs e)
        {
            // Panggil metode asinkron untuk memuat data Surah
            await LoadAllSurahsAsync();
            // Setelah semua Surah dimuat, isi ComboBox Surah awal
            InitializeSurahComboBoxes();
            // Inisialisasi ComboBox ayat setelah Surah terpilih
            PopulateAyatForMulaiSurah();
            PopulateAyatForHinggaSurah();
            InitializeDataGridView();
            MuatHafalanDariFile();
            RefreshDataGrid();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("TanggalMulai", "Tanggal Mulai");
            dataGridView1.Columns.Add("TanggalAkhir", "Tanggal Akhir");
            dataGridView1.Columns.Add("SurahAwal", "Surah Awal");
            dataGridView1.Columns.Add("AyatAwal", "Ayat Awal");
            dataGridView1.Columns.Add("SurahAkhir", "Surah Akhir");
            dataGridView1.Columns.Add("AyatAkhir", "Ayat Akhir");
            dataGridView1.Columns.Add("Status", "Status");

            // Tombol Selesai
            if (!dataGridView1.Columns.Contains("Selesai"))
            {
                DataGridViewButtonColumn selesaiBtn = new DataGridViewButtonColumn();
                selesaiBtn.Name = "Selesai";
                selesaiBtn.HeaderText = "Aksi";
                selesaiBtn.Text = "Selesai";
                selesaiBtn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(selesaiBtn);
            }

            // Tombol Hapus
            DataGridViewButtonColumn hapusBtn = new DataGridViewButtonColumn();
            hapusBtn.Name = "Hapus";
            hapusBtn.HeaderText = " ";
            hapusBtn.Text = "Hapus";
            hapusBtn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(hapusBtn);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void PastikanFolderHafalanAda()
        {
            string folderPath = Path.Combine(_projectRootPath, "FiturHafalan");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        //teknik runtime
        private async Task LoadAllSurahsAsync()
        {
            try
            {
                // Menggunakan API: Melakukan panggilan HTTP GET
                HttpResponseMessage response = await _httpClient.GetAsync(ApiSurahUrl);
                response.EnsureSuccessStatusCode(); 

                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Code Reuse/Library: Menggunakan Newtonsoft.Json untuk deserialisasi
                _allSurahs = JsonConvert.DeserializeObject<List<Surah>>(jsonResponse);

                if (_allSurahs == null || _allSurahs.Count == 0)
                {
                    MessageBox.Show("Tidak ada data Surah yang ditemukan dari API.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Kesalahan jaringan saat memuat Surah: {httpEx.Message}", "Error Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allSurahs = new List<Surah>(); // Inisialisasi kosong jika terjadi kesalahan
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"Kesalahan saat memproses data Surah (JSON): {jsonEx.Message}", "Error Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allSurahs = new List<Surah>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan tak terduga saat memuat Surah: {ex.Message}", "Error Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allSurahs = new List<Surah>();
            }
        }

        // Metode untuk mengisi ComboBox Surah dan mengatur nilai awal
        private void InitializeSurahComboBoxes()
        {
            if (_allSurahs == null || _allSurahs.Count == 0) return;

            // Parameterization/Generics: Menggunakan DataSource dan properti untuk display/value
            // Untuk comboBox1 (Mulai Surah)
            comboBox1.DisplayMember = "nama_latin";
            comboBox1.ValueMember = "nomor";
            comboBox1.DataSource = new List<Surah>(_allSurahs); // Buat salinan agar ComboBox memiliki DataSource independen
            comboBox1.SelectedIndex = 0;

            // Untuk comboBox3 (Hingga Surah)
            comboBox3.DisplayMember = "nama_latin";
            comboBox3.ValueMember = "nomor";
            comboBox3.DataSource = new List<Surah>(_allSurahs); // Buat salinan lagi
            // Secara default pilih Surah terakhir untuk "Hingga Surah"
            comboBox3.SelectedIndex = comboBox3.Items.Count - 1;
        }

        // Metode untuk mengisi ComboBox ayat berdasarkan Surah yang dipilih
        private void PopulateAyatComboBox(ComboBox ayatComboBox, Surah selectedSurah, bool selectLast = false)
        {
            ayatComboBox.Items.Clear();
            if (selectedSurah == null) return;

            for (int i = 1; i <= selectedSurah.jumlah_ayat; i++)
            {
                ayatComboBox.Items.Add(i);
            }

            if (ayatComboBox.Items.Count > 0)
            {
                ayatComboBox.SelectedIndex = selectLast ? ayatComboBox.Items.Count - 1 : 0;
            }
        }

        private void PopulateAyatForMulaiSurah()
        {
            if (comboBox1.SelectedItem is Surah selectedSurahMulai)
            {
                PopulateAyatComboBox(comboBox4, selectedSurahMulai, false); // Pilih ayat pertama
            }
        }

        // Wrapper untuk mengisi ayat hingga
        private void PopulateAyatForHinggaSurah()
        {
            if (comboBox3.SelectedItem is Surah selectedSurahHingga)
            {
                PopulateAyatComboBox(comboBox2, selectedSurahHingga, true); // Pilih ayat terakhir
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAyatForMulaiSurah();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAyatForHinggaSurah();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SimpanHafalanKeFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(daftarHafalan, Formatting.Indented);
                File.WriteAllText(_pathHafalanFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan hafalan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MuatHafalanDariFile()
        {
            try
            {
                if (File.Exists(_pathHafalanFile))
                {
                    string json = File.ReadAllText(_pathHafalanFile);
                    daftarHafalan = JsonConvert.DeserializeObject<List<HafalanData>>(json) ?? new List<HafalanData>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data hafalan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                daftarHafalan = new List<HafalanData>(); // fallback
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is Surah surahAwal && comboBox3.SelectedItem is Surah surahAkhir &&
                comboBox4.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                var hafalanBaru = new HafalanData
                {
                    TanggalMulai = dateTimePicker1.Value,
                    TanggalAkhir = dateTimePicker2.Value,
                    SurahAwal = surahAwal.nama_latin,
                    AyatAwal = Convert.ToInt32(comboBox4.SelectedItem),
                    SurahAkhir = surahAkhir.nama_latin,
                    AyatAkhir = Convert.ToInt32(comboBox2.SelectedItem),
                    Status = "Belum Selesai"
                };

                daftarHafalan.Add(hafalanBaru);                
                RefreshDataGrid();
                SimpanHafalanKeFile();
            }
            else
            {
                MessageBox.Show("Pastikan semua pilihan telah dipilih.", "Input tidak lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void RefreshDataGrid()
        {
            dataGridView1.Rows.Clear();

            foreach (var h in daftarHafalan)
            {
                dataGridView1.Rows.Add(
                    h.TanggalMulai.ToShortDateString(),
                    h.TanggalAkhir.ToShortDateString(),
                    h.SurahAwal,
                    h.AyatAwal,
                    h.SurahAkhir,
                    h.AyatAkhir,
                    h.Status
                );
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (e.RowIndex >= 0)
            {
                var columnName = dataGridView1.Columns[e.ColumnIndex].Name;

                if (columnName == "Selesai")
                {
                    if (e.RowIndex < daftarHafalan.Count)
                    {
                        MessageBox.Show("Hafalan ditandai selesai!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        daftarHafalan[e.RowIndex].Status = "Selesai";
                        RefreshDataGrid();
                        SimpanHafalanKeFile();
                    }
                }
                else if (columnName == "Hapus")
                {
                    if (e.RowIndex < daftarHafalan.Count)
                    {
                        daftarHafalan.RemoveAt(e.RowIndex);
                        SimpanHafalanKeFile();
                        RefreshDataGrid();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }
    }
}
