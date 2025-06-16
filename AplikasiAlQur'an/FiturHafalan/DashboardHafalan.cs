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
    public partial class DashboardHafalan : Form
    {
        private List<Surah> _allSurahs;
        private readonly HttpClient _httpClient;
        private List<HafalanData> daftarHafalan = new List<HafalanData>();
        private readonly string _projectRootPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
        private readonly string _pathHafalanFile;
        private const string ApiSurahUrl = "https://equran.id/api/surat";

        // PERBAIKAN (Teknik Table-Driven):
        // "Tabel" (Dictionary) yang memetakan nama kolom ke sebuah metode (Action).
        private Dictionary<string, Action<int>> _dataGridViewActions;

        public DashboardHafalan()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _pathHafalanFile = Path.Combine(_projectRootPath, "FiturHafalan", "hafalan.json");

            // Inisialisasi "tabel" aksi untuk DataGridView.
            InitializeActionsTable();
            PastikanFolderHafalanAda();
        }

        private void InitializeActionsTable()
        {
            _dataGridViewActions = new Dictionary<string, Action<int>>
            {
                // Mapping: Kolom "Selesai" akan menjalankan metode MarkAsSelesai.
                { "Selesai", MarkAsSelesai },
                // Mapping: Kolom "Hapus" akan menjalankan metode DeleteHafalan.
                { "Hapus", DeleteHafalan }
            };
        }

        private async void DashboardHafalan_Load(object sender, EventArgs e)
        {
            await LoadAllSurahsAsync();
            InitializeSurahComboBoxes();
            PopulateAyatForMulaiSurah();
            PopulateAyatForHinggaSurah();
            InitializeDataGridView();
            MuatHafalanDariFile();
            RefreshDataGrid();
        }

        #region Metode Aksi untuk Table-Driven

        private void MarkAsSelesai(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < daftarHafalan.Count)
            {
                // PERBAIKAN (Teknik Automata): Mengubah state hafalan.
                daftarHafalan[rowIndex].Status = HafalanStatus.Selesai;
                MessageBox.Show("Hafalan ditandai selesai!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAndSave();
            }
        }

        private void DeleteHafalan(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < daftarHafalan.Count)
            {
                if (MessageBox.Show("Anda yakin ingin menghapus target ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    daftarHafalan.RemoveAt(rowIndex);
                    RefreshAndSave();
                }
            }
        }

        private void RefreshAndSave()
        {
            SimpanHafalanKeFile();
            RefreshDataGrid();
        }

        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Abaikan klik pada header

            var columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            // PERBAIKAN (Logika Table-Driven):
            // Mengganti blok if-else dengan pencarian di "tabel" aksi.
            if (_dataGridViewActions.ContainsKey(columnName))
            {
                // Menjalankan aksi yang sesuai dari tabel.
                _dataGridViewActions[columnName].Invoke(e.RowIndex);
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
                    SurahAwal = surahAwal.NamaLatin,
                    AyatAwal = Convert.ToInt32(comboBox4.SelectedItem),
                    SurahAkhir = surahAkhir.NamaLatin,
                    AyatAkhir = Convert.ToInt32(comboBox2.SelectedItem),

                    // PERBAIKAN (Teknik Automata): Mengatur state awal.
                    Status = HafalanStatus.BelumSelesai
                };

                daftarHafalan.Add(hafalanBaru);
                RefreshAndSave();
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
                // PERBAIKAN (Teknik Automata): Mengonversi enum ke string untuk ditampilkan.
                string statusText = h.Status == HafalanStatus.Selesai ? "Selesai" : "Belum Selesai";

                dataGridView1.Rows.Add(
                    h.TanggalMulai.ToShortDateString(),
                    h.TanggalAkhir.ToShortDateString(),
                    h.SurahAwal,
                    h.AyatAwal,
                    h.SurahAkhir,
                    h.AyatAkhir,
                    statusText // Menggunakan statusText yang sudah diproses
                );
            }
        }

        #region Kode Asli (Tidak Diubah)

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

            if (!dataGridView1.Columns.Contains("Selesai"))
            {
                DataGridViewButtonColumn selesaiBtn = new DataGridViewButtonColumn
                {
                    Name = "Selesai",
                    HeaderText = "Aksi",
                    Text = "Selesai",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(selesaiBtn);
            }
            DataGridViewButtonColumn hapusBtn = new DataGridViewButtonColumn
            {
                Name = "Hapus",
                HeaderText = " ",
                Text = "Hapus",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(hapusBtn);
        }

        private void PastikanFolderHafalanAda()
        {
            string folderPath = Path.Combine(_projectRootPath, "FiturHafalan");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private async Task LoadAllSurahsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(ApiSurahUrl);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                _allSurahs = JsonConvert.DeserializeObject<List<Surah>>(jsonResponse);
                if (_allSurahs == null || _allSurahs.Count == 0)
                {
                    MessageBox.Show("Tidak ada data Surah yang ditemukan dari API.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat memuat Surah: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allSurahs = new List<Surah>();
            }
        }

        private void InitializeSurahComboBoxes()
        {
            if (_allSurahs == null || _allSurahs.Count == 0) return;
            comboBox1.DisplayMember = "NamaLatin";
            comboBox1.ValueMember = "Nomor";
            comboBox1.DataSource = new List<Surah>(_allSurahs);
            comboBox1.SelectedIndex = 0;
            comboBox3.DisplayMember = "NamaLatin";
            comboBox3.ValueMember = "Nomor";
            comboBox3.DataSource = new List<Surah>(_allSurahs);
            comboBox3.SelectedIndex = comboBox3.Items.Count - 1;
        }

        private void PopulateAyatComboBox(ComboBox ayatComboBox, Surah selectedSurah, bool selectLast = false)
        {
            ayatComboBox.Items.Clear();
            if (selectedSurah == null) return;
            for (int i = 1; i <= selectedSurah.JumlahAyat; i++)
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
                PopulateAyatComboBox(comboBox4, selectedSurahMulai, false);
            }
        }

        private void PopulateAyatForHinggaSurah()
        {
            if (comboBox3.SelectedItem is Surah selectedSurahHingga)
            {
                PopulateAyatComboBox(comboBox2, selectedSurahHingga, true);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => PopulateAyatForMulaiSurah();
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) => PopulateAyatForHinggaSurah();

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
                daftarHafalan = new List<HafalanData>();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        // PERMINTAAN: Event handler kosong ini sengaja tidak dihapus
        // agar tidak merusak file Desainer Form Anda.
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        #endregion
    }
}