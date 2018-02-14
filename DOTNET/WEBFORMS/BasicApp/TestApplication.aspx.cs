using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["Count"] == null)
        {
            Application["Count"] = 0;
        }
        oldValue.Text = Application["Count"].ToString();
        Application["Count"] = Convert.ToInt32(Application["Count"]) + 1;
        newValue.Text = Application["Count"].ToString();
    }
}