using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using AplikasiAlQur_an.FiturAlQuran; // Tambahkan ini untuk mengakses class Surat Anda

namespace AplikasiAlQur_an.FiturHafalan
{
    public partial class Hafalan : Form
    {
        private List<Surah> _surahs; // Menggunakan class Surat yang sudah ada
        private const string ApiUrl = "https://equran.id/api/surat";

        public Hafalan()
        {
            InitializeComponent();
        }

        // --- HAPUS DEFINISI CLASS SURAT DARI SINI ---
        // Anda sudah memiliki definisi class Surat di folder AlQuran Anda.
        // --- HAPUS DEFINISI CLASS SURAT DARI SINI ---

        private async void Hafalan_Load(object sender, EventArgs e)
        {
            await LoadSurahsAsync();
        }

        private async Task LoadSurahsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ApiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    _surahs = JsonConvert.DeserializeObject<List<Surah>>(responseBody); // Menggunakan class Surat yang sudah ada

                    if (_surahs != null)
                    {
                        // ----- Isi comboBox1 (Mulai Surah) -----
                        comboBox1.DisplayMember = "nama_latin";
                        comboBox1.ValueMember = "nomor";
                        // Penting: Buat salinan list agar ComboBox memiliki DataSource sendiri
                        comboBox1.DataSource = new List<Surah>(_surahs);

                        // ----- Isi comboBox3 (Hingga Surah) -----
                        comboBox3.DisplayMember = "nama_latin";
                        comboBox3.ValueMember = "nomor";
                        // Penting: Buat salinan list agar ComboBox memiliki DataSource sendiri
                        comboBox3.DataSource = new List<Surah>(_surahs);

                        // Atur item pertama sebagai yang terpilih jika ada
                        if (comboBox1.Items.Count > 0)
                        {
                            comboBox1.SelectedIndex = 0;
                            // Panggil secara manual untuk mengisi comboBox4 saat form load
                            PopulateAyatForMulaiSurah();
                        }
                        if (comboBox3.Items.Count > 0)
                        {
                            comboBox3.SelectedIndex = comboBox3.Items.Count - 1; // Pilih surah terakhir sebagai default
                            // Panggil secara manual untuk mengisi comboBox2 saat form load
                            PopulateAyatForHinggaSurah();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gagal memuat data Surah.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error Permintaan HTTP: {httpEx.Message}", "Error Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"Error Deserialisasi JSON: {jsonEx.Message}", "Error Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan tak terduga: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateAyatForMulaiSurah()
        {
            if (comboBox1.SelectedItem is Surah selectedSurahMulai) // Menggunakan class Surat yang sudah ada
            {
                comboBox4.Items.Clear();
                for (int i = 1; i <= selectedSurahMulai.jumlah_ayat; i++)
                {
                    comboBox4.Items.Add(i);
                }
                if (comboBox4.Items.Count > 0)
                {
                    comboBox4.SelectedIndex = 0;
                }
            }
        }

        private void PopulateAyatForHinggaSurah()
        {
            if (comboBox3.SelectedItem is Surah selectedSurahHingga) // Menggunakan class Surat yang sudah ada
            {
                comboBox2.Items.Clear();
                for (int i = 1; i <= selectedSurahHingga.jumlah_ayat; i++)
                {
                    comboBox2.Items.Add(i);
                }
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
    }
}