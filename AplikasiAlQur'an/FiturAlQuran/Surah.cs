using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiAlQur_an
{
    public class Surah
    {

        [JsonProperty("nomor")]
        public int Nomor { get; set; }

        [JsonProperty("nama")]
        public string Nama { get; set; }

        [JsonProperty("nama_latin")]
        public string NamaLatin { get; set; }

        [JsonProperty("jumlah_ayat")]
        public int JumlahAyat { get; set; }

        [JsonProperty("tempat_turun")]
        public string TempatTurun { get; set; }

        [JsonProperty("arti")]
        public string Arti { get; set; }

        public override string ToString()
        {
            return this.NamaLatin;
        }
    }
}