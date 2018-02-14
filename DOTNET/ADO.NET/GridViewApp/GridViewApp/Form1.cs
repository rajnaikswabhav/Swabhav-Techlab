using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GridViewApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fillBtn_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from EMP", connection);
            SqlCommand cmd2 = new SqlCommand("Select * from DEPT", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            try
            {
                connection.Open();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Employee Table");

                adapter.SelectCommand = cmd2;
                adapter.Fill(dataSet);

                adapter.Dispose();
                cmd.Dispose();
                connection.Close();

                dataGridView1.DataSource = dataSet.Tables[0];
                empLabel.Show();
                dataGridView1.Show();
                dataGridView2.DataSource = dataSet.Tables[1];
                deptLabel.Show();
                dataGridView2.Show();



            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception" + exception);
            }
        }
    }
}
