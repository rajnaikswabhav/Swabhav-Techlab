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
    public partial class SearchForm : Form
    {
        private List<Student> studentList = new List<Student>();
        public SearchForm()
        {
            InitializeComponent();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Show();
            var name = nameTxtbox.Text;
            var id = ageTxt.Text;
            StudentService studentService = new StudentService();
            studentList = studentService.Search(s => s.StudentName.Contains(name) ||
            s.Id.Contains(id));

            dataGridView2.ColumnCount = 4;
            dataGridView2.Columns[0].Name = "Student Name";
            dataGridView2.Columns[1].Name = "Student Age";
            dataGridView2.Columns[2].Name = "Student Location";
            dataGridView2.Columns[3].Name = "Student Id";
            string[] row;
            foreach (var student in studentList)
            {
                row = new string[] { student.StudentName, (student.Age).ToString(), student.Address, student.Id };
                dataGridView2.Rows.Add(row);
            }
        }
    }
}
