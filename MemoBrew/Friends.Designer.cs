namespace MemoBrew
{
    partial class Friends
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Friends));
            this.searchtextBox = new System.Windows.Forms.TextBox();
            this.friendLabel = new System.Windows.Forms.Label();
            this.friendInfoPanel = new System.Windows.Forms.Panel();
            this.FriendName = new System.Windows.Forms.Label();
            this.searchFriend = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.friendlistLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.friendsButton = new System.Windows.Forms.Button();
            this.eventButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.friendInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // searchtextBox
            // 
            resources.ApplyResources(this.searchtextBox, "searchtextBox");
            this.searchtextBox.Name = "searchtextBox";
            // 
            // friendLabel
            // 
            resources.ApplyResources(this.friendLabel, "friendLabel");
            this.friendLabel.BackColor = System.Drawing.Color.Black;
            this.friendLabel.ForeColor = System.Drawing.Color.SeaShell;
            this.friendLabel.Name = "friendLabel";
            // 
            // friendInfoPanel
            // 
            resources.ApplyResources(this.friendInfoPanel, "friendInfoPanel");
            this.friendInfoPanel.Controls.Add(this.FriendName);
            this.friendInfoPanel.Name = "friendInfoPanel";
            // 
            // FriendName
            // 
            resources.ApplyResources(this.FriendName, "FriendName");
            this.FriendName.Name = "FriendName";
            // 
            // searchFriend
            // 
            resources.ApplyResources(this.searchFriend, "searchFriend");
            this.searchFriend.BackColor = System.Drawing.Color.SandyBrown;
            this.searchFriend.Name = "searchFriend";
            this.searchFriend.UseVisualStyleBackColor = false;
            this.searchFriend.Click += new System.EventHandler(this.searchFriend_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.BackColor = System.Drawing.Color.SandyBrown;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // friendlistLabel
            // 
            resources.ApplyResources(this.friendlistLabel, "friendlistLabel");
            this.friendlistLabel.Name = "friendlistLabel";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::MemoBrew.Properties.Resources.black;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
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
            // Friends
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.friendlistLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.searchFriend);
            this.Controls.Add(this.friendInfoPanel);
            this.Controls.Add(this.friendLabel);
            this.Controls.Add(this.searchtextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.friendsButton);
            this.Controls.Add(this.eventButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.pictureBox2);
            this.Name = "Friends";
            this.friendInfoPanel.ResumeLayout(false);
            this.friendInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button friendsButton;
        private System.Windows.Forms.Button eventButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox searchtextBox;
        private System.Windows.Forms.Label friendLabel;
        private System.Windows.Forms.Panel friendInfoPanel;
        private System.Windows.Forms.Label FriendName;
        private System.Windows.Forms.Button searchFriend;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label friendlistLabel;
    }
}