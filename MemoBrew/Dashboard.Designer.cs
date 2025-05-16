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
            this.label1 = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.friendsButton = new System.Windows.Forms.Button();
            this.eventButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.occasionNameLabel = new System.Windows.Forms.Label();
            this.occasionEndDateLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SeaShell;
            this.label1.Location = new System.Drawing.Point(145, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome, Username";
            // 
            // logoutButton
            // 
            this.logoutButton.BackColor = System.Drawing.Color.Black;
            this.logoutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoutButton.ForeColor = System.Drawing.Color.SeaShell;
            this.logoutButton.Location = new System.Drawing.Point(862, 30);
            this.logoutButton.Margin = new System.Windows.Forms.Padding(0);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(185, 97);
            this.logoutButton.TabIndex = 15;
            this.logoutButton.Text = "Log Out";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // friendsButton
            // 
            this.friendsButton.BackColor = System.Drawing.Color.SandyBrown;
            this.friendsButton.BackgroundImage = global::MemoBrew.Properties.Resources.add_user;
            this.friendsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.friendsButton.Location = new System.Drawing.Point(862, 1420);
            this.friendsButton.Margin = new System.Windows.Forms.Padding(7);
            this.friendsButton.Name = "friendsButton";
            this.friendsButton.Size = new System.Drawing.Size(126, 113);
            this.friendsButton.TabIndex = 5;
            this.friendsButton.UseVisualStyleBackColor = false;
            this.friendsButton.Click += new System.EventHandler(this.friendsButton_Click);
            // 
            // eventButton
            // 
            this.eventButton.BackColor = System.Drawing.Color.SandyBrown;
            this.eventButton.BackgroundImage = global::MemoBrew.Properties.Resources.plus;
            this.eventButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eventButton.Location = new System.Drawing.Point(444, 1388);
            this.eventButton.Margin = new System.Windows.Forms.Padding(7);
            this.eventButton.Name = "eventButton";
            this.eventButton.Size = new System.Drawing.Size(164, 146);
            this.eventButton.TabIndex = 4;
            this.eventButton.UseVisualStyleBackColor = false;
            this.eventButton.Click += new System.EventHandler(this.eventButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.SandyBrown;
            this.homeButton.BackgroundImage = global::MemoBrew.Properties.Resources.home__1_;
            this.homeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.homeButton.Location = new System.Drawing.Point(71, 1420);
            this.homeButton.Margin = new System.Windows.Forms.Padding(0);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(135, 113);
            this.homeButton.TabIndex = 3;
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MemoBrew.Properties.Resources.orange;
            this.pictureBox2.Location = new System.Drawing.Point(-93, 1348);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1244, 384);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::MemoBrew.Properties.Resources.black;
            this.pictureBox1.Location = new System.Drawing.Point(-93, -134);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1244, 384);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // occasionNameLabel
            // 
            this.occasionNameLabel.AutoSize = true;
            this.occasionNameLabel.Location = new System.Drawing.Point(31, 35);
            this.occasionNameLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.occasionNameLabel.Name = "occasionNameLabel";
            this.occasionNameLabel.Size = new System.Drawing.Size(239, 37);
            this.occasionNameLabel.TabIndex = 0;
            this.occasionNameLabel.Text = "occasion Name";
            // 
            // occasionEndDateLabel
            // 
            this.occasionEndDateLabel.AutoSize = true;
            this.occasionEndDateLabel.Location = new System.Drawing.Point(758, 35);
            this.occasionEndDateLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.occasionEndDateLabel.Name = "occasionEndDateLabel";
            this.occasionEndDateLabel.Size = new System.Drawing.Size(141, 37);
            this.occasionEndDateLabel.TabIndex = 1;
            this.occasionEndDateLabel.Text = "end date";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.occasionEndDateLabel);
            this.panel1.Controls.Add(this.occasionNameLabel);
            this.panel1.Location = new System.Drawing.Point(57, 298);
            this.panel1.Margin = new System.Windows.Forms.Padding(7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(931, 859);
            this.panel1.TabIndex = 16;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1071, 1561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.friendsButton);
            this.Controls.Add(this.eventButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "Dashboard";
            this.Text = "MemoBrew";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Label occasionNameLabel;
        private System.Windows.Forms.Label occasionEndDateLabel;
        private System.Windows.Forms.Panel panel1;
    }
}