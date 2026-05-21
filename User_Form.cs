using P5_Frontend_Car_App.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using P5_Frontend_Car_App.DTOs.User;

namespace P5_Frontend_Car_App
{
    public partial class User_Form : Form
    {
        int selectedUserId = 0;

        private readonly IApiService api;

        public User_Form(IApiService apiService)
        {
            InitializeComponent();

            this.AutoScroll = true;

            api = apiService;
        }

        private async void User_Form_Load(
            object sender,
            EventArgs e)
        {
            ApplyTheme();

            StyleButton(btnAdd);

            ApplyResponsiveLayout();

            txtPassword.UseSystemPasswordChar = true;

            dataGridViewUser.AutoGenerateColumns = true;

            dataGridViewUser.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            await LoadUsers();

            ClearForm();
        }

        void ApplyTheme()
        {
            this.BackColor = Color.White;
        }

        void StyleButton(Button btn)
        {
            btn.BackColor =
                Color.FromArgb(0, 120, 215);

            btn.ForeColor = Color.White;

            btn.FlatStyle = FlatStyle.Flat;

            btn.FlatAppearance.BorderSize = 0;
        }

        void ApplyResponsiveLayout()
        {
            dataGridViewUser.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtFullName.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtEmail.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtUsername.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtPassword.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            btnAdd.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;
        }
        void AddButtonsToGrid()
        {
            if (dataGridViewUser.Columns["Edit"] != null)
                return;

            dataGridViewUser.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                });

            dataGridViewUser.Columns.Add(
                new DataGridViewButtonColumn
                {
                    Name = "Delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                });
        }

        void StyleGridButtons()
        {
            foreach (DataGridViewRow row
                in dataGridViewUser.Rows)
            {
                DataGridViewButtonCell editBtn =
                    (DataGridViewButtonCell)
                    row.Cells["Edit"];

                editBtn.Style.BackColor =
                    Color.DodgerBlue;

                editBtn.Style.ForeColor =
                    Color.White;

                DataGridViewButtonCell deleteBtn =
                    (DataGridViewButtonCell)
                    row.Cells["Delete"];

                deleteBtn.Style.BackColor =
                    Color.Red;

                deleteBtn.Style.ForeColor =
                    Color.White;
            }
        }

        private async void dataGridViewUser_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int id =
                Convert.ToInt32(
                    dataGridViewUser
                    .Rows[e.RowIndex]
                    .Cells["Id"].Value);

            string col =
                dataGridViewUser
                .Columns[e.ColumnIndex]
                .Name;

            if (col == "Edit")
            {
                LoadUserToForm(e.RowIndex);
            }
            else if (col == "Delete")
            {
                var btnCell = (DataGridViewButtonCell)
        dataGridViewUser.Rows[e.RowIndex].Cells[e.ColumnIndex];

                await DeleteUser(id, btnCell);
            }
        }

        void LoadUserToForm(int rowIndex)
        {
            var row =
                dataGridViewUser.Rows[rowIndex];

            selectedUserId =
                Convert.ToInt32(
                    row.Cells["Id"].Value);

            txtFullName.Text =
                row.Cells["FullName"]
                .Value.ToString();

            txtEmail.Text =
                row.Cells["Email"]
                .Value.ToString();

            txtUsername.Text =
                row.Cells["Username"]
                .Value.ToString();

            btnAdd.Text = "Update";
        }

       
        async Task LoadUsers()
        {
            try
            {
                var users = await api.GetAsync<List<UserDto>>("user");

                if (users == null || users.Count == 0)
                {
                    dataGridViewUser.DataSource = null;
                    return;
                }

                dataGridViewUser.DataSource = users;

                AddButtonsToGrid();
                StyleGridButtons();

                // Optional: hide columns you don’t want
                // dataGridViewUser.Columns["Password"]?.Visible = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load users");
                MessageBox.Show(ex.Message);
            }
        }
        async Task DeleteUser(int id, DataGridViewButtonCell btnCell)
        {
            // Prevent double click
            if (btnCell.ReadOnly)
                return;

            var confirm = MessageBox.Show(
                "Delete this user?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            bool gridRefreshed = false;

            try
            {
                // "Disable" button
                btnCell.ReadOnly = true;
                btnCell.Value = "Deleting...";
                btnCell.Style.ForeColor = Color.Gray;

                dataGridViewUser.Refresh();

                Cursor.Current = Cursors.WaitCursor;

                await api.DeleteAsync($"User/{id}");

                Log.Information("User deleted: {Id}", id);

                await LoadUsers();

                gridRefreshed = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Delete failed for user {Id}", id);

                MessageBox.Show(
                    $"Delete failed:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Restore ONLY if grid wasn't refreshed
                if (!gridRefreshed)
                {
                    btnCell.ReadOnly = false;
                    btnCell.Value = "Delete";
                    btnCell.Style.ForeColor = Color.White;
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }

                if (selectedUserId == 0 &&
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required");
                    return;
                }

                var dto = new CreateUserRequestDto
                {
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Text
                };

                if (selectedUserId == 0)
                {
                    await api.PostAsync<UserDto>("user", dto);

                    Log.Information("User added: {Username}", dto.Username);
                }
                else
                {
                    await api.PutAsync($"user/{selectedUserId}", dto);

                    Log.Information("User updated: {Id}", selectedUserId);
                }

                MessageBox.Show("Operation successful");

                ClearForm();
                await LoadUsers();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to save user");
                MessageBox.Show(ex.Message);
            }
        }

        void ClearForm()
        {
            selectedUserId = 0;

            btnAdd.Text = "Add";

            txtFullName.Clear();

            txtEmail.Clear();

            txtUsername.Clear();

            txtPassword.Clear();
        }
    }
}
    

