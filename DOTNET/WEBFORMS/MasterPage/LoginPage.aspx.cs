using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void Login(object sender, EventArgs e)
    {
        var username = userTxt.Text;
        var password = passTxt.Text;
        var url = Request.QueryString["url"];

        if(username == "Akash" && password == "akash")
        {
            Session["User"] = username;
            Session["Logged"] = "Yes";
            Response.Redirect(url);
        }
        else
        {
            authLabel.Text = "User is not valid......";
        }
    }
}