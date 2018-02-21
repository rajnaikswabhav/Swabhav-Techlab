using BookmarkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookmarkApplication
{
    public partial class UserForm : Form
    {
        private String name;
        private BookmarkService service = new BookmarkService();
        int id;
        public UserForm(String userName)
        {
            name = userName;
            id = service.GetUserByName(name);
            this.Text = "Welcome @ " + userName;
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
           
            service.Save(urlTxt.Text,id);
            urlTxt.Text = "";
            result.Text = "Url is Bookmarked...";
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.WindowState = FormWindowState.Maximized;
            login.Show();
            login.MdiParent = this.ParentForm;
            this.Hide();
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            
            DataSet dataset = new DataSet();
            dataset = service.GetBookmarks(id);
            dataGridView1.DataSource = dataset.Tables[0];
            tableLabel.Show();
            dataGridView1.Show();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            service.ExportToHTMl(name,id);
        }
    }
}
