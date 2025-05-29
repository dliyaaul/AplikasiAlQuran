using AplikasiAlQur_an.FiturDashboard;
using AplikasiAlQur_an.FiturHafalan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiAlQur_an
{
    enum AppState
    {
        Exit,
        Form1,
        Form2,
        AlQuran,
        JadwalSholat,
        Hafalan,
        Dashboard
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>        
        [STAThread]

        static AppState RunForm1()
        {
            using (Form1 form = new Form1())
            {
                Application.Run(form);
                return form.DialogResult == DialogResult.OK ? AppState.Form2 : AppState.Exit;
            }
        }

        static AppState RunForm2()
        {
            using (Form2 form = new Form2())
            {
                Application.Run(form);
                if (form.DialogResult == DialogResult.Retry)
                    return AppState.AlQuran; // lanjut ke AlQuran

                if (form.DialogResult == DialogResult.OK)
                    return AppState.JadwalSholat; // kembali ke Form2

                if (form.DialogResult == DialogResult.Cancel)
                    return AppState.Dashboard; // kembali ke Form2

                if (form.DialogResult == DialogResult.Yes)
                    return AppState.Hafalan; // kembali ke Form2

                if (form.DialogResult == DialogResult.Abort)
                    return AppState.Form1; // kembali ke Form1

                return AppState.Exit; // keluar
            }
        }

        static AppState RunAlQuran()
        {
            using (AlQuran form = new AlQuran())
            {
                Application.Run(form);
                return form.DialogResult == DialogResult.Ignore ? AppState.Form2 : AppState.Exit;
            }
        }

        static AppState RunJadwalSholat()
        {
            using (JadwalSholat form = new JadwalSholat())
            {
                Application.Run(form);
                return form.DialogResult == DialogResult.Ignore ? AppState.Form2 : AppState.Exit;
            }
        }

        static AppState RunHafalan()
        {
            using (Hafalan form = new Hafalan())
            {
                Application.Run(form);
                return form.DialogResult == DialogResult.Ignore ? AppState.Form2 : AppState.Exit;
            }
        }

        static AppState RunDashboard()
        {
            using (Dashboard form = new Dashboard())
            {
                Application.Run(form);
                return form.DialogResult == DialogResult.Ignore ? AppState.Form2 : AppState.Exit;
            }
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppState currentState = AppState.Form1;
            
            while (currentState != AppState.Exit)
            {
                switch (currentState)
                {
                    case AppState.Form1:
                        currentState = RunForm1();
                        break;
                    case AppState.Form2:
                        currentState = RunForm2();
                        break;
                    case AppState.AlQuran:
                        currentState = RunAlQuran();
                        break;
                    case AppState.JadwalSholat:
                        currentState = RunJadwalSholat();
                        break;
                    case AppState.Hafalan:
                        currentState = RunHafalan();
                        break;
                    case AppState.Dashboard:
                        currentState = RunDashboard();
                        break;
                }
            }
        }
    }
}
