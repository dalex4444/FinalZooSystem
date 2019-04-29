namespace Boundary
{
    partial class LoginForm
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
            this.unameLabel = new System.Windows.Forms.Label();
            this.pwordLabel = new System.Windows.Forms.Label();
            this.unameTextbox = new System.Windows.Forms.TextBox();
            this.pwordTextbox = new System.Windows.Forms.TextBox();
            this.Login_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // unameLabel
            // 
            this.unameLabel.AutoSize = true;
            this.unameLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unameLabel.Location = new System.Drawing.Point(169, 169);
            this.unameLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.unameLabel.Name = "unameLabel";
            this.unameLabel.Size = new System.Drawing.Size(161, 36);
            this.unameLabel.TabIndex = 0;
            this.unameLabel.Text = "Username";
            // 
            // pwordLabel
            // 
            this.pwordLabel.AutoSize = true;
            this.pwordLabel.Font = new System.Drawing.Font("Arial", 24F);
            this.pwordLabel.Location = new System.Drawing.Point(169, 246);
            this.pwordLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.pwordLabel.Name = "pwordLabel";
            this.pwordLabel.Size = new System.Drawing.Size(153, 36);
            this.pwordLabel.TabIndex = 1;
            this.pwordLabel.Text = "Password";
            // 
            // unameTextbox
            // 
            this.unameTextbox.Location = new System.Drawing.Point(347, 166);
            this.unameTextbox.Name = "unameTextbox";
            this.unameTextbox.Size = new System.Drawing.Size(281, 44);
            this.unameTextbox.TabIndex = 2;
            // 
            // pwordTextbox
            // 
            this.pwordTextbox.Location = new System.Drawing.Point(347, 246);
            this.pwordTextbox.Name = "pwordTextbox";
            this.pwordTextbox.PasswordChar = '*';
            this.pwordTextbox.Size = new System.Drawing.Size(281, 44);
            this.pwordTextbox.TabIndex = 3;
            // 
            // Login_Button
            // 
            this.Login_Button.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login_Button.Location = new System.Drawing.Point(431, 328);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(113, 41);
            this.Login_Button.TabIndex = 4;
            this.Login_Button.Text = "Login";
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.Login_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 36F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 514);
            this.Controls.Add(this.Login_Button);
            this.Controls.Add(this.pwordTextbox);
            this.Controls.Add(this.unameTextbox);
            this.Controls.Add(this.pwordLabel);
            this.Controls.Add(this.unameLabel);
            this.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label unameLabel;
        private System.Windows.Forms.Label pwordLabel;
        public System.Windows.Forms.TextBox unameTextbox;
        public System.Windows.Forms.TextBox pwordTextbox;
        private System.Windows.Forms.Button Login_Button;
    }
}