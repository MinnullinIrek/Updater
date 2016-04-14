using System;
using System.Windows.Forms;

namespace XMLDb
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form2());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Loger.Write(ex.ToString());
            }
            finally
            {
                Loger.End();
            }
        }
    }
}
