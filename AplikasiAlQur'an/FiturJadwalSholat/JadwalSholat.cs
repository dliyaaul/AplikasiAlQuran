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
using Newtonsoft.Json;
using AplikasiAlQur_an.FiturJadwalSholat;
using static AplikasiAlQur_an.FiturJadwalSholat.APILokasi;

namespace AplikasiAlQur_an
{
    public partial class JadwalSholat: Form
    {
        public JadwalSholat()
        {
            InitializeComponent();
            timer1.Start();

        }

        private async void JadwalSholat_Load(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            string hijriDate = HijriLib.GetHijriDate(dateTimePicker1.Value);
            TanggalHijri.Text = "✨ " + hijriDate+ " ✨";
            PrayerTimesResponse prayerTimes = await APIJadwalSholat.GetJadwalSholat(selectedDate);
            LocationResponse locationResponse = await APILokasi.GetCoordinatesFromIp();

            label15.Text = label15.Text + " " + locationResponse.city +", "+ locationResponse.region;

            label7.Text = prayerTimes.data.timings.Fajr;
            label9.Text = prayerTimes.data.timings.Sunrise;
            label10.Text = prayerTimes.data.timings.Dhuhr;
            label11.Text = prayerTimes.data.timings.Asr;
            label12.Text = prayerTimes.data.timings.Maghrib;
            label13.Text = prayerTimes.data.timings.Isha;

            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            panel3.Location = new Point((formWidth - panel3.Width) / 2, (formHeight - panel3.Height));
            panel8.Location = new Point(panel3.Location.X, panel3.Location.Y - 120);
            panel8.Size = new Size(panel3.Width, panel8.Height);

            label1.Location = new Point(label1.Location.X, (panel1.Height - label1.Height) / 2);
            label2.Location = label1.Location;
            label3.Location = label1.Location;
            label4.Location = label1.Location;
            label5.Location = label1.Location;
            label6.Location = label1.Location;

            label7.Location = new Point(panel1.Width - 70, (panel1.Height - label1.Height) / 2);
            label9.Location = label7.Location;
            label10.Location = label7.Location;
            label11.Location = label7.Location;
            label12.Location = label7.Location;
            label13.Location = label7.Location;
            //label15.Padding = new Padding(0,2,0,0);
            Judul.Location = new Point((formWidth - Judul.Width)/2, Judul.Location.Y);
            Tanggalan.Height = PanahKiri.Height + 7;
            Tanggalan.Location = new Point((formWidth - Tanggalan.Width)  / 2, panel8.Location.Y + panel8.Height );

            //PanahKiri.Location = new Point((panel8.Width - dateTimePicker1.Width) / 2 - PanahKiri.Width, dateTimePicker1.Location.Y - 7);
            //PanahKanan.Location = new Point(Tanggalan.Width - PanahKanan.Width, PanahKiri.Location.Y);
            label15.Location = new Point(Tanggalan.Location.X, label15.Location.Y );
            Tanggal.Text = dateTimePicker1.Value.ToString("dddd, dd MMMM yyyy");
            Console.WriteLine(Judul.Location);
            
            dateTimePicker1.Location = new Point((Tanggalan.Width - Tanggal.Width) / 2, PanahKiri.Location.Y + 7 + (1 / 2));
            Tanggal.Location = new Point(dateTimePicker1.Location.X + 20, PanahKiri.Location.Y + 7 + (1 / 2));
            Jam.Location = new Point(panel8.Width - Jam.Width, label15.Location.Y);
            TanggalHijri.Location = new Point((panel8.Width - TanggalHijri.Width)/2, (panel8.Height - Tanggal.Height)/2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private async void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Tanggal.Text = dateTimePicker1.Value.ToString("dddd, dd MMMM yyyy");
            DateTime selectedDate = dateTimePicker1.Value;
            string hijriDate = HijriLib.GetHijriDate(dateTimePicker1.Value);
            TanggalHijri.Text = "✨ " + hijriDate + " ✨";
            TanggalHijri.Location = new Point((panel8.Width - TanggalHijri.Width) / 2, (panel8.Height - Tanggal.Height) / 2);
            PrayerTimesResponse prayerTimes = await APIJadwalSholat.GetJadwalSholat(selectedDate);
            label7.Text = prayerTimes.data.timings.Fajr;
            label9.Text = prayerTimes.data.timings.Sunrise;
            label10.Text = prayerTimes.data.timings.Dhuhr;
            label11.Text = prayerTimes.data.timings.Asr;
            label12.Text = prayerTimes.data.timings.Maghrib;
            label13.Text = prayerTimes.data.timings.Isha;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void Judul_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_2(object sender, EventArgs e)
        {

        }

        private void label14_Click_1(object sender, EventArgs e)
        {

        }

        private void PanahKanan_Click(object sender, EventArgs e)
        {

        }

        private void Tanggal_Click(object sender, EventArgs e)
        {

        }

        private void PanahKiri_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
        }

        private void PanahKanan_Click_1(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Jam.Text = DateTime.Now.ToString("HH:mm:ss") + " WIB";
        }

        private void Jam_Click(object sender, EventArgs e)
        {

        }

        private void TanggalHijri_Click(object sender, EventArgs e)
        {

        }
    }
}
