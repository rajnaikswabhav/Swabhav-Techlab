using System;
using System.Drawing;

public partial class TestTimeStamp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        timeStamp.BorderColor = Color.Red;
        timeStamp2.BorderColor = Color.Green;
    }
}