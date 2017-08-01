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
    public partial class userSetting : Form
    {
        public userSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Password.Text == RePassword.Text)
            {
                if (current.Text == Properties.Settings.Default.password)
                {
                    Properties.Settings.Default.password = Password.Text;
                    Properties.Settings.Default.Save();
                    this.Close();
                 }
                else
                {
                    MessageBox.Show("Current password not match");
                }
            }
            else
            {
                MessageBox.Show("password not match");
            }
        }

        private void userSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
