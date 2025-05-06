using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RestaurantSystem
{
    public partial class LoginForm : Form
    {
        // Hardcoded credentials for demo purposes
        private readonly string[] validUsernames = { "customer1", "waiter1", "chef1", "admin1" };
        private readonly string[] validPasswords = { "customer123", "waiter123", "chef123", "admin123" };
        private readonly string[] userRoles = { "Customer", "Waiter", "Chef", "Administrator" };

        public LoginForm()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            UpdateRegisterButtonVisibility();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.MaskedTextBox();
            this.gbUserType = new System.Windows.Forms.GroupBox();
            this.rbCustomer = new System.Windows.Forms.RadioButton();
            this.rbWaiter = new System.Windows.Forms.RadioButton();
            this.rbChef = new System.Windows.Forms.RadioButton();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbUserType.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(70, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Gourmet Dining System";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(50, 80);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(100, 20);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "&Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(150, 80);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(220, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(50, 120);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 20);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "&Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(150, 120);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(220, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // gbUserType
            // 
            this.gbUserType.Controls.Add(this.rbCustomer);
            this.gbUserType.Controls.Add(this.rbWaiter);
            this.gbUserType.Controls.Add(this.rbChef);
            this.gbUserType.Controls.Add(this.rbAdmin);
            this.gbUserType.Location = new System.Drawing.Point(0, 160);
            this.gbUserType.Name = "gbUserType";
            this.gbUserType.Size = new System.Drawing.Size(452, 70);
            this.gbUserType.TabIndex = 5;
            this.gbUserType.TabStop = false;
            this.gbUserType.Text = "Select User Type";
            // 
            // rbCustomer
            // 
            this.rbCustomer.Checked = true;
            this.rbCustomer.Location = new System.Drawing.Point(20, 30);
            this.rbCustomer.Name = "rbCustomer";
            this.rbCustomer.Size = new System.Drawing.Size(104, 24);
            this.rbCustomer.TabIndex = 0;
            this.rbCustomer.TabStop = true;
            this.rbCustomer.Text = "C&ustomer";
            this.rbCustomer.CheckedChanged += new System.EventHandler(this.RbUserType_CheckedChanged);
            // 
            // rbWaiter
            // 
            this.rbWaiter.Location = new System.Drawing.Point(130, 30);
            this.rbWaiter.Name = "rbWaiter";
            this.rbWaiter.Size = new System.Drawing.Size(79, 24);
            this.rbWaiter.TabIndex = 1;
            this.rbWaiter.Text = "&Waiter";
            this.rbWaiter.CheckedChanged += new System.EventHandler(this.RbUserType_CheckedChanged);
            // 
            // rbChef
            // 
            this.rbChef.Location = new System.Drawing.Point(230, 30);
            this.rbChef.Name = "rbChef";
            this.rbChef.Size = new System.Drawing.Size(72, 24);
            this.rbChef.TabIndex = 2;
            this.rbChef.Text = "C&hef";
            this.rbChef.CheckedChanged += new System.EventHandler(this.RbUserType_CheckedChanged);
            // 
            // rbAdmin
            // 
            this.rbAdmin.Location = new System.Drawing.Point(308, 30);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Size = new System.Drawing.Size(146, 24);
            this.rbAdmin.TabIndex = 3;
            this.rbAdmin.Text = "&Administrator";
            this.rbAdmin.CheckedChanged += new System.EventHandler(this.RbUserType_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(78, 236);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 30);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(270, 236);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(100, 30);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.Text = "Register";
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 385);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(470, 22);
            this.statusStrip.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(83, 17);
            this.lblStatus.Text = "Ready to login";
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(470, 407);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.gbUserType);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restaurant System Login";
            this.gbUserType.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "Restaurant System";
            notifyIcon.Visible = true;

            // Using a system icon for the demo
            notifyIcon.Icon = System.Drawing.SystemIcons.Application;

            // Create context menu for notify icon
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem openItem = new ToolStripMenuItem("Open");
            openItem.Click += (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; };

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => { Application.Exit(); };

            contextMenu.Items.Add(openItem);
            contextMenu.Items.Add(exitItem);

            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.DoubleClick += (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; };
        }

        private void RbUserType_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRegisterButtonVisibility();
        }

        private void UpdateRegisterButtonVisibility()
        {
            // Show Register button only for Customer role
            btnRegister.Visible = rbCustomer.Checked;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string role = "";

            if (rbCustomer.Checked)
                role = "Customer";
            else if (rbWaiter.Checked)
                role = "Waiter";
            else if (rbChef.Checked)
                role = "Chef";
            else if (rbAdmin.Checked)
                role = "Administrator";

            lblStatus.Text = "Verifying credentials...";
            Application.DoEvents();

            // Simulate login delay
            System.Threading.Thread.Sleep(500);

            // Check credentials
            bool isValid = false;
            int userIndex = -1;

            for (int i = 0; i < validUsernames.Length; i++)
            {
                if (username == validUsernames[i] && password == validPasswords[i] && userRoles[i] == role)
                {
                    userIndex = i;
                    isValid = true;
                    break;
                }
            }

            if (isValid)
            {
                // Success
                ShowLoginSuccessMessage(role);

                // Route to appropriate form based on role
                switch (role)
                {
                    case "Administrator":
                        // Update the constructor call to explicitly specify the namespace and resolve ambiguity
                        RestaurantSystem.UserManagementForm adminForm = new RestaurantSystem.UserManagementForm();
                        this.Hide();
                        adminForm.FormClosed += (s, args) => this.Close();
                        adminForm.Show();
                        break;
                    case "Waiter":
                        WaiterOrdersForm waiterForm = new WaiterOrdersForm(username);
                        this.Hide();
                        waiterForm.FormClosed += (s, args) => this.Close();
                        waiterForm.Show();
                        break;
                    case "Chef":
                        RecipesForm chefForm = new RecipesForm();
                        this.Hide();
                        chefForm.FormClosed += (s, args) => this.Close();
                        chefForm.Show();
                        break;
                    case "Customer":
                        CustomerOrdersForm customerForm = new CustomerOrdersForm(username);
                        this.Hide();
                        customerForm.FormClosed += (s, args) => this.Close();
                        customerForm.Show();
                        break;
                    default:
                        MessageBox.Show("Role not recognized. Contact system administrator.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                // Failure
                ShowLoginFailureMessage();
                lblStatus.Text = "Login failed. Try again.";
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // This button should only be visible for Customer role
            if (!rbCustomer.Checked)
            {
                MessageBox.Show("Only customers can self-register. Staff accounts must be created by an administrator.",
                    "Registration Restricted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // For now, just show a message - in a real system, this would open a registration form
            MessageBox.Show("Customer registration functionality will be implemented with database integration.",
                "Customer Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblStatus.Text = "Customer registration option selected";
        }

        private void ShowLoginSuccessMessage(string role)
        {
            notifyIcon.BalloonTipTitle = "Login Successful";
            notifyIcon.BalloonTipText = $"Welcome! You are logged in as {role}";
            notifyIcon.ShowBalloonTip(3000);

            MessageBox.Show($"Login successful! You are logged in as a {role}.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowLoginFailureMessage()
        {
            MessageBox.Show("Invalid username or password. Please try again or check your user type selection.",
                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Form control variables
        private Label lblTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private MaskedTextBox txtPassword;
        private GroupBox gbUserType;
        private RadioButton rbCustomer;
        private RadioButton rbWaiter;
        private RadioButton rbChef;
        private RadioButton rbAdmin;
        private Button btnLogin;
        private Button btnRegister;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private NotifyIcon notifyIcon;
    }
    public class RecipesForm : Form
    {
        public RecipesForm()
        {
            this.Text = "Recipes";
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RecipesForm
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "RecipesForm";
            this.Text = "Recipes";
            this.ResumeLayout(false);
        }
    }
    public class WaiterOrdersForm : Form
    {
        private string username;

        public WaiterOrdersForm(string username)
        {
            this.username = username;
            this.Text = "Waiter Orders";
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WaiterOrdersForm
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "WaiterOrdersForm";
            this.Text = "Waiter Orders";
            this.ResumeLayout(false);
        }
    }
    public class UserManagementForm : Form
    {
        private string username;

        public UserManagementForm(string username)
        {
            this.username = username;
            this.Text = "User Management";
            InitializeComponent(); // This is the correct method for initializing components
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UserManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "UserManagementForm";
            this.Text = "User Management";
            this.ResumeLayout(false);
        }

        private void InitializeSampleUsers()
        {
            // This method should not conflict with InitializeComponent
            // Add logic for initializing sample users here
        }
    }
}
