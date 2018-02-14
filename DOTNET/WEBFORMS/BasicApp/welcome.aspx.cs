using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String name = Request.QueryString["user"];
        if (!this.IsPostBack)
        {
            Response.Write("<h1>Welcome Mr."+name+"</h1>");

        }
        else
        {
            Response.Write("<h1>Method Post..</h1>");
        }
    }

    public void btnHello_click(Object sender, EventArgs args)
    {
        String name = textBox.Text;
        label2.Text = name;
    }
}