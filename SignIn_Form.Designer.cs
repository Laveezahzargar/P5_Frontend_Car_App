namespace P5_Frontend_Car_App
{
    partial class SignIn_Form
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            label5 = new Label();
            linkForgot = new LinkLabel();
            linkRegister = new LinkLabel();
            chkShowPassword = new CheckBox();
            label6 = new Label();
            lblOtp = new Label();
            txtOtp = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(657, 102);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 1;
            label2.Text = "Welcome Back";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(571, 148);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 2;
            label3.Text = "Username";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(576, 188);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 3;
            label4.Text = "Password";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(686, 148);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(198, 27);
            txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(686, 181);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(198, 27);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(780, 277);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 29);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(642, 446);
            label5.Name = "label5";
            label5.Size = new Size(167, 20);
            label5.TabIndex = 7;
            label5.Text = "Don't have an account ?";
            // 
            // linkForgot
            // 
            linkForgot.AutoSize = true;
            linkForgot.LinkBehavior = LinkBehavior.NeverUnderline;
            linkForgot.LinkColor = Color.DimGray;
            linkForgot.Location = new Point(657, 416);
            linkForgot.Name = "linkForgot";
            linkForgot.Size = new Size(125, 20);
            linkForgot.TabIndex = 8;
            linkForgot.TabStop = true;
            linkForgot.Text = "Forgot Password?";
            // 
            // linkRegister
            // 
            linkRegister.AutoSize = true;
            linkRegister.LinkBehavior = LinkBehavior.NeverUnderline;
            linkRegister.LinkColor = Color.DimGray;
            linkRegister.Location = new Point(672, 482);
            linkRegister.Name = "linkRegister";
            linkRegister.Size = new Size(110, 20);
            linkRegister.TabIndex = 9;
            linkRegister.TabStop = true;
            linkRegister.Text = "Create Account";
            linkRegister.Click += btnRegister_Click;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(571, 324);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(132, 24);
            chkShowPassword.TabIndex = 10;
            chkShowPassword.Text = "Show Password";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.Click += chkShowPassword_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(571, 570);
            label6.Name = "label6";
            label6.Size = new Size(303, 20);
            label6.TabIndex = 11;
            label6.Text = "© 2026 Car Showroom Management System";
            // 
            // lblOtp
            // 
            lblOtp.AutoSize = true;
            lblOtp.Location = new Point(576, 230);
            lblOtp.Name = "lblOtp";
            lblOtp.Size = new Size(35, 20);
            lblOtp.TabIndex = 13;
            lblOtp.Text = "OTP";
            // 
            // txtOtp
            // 
            txtOtp.Location = new Point(686, 227);
            txtOtp.Name = "txtOtp";
            txtOtp.Size = new Size(198, 27);
            txtOtp.TabIndex = 14;
            txtOtp.TextChanged += textBox1_TextChanged;
            // 
            // SignIn_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1432, 726);
            Controls.Add(txtOtp);
            Controls.Add(lblOtp);
            Controls.Add(label6);
            Controls.Add(chkShowPassword);
            Controls.Add(linkRegister);
            Controls.Add(linkForgot);
            Controls.Add(label5);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "SignIn_Form";
            Text = "SignIn_Form";
            Click += SignIn_Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label label5;
        private LinkLabel linkForgot;
        private LinkLabel linkRegister;
        private CheckBox chkShowPassword;
        private Label label6;
        private Label lblOtp;
        private TextBox txtOtp;
    }
}