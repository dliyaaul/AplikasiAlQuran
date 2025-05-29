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
    public partial class FormAyat: Form
    {
        private int nomorSurah;
        private int mulaiAyat = 1;
        private List<Ayat> semuaAyat = new List<Ayat>();

        public FormAyat(int nomorSurah)
        {
            InitializeComponent();
            this.nomorSurah = nomorSurah;
            this.Load += FormAyat_Load;
            richAyat.MouseDoubleClick += richAyat_MouseDoubleClick;
        }
        private async void FormAyat_Load(object sender, EventArgs e)
        {
            await LoadSurahAsync();
        }

        private async Task LoadSurahAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://equran.id/api/surat/{nomorSurah}";
                    string response = await client.GetStringAsync(url);

                    SurahDetail detail = JsonConvert.DeserializeObject<SurahDetail>(response);
                    semuaAyat = detail.ayat;

                    label1.Text = $"{detail.nama_latin} - {detail.nama}";

                    richAyat.Clear();

                    foreach (var ayat in detail.ayat.Where(a => a.nomor >= mulaiAyat))
                    {
                        richAyat.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
                        richAyat.AppendText($"[{ayat.nomor}]\n");

                        richAyat.SelectionFont = new Font("Traditional Arabic", 16, FontStyle.Regular);
                        richAyat.SelectionAlignment = HorizontalAlignment.Right;
                        richAyat.AppendText($"{ayat.ar}\n");

                        richAyat.SelectionAlignment = HorizontalAlignment.Left;
                        richAyat.SelectionFont = new Font("Segoe UI", 10, FontStyle.Italic);
                        richAyat.AppendText($"{ayat.idn}\n\n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil ayat: " + ex.Message);
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if(nomorSurah < 114)
            {
                nomorSurah++;
                await LoadSurahAsync();
            }
            else
            {
                MessageBox.Show("Ini adalah surah terakhir.");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (nomorSurah > 1)
            {
                nomorSurah--;
                await LoadSurahAsync();
            }
            else
            {
                MessageBox.Show("Ini adalah surah pertama.");
            }         
        }

        private void FormAyat_Load_1(object sender, EventArgs e)
        {

        }

        private void richAyat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = richAyat.GetCharIndexFromPosition(e.Location);

            // Pastikan index tidak out of bounds
            if (index < 0 || index >= richAyat.Text.Length)
            {
                MessageBox.Show("Silakan klik langsung pada teks ayat (bukan area kosong).", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ambil kata/kalimat yang diklik
            string clickedText = GetWordAtIndex(richAyat.Text, index).Trim();

            if (string.IsNullOrEmpty(clickedText))
            {
                MessageBox.Show("Silakan klik langsung pada teks ayat (bukan area kosong).", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Coba cocokkan dengan ayat
            var ayatDitemukan = semuaAyat.FirstOrDefault(a =>
                clickedText.Contains($"[{a.nomor}]") ||
                a.ar.Contains(clickedText) ||
                a.idn.Contains(clickedText));

            if (ayatDitemukan != null)
            {
                string namaSurah = label1.Text;
                string dataBookmark = $"[{namaSurah} : {ayatDitemukan.nomor}] {ayatDitemukan.ar}";

                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Bagikan", null, (s, ea) => BagikanAyat(dataBookmark));
                menu.Items.Add("Tandai Terakhir Dibaca", null, (s, ea) => TandaiTerakhirDibaca(dataBookmark));
                menu.Items.Add("Simpan ke Bookmark", null, (s, ea) => SimpanBookmark(dataBookmark));
                menu.Show(Cursor.Position);
            }
            else
            {
                MessageBox.Show("Gagal menemukan nomor ayat. Coba klik langsung teks Arab ayat.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetWordAtIndex(string text, int index)
        {
            if (string.IsNullOrEmpty(text) || index >= text.Length)
                return string.Empty;

            int start = index;
            int end = index;

            // Geser ke kiri sampai ketemu spasi
            while (start > 0 && !char.IsWhiteSpace(text[start - 1]))
                start--;

            // Geser ke kanan sampai ketemu spasi
            while (end < text.Length && !char.IsWhiteSpace(text[end]))
                end++;

            return text.Substring(start, end - start);
        }

        private void BagikanAyat(string ayatText)
        {
            try
            {
                Clipboard.SetText(ayatText);
                MessageBox.Show("Ayat disalin ke clipboard. Silakan tempelkan (paste) di tempat lain untuk membagikan.", "Bagikan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyalin ayat: " + ex.Message);
            }
        }

        private void TandaiTerakhirDibaca(string ayatText)
        {
            Properties.Settings.Default.TerakhirDibaca = ayatText;
            Properties.Settings.Default.Save();
            MessageBox.Show("Ayat ditandai sebagai terakhir dibaca.", "Tandai", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SimpanBookmark(string ayatText)
        {
            if (Properties.Settings.Default.Bookmarks == null)
            {
                Properties.Settings.Default.Bookmarks = new System.Collections.Specialized.StringCollection();
            }

            Properties.Settings.Default.Bookmarks.Add(ayatText);
            Properties.Settings.Default.Save();
            MessageBox.Show("Ayat disimpan ke bookmark.", "Bookmark", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
