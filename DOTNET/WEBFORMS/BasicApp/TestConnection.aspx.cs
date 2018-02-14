using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class TestConnection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ShowDetails(Object sender , EventArgs args)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();

        statusLabel.Text = connection.State.ToString();
        timeoutLabel.Text = connection.ConnectionTimeout.ToString();
        connectionId.Text = connection.ClientConnectionId.ToString();
        databaseLabel.Text = connection.Database.ToString();
        dataSource.Text = connection.DataSource.ToString();
        packetSize.Text = connection.PacketSize.ToString();
        serverLabel.Text = connection.ServerVersion.ToString();
        
        
    }
}