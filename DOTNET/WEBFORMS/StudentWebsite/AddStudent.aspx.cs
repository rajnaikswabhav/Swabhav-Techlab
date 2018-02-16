using StudentCore;
using System;
using System.Collections.Generic;

public partial class StudentAdd : System.Web.UI.Page
{
    List<Student> studentList = new List<Student>();
    StudentService studentService = new StudentService();
    protected void Page_Load(object sender, EventArgs e)
    {
        userLabel.Text = "Welcome @ " + Session["user"].ToString();
    }

    public void Add(object sender, EventArgs e)
    {

        Student student = new Student(nameTxt.Text, int.Parse(ageTxt.Text), locationTxt.Text);
        studentList.Add(student);
        studentService.AddStudent(studentList);
        nameTxt.Text = "";
        ageTxt.Text = "";
        locationTxt.Text = "";
    }
}