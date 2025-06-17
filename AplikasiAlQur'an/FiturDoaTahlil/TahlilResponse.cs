using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplikasiAlQur_an.FiturDoaTahlil
{
    public class TahlilResponse
    {
        public int code { get; set; }
        public string status { get; set; }
        public List<TahlilItem> data { get; set; }
    }
}
