using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TransactionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logged"].Equals("No"))
        {
            Server.Transfer("LoginPage.aspx?url=Transaction.aspx");
        }
        else
        {
            var name = Session["User"].ToString();
            tranLabel.Text = "Welcome to Transaction Page @ "+name;
        }
    }

    
}