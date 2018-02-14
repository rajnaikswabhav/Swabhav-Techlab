using System;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoginUser(object sender , EventArgs args)
    {
        if(userTxt.Text == "")
        {

        }
        else
        {
            //Response.Redirect("./welcome.aspx?user="+userTxt.Text+"");
            Server.Transfer("welcome.aspx?user="+userTxt.Text+"");
        }
        
    }
}