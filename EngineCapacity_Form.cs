using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P5_Frontend_Car_App.Models;
using Serilog;

namespace P5_Frontend_Car_App
{
    public partial class EngineCapacity_Form : Form
    {
        int selectedEngineId = 0;
        ApiService api = new ApiService();

        public EngineCapacity_Form()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        private async void EngineCapacity_form_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            StyleButton(button1);
            ApplyResponsiveLayout();

            dataGridViewEngine.AutoGenerateColumns = true;
            dataGridViewEngine.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            await LoadEngineCapacity();
        }

        void ApplyResponsiveLayout()
        {
            dataGridViewEngine.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCapacity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        void StyleButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 120, 215);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
        }

        void ApplyTheme()
        {
            this.BackColor = Color.White;
        }

        async Task LoadEngineCapacity()
        {
            try
            {
                var list = await api.GetAsync<List<EngineCapacity>>("EngineCapacity");

                dataGridViewEngine.DataSource = list
                    .Select(e => new
                    {
                        e.Id,
                        e.Name,
                        e.Description,
                        e.Capacity
                    })
                    .ToList();

                AddButtons();
                StyleGridButtons();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load engine capacity.");
                MessageBox.Show("Failed to load data");
            }
        }

        void AddButtons()
        {
            if (dataGridViewEngine.Columns["ViewCars"] != null)
                return;

            dataGridViewEngine.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "ViewCars",
                Text = "View Cars",
                UseColumnTextForButtonValue = true
            });

            dataGridViewEngine.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true
            });

            dataGridViewEngine.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            });
        }

        private async void dataGridViewEngine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dataGridViewEngine.Rows[e.RowIndex].Cells["Id"].Value);
            string col = dataGridViewEngine.Columns[e.ColumnIndex].Name;

            if (col == "ViewCars")
            {
                var form = new Car_Form(null, id);
                form.ShowDialog();
            }
            else if (col == "Edit")
            {
                txtName.Text = dataGridViewEngine.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtDescription.Text = dataGridViewEngine.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                txtCapacity.Text = dataGridViewEngine.Rows[e.RowIndex].Cells["Capacity"].Value.ToString();

                selectedEngineId = id;
                button1.Text = "Update";
            }
            else if (col == "Delete")
            {
                await DeleteEngine(id);
            }
        }
        void StyleGridButtons()
        {
            foreach (DataGridViewRow row in dataGridViewEngine.Rows)
            {
                // Edit button
                DataGridViewButtonCell editBtn =
                    (DataGridViewButtonCell)row.Cells["Edit"];

                editBtn.Style.BackColor = Color.DodgerBlue;
                editBtn.Style.ForeColor = Color.White;
                editBtn.Style.SelectionBackColor = Color.RoyalBlue;
                editBtn.Style.SelectionForeColor = Color.White;

                // Delete button
                DataGridViewButtonCell deleteBtn =
                    (DataGridViewButtonCell)row.Cells["Delete"];

                deleteBtn.Style.BackColor = Color.Red;
                deleteBtn.Style.ForeColor = Color.White;
                deleteBtn.Style.SelectionBackColor = Color.DarkRed;
                deleteBtn.Style.SelectionForeColor = Color.White;

                //view cars button
                DataGridViewButtonCell viewBtn =
                   (DataGridViewButtonCell)row.Cells["ViewCars"];

                viewBtn.Style.BackColor = Color.SeaGreen;
                viewBtn.Style.ForeColor = Color.White;
                viewBtn.Style.SelectionBackColor = Color.DarkGreen;
                viewBtn.Style.SelectionForeColor = Color.White;
            }
        }
        async Task DeleteEngine(int id)
        {
            try
            {
                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this engine capacity?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                await api.DeleteAsync($"api/EngineCapacity/{id}");

                await LoadEngineCapacity();

                Log.Warning("Engine Capacity deleted: {Id}", id);

                MessageBox.Show("Engine capacity deleted successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to delete engine capacity.");

                MessageBox.Show(
                    "Cannot delete engine capacity because it is linked to cars.");
            }
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtDescription.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacity.Text))
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }
                var engines = await api.GetAsync<List<EngineCapacity>>("EngineCapacity");

                bool exists = engines.Any(e =>
                   (e.Name.Trim().ToLower() == txtName.Text.Trim().ToLower()
                  ||e.Capacity.Trim().ToLower() == txtCapacity.Text.Trim().ToLower())
                  && e.Id != selectedEngineId);

                if (exists)
                {
                    MessageBox.Show("Engine name or capacity already exists");
                    return;
                }

                var data = new EngineCapacity
                {
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Capacity = txtCapacity.Text.Trim()
                };

                if (selectedEngineId == 0)
                {
                    await api.PostAsync("EngineCapacity", data);
                    Log.Information("User added new engine capacity: {EngineCapacityName}", txtName.Text);
                }
                else
                {
                    data.Id = selectedEngineId;
                    await api.PutAsync($"EngineCapacity/{selectedEngineId}", data);
                    Log.Information("User updated engine capacity: {EngineCapacityId}, Name: {EngineCapacityName}", selectedEngineId, txtName.Text);
                }

                ResetForm();
                await LoadEngineCapacity();
            }
            catch( Exception ex ) 
            {
                Log.Error(ex, "Failed to add/update engine capacity.");
                MessageBox.Show("Operation failed");
            }
        }

        void ResetForm()
        {
            selectedEngineId = 0;
            button1.Text = "Add";

            txtName.Clear();
            txtDescription.Clear();
            txtCapacity.Clear();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
