using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;

namespace CompletePro
{
    public partial class Inventory : Form
    {
        int defualtAmount = 0;
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr;
        public Inventory()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(46, 204, 113);
            Save.TabStop = false;
            Save.FlatStyle = FlatStyle.Flat;
            Save.FlatAppearance.BorderSize = 0;
            Save.BackColor = Color.FromArgb(38, 166, 91);

            Cancel.TabStop = false;
            Cancel.FlatStyle = FlatStyle.Flat;
            Cancel.FlatAppearance.BorderSize = 0;
            Cancel.BackColor = Color.FromArgb(245, 171, 53);

            IdNumberShow.Enabled = false;
            NameShow.Enabled = false;
            AmountShow.Enabled = false;

            using (OleDbConnection connection = new OleDbConnection())
            {
                try
                {

                    connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                    cmd.Connection = connection;//+ Form1.Idnumber
                    cmd.CommandText = "SELECT * FROM Product WHERE IdNumber = '" + Form1.Idnumber +"'";
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            IdNumberShow.Text = dr[1].ToString();
                            NameShow.Text = dr[2].ToString();
                            AmountShow.Text = dr[3].ToString();
                            defualtAmount = int.Parse(dr[3].ToString());

                        }
                        connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection())
            {
                try
                {

                    connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                    cmd.Connection = connection;
                    //(int)increaseA.Value;
                    cmd.CommandText = "update Product set Amount = " + ((defualtAmount + (int)increaseA.Value) - (int)decreaseA.Value) + " WHERE IdNumber = '" + Form1.Idnumber +"'";
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    cmd.ExecuteNonQuery();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            //IdNumberSearch.Text = null;
        }
    }
}
