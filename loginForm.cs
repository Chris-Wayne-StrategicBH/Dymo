using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPGPP
{
   public partial class loginForm : Form
   {
      public loginForm()
      {
         InitializeComponent();
      }

      private void BTN_OK_Click(object sender, EventArgs e)
      {
         if (TB_UserName.Text == "Chris" && TB_Password.Text == "1234")
         {
            this.DialogResult = DialogResult.OK;
            this.Close();
         }
         else
         {
            MessageBox.Show("Invalid User Name or Password.. Contact the Great Gazoo");
            TB_UserName.Text = "";
            TB_UserName.Focus();
            TB_Password.Text = "";
         }
      }

      private void BTN_Cancel_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.Cancel;
         this.Close();
      }
   }
}
