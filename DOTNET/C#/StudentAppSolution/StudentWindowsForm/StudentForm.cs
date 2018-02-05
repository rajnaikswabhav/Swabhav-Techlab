using System;
using System.Collections.Generic;
using StudentCore;
using System.Windows.Forms;

namespace StudentWindowsForm
{
    public partial class StudentForm : Form
    {
        private List<Student> studentList = new List<Student>();
        private StudentService studentService = new StudentService();
        public StudentForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Student student = new Student(nameTextbox.Text, int.Parse(ageTextbox.Text), addressTextBox.Text);
            studentList.Add(student);
            studentService.AddStudent(studentList);
            nameTextbox.Text = "";
            ageTextbox.Text = "";
            addressTextBox.Text = "";
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Show();
            studentList = studentService.GetDetails();
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Student Name";
            dataGridView1.Columns[1].Name = "Student Age";
            dataGridView1.Columns[2].Name = "Student Location";
            dataGridView1.Columns[3].Name = "Student Id";
            string[] row;
            foreach (var student in studentList)
            {
                row = new string[] { student.StudentName, (student.Age).ToString(), student.Address, student.Id };
                dataGridView1.Rows.Add(row);
            }
        }
    }
}
