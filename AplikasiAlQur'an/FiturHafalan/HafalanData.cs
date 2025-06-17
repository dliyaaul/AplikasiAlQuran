using System;

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

        public HafalanStatus Status { get; set; }
    }
}