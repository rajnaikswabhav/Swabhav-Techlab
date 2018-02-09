using System;

public partial class Welcome : System.Web.UI.Page
{
    public void Page_Load (Object sender,EventArgs arg) {
        if(!this.IsPostBack){
            Response.Write("<h1>Method Get...</h1>");
        }else{
            Response.Write("<h1>Method Post..</h1>");
        }
    }

    public void btnHello_click(Object sender , EventArgs args){
        String name = textBox.Text;
        label2.Text = name; 
    }
}