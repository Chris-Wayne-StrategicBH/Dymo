using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPGPP
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         loginForm objLogin = new loginForm();
         if(objLogin.ShowDialog() == DialogResult.OK)
         {
            Application.Run(new Form1());
         }
      }
   }
}
