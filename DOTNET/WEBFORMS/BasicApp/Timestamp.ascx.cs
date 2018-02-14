using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Timestamp : System.Web.UI.UserControl
{
    private Color borderColor = Color.Blue;
    protected void Page_Load(object sender, EventArgs e)
    {
        timeStampLabel.Text = DateTime.Now.ToLongTimeString();
        timeStampLabel.BorderColor = borderColor;
    }

    public Color BorderColor { set { borderColor = value; } }
}