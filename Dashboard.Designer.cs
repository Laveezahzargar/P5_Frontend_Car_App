namespace P5_Frontend_Car_App
{
    partial class Dashboard
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
            label = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            panelCars = new Panel();
            lblTotalCars = new Label();
            panelManufacturers = new Panel();
            lblTotalManufacturers = new Label();
            panelEngines = new Panel();
            lblTotalEngines = new Label();
            panelAverage = new Panel();
            lblAveragePrice = new Label();
            panelFuel = new Panel();
            lblTopFuel = new Label();
            panelYear = new Panel();
            lblNewestYear = new Label();
            panelCars.SuspendLayout();
            panelManufacturers.SuspendLayout();
            panelEngines.SuspendLayout();
            panelAverage.SuspendLayout();
            panelFuel.SuspendLayout();
            panelYear.SuspendLayout();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(34, 17);
            label.Name = "label";
            label.Size = new Size(74, 20);
            label.TabIndex = 0;
            label.Text = "Total Cars";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(34, 18);
            label7.Name = "label7";
            label7.Size = new Size(140, 20);
            label7.TabIndex = 1;
            label7.Text = "Total Manufacturers";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(34, 15);
            label8.Name = "label8";
            label8.Size = new Size(97, 20);
            label8.TabIndex = 2;
            label8.Text = "Total Engines";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(34, 16);
            label9.Name = "label9";
            label9.Size = new Size(100, 20);
            label9.TabIndex = 3;
            label9.Text = "Average Price";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(34, 15);
            label10.Name = "label10";
            label10.Size = new Size(65, 20);
            label10.TabIndex = 4;
            label10.Text = "Top Fuel";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(34, 15);
            label11.Name = "label11";
            label11.Size = new Size(90, 20);
            label11.TabIndex = 5;
            label11.Text = "Newest Year";
            label11.Click += label6_Click;
            // 
            // panelCars
            // 
            panelCars.Controls.Add(lblTotalCars);
            panelCars.Controls.Add(label);
            panelCars.Location = new Point(271, 77);
            panelCars.Name = "panelCars";
            panelCars.Size = new Size(769, 52);
            panelCars.TabIndex = 6;
            // 
            // lblTotalCars
            // 
            lblTotalCars.AutoSize = true;
            lblTotalCars.Location = new Point(301, 17);
            lblTotalCars.Name = "lblTotalCars";
            lblTotalCars.Size = new Size(50, 20);
            lblTotalCars.TabIndex = 1;
            lblTotalCars.Text = "label1";
            // 
            // panelManufacturers
            // 
            panelManufacturers.Controls.Add(lblTotalManufacturers);
            panelManufacturers.Controls.Add(label7);
            panelManufacturers.Location = new Point(271, 135);
            panelManufacturers.Name = "panelManufacturers";
            panelManufacturers.Size = new Size(769, 52);
            panelManufacturers.TabIndex = 7;
            // 
            // lblTotalManufacturers
            // 
            lblTotalManufacturers.AutoSize = true;
            lblTotalManufacturers.Location = new Point(301, 18);
            lblTotalManufacturers.Name = "lblTotalManufacturers";
            lblTotalManufacturers.Size = new Size(50, 20);
            lblTotalManufacturers.TabIndex = 2;
            lblTotalManufacturers.Text = "label2";
            // 
            // panelEngines
            // 
            panelEngines.Controls.Add(lblTotalEngines);
            panelEngines.Controls.Add(label8);
            panelEngines.Location = new Point(271, 193);
            panelEngines.Name = "panelEngines";
            panelEngines.Size = new Size(769, 52);
            panelEngines.TabIndex = 8;
            // 
            // lblTotalEngines
            // 
            lblTotalEngines.AutoSize = true;
            lblTotalEngines.Location = new Point(301, 15);
            lblTotalEngines.Name = "lblTotalEngines";
            lblTotalEngines.Size = new Size(50, 20);
            lblTotalEngines.TabIndex = 3;
            lblTotalEngines.Text = "label3";
            // 
            // panelAverage
            // 
            panelAverage.Controls.Add(lblAveragePrice);
            panelAverage.Controls.Add(label9);
            panelAverage.Location = new Point(271, 251);
            panelAverage.Name = "panelAverage";
            panelAverage.Size = new Size(769, 52);
            panelAverage.TabIndex = 9;
            // 
            // lblAveragePrice
            // 
            lblAveragePrice.AutoSize = true;
            lblAveragePrice.Location = new Point(301, 16);
            lblAveragePrice.Name = "lblAveragePrice";
            lblAveragePrice.Size = new Size(50, 20);
            lblAveragePrice.TabIndex = 4;
            lblAveragePrice.Text = "label4";
            // 
            // panelFuel
            // 
            panelFuel.Controls.Add(lblTopFuel);
            panelFuel.Controls.Add(label10);
            panelFuel.Location = new Point(271, 309);
            panelFuel.Name = "panelFuel";
            panelFuel.Size = new Size(769, 52);
            panelFuel.TabIndex = 10;
            // 
            // lblTopFuel
            // 
            lblTopFuel.AutoSize = true;
            lblTopFuel.Location = new Point(301, 15);
            lblTopFuel.Name = "lblTopFuel";
            lblTopFuel.Size = new Size(50, 20);
            lblTopFuel.TabIndex = 5;
            lblTopFuel.Text = "label5";
            // 
            // panelYear
            // 
            panelYear.Controls.Add(lblNewestYear);
            panelYear.Controls.Add(label11);
            panelYear.Location = new Point(271, 367);
            panelYear.Name = "panelYear";
            panelYear.Size = new Size(769, 52);
            panelYear.TabIndex = 11;
            // 
            // lblNewestYear
            // 
            lblNewestYear.AutoSize = true;
            lblNewestYear.Location = new Point(301, 15);
            lblNewestYear.Name = "lblNewestYear";
            lblNewestYear.Size = new Size(50, 20);
            lblNewestYear.TabIndex = 6;
            lblNewestYear.Text = "label6";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1358, 739);
            Controls.Add(panelYear);
            Controls.Add(panelFuel);
            Controls.Add(panelAverage);
            Controls.Add(panelEngines);
            Controls.Add(panelManufacturers);
            Controls.Add(panelCars);
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Form_Load;
            Resize += Dashboard_Resize;
            panelCars.ResumeLayout(false);
            panelCars.PerformLayout();
            panelManufacturers.ResumeLayout(false);
            panelManufacturers.PerformLayout();
            panelEngines.ResumeLayout(false);
            panelEngines.PerformLayout();
            panelAverage.ResumeLayout(false);
            panelAverage.PerformLayout();
            panelFuel.ResumeLayout(false);
            panelFuel.PerformLayout();
            panelYear.ResumeLayout(false);
            panelYear.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Panel panelCars;
        private Panel panelManufacturers;
        private Panel panelEngines;
        private Panel panelAverage;
        private Panel panelFuel;
        private Panel panelYear;
        private Label lblTotalCars;
        private Label lblTotalManufacturers;
        private Label lblTotalEngines;
        private Label lblAveragePrice;
        private Label lblTopFuel;
        private Label lblNewestYear;
    }
}