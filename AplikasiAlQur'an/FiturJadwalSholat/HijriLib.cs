using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AplikasiAlQur_an.FiturJadwalSholat
{
    public class HijriLib
    {
        private static readonly Dictionary<int, string> BulanHijri = new Dictionary<int, string>
        {
            { 1, "Muharram" },
            { 2, "Safar" },
            { 3, "Rabi'ul Awal" },
            { 4, "Rabi'ul Akhir" },
            { 5, "Jumada'ul Awal" },
            { 6, "Jumada'ul Akhir" },
            { 7, "Rajab" },
            { 8, "Sha'ban" },
            { 9, "Ramadan" },
            { 10, "Shawwal" },
            { 11, "Dhul-Qa'dah" },
            { 12, "Dhul-Hijjah" }
        };

        public static string GetHijriDate(DateTime TanggalanBiasa)
        {
            HijriCalendar TanggalanHijri = new HijriCalendar();

            int TahunHijri = TanggalanHijri.GetYear(TanggalanBiasa);
            int BulanHijriyah = TanggalanHijri.GetMonth(TanggalanBiasa);
            int HariHijri = TanggalanHijri.GetDayOfMonth(TanggalanBiasa);

            if (BulanHijri.TryGetValue(BulanHijriyah, out string monthName))
            {
                return $"{HariHijri} {monthName} {TahunHijri}";
            }
            else
            {
                return $"{HariHijri} UnknownMonth {TahunHijri}";
            }
        }
    }
}
