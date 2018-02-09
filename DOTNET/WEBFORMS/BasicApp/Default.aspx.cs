using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Response.Write("<h1>Method Get...</h1>");
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