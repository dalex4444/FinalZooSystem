namespace Boundary
{
    partial class MainMenu
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
            this.AddEnclosureButton = new System.Windows.Forms.Button();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.EnclosureListBox = new System.Windows.Forms.ListBox();
            this.UpdateScheduleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddEnclosureButton
            // 
            this.AddEnclosureButton.Location = new System.Drawing.Point(691, 127);
            this.AddEnclosureButton.Name = "AddEnclosureButton";
            this.AddEnclosureButton.Size = new System.Drawing.Size(97, 23);
            this.AddEnclosureButton.TabIndex = 1;
            this.AddEnclosureButton.Text = "Add Enclosure";
            this.AddEnclosureButton.UseVisualStyleBackColor = true;
            this.AddEnclosureButton.Click += new System.EventHandler(this.AddEnclosureButton_Click);
            // 
            // LogoutButton
            // 
            this.LogoutButton.Location = new System.Drawing.Point(713, 12);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(75, 23);
            this.LogoutButton.TabIndex = 2;
            this.LogoutButton.Text = "Logout";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // EnclosureListBox
            // 
            this.EnclosureListBox.FormattingEnabled = true;
            this.EnclosureListBox.Location = new System.Drawing.Point(12, 156);
            this.EnclosureListBox.Name = "EnclosureListBox";
            this.EnclosureListBox.Size = new System.Drawing.Size(776, 277);
            this.EnclosureListBox.TabIndex = 3;
            // 
            // UpdateScheduleButton
            // 
            this.UpdateScheduleButton.Location = new System.Drawing.Point(12, 127);
            this.UpdateScheduleButton.Name = "UpdateScheduleButton";
            this.UpdateScheduleButton.Size = new System.Drawing.Size(156, 23);
            this.UpdateScheduleButton.TabIndex = 4;
            this.UpdateScheduleButton.Text = "Update Selected Schedule";
            this.UpdateScheduleButton.UseVisualStyleBackColor = true;
            this.UpdateScheduleButton.Click += new System.EventHandler(this.UpdateSchedule_Click);
            // 
            // Main_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UpdateScheduleButton);
            this.Controls.Add(this.EnclosureListBox);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.AddEnclosureButton);
            this.Name = "Main_Menu";
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button AddEnclosureButton;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.ListBox EnclosureListBox;
        private System.Windows.Forms.Button UpdateScheduleButton;
    }
}

