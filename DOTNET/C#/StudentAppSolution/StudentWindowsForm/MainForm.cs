using System;
using System.Windows.Forms;

namespace StudentWindowsForm
{
    public partial class MainForm : Form
    {
        private LoginForm loginForm = new LoginForm();
        public MainForm()
        {
            InitializeComponent();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginForm.MdiParent = this;
            loginForm.Show();
        }

        private void studentFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            StudentForm studentForm = new StudentForm();
            studentForm.MdiParent = this;
            studentForm.Show();

        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.MdiParent = this;
            searchForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loginForm.MdiParent = this;
            loginForm.Show();
        }
    }
}
