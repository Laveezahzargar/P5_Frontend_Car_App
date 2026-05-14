namespace P5_Frontend_Car_App
{
    partial class User_Form
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
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtFullName = new TextBox();
            txtEmail = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnAdd = new Button();
            dataGridViewUser = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUser).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(166, 118);
            label1.Name = "label1";
            label1.Size = new Size(24, 20);
            label1.TabIndex = 0;
            label1.Text = "1 .";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(166, 153);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 1;
            label2.Text = "2 .";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(166, 188);
            label3.Name = "label3";
            label3.Size = new Size(24, 20);
            label3.TabIndex = 2;
            label3.Text = "3 .";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(166, 222);
            label4.Name = "label4";
            label4.Size = new Size(24, 20);
            label4.TabIndex = 3;
            label4.Text = "4 .";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(359, 118);
            label5.Name = "label5";
            label5.Size = new Size(76, 20);
            label5.TabIndex = 4;
            label5.Text = "Full Name";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(359, 155);
            label6.Name = "label6";
            label6.Size = new Size(46, 20);
            label6.TabIndex = 5;
            label6.Text = "Email";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(359, 188);
            label7.Name = "label7";
            label7.Size = new Size(75, 20);
            label7.TabIndex = 6;
            label7.Text = "Username";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(359, 222);
            label8.Name = "label8";
            label8.Size = new Size(70, 20);
            label8.TabIndex = 7;
            label8.Text = "Password";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(570, 111);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(702, 27);
            txtFullName.TabIndex = 8;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(570, 148);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(702, 27);
            txtEmail.TabIndex = 9;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(570, 181);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(702, 27);
            txtUsername.TabIndex = 10;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(570, 215);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(702, 27);
            txtPassword.TabIndex = 11;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(1116, 259);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 45);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // dataGridViewUser
            // 
            dataGridViewUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUser.Location = new Point(81, 315);
            dataGridViewUser.Name = "dataGridViewUser";
            dataGridViewUser.RowHeadersWidth = 51;
            dataGridViewUser.Size = new Size(1270, 393);
            dataGridViewUser.TabIndex = 13;
            dataGridViewUser.CellContentClick += dataGridViewUser_CellContentClick;
            // 
            // User_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1433, 738);
            Controls.Add(dataGridViewUser);
            Controls.Add(btnAdd);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(txtEmail);
            Controls.Add(txtFullName);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "User_Form";
            Text = "User_Form";
            Load += User_Form_Load;
            Click += User_Form_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewUser).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnAdd;
        private DataGridView dataGridViewUser;
    }
}