namespace MemoBrew
{
    partial class Signup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Signup));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.firstnameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.heightlabel = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.weightLabel = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.DOBlabel = new System.Windows.Forms.Label();
            this.txtDateOfBirth = new System.Windows.Forms.TextBox();
            this.signupButton = new System.Windows.Forms.Button();
            this.languageLabel = new System.Windows.Forms.Label();
            this.selectLanguageBox = new System.Windows.Forms.ComboBox();
            this.gobackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtFirstName
            // 
            resources.ApplyResources(this.txtFirstName, "txtFirstName");
            this.txtFirstName.BackColor = System.Drawing.Color.SandyBrown;
            this.txtFirstName.Name = "txtFirstName";
            // 
            // firstnameLabel
            // 
            resources.ApplyResources(this.firstnameLabel, "firstnameLabel");
            this.firstnameLabel.Name = "firstnameLabel";
            // 
            // passwordLabel
            // 
            resources.ApplyResources(this.passwordLabel, "passwordLabel");
            this.passwordLabel.Name = "passwordLabel";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.BackColor = System.Drawing.Color.SandyBrown;
            this.txtPassword.Name = "txtPassword";
            // 
            // txtLastName
            // 
            resources.ApplyResources(this.txtLastName, "txtLastName");
            this.txtLastName.BackColor = System.Drawing.Color.SandyBrown;
            this.txtLastName.Name = "txtLastName";
            // 
            // lastNameLabel
            // 
            resources.ApplyResources(this.lastNameLabel, "lastNameLabel");
            this.lastNameLabel.Name = "lastNameLabel";
            // 
            // heightlabel
            // 
            resources.ApplyResources(this.heightlabel, "heightlabel");
            this.heightlabel.Name = "heightlabel";
            // 
            // txtHeight
            // 
            resources.ApplyResources(this.txtHeight, "txtHeight");
            this.txtHeight.BackColor = System.Drawing.Color.SandyBrown;
            this.txtHeight.Name = "txtHeight";
            // 
            // weightLabel
            // 
            resources.ApplyResources(this.weightLabel, "weightLabel");
            this.weightLabel.Name = "weightLabel";
            // 
            // txtWeight
            // 
            resources.ApplyResources(this.txtWeight, "txtWeight");
            this.txtWeight.BackColor = System.Drawing.Color.SandyBrown;
            this.txtWeight.Name = "txtWeight";
            // 
            // txtUsername
            // 
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.BackColor = System.Drawing.Color.SandyBrown;
            this.txtUsername.Name = "txtUsername";
            // 
            // usernameLabel
            // 
            resources.ApplyResources(this.usernameLabel, "usernameLabel");
            this.usernameLabel.Name = "usernameLabel";
            // 
            // rbMale
            // 
            resources.ApplyResources(this.rbMale, "rbMale");
            this.rbMale.Name = "rbMale";
            this.rbMale.TabStop = true;
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // rbFemale
            // 
            resources.ApplyResources(this.rbFemale, "rbFemale");
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.TabStop = true;
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // DOBlabel
            // 
            resources.ApplyResources(this.DOBlabel, "DOBlabel");
            this.DOBlabel.Name = "DOBlabel";
            // 
            // txtDateOfBirth
            // 
            resources.ApplyResources(this.txtDateOfBirth, "txtDateOfBirth");
            this.txtDateOfBirth.BackColor = System.Drawing.Color.SandyBrown;
            this.txtDateOfBirth.Name = "txtDateOfBirth";
            // 
            // signupButton
            // 
            resources.ApplyResources(this.signupButton, "signupButton");
            this.signupButton.BackColor = System.Drawing.Color.Black;
            this.signupButton.ForeColor = System.Drawing.Color.SeaShell;
            this.signupButton.Name = "signupButton";
            this.signupButton.UseVisualStyleBackColor = false;
            this.signupButton.Click += new System.EventHandler(this.signupButton_Click);
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.Name = "languageLabel";
            // 
            // selectLanguageBox
            // 
            resources.ApplyResources(this.selectLanguageBox, "selectLanguageBox");
            this.selectLanguageBox.FormattingEnabled = true;
            this.selectLanguageBox.Items.AddRange(new object[] {
            resources.GetString("selectLanguageBox.Items"),
            resources.GetString("selectLanguageBox.Items1"),
            resources.GetString("selectLanguageBox.Items2")});
            this.selectLanguageBox.Name = "selectLanguageBox";
            this.selectLanguageBox.SelectedIndexChanged += new System.EventHandler(this.selectLanguageBox_SelectedIndexChanged);
            // 
            // gobackButton
            // 
            resources.ApplyResources(this.gobackButton, "gobackButton");
            this.gobackButton.BackColor = System.Drawing.Color.Black;
            this.gobackButton.ForeColor = System.Drawing.Color.SeaShell;
            this.gobackButton.Name = "gobackButton";
            this.gobackButton.UseVisualStyleBackColor = false;
            this.gobackButton.Click += new System.EventHandler(this.gobackButton_Click);
            // 
            // Signup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.Controls.Add(this.gobackButton);
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.selectLanguageBox);
            this.Controls.Add(this.signupButton);
            this.Controls.Add(this.DOBlabel);
            this.Controls.Add(this.txtDateOfBirth);
            this.Controls.Add(this.rbFemale);
            this.Controls.Add(this.rbMale);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.heightlabel);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.weightLabel);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.firstnameLabel);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Signup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label firstnameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.Label heightlabel;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.Label DOBlabel;
        private System.Windows.Forms.TextBox txtDateOfBirth;
        private System.Windows.Forms.Button signupButton;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.ComboBox selectLanguageBox;
        private System.Windows.Forms.Button gobackButton;
    }
}