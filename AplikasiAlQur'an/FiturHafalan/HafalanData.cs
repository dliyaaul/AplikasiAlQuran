using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiAlQur_an.FiturHafalan
{
    public class HafalanData
    {
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalAkhir { get; set; }
        public String SurahAwal { get; set; }
        public String SurahAkhir { get; set; }
        public int AyatAwal { get; set; }
        public int AyatAkhir { get; set; }
        public String Status { get; set; }
    }
}
