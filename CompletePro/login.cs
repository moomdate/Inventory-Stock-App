using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompletePro
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (username.Text != "" && password.Text != "")
            {
                
                if(Properties.Settings.Default.username == username.Text && Properties.Settings.Default.password == password.Text) {
                    Form1 loginsu = new Form1();
                    loginsu.Show();
                    this.Hide();
                    //loginsu.ShowInTaskbar = true;
                }
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
