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
            this.Text = "Welcome @ " + userName;
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
           
            service.Save(urlTxt.Text,id);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {

        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            DataSet dataset = new DataSet();
            dataset = service.GetBookmarks(id);
            dataGridView1.DataSource = dataset.Tables[0];
            dataGridView1.Show();
        }
    }
}
