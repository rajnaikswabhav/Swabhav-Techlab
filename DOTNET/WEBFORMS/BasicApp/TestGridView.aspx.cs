using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestGridView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void DisplayData(Object sender, EventArgs args)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand("Select * from EMP", connection);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            gridView.DataSource = reader;
            gridView.DataBind();
        }
        catch(Exception exception)
        {
            
        }
    }
}