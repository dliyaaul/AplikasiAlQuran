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

namespace AplikasiAlQur_an.FiturDoaTahlil
{
    public partial class DoaTahlil: Form
    {
        private List<Doa> semuaDoa = new List<Doa>();

        public DoaTahlil()
        {
            InitializeComponent();
            this.Load += DoaTahlil_Load;
        }

        private async void DoaTahlil_Load(object sender, EventArgs e)
        {
            await LoadDoaAsync();
            await LoadTahlilAsync();
        }

        private async Task LoadDoaAsync()
        {
            string url = "https://open-api.my.id/api/doa";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url);
                    semuaDoa = JsonConvert.DeserializeObject<List<Doa>>(response);

                    listBox1.Items.Clear();
                    foreach (var doa in semuaDoa)
                    {
                        listBox1.Items.Add(doa); // akan menampilkan nama doa via ToString()
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat doa: " + ex.Message);
            }
        }
        private async Task LoadTahlilAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string json = await client.GetStringAsync("https://islamic-api.vwxyz.id/tahlil");
                    var semuaTahlil = JsonConvert.DeserializeObject<TahlilResponse>(json);

                    StringBuilder sb = new StringBuilder();
                    int index = 1;
                    foreach (var item in semuaTahlil.data)
                    {
                        sb.AppendLine($"Bacaan ke-{index++}: {item.judul}");
                        sb.AppendLine();
                        sb.AppendLine(item.arab); // Teks arab, bisa kanan kalau mau
                        sb.AppendLine();
                        sb.AppendLine("Arti:");
                        sb.AppendLine(item.id);
                        sb.AppendLine();
                        sb.AppendLine(new string('-', 50));
                        sb.AppendLine();
                    }

                    richTextBox2.Text = sb.ToString();
                    richTextBox2.SelectionStart = 0;
                    richTextBox2.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat tahlil: " + ex.Message);
            }
        }        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Doa selectedDoa)
            {
                // Kosongkan dulu isinya
                richTextBox1.Clear();

                // Format Nama Doa - Tengah
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.SelectionFont = new Font("Segoe UI", 14, FontStyle.Bold);
                richTextBox1.AppendText(selectedDoa.judul + Environment.NewLine + Environment.NewLine);

                // Format Ayat Arab - Kanan
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                richTextBox1.SelectionFont = new Font("Segoe UI", 16, FontStyle.Regular);
                richTextBox1.AppendText(selectedDoa.arab + Environment.NewLine + Environment.NewLine);

                // Format Latin - Kiri
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                richTextBox1.SelectionFont = new Font("Segoe UI", 12, FontStyle.Italic);
                richTextBox1.AppendText(selectedDoa.latin + Environment.NewLine + Environment.NewLine);

                // Format Terjemah - Kiri
                richTextBox1.SelectionFont = new Font("Segoe UI", 12, FontStyle.Regular);
                richTextBox1.AppendText(selectedDoa.terjemah);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }
    }
}
