using System;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
            //added langaugeManagerclass
            LanguageManager.ApplyLanguage();
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
        //pick the language from the dropdown
        private void selectLanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (selectLanguageBox.SelectedIndex)
            {
                case 0:
                    LanguageManager.ChangeLanguage("en-US", this);
                    break;
                case 1:
                    LanguageManager.ChangeLanguage("fr-FR", this);
                    break;
                case 2:
                    LanguageManager.ChangeLanguage("es-ES", this);
                    break;
            }
        }
    }
}