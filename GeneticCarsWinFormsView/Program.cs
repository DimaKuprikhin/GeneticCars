using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticCarsWinFormsView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WinFormsView view = new WinFormsView();
            GeneticCarsPresenter.Presenter presenter =
                new GeneticCarsPresenter.Presenter(view);
            Application.Run(view);
        }
    }
}
