namespace MemoBrew
{
    partial class AddFriendsOccasion
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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.addFriendsButton = new System.Windows.Forms.Button();
            this.goBackOccasionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.SeaShell;
            this.txtUsername.Location = new System.Drawing.Point(65, 24);
            this.txtUsername.Multiline = true;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(314, 35);
            this.txtUsername.TabIndex = 3;
            // 
            // addFriendsButton
            // 
            this.addFriendsButton.BackColor = System.Drawing.Color.Black;
            this.addFriendsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addFriendsButton.ForeColor = System.Drawing.Color.SeaShell;
            this.addFriendsButton.Location = new System.Drawing.Point(65, 80);
            this.addFriendsButton.Name = "addFriendsButton";
            this.addFriendsButton.Size = new System.Drawing.Size(142, 36);
            this.addFriendsButton.TabIndex = 31;
            this.addFriendsButton.Text = "Add";
            this.addFriendsButton.UseVisualStyleBackColor = false;
            // 
            // goBackOccasionButton
            // 
            this.goBackOccasionButton.BackColor = System.Drawing.Color.Black;
            this.goBackOccasionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goBackOccasionButton.ForeColor = System.Drawing.Color.SeaShell;
            this.goBackOccasionButton.Location = new System.Drawing.Point(237, 80);
            this.goBackOccasionButton.Name = "goBackOccasionButton";
            this.goBackOccasionButton.Size = new System.Drawing.Size(142, 36);
            this.goBackOccasionButton.TabIndex = 32;
            this.goBackOccasionButton.Text = "Back";
            this.goBackOccasionButton.UseVisualStyleBackColor = false;
            this.goBackOccasionButton.Click += new System.EventHandler(this.goBackOccasionButton_Click);
            // 
            // AddFriendsOccasion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(445, 145);
            this.Controls.Add(this.goBackOccasionButton);
            this.Controls.Add(this.addFriendsButton);
            this.Controls.Add(this.txtUsername);
            this.Name = "AddFriendsOccasion";
            this.Text = "AddFriendsOccasion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button addFriendsButton;
        private System.Windows.Forms.Button goBackOccasionButton;
    }
}