using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using AplikasiAlQur_an.FiturAlQuran; // Menggunakan class Surah dari namespace ini

namespace AplikasiAlQur_an.FiturHafalan
{
    public partial class Hafalan : Form
    {
        private List<Surah> _allSurahs; // Menyimpan semua data Surah yang dimuat dari API
        private readonly HttpClient _httpClient; // Gunakan satu instance HttpClient untuk menghindari socket exhaustion

        // URL API 
        private const string ApiSurahUrl = "https://equran.id/api/surat";

        public Hafalan()
        {
            InitializeComponent();
            _httpClient = new HttpClient(); // Inisialisasi HttpClient sekali
        }

        private async void Hafalan_Load(object sender, EventArgs e)
        {
            // Panggil metode asinkron untuk memuat data Surah
            await LoadAllSurahsAsync();

            // Setelah semua Surah dimuat, isi ComboBox Surah awal
            InitializeSurahComboBoxes();

            // Inisialisasi ComboBox ayat setelah Surah terpilih
            PopulateAyatForMulaiSurah();
            PopulateAyatForHinggaSurah();
        }

        // Metode untuk memuat semua data Surah dari API
        private async Task LoadAllSurahsAsync()
        {
            try
            {
                // Menggunakan API: Melakukan panggilan HTTP GET
                HttpResponseMessage response = await _httpClient.GetAsync(ApiSurahUrl);
                response.EnsureSuccessStatusCode(); // Akan melempar HttpRequestException jika status kode bukan sukses

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

        // Event handler untuk ComboBox Mulai Surah
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAyatForMulaiSurah();
        }

        // Event handler untuk ComboBox Hingga Surah
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAyatForHinggaSurah();
        }

        // Wrapper untuk mengisi ayat mulai
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

        // Metode yang sudah ada
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
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
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { } // Logika sudah dihandle di PopulateAyatForHinggaSurah
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { } // Logika sudah dihandle di PopulateAyatForMulaiSurah
        private void button2_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
    }
}