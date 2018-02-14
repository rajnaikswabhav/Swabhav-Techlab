using StudentCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchStudent : System.Web.UI.Page
{
    private List<Student> studentList = new List<Student>();
    protected void Page_Load(object sender, EventArgs e)
    {
        userLabel.Text = " Welcome @ " + Session["user"].ToString();
    }

    public void findStudent(object sender, EventArgs e)
    {
        var name = nameTxt.Text;
        StudentService service = new StudentService();
        studentList = service.Search(s => s.StudentName.Contains(name));
        searchGridView.DataSource = studentList;
        searchGridView.DataBind();
    }
}