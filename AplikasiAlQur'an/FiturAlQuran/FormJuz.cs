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

namespace AplikasiAlQur_an.FiturAlQuran
{
    public partial class FormJuz: Form
    {
        private JuzInfo selectedJuz;
        public FormJuz(JuzInfo juzInfo)
        {
            InitializeComponent();
            selectedJuz = juzInfo;
            this.Load += FormJuz_Load;
        }

        private void richJuz_TextChanged(object sender, EventArgs e)
        {
        }

        private async void FormJuz_Load(object sender, EventArgs e)
        {
            label1.Text = $"Juz {selectedJuz.Juz}: QS {selectedJuz.Surah}:{selectedJuz.Ayat}";
            await LoadIsiJuzAsync();
        }

        private void listSurahInJuz_DoubleClick(object sender, EventArgs e)
        {            
        }

        private async Task LoadIsiJuzAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    richJuz.Clear();

                    var currentJuzIndex = selectedJuz.Juz - 1;
                    var start = selectedJuz;
                    var end = (currentJuzIndex + 1 < 30) ? AlQuran.daftarJuz[currentJuzIndex + 1] : null;

                    int endSurah = end?.Surah ?? 114;
                    int endAyat = end?.Ayat - 1 ?? int.MaxValue;

                    for (int surah = start.Surah; surah <= endSurah; surah++)
                    {
                        string url = $"https://equran.id/api/surat/{surah}";
                        string response = await client.GetStringAsync(url);
                        var detail = JsonConvert.DeserializeObject<SurahDetail>(response);

                        var ayatStart = (surah == start.Surah) ? start.Ayat : 1;
                        var ayatEnd = (surah == endSurah) ? endAyat : detail.ayat.Count;

                        var ayatList = detail.ayat.Where(a => a.nomor >= ayatStart && a.nomor <= ayatEnd).ToList();

                        foreach (var ayat in ayatList)
                        {
                            richJuz.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
                            richJuz.AppendText($"[{detail.nama_latin} - {ayat.nomor}]");

                            richJuz.SelectionFont = new Font("Traditional Arabic", 16);
                            richJuz.SelectionAlignment = HorizontalAlignment.Right;
                            richJuz.AppendText($"\n{ayat.ar}\n");

                            richJuz.SelectionFont = new Font("Segoe UI", 10, FontStyle.Italic);
                            richJuz.SelectionAlignment = HorizontalAlignment.Left;
                            richJuz.AppendText($"{ayat.idn}\n\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil data Juz: " + ex.Message);
                }
            }
        }

        private void FormJuz_Load_1(object sender, EventArgs e)
        {

        }

        private void richJuz_Click(object sender, EventArgs e)
        {

        }

        private void listJuz_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int currentIndex = AlQuran.daftarJuz.FindIndex(j => j.Juz == selectedJuz.Juz);
            if (currentIndex < AlQuran.daftarJuz.Count - 1)
            {
                var nextJuz = AlQuran.daftarJuz[currentIndex + 1];
                var newForm = new FormJuz(nextJuz);
                this.Hide();
                newForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sudah di Juz terakhir.");
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int currentIndex = AlQuran.daftarJuz.FindIndex(j => j.Juz == selectedJuz.Juz);
            if (currentIndex > 0)
            {
                var prevJuz = AlQuran.daftarJuz[currentIndex - 1];
                var newForm = new FormJuz(prevJuz);
                this.Hide();
                newForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sudah di Juz pertama.");
            }
        }
    }
}
