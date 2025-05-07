using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RestaurantSystem
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public User(string username, string password, string role, string fullName)
        {
            Username = username;
            Password = password;
            Role = role;
            FullName = fullName;
            CreatedDate = DateTime.Now;
            IsActive = true;
        }

        internal static void Add(User user)
        {
            throw new NotImplementedException();
        }
    }

    public class UserManagementForm : Form
    {
        private string adminUsername;

        // Sample user list for demo purposes
        private List<User> User = new List<User>();

        // Controls
        private ListView lvwUsers;
        private Button btnAddUser;
        private Button btnEditUser;
        private Button btnDeleteUser;
        private Button btnBack;
        private GroupBox gbUserDetails;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;

        public UserManagementForm(string adminUsername)
        {
            this.adminUsername = adminUsername;
            InitializeSampleUsers();
            InitializeComponent();
            RefreshUserList();
        }

        // Method renamed to avoid ambiguity
        private void InitializeSampleUsersV2()
        {
            // Add some sample users for the demo
            User.Add(new User("waiter1", "waiter123", "Waiter", "John Smith"));
            User.Add(new User("waiter2", "waiter123", "Waiter", "Emma Johnson"));
            User.Add(new User("chef1", "chef123", "Chef", "Michael Brown"));
            User.Add(new User("chef2", "chef123", "Chef", "Sarah Davis"));
            User.Add(new User("admin1", "admin123", "Administrator", "Admin User"));
        }

        // Rename the duplicate InitializeComponent method in UserManagementFormV2
        private void InitializeComponentV2()
        {
            this.Text = "User Management V2";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Additional initialization logic for UserManagementFormV2 can go here
        }

        // Update the constructor of UserManagementFormV2 to call the renamed method
        public UserManagementForm()
        {
            this.adminUsername = adminUsername;
            InitializeSampleUsersV2(); // Call the renamed method
            InitializeComponentV2();   // Call the renamed method
            RefreshUserList();
        }

        private void InitializeComponent()
        {
            this.Text = "User Management";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // User list view
            lvwUsers = new ListView();
            lvwUsers.View = View.Details;
            lvwUsers.FullRowSelect = true;
            lvwUsers.GridLines = true;
            lvwUsers.Size = new Size(600, 450);
            lvwUsers.Location = new Point(20, 20);
            lvwUsers.SelectedIndexChanged += LvwUsers_SelectedIndexChanged;

            // Add columns to the list view
            lvwUsers.Columns.Add("Username", 100);
            lvwUsers.Columns.Add("Full Name", 150);
            lvwUsers.Columns.Add("Role", 100);
            lvwUsers.Columns.Add("Created Date", 150);
            lvwUsers.Columns.Add("Status", 100);

            // User details group box
            gbUserDetails = new GroupBox();
            gbUserDetails.Text = "User Actions";
            gbUserDetails.Size = new Size(220, 450);
            gbUserDetails.Location = new Point(640, 20);

            // Action buttons
            btnAddUser = new Button();
            btnAddUser.Text = "Add New User";
            btnAddUser.Size = new Size(180, 40);
            btnAddUser.Location = new Point(20, 40);
            btnAddUser.Click += BtnAddUser_Click;

            btnEditUser = new Button();
            btnEditUser.Text = "Edit Selected User";
            btnEditUser.Size = new Size(180, 40);
            btnEditUser.Location = new Point(20, 100);
            btnEditUser.Enabled = false;
            btnEditUser.Click += BtnEditUser_Click;

            btnDeleteUser = new Button();
            btnDeleteUser.Text = "Delete Selected User";
            btnDeleteUser.Size = new Size(180, 40);
            btnDeleteUser.Location = new Point(20, 160);
            btnDeleteUser.Enabled = false;
            btnDeleteUser.Click += BtnDeleteUser_Click;

            btnBack = new Button();
            btnBack.Text = "Back to Login";
            btnBack.Size = new Size(180, 40);
            btnBack.Location = new Point(20, 380);
            btnBack.Click += BtnBack_Click;

            // Status strip
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblStatus.Text = $"Logged in as Administrator: {adminUsername}";
            statusStrip.Items.Add(lblStatus);

            // Add controls to the form
            gbUserDetails.Controls.Add(btnAddUser);
            gbUserDetails.Controls.Add(btnEditUser);
            gbUserDetails.Controls.Add(btnDeleteUser);
            gbUserDetails.Controls.Add(btnBack);

            this.Controls.Add(lvwUsers);
            this.Controls.Add(gbUserDetails);
            this.Controls.Add(statusStrip);
        }

        private void RefreshUserList()
        {
            lvwUsers.Items.Clear();

            foreach (User user in User)
            {
                ListViewItem item = new ListViewItem(user.Username);
                item.SubItems.Add(user.FullName);
                item.SubItems.Add(user.Role);
                item.SubItems.Add(user.CreatedDate.ToString("MM/dd/yyyy HH:mm"));
                item.SubItems.Add(user.IsActive ? "Active" : "Inactive");
                item.Tag = user;

                lvwUsers.Items.Add(item);
            }

            // Reset button states
            btnEditUser.Enabled = false;
            btnDeleteUser.Enabled = false;
        }

        private void LvwUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasSelection = lvwUsers.SelectedItems.Count > 0;
            btnEditUser.Enabled = hasSelection;
            btnDeleteUser.Enabled = hasSelection;
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            // Open a form to add a new user
            using (UserForm userForm = new UserForm(null))
            {
                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    User newUser = userForm.GetUser();
                    User.Add(newUser);
                    RefreshUserList();
                    lblStatus.Text = $"New {newUser.Role} user '{newUser.Username}' has been added.";
                }
            }
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            if (lvwUsers.SelectedItems.Count > 0)
            {
                User selectedUser = lvwUsers.SelectedItems[0].Tag as User;
                
                // Open a form to edit the selected user
                using (UserForm userForm = new UserForm(selectedUser))
                {
                    if (userForm.ShowDialog() == DialogResult.OK)
                    {
                        // Update the user with the new values
                        User updatedUser = userForm.GetUser();
                        selectedUser.FullName = updatedUser.FullName;
                        selectedUser.Password = updatedUser.Password;
                        selectedUser.Role = updatedUser.Role;
                        selectedUser.IsActive = updatedUser.IsActive;
                        
                        RefreshUserList();
                        lblStatus.Text = $"User '{selectedUser.Username}' has been updated.";
                    }
                }
            }
        }

        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lvwUsers.SelectedItems.Count > 0)
            {
                User selectedUser = lvwUsers.SelectedItems[0].Tag as User;
                
                // Confirm deletion
                if (MessageBox.Show($"Are you sure you want to delete user '{selectedUser.Username}'?", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // In a real application, we might just mark as inactive
                    // For this demo, we'll actually remove from the list
                    User.Remove(selectedUser);
                    RefreshUserList();
                    lblStatus.Text = $"User '{selectedUser.Username}' has been deleted.";
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Return to the login form
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }

    public class UserForm : Form
    {
        private User userToEdit;
        private TextBox txtUsername;
        private TextBox txtFullName;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private CheckBox chkActive;
        private Button btnSave;
        private Button btnCancel;

        public UserForm(User user)
        {
            userToEdit = user;
            InitializeComponent();
            PopulateFields();
        }

        private void InitializeComponent()
        {
            this.Text = userToEdit == null ? "Add New User" : "Edit User";
            this.Size = new Size(350, 280);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Username
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new Point(20, 20);
            lblUsername.AutoSize = true;

            txtUsername = new TextBox();
            txtUsername.Location = new Point(120, 20);
            txtUsername.Size = new Size(180, 20);
            txtUsername.Enabled = userToEdit == null; // Only enable for new users

            // Full Name
            Label lblFullName = new Label();
            lblFullName.Text = "Full Name:";
            lblFullName.Location = new Point(20, 50);
            lblFullName.AutoSize = true;

            txtFullName = new TextBox();
            txtFullName.Location = new Point(120, 50);
            txtFullName.Size = new Size(180, 20);

            // Password
            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(20, 80);
            lblPassword.AutoSize = true;

            txtPassword = new TextBox();
            txtPassword.Location = new Point(120, 80);
            txtPassword.Size = new Size(180, 20);
            txtPassword.PasswordChar = '•';

            // Role
            Label lblRole = new Label();
            lblRole.Text = "Role:";
            lblRole.Location = new Point(20, 110);
            lblRole.AutoSize = true;

            cmbRole = new ComboBox();
            cmbRole.Location = new Point(120, 110);
            cmbRole.Size = new Size(180, 20);
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Items.AddRange(new string[] { "Waiter", "Chef", "Administrator" });

            // Active status
            chkActive = new CheckBox();
            chkActive.Text = "Active";
            chkActive.Location = new Point(120, 140);
            chkActive.Size = new Size(180, 20);
            chkActive.Checked = true;

            // Save button
            btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.DialogResult = DialogResult.OK;
            btnSave.Location = new Point(70, 180);
            btnSave.Size = new Size(100, 30);
            btnSave.Click += BtnSave_Click;

            // Cancel button
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(180, 180);
            btnCancel.Size = new Size(100, 30);

            // Add controls to the form
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblFullName);
            this.Controls.Add(txtFullName);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblRole);
            this.Controls.Add(cmbRole);
            this.Controls.Add(chkActive);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void PopulateFields()
        {
            if (userToEdit != null)
            {
                txtUsername.Text = userToEdit.Username;
                txtFullName.Text = userToEdit.FullName;
                txtPassword.Text = userToEdit.Password;
                cmbRole.SelectedItem = userToEdit.Role;
                chkActive.Checked = userToEdit.IsActive;
            }
            else
            {
                cmbRole.SelectedIndex = 0; // Default to "Waiter" for new users
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || 
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            // Additional validation could be added here
        }

        public User GetUser()
        {
            if (userToEdit == null)
            {
                // Creating a new user
                return new User(
                    txtUsername.Text,
                    txtPassword.Text,
                    cmbRole.SelectedItem.ToString(),
                    txtFullName.Text
                )
                {
                    IsActive = chkActive.Checked
                };
            }
            else
            {
                // Return the edited user
                userToEdit.Password = txtPassword.Text;
                userToEdit.FullName = txtFullName.Text;
                userToEdit.Role = cmbRole.SelectedItem.ToString();
                userToEdit.IsActive = chkActive.Checked;
                return userToEdit;
            }
        }
    }
    public class UserManagementFormV2 : Form
    {
        private string adminUsername;

        // Sample user list for demo purposes
        private List<User> User = new List<User>();

        // Controls
        private ListView lvwUsers;
        private Button btnAddUser;
        private Button btnEditUser;
        private Button btnDeleteUser;
        private Button btnBack;
        private GroupBox gbUserDetails;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;

        // Update the constructor of UserManagementFormV2 to call the renamed method
        public UserManagementFormV2(string adminUsername)
        {
            this.adminUsername = adminUsername;
            InitializeSampleUsers(); // Call the existing method
            InitializeComponentV2(); // Call the renamed method
            RefreshUserList();
        }

        {
            // Add some sample users for the demo
            User.Add(new User("waiter1", "waiter123", "Waiter", "John Smith"));
            User.Add(new User("waiter2", "waiter123", "Waiter", "Emma Johnson"));
            User.Add(new User("chef1", "chef123", "Chef", "Michael Brown"));
            User.Add(new User("chef2", "chef123", "Chef", "Sarah Davis"));
            User.Add(new User("admin1", "admin123", "Administrator", "Admin User"));
        }

        {
            this.Text = "User Management V2";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

        }

        private void RefreshUserList()
        {
            lvwUsers.Items.Clear();

            foreach (User user in User)
            {
                ListViewItem item = new ListViewItem(user.Username);
                item.SubItems.Add(user.FullName);
                item.SubItems.Add(user.Role);
                item.SubItems.Add(user.CreatedDate.ToString("MM/dd/yyyy HH:mm"));
                item.SubItems.Add(user.IsActive ? "Active" : "Inactive");
                item.Tag = user;

                lvwUsers.Items.Add(item);
            }

            // Reset button states
            btnEditUser.Enabled = false;
            btnDeleteUser.Enabled = false;
        }
    }
}
