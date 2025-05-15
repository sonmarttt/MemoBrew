using System;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form_FormClosing);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            CloseAndOpenNewForm(loginForm);
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            Signup signupForm = new Signup();
            CloseAndOpenNewForm(signupForm);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void CloseAndOpenNewForm(Form newForm)
        {
            newForm.Show();
            this.Hide();

            newForm.FormClosed += (s, args) => this.Close();
        }
    }
}