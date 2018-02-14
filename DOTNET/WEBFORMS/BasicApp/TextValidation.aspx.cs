using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TextValidation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
        }
    }

    public void SubmitPage(object sender, EventArgs args)
    {
        resNamelabel.Text = nameTxt.Text;
        resAgeLabel.Text = ageTxt.Text;
        resSalaryLabel.Text = salaryTxt.Text;
        resEmailLabel.Text = emailTxt.Text;
    }
}