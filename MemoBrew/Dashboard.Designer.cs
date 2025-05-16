namespace MemoBrew
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.label1 = new System.Windows.Forms.Label();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.occasionEndDateLabel = new System.Windows.Forms.Label();
            this.occasionNameLabel = new System.Windows.Forms.Label();
            this.languageLabel = new System.Windows.Forms.Label();
            this.selectLanguageBox = new System.Windows.Forms.ComboBox();
            this.friendsButton = new System.Windows.Forms.Button();
            this.eventButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.ForeColor = System.Drawing.Color.SeaShell;
            this.label1.Name = "label1";
            // 
            // welcomeLabel
            // 
            resources.ApplyResources(this.welcomeLabel, "welcomeLabel");
            this.welcomeLabel.Name = "welcomeLabel";
            // 
            // logoutButton
            // 
            resources.ApplyResources(this.logoutButton, "logoutButton");
            this.logoutButton.BackColor = System.Drawing.Color.Black;
            this.logoutButton.ForeColor = System.Drawing.Color.SeaShell;
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.occasionEndDateLabel);
            this.panel1.Controls.Add(this.occasionNameLabel);
            this.panel1.Name = "panel1";
            // 
            // occasionEndDateLabel
            // 
            resources.ApplyResources(this.occasionEndDateLabel, "occasionEndDateLabel");
            this.occasionEndDateLabel.Name = "occasionEndDateLabel";
            // 
            // occasionNameLabel
            // 
            resources.ApplyResources(this.occasionNameLabel, "occasionNameLabel");
            this.occasionNameLabel.Name = "occasionNameLabel";
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.languageLabel.ForeColor = System.Drawing.Color.SeaShell;
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
            // friendsButton
            // 
            resources.ApplyResources(this.friendsButton, "friendsButton");
            this.friendsButton.BackColor = System.Drawing.Color.SandyBrown;
            this.friendsButton.BackgroundImage = global::MemoBrew.Properties.Resources.add_user;
            this.friendsButton.Name = "friendsButton";
            this.friendsButton.UseVisualStyleBackColor = false;
            this.friendsButton.Click += new System.EventHandler(this.friendsButton_Click);
            // 
            // eventButton
            // 
            resources.ApplyResources(this.eventButton, "eventButton");
            this.eventButton.BackColor = System.Drawing.Color.SandyBrown;
            this.eventButton.BackgroundImage = global::MemoBrew.Properties.Resources.plus;
            this.eventButton.Name = "eventButton";
            this.eventButton.UseVisualStyleBackColor = false;
            this.eventButton.Click += new System.EventHandler(this.eventButton_Click);
            // 
            // homeButton
            // 
            resources.ApplyResources(this.homeButton, "homeButton");
            this.homeButton.BackColor = System.Drawing.Color.SandyBrown;
            this.homeButton.BackgroundImage = global::MemoBrew.Properties.Resources.home__1_;
            this.homeButton.Name = "homeButton";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::MemoBrew.Properties.Resources.orange;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::MemoBrew.Properties.Resources.black;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Dashboard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.selectLanguageBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.welcomeLabel);
            this.Controls.Add(this.friendsButton);
            this.Controls.Add(this.eventButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Dashboard";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Button eventButton;
        private System.Windows.Forms.Button friendsButton;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label occasionEndDateLabel;
        private System.Windows.Forms.Label occasionNameLabel;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.ComboBox selectLanguageBox;
    }
}