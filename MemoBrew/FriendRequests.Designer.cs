namespace MemoBrew
{
    partial class FriendRequests
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendRequests));
            this.friendInfoPanel = new System.Windows.Forms.Panel();
            this.rejectFriendButton = new System.Windows.Forms.Button();
            this.acceptFriendButton = new System.Windows.Forms.Button();
            this.FriendName = new System.Windows.Forms.Label();
            this.friendLabel = new System.Windows.Forms.Label();
            this.friendsButton = new System.Windows.Forms.Button();
            this.eventButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.friendInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // friendInfoPanel
            // 
            this.friendInfoPanel.Controls.Add(this.rejectFriendButton);
            this.friendInfoPanel.Controls.Add(this.acceptFriendButton);
            this.friendInfoPanel.Controls.Add(this.FriendName);
            resources.ApplyResources(this.friendInfoPanel, "friendInfoPanel");
            this.friendInfoPanel.Name = "friendInfoPanel";
            // 
            // rejectFriendButton
            // 
            this.rejectFriendButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rejectFriendButton.ForeColor = System.Drawing.Color.SeaShell;
            resources.ApplyResources(this.rejectFriendButton, "rejectFriendButton");
            this.rejectFriendButton.Name = "rejectFriendButton";
            this.rejectFriendButton.UseVisualStyleBackColor = false;
            // 
            // acceptFriendButton
            // 
            this.acceptFriendButton.BackColor = System.Drawing.Color.SandyBrown;
            resources.ApplyResources(this.acceptFriendButton, "acceptFriendButton");
            this.acceptFriendButton.Name = "acceptFriendButton";
            this.acceptFriendButton.UseVisualStyleBackColor = false;
            // 
            // FriendName
            // 
            resources.ApplyResources(this.FriendName, "FriendName");
            this.FriendName.Name = "FriendName";
            // 
            // friendLabel
            // 
            resources.ApplyResources(this.friendLabel, "friendLabel");
            this.friendLabel.BackColor = System.Drawing.Color.Black;
            this.friendLabel.ForeColor = System.Drawing.Color.SeaShell;
            this.friendLabel.Name = "friendLabel";
            // 
            // friendsButton
            // 
            this.friendsButton.BackColor = System.Drawing.Color.SandyBrown;
            this.friendsButton.BackgroundImage = global::MemoBrew.Properties.Resources.add_user;
            resources.ApplyResources(this.friendsButton, "friendsButton");
            this.friendsButton.Name = "friendsButton";
            this.friendsButton.UseVisualStyleBackColor = false;
            // 
            // eventButton
            // 
            this.eventButton.BackColor = System.Drawing.Color.SandyBrown;
            this.eventButton.BackgroundImage = global::MemoBrew.Properties.Resources.plus;
            resources.ApplyResources(this.eventButton, "eventButton");
            this.eventButton.Name = "eventButton";
            this.eventButton.UseVisualStyleBackColor = false;
            this.eventButton.Click += new System.EventHandler(this.eventButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.SandyBrown;
            this.homeButton.BackgroundImage = global::MemoBrew.Properties.Resources.home__1_;
            resources.ApplyResources(this.homeButton, "homeButton");
            this.homeButton.Name = "homeButton";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MemoBrew.Properties.Resources.orange;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MemoBrew.Properties.Resources.black;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // FriendRequests
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.friendsButton);
            this.Controls.Add(this.eventButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.friendLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.friendInfoPanel);
            this.Name = "FriendRequests";
            this.friendInfoPanel.ResumeLayout(false);
            this.friendInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel friendInfoPanel;
        private System.Windows.Forms.Label FriendName;
        private System.Windows.Forms.Button rejectFriendButton;
        private System.Windows.Forms.Button acceptFriendButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label friendLabel;
        private System.Windows.Forms.Button friendsButton;
        private System.Windows.Forms.Button eventButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}