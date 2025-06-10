using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AplikasiAlQur_an.FiturAlQuran;

namespace AplikasiAlQur_an.FiturHafalan
{
    public partial class Hafalan: Form
    {
            private List<SurahDetail> SurahDetailList = new List<SurahDetail>();
            private List<Surah> SurahList;

            public Hafalan()
            {
                InitializeComponent();
                this.SurahList = new List<Surah>();
                InitializeDropdowns();
            }

            public void SetSurahDetails(List<SurahDetail> details)
            {
                this.SurahDetailList = details;
            }


        private void InitializeDropdowns()
        {
            comboBox1.DataSource = SurahList;
            comboBox1.DisplayMember = "nama_latin";
            comboBox1.ValueMember = "nomor";
        }        
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedSurahNumber = (int)comboBox1.SelectedValue;

            // Ambil Surah yang sesuai berdasarkan nama_latin
            var selectedSurahName = ((Surah)comboBox1.SelectedItem).nama_latin;
            var selectedDetail = SurahDetailList.FirstOrDefault(s => s.nama_latin == selectedSurahName);

            if (selectedDetail != null && selectedDetail.ayat != null)
            {
                comboBox2.DataSource = selectedDetail.ayat;
                comboBox2.DisplayMember = "nomor"; // Nomor ayat yang ditampilkan
                comboBox2.ValueMember = "nomor";
            }
            else
            {
                comboBox2.DataSource = null;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string namaSurah = ((Surah)comboBox1.SelectedItem).nama_latin;
            int nomorAyat = (int)comboBox2.SelectedValue;
            DateTime tanggalMulai = dateTimePicker1.Value;
            DateTime tanggalTarget = dateTimePicker2.Value;

            MessageBox.Show($"Hafalan disimpan:\nSurah: {namaSurah}\nAyat: {nomorAyat}\nMulai: {tanggalMulai:d}\nTarget: {tanggalTarget:d}");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Hafalan_Load(object sender, EventArgs e)
        {

        }
    }
}
