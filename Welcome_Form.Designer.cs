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
            btnExploreCars = new Button();
            lblUsername = new Label();
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
            btnHomepage.Location = new Point(550, 536);
            btnHomepage.Name = "btnHomepage";
            btnHomepage.Size = new Size(138, 49);
            btnHomepage.TabIndex = 2;
            btnHomepage.Text = "Home Page";
            btnHomepage.UseVisualStyleBackColor = true;
            btnHomepage.Click += btnHomepage_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(654, 591);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(94, 49);
            btnExit.TabIndex = 3;
            btnExit.Text = "Logout";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // pictureBoxBanner
            // 
            pictureBoxBanner.Image = (Image)resources.GetObject("pictureBoxBanner.Image");
            pictureBoxBanner.Location = new Point(262, 156);
            pictureBoxBanner.Name = "pictureBoxBanner";
            pictureBoxBanner.Size = new Size(943, 356);
            pictureBoxBanner.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxBanner.TabIndex = 4;
            pictureBoxBanner.TabStop = false;
            // 
            // lblFooter
            // 
            lblFooter.AutoSize = true;
            lblFooter.Location = new Point(550, 681);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(303, 20);
            lblFooter.TabIndex = 5;
            lblFooter.Text = "© 2026 Car Showroom Management System";
            // 
            // btnExploreCars
            // 
            btnExploreCars.Location = new Point(715, 536);
            btnExploreCars.Name = "btnExploreCars";
            btnExploreCars.Size = new Size(138, 49);
            btnExploreCars.TabIndex = 8;
            btnExploreCars.Text = "Explore Cars";
            btnExploreCars.UseVisualStyleBackColor = true;
            btnExploreCars.Click += btnExploreCars_Click;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(262, 60);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(50, 20);
            lblUsername.TabIndex = 10;
            lblUsername.Text = "label2";
            // 
            // Welcome_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1468, 741);
            Controls.Add(lblUsername);
            Controls.Add(btnExploreCars);
            Controls.Add(lblFooter);
            Controls.Add(pictureBoxBanner);
            Controls.Add(btnExit);
            Controls.Add(btnHomepage);
            Controls.Add(lblTagline);
            Controls.Add(lblTitle);
            Name = "Welcome_Form";
            Text = "Welcome_Form";
            Load += WelcomeForm_Load;
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
        private Button btnExploreCars;
        private Label lblUsername;
    }
}