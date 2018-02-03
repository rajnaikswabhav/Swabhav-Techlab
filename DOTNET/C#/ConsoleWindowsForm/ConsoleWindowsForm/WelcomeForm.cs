using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleWindowsForm
{
    class WelcomeForm : Form
    {
        private Button wishButton;
        private Button exitButton;

        public WelcomeForm()
        {
            this.Text = "WelcomeForm";
            this.Width = 300;
            this.Height = 300;
            this.Name = "Welcome Form";
            this.wishButton = new Button();
            this.exitButton = new Button();    
            this.wishButton.Text = "Wish";
            this.exitButton.Text = "Exit";
            this.Controls.Add(wishButton);
            this.Controls.Add(exitButton);
            exitButton.SetBounds(200,100,25,20);
            this.wishButton.Click += WishButton_Click;
            this.exitButton.Click += ExitButton_Click;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Inside exit .....");
            Application.Exit();
        }

        private void WishButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Inside click method......");
            MessageBox.Show("Click Button.....");
        }
    }
}
