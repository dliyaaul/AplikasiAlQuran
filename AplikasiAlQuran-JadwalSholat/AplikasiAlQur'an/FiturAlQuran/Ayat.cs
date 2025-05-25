using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiAlQur_an
{
    public class Ayat
    {
        public int nomor { get; set; }
        public string ar { get; set; }          // Teks Arab
        public string idn { get; set; }          // Terjemahan
        public string tr { get; set; }
    }
}
