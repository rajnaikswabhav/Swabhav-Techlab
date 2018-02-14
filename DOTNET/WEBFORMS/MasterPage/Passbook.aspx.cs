using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Passbook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logged"].Equals("No"))
        {
            Server.Transfer("LoginPage.aspx?url=Passbook.aspx");
        }
        else
        {
            var name = Session["User"].ToString();
            passbookLabel.Text = "Welcome to Passbook Page @ "+name;
        }
    }
}