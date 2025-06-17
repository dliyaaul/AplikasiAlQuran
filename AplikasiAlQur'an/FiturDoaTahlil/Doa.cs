using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiAlQur_an.FiturDoaTahlil
{
    public class Doa
    {
        public string judul { get; set; }
        public string arab { get; set; }
        public string latin { get; set; }
        public string terjemah { get; set; }

        public override string ToString()
        {
            return judul;
        }
    }
}
