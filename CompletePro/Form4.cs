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
    public partial class Form4 : Form
    {
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.ColorTranslator.FromHtml("#CAD6E2");
            using (OleDbConnection connection = new OleDbConnection())
            {
                try
                {
                   // MessageBox.Show(Form1.IdnumberFromGrid);
                    connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                    cmd.Connection = connection;
                    //   cmd.CommandText = "SELECT * FROM Product WHERE IdNumber = " + Form1.Idnumber;

                    cmd.CommandText = "select * from Product where IdNumber = '" + Form1.IdnumberFromGrid + "'";
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    dr = cmd.ExecuteReader();
                    while (dr.Read()) {
                       IdNumber.Text = dr[1].ToString();
                        NameData.Text = dr[2].ToString();
                        Amount.Text = dr[3].ToString();
                        Location.Text = dr[4].ToString();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void IdNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cancel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void Save_MouseClick(object sender, MouseEventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection())
            {
                try
                {

                    connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                    cmd.Connection = connection;
                    // cmd.CommandText = "update Product set IdNumber = " + IdNumber.Text + ",Name = " + NameData.Text + ",Amount = " + Amount.Text + ",Location = " + Location.Text + " where IdNumber = " + Form1.IdnumberFromGrid;
                    cmd.CommandText = "UPDATE Product SET IdNumber = @id, Name = @name, Amount = @amount, Location = @location WHERE IdNumber ='" + Form1.IdnumberFromGrid +"'";

                    cmd.Parameters.AddWithValue("@id", IdNumber.Text);
                    cmd.Parameters.AddWithValue("@name", NameData.Text);
                    cmd.Parameters.AddWithValue("@amount", Amount.Text);
                    cmd.Parameters.AddWithValue("@location", Location.Text);

                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                   cmd.ExecuteNonQuery();
                   
                    connection.Close();
                    this.Close();
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error");
                    connection.Close();
                }
            }
        }
    }
}
