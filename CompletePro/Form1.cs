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
using System.Text.RegularExpressions;

namespace CompletePro
{
    public partial class Form1 : Form
    {
        OleDbCommand cmd = new OleDbCommand();
        //OleDbConnection cn = new OleDbConnection();
        OleDbDataReader dr;
        public static string Idnumber; //anothor form get this id 
        public static string IdnumberFromGrid;  //anothor form get this id 
        public Form1()
        {
            // Application.Run(new login());
            

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            UILoad();
            loadData();

        }

        private void UILoad()
        {

            ////////////////////////data grid header////////////////////////
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "ID Nubmer";
            dataGridView1.Columns[0].Width = 110;

            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[1].Width = 210;

            dataGridView1.Columns[2].Name = "Amount";
            dataGridView1.Columns[2].Width = 120;

            dataGridView1.Columns[3].Name = "Location";

            ////////////////////////////////////form ui///////////////////////////////////////////////
            //   dataGridView1.Columns[3].Width = 210;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            menuStrip2.BackColor = Color.FromArgb(44, 62, 80);

            splitContainer2.BackColor = Color.FromArgb(44, 62, 80);
            splitContainer1.BackColor = Color.FromArgb(44, 62, 80);
            fileToolStripMenuItem.BackColor = Color.FromArgb(44, 62, 80);
            panel4.BackColor = Color.FromArgb(44, 62, 80);
            panel5.BackColor = Color.FromArgb(44, 62, 80);
            panel7.BackColor = Color.FromArgb(44, 62, 80);
            panel1.BackColor = Color.FromArgb(44, 62, 80);
            panel6.BackColor = Color.FromArgb(44, 62, 80);
            tableLayoutPanel1.BackColor = Color.FromArgb(52, 73, 94);

            //////////////////////////button //////////////


            button1.TabStop = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.BackColor = Color.FromArgb(87, 122,158);

            button2.TabStop = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.FromArgb(87, 122, 158);

            button3.TabStop = false;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.FromArgb(87, 122, 158);

            button4.TabStop = false;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button4.BackColor = Color.FromArgb(87, 122, 158);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void IdNumberSearch_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void IdNumberSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {

                if (IdNumberSearch.Text != "")
                {
                    //t = "INSERT INTO Product (IdNumber, Name) VALUES (@Username, @Password)";
                    using (OleDbConnection connection = new OleDbConnection())
                    {
                        try
                        {
                            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                            cmd.Connection = connection;
                            // connection.Open();
                            cmd.CommandText = "SELECT * FROM Product WHERE IdNumber = " + IdNumberSearch.Text;
                            if (connection.State != ConnectionState.Open)
                                connection.Open();
                            dr = cmd.ExecuteReader();
                            this.dataGridView1.Rows.Clear();
                            if (dr.HasRows)
                            {
                                Idnumber = IdNumberSearch.Text;
                                Inventory from2 = new Inventory();
                                from2.ShowDialog();

                                connection.Close();
                                IdNumberSearch.Text = null;
                                IdNumberSearch.Select();
                                loadData();

                            }
                            else
                            {
                                MessageBox.Show("Not found ");

                            }
                            connection.Close();
                            connection.Dispose();
                            dr.Close();
                            //do something here

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            connection.Close();
                        }
                        finally
                        {
                            connection.Close();
                        }

                    }
                }

            }
        }

        private void loadData()
        {

            using (OleDbConnection connection = new OleDbConnection())
            {
                try
                {
                    connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                    cmd.Connection = connection;
                    // connection.Open();
                    cmd.CommandText = "SELECT * FROM Product";
                    // cmd.Parameters.AddWithValue("@IdNumber", IdNumberSearch.Text);
                    //cmd.Connection = connection;
                   if (connection.State != ConnectionState.Open)
                        connection.Open();
                  //  dr = cmd.ExecuteReader();
                    this.dataGridView1.Rows.Clear();
                    dr = cmd.ExecuteReader();
                    //dataGridView1.DataSource = dr.ToString();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                         //   Debug.WriteLine(dr[2].ToString());
                            dataGridView1.Rows.Add(dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                        }
                    }
                    dr.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
                finally
                {
                    connection.Close();
                }

            }
           
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            loadData();
           // this.dataGridView1.current("PO").ToString();
           // ((DataRowView)this.BindingSource1.Current).Row["PO"].ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
         
        }

        private void button3_Click(object sender, EventArgs e)
        {

            
            try
            {
                string firstCellValue = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                using (OleDbConnection connection = new OleDbConnection())
                {
                    try
                    {
                        DialogResult result = MessageBox.Show("Do you really want to delete the Row Id Number: " + firstCellValue + "?", "Confirm  deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                            cmd.Connection = connection;
                            //   cmd.CommandText = "SELECT * FROM Product WHERE IdNumber = " + Form1.Idnumber;

                            cmd.CommandText = "delete from Product where IdNumber = 0" + firstCellValue;
                            if (connection.State != ConnectionState.Open)
                                connection.Open();

                            MessageBox.Show("Delete ");
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            loadData();
                            //  this.Close();
                        }




                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No data");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("not select Data");
            }

          //  MessageBox.Show(firstCellValue);

            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                IdnumberFromGrid = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                Form4 edit = new Form4();
                edit.ShowDialog();
                edit.Close();
                loadData();
            }
            catch (Exception )
            {
                MessageBox.Show("No data");
            }
        }

        private void NameSeach_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchBtnID_Click(object sender, EventArgs e)
        {
            if (IdSearch.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection())
                {
                    try
                    {
                        connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM Product WHERE IdNumber = " + IdSearch.Text;
                        // cmd.Parameters.AddWithValue("@IdNumber", IdNumberSearch.Text);
                        //cmd.Connection = connection;
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        dr = cmd.ExecuteReader();
                        this.dataGridView1.Rows.Clear();
                        if (dr.HasRows)
                        {
                         
                             while (dr.Read())
                             {
                                 dataGridView1.Rows.Add(dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                             }

                            connection.Close();
                          //  loadData();
                           

                        }
                        else
                        {
                            MessageBox.Show("Can not find ID Number");
                            loadData();
                        }
                        connection.Close();
                        connection.Dispose();
                        dr.Close();
                        //do something here

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
        }

        private void searchBtnName_Click(object sender, EventArgs e)
        {
            if (NameSeach.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection())
                {
                    try
                    {
                        connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database/dataBase.mdb";
                        cmd.Connection = connection;
                        cmd.CommandText = "SELECT * FROM Product WHERE Name LIKE '%" + NameSeach.Text + "%'";
                        // cmd.Parameters.AddWithValue("@IdNumber", IdNumberSearch.Text);
                        //cmd.Connection = connection;
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        dr = cmd.ExecuteReader();
                        this.dataGridView1.Rows.Clear();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                dataGridView1.Rows.Add(dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                            }

                            connection.Close();
                            //  loadData();


                        }
                        else
                        {
                            MessageBox.Show("Can not find");
                            loadData();
                        }
                        connection.Close();
                        connection.Dispose();
                        dr.Close();
                        //do something here

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
        
    }

        private void button5_Click(object sender, EventArgs e)
        {
           

        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportToExcelToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exportToExcelToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "c:";
            saveFileDialog1.Title = "Save as Excel File";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel File(2003)|*.xls|Excel File(2007)|*.xlsx";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                exportLoad.Show();
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 40;

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {

                    ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    exportLoad.Increment(100 / (dataGridView1.Rows.Count));
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                exportLoad.Hide();
                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
                // ApplicationClass.ApplicationClass()
                MessageBox.Show("Export finished");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
          //  this.Hide();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addminstaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userSetting sett = new userSetting();
            sett.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult MsgResult = MessageBox.Show("Do you want to save and exit ", "Warning", MessageBoxButtons.YesNo);
            if (MsgResult == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
             //   return;
            }
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void browsDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"database");

        }
    }
}
