namespace P5_Frontend_Car_App
{
    partial class SignUp_Form
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
            lblTitle = new Label();
            lblTagline = new Label();
            lblFullName = new Label();
            lblEmail = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            lblConfirmPassword = new Label();
            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            lblFooter = new Label();
            btnSignUp = new Button();
            lblInfo = new Label();
            linkLogin = new LinkLabel();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(623, 52);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(132, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CREATE ACCOUNT";
            // 
            // lblTagline
            // 
            lblTagline.AutoSize = true;
            lblTagline.Location = new Point(601, 84);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(194, 20);
            lblTagline.TabIndex = 1;
            lblTagline.Text = "Join the Car Showroom App";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(513, 131);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(76, 20);
            lblFullName.TabIndex = 2;
            lblFullName.Text = "Full Name";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(513, 168);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(513, 205);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(75, 20);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(513, 243);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(70, 20);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password";
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(513, 281);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(127, 20);
            lblConfirmPassword.TabIndex = 6;
            lblConfirmPassword.Text = "Confirm Password";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(670, 124);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(201, 27);
            txtFullName.TabIndex = 7;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(670, 161);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(201, 27);
            txtEmail.TabIndex = 8;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(670, 198);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(201, 27);
            txtUsername.TabIndex = 9;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(670, 231);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(201, 27);
            txtPassword.TabIndex = 10;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(670, 274);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(201, 27);
            txtConfirmPassword.TabIndex = 11;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblFooter
            // 
            lblFooter.AutoSize = true;
            lblFooter.Location = new Point(539, 501);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(303, 20);
            lblFooter.TabIndex = 12;
            lblFooter.Text = "© 2026 Car Showroom Management System";
            // 
            // btnSignUp
            // 
            btnSignUp.Location = new Point(748, 330);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(94, 29);
            btnSignUp.TabIndex = 13;
            btnSignUp.Text = "Sign Up";
            btnSignUp.UseVisualStyleBackColor = true;
            btnSignUp.Click += btnSignUp_Click;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(601, 390);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(182, 20);
            lblInfo.TabIndex = 15;
            lblInfo.Text = "Already have an account ?";
            // 
            // linkLogin
            // 
            linkLogin.AutoSize = true;
            linkLogin.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLogin.LinkColor = Color.DimGray;
            linkLogin.Location = new Point(661, 420);
            linkLogin.Name = "linkLogin";
            linkLogin.Size = new Size(46, 20);
            linkLogin.TabIndex = 16;
            linkLogin.TabStop = true;
            linkLogin.Text = "Login";
            linkLogin.Click += btnLogin_Click;
            // 
            // SignUp_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1432, 733);
            Controls.Add(linkLogin);
            Controls.Add(lblInfo);
            Controls.Add(btnSignUp);
            Controls.Add(lblFooter);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(txtEmail);
            Controls.Add(txtFullName);
            Controls.Add(lblConfirmPassword);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(lblEmail);
            Controls.Add(lblFullName);
            Controls.Add(lblTagline);
            Controls.Add(lblTitle);
            Name = "SignUp_Form";
            Text = "SignUp_Form";
            Load += SignUp_Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblTagline;
        private Label lblFullName;
        private Label lblEmail;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirmPassword;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Label lblFooter;
        private Button btnSignUp;
        private Label lblInfo;
        private LinkLabel linkLogin;
    }
}