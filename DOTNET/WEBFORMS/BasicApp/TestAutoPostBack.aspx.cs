using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class TestAutoPostBack : System.Web.UI.Page
{
    private static string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
    private SqlConnection connection = new SqlConnection(connectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            CheckDropdown();
            DisplayEmployee();
        }
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayEmployee();
    }

    public void CheckDropdown()
    {

        SqlCommand command = new SqlCommand("Select * from DEPT", connection);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        deptList.DataSource = reader;
        deptList.DataTextField = "DNAME";
        deptList.DataValueField = "DEPTNO";
        deptList.DataBind();
        deptList.AutoPostBack = true;
        reader.Close();
        connection.Close();
    }

    public void DisplayEmployee()
    {
        SqlCommand command = new SqlCommand("Select * from EMP Where DEPTNO=" + deptList.SelectedItem.Value, connection);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        gridView.DataSource = reader;
        gridView.DataBind();
        reader.Close();
        connection.Close();
    }
}
