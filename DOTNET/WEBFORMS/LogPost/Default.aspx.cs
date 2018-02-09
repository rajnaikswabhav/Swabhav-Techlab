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

            messageListBox.Items.Add("Hello");
            messageListBox.Items.Add("Hello World..");
            messageListBox.Items.Add("How are You..");
        }
    }

    public void AddItem(object sender, EventArgs args)
    {
        if (txtMessage.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Type anything in Message box')", true);
        }
        this.messageListBox.Items.Add(txtMessage.Text.ToString());
        txtMessage.Text = "";
    }

    public void DeleteItem(object sender, EventArgs args)
    {
        if (this.messageListBox.SelectedIndex < 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Select Any Item...')", true);
        }

        this.messageListBox.Items.Remove(this.messageListBox.SelectedItem);
    }

    public void ClearItems(object sender , EventArgs args)
    {
        this.messageListBox.Items.Clear();
    }
}