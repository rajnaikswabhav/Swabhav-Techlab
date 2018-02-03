using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using StudentCore;
using System.Windows.Forms;

namespace StudentWindowsForm
{
    public partial class Form1 : Form
    {
        private List<Student> studentList = new List<Student>();
        private StudentService studentService = new StudentService();
        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Student student = new Student(nameTextbox.Text,int.Parse(ageTextbox.Text),addressTextBox.Text);
            studentList.Add(student);
            studentService.AddStudent(studentList);
            nameTextbox.Text = "";
            ageTextbox.Text = "";
            addressTextBox.Text = "";
        }
       
        private void displayButton_Click(object sender, EventArgs e)
        {
            studentList=studentService.GetDetails();
            DataGridView gridView = new DataGridView();         
            foreach (var student in studentList)
            {
                dataGridView1.
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                                
        }
    }
}
