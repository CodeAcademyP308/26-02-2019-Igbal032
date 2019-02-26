using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace P308_SQLCSharp
{
    public partial class Form1 : Form
    {
        private readonly string connectionString;
        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["CodeAcademyContext"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //connect to sql server
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            //execute query (command) in sql server
            string query = "select * from Groups";
            SqlCommand command = new SqlCommand(query, conn);

            SqlDataReader reader = command.ExecuteReader();

            //retrieve data
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //read data
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);

                    //load to combobox
                    ComboBoxItem item = new ComboBoxItem(id, name);

                    cmbGroups.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Hec ne yoxdu");
            }

            conn.Close();
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem groupName = (ComboBoxItem)cmbGroups.SelectedItem;

            #region Old way of sqlconnection
            //SqlConnection conn = null;
            //try
            //{
            //    conn = new SqlConnection(connectionString);
            //    conn.Open();
            //}
            //catch (Exception)
            //{
            //}
            //finally
            //{
            //    conn.Close();
            //}
            #endregion


            FillDataGridStudents(groupName.Id);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string name = txtSearchByFirstname.Text.Trim();
            FillDataGridStudents(firstname: name);
        }

        private void FillDataGridStudents(int groupId = 0, string firstname = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"select * from StudentsFullInfo where ";

                if(groupId != 0)
                {
                    query += $"GroupId = { groupId}";
                }

                if(firstname != null)
                {
                    query += $"Firstname LIKE '%{firstname}%'";
                }

                conn.Open();

                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dataSet = new DataTable();
                    sqlDataAdapter.Fill(dataSet);

                    dgwStudents.DataSource = dataSet;

                    dgwStudents.Columns[0].Visible = false;
                    dgwStudents.Columns[1].Visible = false;
                }
            }
        }
    }
}
