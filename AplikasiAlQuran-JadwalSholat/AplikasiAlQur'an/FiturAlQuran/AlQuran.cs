using AplikasiAlQur_an.FiturAlQuran;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiAlQur_an
{
    public partial class AlQuran: Form
    {
        public AlQuran()
        {
            InitializeComponent();
            this.Load += AlQuran_Load;
            listJuz.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            foreach (var juz in daftarJuz)
            {
                listJuz.Items.Add($"Juz {juz.Juz}: QS {juz.Surah}:{juz.Ayat}");
            }
        }

        private List<Surah> allSurah = new List<Surah>();
        public static List<JuzInfo> daftarJuz = new List<JuzInfo>
        {
            new JuzInfo { Juz = 1, Surah = 1, Ayat = 1 },
            new JuzInfo { Juz = 2, Surah = 2, Ayat = 142 },
            new JuzInfo { Juz = 3, Surah = 2, Ayat = 253 },
            new JuzInfo { Juz = 4, Surah = 3, Ayat = 92 },
            new JuzInfo { Juz = 5, Surah = 4, Ayat = 24 },
            new JuzInfo { Juz = 6, Surah = 4, Ayat = 148 },
            new JuzInfo { Juz = 7, Surah = 5, Ayat = 83 },
            new JuzInfo { Juz = 8, Surah = 6, Ayat = 111 },
            new JuzInfo { Juz = 9, Surah = 7, Ayat = 88 },
            new JuzInfo { Juz = 10, Surah = 8, Ayat = 41 },
            new JuzInfo { Juz = 11, Surah = 9, Ayat = 93 },
            new JuzInfo { Juz = 12, Surah = 11, Ayat = 6 },
            new JuzInfo { Juz = 13, Surah = 12, Ayat = 53 },
            new JuzInfo { Juz = 14, Surah = 15, Ayat = 1 },
            new JuzInfo { Juz = 15, Surah = 17, Ayat = 1 },
            new JuzInfo { Juz = 16, Surah = 18, Ayat = 75 },
            new JuzInfo { Juz = 17, Surah = 21, Ayat = 1 },
            new JuzInfo { Juz = 18, Surah = 23, Ayat = 1 },
            new JuzInfo { Juz = 19, Surah = 25, Ayat = 21 },
            new JuzInfo { Juz = 20, Surah = 27, Ayat = 56 },
            new JuzInfo { Juz = 21, Surah = 29, Ayat = 46 },
            new JuzInfo { Juz = 22, Surah = 33, Ayat = 31 },
            new JuzInfo { Juz = 23, Surah = 36, Ayat = 28 },
            new JuzInfo { Juz = 24, Surah = 39, Ayat = 32 },
            new JuzInfo { Juz = 25, Surah = 41, Ayat = 47 },
            new JuzInfo { Juz = 26, Surah = 46, Ayat = 1 },
            new JuzInfo { Juz = 27, Surah = 51, Ayat = 31 },
            new JuzInfo { Juz = 28, Surah = 58, Ayat = 1 },
            new JuzInfo { Juz = 29, Surah = 67, Ayat = 1 },
            new JuzInfo { Juz = 30, Surah = 78, Ayat = 1 },
        };

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        private void AlQuran_Load(object sender, EventArgs e)
        {
            _ = LoadSurahFromApiAsync();
            LoadJuzFromTable();
            LoadBookmarks();
        }

        private async Task LoadSurahFromApiAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync("https://equran.id/api/surat");

                    allSurah = JsonConvert.DeserializeObject<List<Surah>>(response);

                    listSurah.Items.Clear();
                    foreach (var surah in allSurah)
                    {
                        listSurah.Items.Add($"{surah.nomor}. {surah.nama_latin} - {surah.arti}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchSurah.Text.Trim().ToLower();
            var filteredSurah = allSurah
            .Where(s => s.nama_latin.ToLower().Contains(keyword))
            .ToList();

            listSurah.Items.Clear();
            foreach (var surah in filteredSurah)
            {
                listSurah.Items.Add($"{surah.nomor}. {surah.nama_latin} - {surah.arti}");
            }

            if (filteredSurah.Count == 0)
            {
                MessageBox.Show("Surah tidak ditemukan.", "Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void listSurah_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listSurah.SelectedIndex == -1) return;

            string selected = listSurah.SelectedItem.ToString();
            int dotIndex = selected.IndexOf('.');
            if (dotIndex == -1) return;

            int nomorSurah = int.Parse(selected.Substring(0, dotIndex));

            FormAyat formAyat = new FormAyat(nomorSurah);
            formAyat.ShowDialog(); // tampilkan form baru
        }
        private void LoadJuzFromTable()
        {
            listJuz.Items.Clear();
            foreach (var juz in daftarJuz)
            {
                listJuz.Items.Add($"Juz {juz.Juz} - QS. {juz.Surah}:{juz.Ayat}");
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listJuz.SelectedIndex;
            if (index >= 0 && index < daftarJuz.Count)
            {
                var selected = daftarJuz[index];
                FormJuz form = new FormJuz(selected);
                form.ShowDialog();

                listJuz.SelectedIndex = -1; // Reset selected index
            }
        }

        private void AlQuran_Load_1(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {           
        }
        private void LoadBookmarks()
        {
            listBookmark.Items.Clear();
            if (Properties.Settings.Default.Bookmarks != null)
            {
                foreach (var item in Properties.Settings.Default.Bookmarks)
                {
                    listBookmark.Items.Add(item);
                }
            }        
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Penanda" || tabControl1.SelectedIndex == 3)
            {
                LoadBookmarks();
            }
        }

        private void listBookmark_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
