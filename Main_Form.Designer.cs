namespace P5_Frontend_Car_App
{
    partial class Main_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(520, 284);
            button1.Name = "button1";
            button1.Size = new Size(136, 60);
            button1.TabIndex = 0;
            button1.Text = "Car Form";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnCars_Click;
            // 
            // button2
            // 
            button2.Location = new Point(691, 284);
            button2.Name = "button2";
            button2.Size = new Size(136, 60);
            button2.TabIndex = 1;
            button2.Text = "Manufacturer Form";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnManf_Click;
            // 
            // button3
            // 
            button3.Location = new Point(868, 284);
            button3.Name = "button3";
            button3.Size = new Size(136, 60);
            button3.TabIndex = 2;
            button3.Text = "Engine Capacity Form";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnEngine_Click;
            // 
            // button4
            // 
            button4.Location = new Point(340, 284);
            button4.Name = "button4";
            button4.Size = new Size(136, 68);
            button4.TabIndex = 3;
            button4.Text = "Dashboard";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnDashboard_Click;
            // 
            // Main_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1362, 736);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Main_Form";
            Text = "Form1";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
