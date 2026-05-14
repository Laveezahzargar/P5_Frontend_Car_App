namespace P5_Frontend_Car_App
{
    partial class Welcome_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome_Form));
            lblTitle = new Label();
            lblTagline = new Label();
            btnHomepage = new Button();
            btnExit = new Button();
            pictureBoxBanner = new PictureBox();
            lblFooter = new Label();
            btnSignIn = new Button();
            btnSignUp = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBanner).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(641, 89);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(107, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Car Showroom";
            lblTitle.Click += label1_Click;
            // 
            // lblTagline
            // 
            lblTagline.AutoSize = true;
            lblTagline.Location = new Point(619, 122);
            lblTagline.Name = "lblTagline";
            lblTagline.Size = new Size(170, 20);
            lblTagline.TabIndex = 1;
            lblTagline.Text = "Drive Your Dream Today";
            // 
            // btnHomepage
            // 
            btnHomepage.Location = new Point(619, 565);
            btnHomepage.Name = "btnHomepage";
            btnHomepage.Size = new Size(138, 49);
            btnHomepage.TabIndex = 2;
            btnHomepage.Text = "Home Page";
            btnHomepage.UseVisualStyleBackColor = true;
            btnHomepage.Click += btnHomepage_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(641, 620);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(94, 49);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // pictureBoxBanner
            // 
            pictureBoxBanner.Image = (Image)resources.GetObject("pictureBoxBanner.Image");
            pictureBoxBanner.Location = new Point(304, 181);
            pictureBoxBanner.Name = "pictureBoxBanner";
            pictureBoxBanner.Size = new Size(815, 362);
            pictureBoxBanner.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBanner.TabIndex = 4;
            pictureBoxBanner.TabStop = false;
            // 
            // lblFooter
            // 
            lblFooter.AutoSize = true;
            lblFooter.Location = new Point(538, 701);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(303, 20);
            lblFooter.TabIndex = 5;
            lblFooter.Text = "© 2026 Car Showroom Management System";
            // 
            // btnSignIn
            // 
            btnSignIn.Location = new Point(314, 37);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(90, 35);
            btnSignIn.TabIndex = 6;
            btnSignIn.Text = "Sign In";
            btnSignIn.UseVisualStyleBackColor = true;
            btnSignIn.Click += btnSignIn_Click;
            // 
            // btnSignUp
            // 
            btnSignUp.Location = new Point(414, 37);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(90, 35);
            btnSignUp.TabIndex = 7;
            btnSignUp.Text = "Sign Up";
            btnSignUp.UseVisualStyleBackColor = true;
            btnSignUp.Click += btnSignUp_Click;
            // 
            // Welcome_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1468, 741);
            Controls.Add(btnSignUp);
            Controls.Add(btnSignIn);
            Controls.Add(lblFooter);
            Controls.Add(pictureBoxBanner);
            Controls.Add(btnExit);
            Controls.Add(btnHomepage);
            Controls.Add(lblTagline);
            Controls.Add(lblTitle);
            Name = "Welcome_Form";
            Text = "Welcome_Form";
            ((System.ComponentModel.ISupportInitialize)pictureBoxBanner).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblTagline;
        private Button btnHomepage;
        private Button btnExit;
        private PictureBox pictureBoxBanner;
        private Label lblFooter;
        private Button btnSignIn;
        private Button btnSignUp;
    }
}