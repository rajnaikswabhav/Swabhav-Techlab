using StudentCore;
using System;
using System.Collections.Generic;

public partial class DisplayStudents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Student> studentList = new List<Student>();
        StudentService studentService = new StudentService();

        userLabel.Text = " Welcome @ "+ Session["user"].ToString();
        studentList = studentService.GetDetails();
        gridView.DataSource = studentList;
        gridView.DataBind();
    }
}