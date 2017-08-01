using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompletePro
{
    public partial class Form3 : Form
    {
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr;
        public Form3()
        {
            InitializeComponent();
            panel1.BackColor = System.Drawing.ColorTranslator.FromHtml("#CAD6E2");
        }

        private void Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IdNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            IdNumber.Select();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (IdNumber.Text == "")
            {
                MessageBox.Show("Please Enter ID Number");
                IdNumber.Select();
            }
            else if (NameData.Text == "")
            {
                MessageBox.Show("Please Enter Name");
                NameData.Select();
            }
            else if (Amount.Text == "")
            {
                MessageBox.Show("Please Enter Amount");
                Amount.Select();
            }
            else if (Location.Text == "")
            {
                MessageBox.Show("Please Enter Location");
                Location.Select();
            }
            else
            {

                using (OleDbConnection connection = new OleDbConnection())
                {
                    try
                    {

                        connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                        cmd.Connection = connection;
                        //   cmd.CommandText = "SELECT * FROM Product WHERE IdNumber = " + Form1.Idnumber;

                        cmd.CommandText = "insert into Product ([IdNumber],[Name],[Amount],[Location]) values (@IdNumber,@Name,@Amount,@Location)";
                        cmd.Parameters.AddWithValue("@IdNumber", int.Parse(IdNumber.Text));
                        cmd.Parameters.AddWithValue("@Name", NameData.Text);
                        cmd.Parameters.AddWithValue("@Amount", int.Parse(Amount.Text));
                        cmd.Parameters.AddWithValue("@Location", Location.Text);

                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        this.Close();


                    }
                     catch (OverflowException ex)
                    {
                        MessageBox.Show("ID Number exceeded the maximum permissible length ");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
