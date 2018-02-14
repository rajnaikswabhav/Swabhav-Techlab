using System;

public partial class TestSession : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Count"] == null)
        {
            Session["Count"] = 0;
        }
            oldValue.Text = Session["Count"].ToString();
            Session["Count"] = Convert.ToInt32(Session["Count"]) + 1;
            newValue.Text = Session["Count"].ToString();
            idValue.Text = Session.SessionID;    
       }
}