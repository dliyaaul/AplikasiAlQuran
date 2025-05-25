using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiAlQur_an.FiturHafalan
{
    public partial class Hafalan: Form
    {
        public Hafalan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }
    }
}
