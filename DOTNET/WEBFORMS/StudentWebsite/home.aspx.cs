using System;
using StudentCore;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void login(object sender, EventArgs e)
    {
        string userName = userTxt.Text;
        string password = passwordTxt.Text;

        if (userName.Equals("") || password.Equals(""))
        {
           
        }
        else
        {
            LoginService loginService = new LoginService();
            bool isValidPerson = loginService.Login(userName, password);
            if (isValidPerson)
            {
                Session["user"] = userName;
                Response.Redirect("DisplayStudents.aspx?user = "+userName);
            }
            else
            {
                
            }
        }
    }
}