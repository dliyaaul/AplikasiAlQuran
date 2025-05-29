using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiAlQur_an
{
    public static class BookmarkManager
    {
        public static List<string> BookmarkList = new List<string>();

        public static void TambahBookmark(string ayat)
        {
            if (!BookmarkList.Contains(ayat))
                BookmarkList.Add(ayat);
        }

        public static void HapusBookmark(string ayat)
        {
            BookmarkList.Remove(ayat);
        }

        public static List<string> AmbilBookmark()
        {
            return BookmarkList;
        }
    }
}
