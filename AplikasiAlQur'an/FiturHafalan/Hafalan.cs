using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using AplikasiAlQur_an.FiturAlQuran;
using AplikasiAlQur_an.FiturDashboard;

namespace AplikasiAlQur_an.FiturHafalan
{
    public partial class Hafalan : Form
    {
        // ----------------------------------------------------
        // Tambahan untuk Komunikasi dengan Dashboard (Event)
        // ----------------------------------------------------
        public delegate void HafalanSavedEventHandler(object sender, HafalanEntry e);
        public event HafalanSavedEventHandler OnHafalanSaved;
        // ----------------------------------------------------

        private List<Surah> _allSurahs;
        private readonly HttpClient _httpClient;
        private const string ApiSurahUrl = "https://equran.id/api/surat";

        public Hafalan()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void Hafalan_Load(object sender, EventArgs e)
        {
            await LoadAllSurahsAsync();
            InitializeSurahComboBoxes();
            PopulateAyatForMulaiSurah();
            PopulateAyatForHinggaSurah();
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
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Kesalahan jaringan saat memuat Surah: {httpEx.Message}", "Error Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allSurahs = new List<Surah>();
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

        private void InitializeSurahComboBoxes()
        {
            if (_allSurahs == null || _allSurahs.Count == 0) return;

            comboBox1.DisplayMember = "nama_latin";
            comboBox1.ValueMember = "nomor";
            comboBox1.DataSource = new List<Surah>(_allSurahs);
            comboBox1.SelectedIndex = 0;

            comboBox3.DisplayMember = "nama_latin";
            comboBox3.ValueMember = "nomor";
            comboBox3.DataSource = new List<Surah>(_allSurahs);
            comboBox3.SelectedIndex = comboBox3.Items.Count - 1;
        }

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAyatForMulaiSurah();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAyatForHinggaSurah();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        // ----------------------------------------------------
        // Logic saat tombol "Mulai Hafalan" diklik
        // ----------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Ambil semua data dari kontrol UI
            DateTime tanggalMulai = dateTimePicker1.Value;
            DateTime tanggalBerakhir = dateTimePicker2.Value;

            Surah surahMulai = comboBox1.SelectedItem as Surah;
            int ayatMulai = Convert.ToInt32(comboBox4.SelectedItem);

            Surah surahHingga = comboBox3.SelectedItem as Surah;
            int ayatHingga = Convert.ToInt32(comboBox2.SelectedItem);

            // 2. Lakukan Validasi (Penting!)
            if (surahMulai == null || surahHingga == null)
            {
                MessageBox.Show("Harap pilih Surah mulai dan Surah hingga.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox4.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Harap pilih Ayat mulai dan Ayat hingga.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tanggalMulai > tanggalBerakhir)
            {
                MessageBox.Show("Tanggal mulai tidak boleh melebihi tanggal berakhir.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi logika rentang surah/ayat
            if (surahMulai.nomor > surahHingga.nomor)
            {
                MessageBox.Show("Surah Mulai tidak boleh setelah Surah Hingga.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (surahMulai.nomor == surahHingga.nomor)
            {
                if (ayatMulai > ayatHingga)
                {
                    MessageBox.Show("Jika Surah sama, Ayat Mulai tidak boleh setelah Ayat Hingga.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // 3. Buat objek HafalanEntry
            HafalanEntry newHafalan = new HafalanEntry
            {
                TanggalMulai = tanggalMulai,
                TanggalBerakhir = tanggalBerakhir,
                SurahMulaiNama = surahMulai.nama_latin,
                SurahMulaiNomor = surahMulai.nomor,
                AyatMulai = ayatMulai,
                SurahHinggaNama = surahHingga.nama_latin,
                SurahHinggaNomor = surahHingga.nomor,
                AyatHingga = ayatHingga
            };

            // 4. Picu Event untuk memberi tahu Dashboard
            // Pastikan event OnHafalanSaved tidak null sebelum dipicu
            OnHafalanSaved?.Invoke(this, newHafalan);

            // 5. Beri umpan balik ke pengguna
            MessageBox.Show("Hafalan berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 6. Tutup form Hafalan setelah data disimpan
            this.Close();
        }

        // Metode yang sudah ada (biarkan kosong jika tidak ada logika)
        private void label1_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
    }
}