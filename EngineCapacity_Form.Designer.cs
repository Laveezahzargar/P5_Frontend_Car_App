namespace P5_Frontend_Car_App
{
    partial class EngineCapacity_Form
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
            txtName = new TextBox();
            txtCapacity = new TextBox();
            txtDescription = new TextBox();
            button1 = new Button();
            dataGridViewEngine = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEngine).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(363, 120);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(363, 158);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 1;
            label2.Text = "Capacity";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(363, 199);
            label3.Name = "label3";
            label3.Size = new Size(85, 20);
            label3.TabIndex = 2;
            label3.Text = "Description";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(215, 120);
            label4.Name = "label4";
            label4.Size = new Size(24, 20);
            label4.TabIndex = 3;
            label4.Text = "1 .";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(215, 158);
            label5.Name = "label5";
            label5.Size = new Size(24, 20);
            label5.TabIndex = 4;
            label5.Text = "2 .";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(215, 199);
            label6.Name = "label6";
            label6.Size = new Size(24, 20);
            label6.TabIndex = 5;
            label6.Text = "3 .";
            // 
            // txtName
            // 
            txtName.Location = new Point(541, 113);
            txtName.Name = "txtName";
            txtName.Size = new Size(640, 27);
            txtName.TabIndex = 6;
            // 
            // txtCapacity
            // 
            txtCapacity.Location = new Point(541, 151);
            txtCapacity.Name = "txtCapacity";
            txtCapacity.Size = new Size(640, 27);
            txtCapacity.TabIndex = 7;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(541, 192);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(640, 27);
            txtDescription.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(1045, 242);
            button1.Name = "button1";
            button1.Size = new Size(105, 47);
            button1.TabIndex = 9;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnAdd_Click;
            // 
            // dataGridViewEngine
            // 
            dataGridViewEngine.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEngine.Location = new Point(75, 317);
            dataGridViewEngine.Name = "dataGridViewEngine";
            dataGridViewEngine.RowHeadersWidth = 51;
            dataGridViewEngine.Size = new Size(1214, 388);
            dataGridViewEngine.TabIndex = 10;
            dataGridViewEngine.CellContentClick += dataGridViewEngine_CellContentClick;
            // 
            // EngineCapacity_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1362, 738);
            Controls.Add(dataGridViewEngine);
            Controls.Add(button1);
            Controls.Add(txtDescription);
            Controls.Add(txtCapacity);
            Controls.Add(txtName);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "EngineCapacity_Form";
            Text = "EngineCapacity_Form";
            Load += EngineCapacity_form_Load;
            Click += EngineCapacity_form_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEngine).EndInit();
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
        private TextBox txtName;
        private TextBox txtCapacity;
        private TextBox txtDescription;
        private Button button1;
        private DataGridView dataGridViewEngine;
    }
}