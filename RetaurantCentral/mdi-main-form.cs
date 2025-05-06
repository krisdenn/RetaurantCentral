using System;
using System.Windows.Forms;
using static RestaurantSystem.MainForm;

namespace RestaurantSystem
{
    public partial class program : Form
    {
        private string currentUsername;
        private string userRole;
        private DateTime loginTime;

        public program(string username, string role)
        {
            currentUsername = username;
            userRole = role;
            loginTime = DateTime.Now;
            
            InitializeComponent();
            SetupFormBasedOnRole();
        }

        private void InitializeComponent()
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Gourmet Dining System";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create menu strip
            menuStrip = new MenuStrip();
            
            // File menu
            fileMenu = new ToolStripMenuItem("&File");
            
            profileMenuItem = new ToolStripMenuItem("My &Profile");
            profileMenuItem.Click += (s, e) => OpenUserProfile();
            
            settingsMenuItem = new ToolStripMenuItem("&Settings");
            settingsMenuItem.Click += (s, e) => OpenSettings();
            
            logoutMenuItem = new ToolStripMenuItem("&Logout");
            logoutMenuItem.ShortcutKeys = Keys.Control | Keys.L;
            logoutMenuItem.Click += LogoutMenuItem_Click;
            
            exitMenuItem = new ToolStripMenuItem("E&xit");
            exitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            exitMenuItem.Click += (s, e) => Application.Exit();
            
            fileMenu.DropDownItems.Add(profileMenuItem);
            fileMenu.DropDownItems.Add(settingsMenuItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(logoutMenuItem);
            fileMenu.DropDownItems.Add(exitMenuItem);

            // View menu (will be populated based on role)
            viewMenu = new ToolStripMenuItem("&View");
            
            // Help menu
            helpMenu = new ToolStripMenuItem("&Help");
            
            aboutMenuItem = new ToolStripMenuItem("&About");
            aboutMenuItem.Click += (s, e) => ShowAboutDialog();
            
            helpMenu.DropDownItems.Add(aboutMenuItem);

            // Add menus to menu strip
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(viewMenu);
            menuStrip.Items.Add(helpMenu);

            // Create status strip
            statusStrip = new StatusStrip();
            
            lblStatus = new ToolStripStatusLabel();
            lblStatus.Text = $"Logged in as {userRole}: {currentUsername}";
            
            lblDateTime = new ToolStripStatusLabel();
            lblDateTime.Alignment = ToolStripItemAlignment.Right;
            UpdateTimeDisplay();
            
            statusStrip.Items.Add(lblStatus);
            statusStrip.Items.Add(lblDateTime);

            // Timer for updating the time display
            timer = new Timer();
            timer.Interval = 1000; // Update every second
            timer.Tick += (s, e) => UpdateTimeDisplay();
            timer.Start();

            // Main content panel
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;

            // Add controls to form
            this.Controls.Add(menuStrip);
            this.Controls.Add(mainPanel);
            this.Controls.Add(statusStrip);
            this.MainMenuStrip = menuStrip;
        }

        private void SetupFormBasedOnRole()
        {
            // Clear existing view menu items
            viewMenu.DropDownItems.Clear();

            // Add role-specific menu items
            switch (userRole)
            {
                case "Administrator":
                    this.Text = "Gourmet Dining System - Administrator Panel";
                    
                    var userMgmtMenuItem = new ToolStripMenuItem("&User Management");
                    userMgmtMenuItem.Click += (s, e) => OpenUserManagement();
                    
                    var reportMenuItem = new ToolStripMenuItem("&Reports");
                    reportMenuItem.Click += (s, e) => ShowNotImplementedMessage("Reports");
                    
                    viewMenu.DropDownItems.Add(userMgmtMenuItem);
                    viewMenu.DropDownItems.Add(reportMenuItem);
                    
                    // Automatically open user management for admin
                    OpenUserManagement();
                    break;

                case "Customer":
                    this.Text = "Gourmet Dining System - Customer Portal";
                    
                    var myOrdersMenuItem = new ToolStripMenuItem("My &Orders");
                    myOrdersMenuItem.Click += (s, e) => OpenCustomerOrders();
                    
                    var menuMenuItem = new ToolStripMenuItem("&Menu");
                    menuMenuItem.Click += (s, e) => ShowNotImplementedMessage("Menu");
                    
                    var reservationMenuItem = new ToolStripMenuItem("&Reservations");
                    reservationMenuItem.Click += (s, e) => ShowNotImplementedMessage("Reservations");
                    
                    viewMenu.DropDownItems.Add(myOrdersMenuItem);
                    viewMenu.DropDownItems.Add(menuMenuItem);
                    viewMenu.DropDownItems.Add(reservationMenuItem);
                    
                    // Automatically open orders for customer
                    OpenCustomerOrders();
                    break;

                case "Waiter":
                    this.Text = "Gourmet Dining System - Waiter Panel";
                    
                    var ordersMenuItem = new ToolStripMenuItem("Active &Orders");
                    ordersMenuItem.Click += (s, e) => OpenWaiterOrders();
                    
                    var tablesMenuItem = new ToolStripMenuItem("&Tables Status");
                    tablesMenuItem.Click += (s, e) => ShowNotImplementedMessage("Tables Status");
                    
                    viewMenu.DropDownItems.Add(ordersMenuItem);
                    viewMenu.DropDownItems.Add(tablesMenuItem);
                    
                    // Automatically open orders view for waiter
                    OpenWaiterOrders();
                    break;

                case "Chef":
                    this.Text = "Gourmet Dining System - Kitchen Panel";
                    
                    var kitchenOrdersItem = new ToolStripMenuItem("&Kitchen Orders");
                    kitchenOrdersItem.Click += (s, e) => ShowNotImplementedMessage("Kitchen Orders");
                    
                    var recipesMenuItem = new ToolStripMenuItem("&Recipes");
                    recipesMenuItem.Click += (s, e) => OpenRecipes();
                    
                    var inventoryMenuItem = new ToolStripMenuItem("&Inventory");
                    inventoryMenuItem.Click += (s, e) => ShowNotImplementedMessage("Inventory");
                    
                    viewMenu.DropDownItems.Add(kitchenOrdersItem);
                    viewMenu.DropDownItems.Add(recipesMenuItem);
                    viewMenu.DropDownItems.Add(inventoryMenuItem);
                    
                    // Automatically open recipes for chef
                    OpenRecipes();
                    break;
            }
        }

        private void UpdateTimeDisplay()
        {
            lblDateTime.Text = $"Login Time: {loginTime:hh:mm tt} | Current Time: {DateTime.Now:hh:mm:ss tt}";
        }

        private void OpenForm(Form form)
        {
            // For MDI parent approach
            if (this.IsMdiContainer)
            {
                form.MdiParent = this;
                form.Show();
            }
            // For panel-based approach
            else
            {
                ShowFormInPanel(form);
            }
        }

        private void OpenSingleInstanceForm(Type formType)
        {
            // Check if an instance of this form is already open (for MDI)
            if (this.IsMdiContainer)
            {
                foreach (Form openForm in this.MdiChildren)
                {
                    if (openForm.GetType() == formType)
                    {
                        // Bring to front if already open
                        openForm.Activate();
                        return;
                    }
                }
            }

            // Create appropriate form with constructor parameters if needed
            Form newForm;
            
            if (formType == typeof(UserProfileForm))
                newForm = new UserProfileForm();
            else if (formType == typeof(SettingsForm))
                newForm = new SettingsForm();
            else if (formType == typeof(UserManagementForm))
                newForm = new UserManagementForm();
            else if (formType == typeof(CustomerOrdersForm))
                newForm = new CustomerOrdersForm(currentUsername);
            else if (formType == typeof(WaiterOrdersForm))
                newForm = new WaiterOrdersForm(currentUsername);
            else if (formType == typeof(RecipesForm))
                newForm = new RecipesForm();
            else
                newForm = (Form)Activator.CreateInstance(formType);

            OpenForm(newForm);
        }

        private void ShowFormInPanel(Form form)
        {
            // Clear existing controls in the panel
            mainPanel.Controls.Clear();

            // Configure the form to be hosted in the panel
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Add and show the form
            mainPanel.Controls.Add(form);
            form.Show();
            
            // Update status message
            lblStatus.Text = $"Logged in as {userRole}: {currentUsername} | {form.Text} opened";
        }

        private void OpenUserProfile()
        {
            OpenSingleInstanceForm(typeof(UserProfileForm));
        }

        private void OpenSettings()
        {
            OpenSingleInstanceForm(typeof(SettingsForm));
        }

        private void OpenUserManagement()
        {
            OpenSingleInstanceForm(typeof(UserManagementForm));
        }

        private void OpenWaiterOrders()
        {
            OpenSingleInstanceForm(typeof(WaiterOrdersForm));
        }

        private void OpenRecipes()
        {
            OpenSingleInstanceForm(typeof(RecipesForm));
        }

        private void OpenCustomerOrders()
        {
            OpenSingleInstanceForm(typeof(CustomerOrdersForm));
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }

        private void ShowAboutDialog()
        {
            MessageBox.Show(
                "Gourmet Dining System\nVersion 1.0\n\nDeveloped for demonstration purposes.",
                "About Restaurant Management System",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ShowNotImplementedMessage(string feature)
        {
            MessageBox.Show(
                $"The {feature} feature is not implemented in this demonstration.",
                "Feature Not Available",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Menu controls
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem profileMenuItem;
        private ToolStripMenuItem settingsMenuItem;
        private ToolStripMenuItem logoutMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem viewMenu;
        private ToolStripMenuItem helpMenu;
        private ToolStripMenuItem aboutMenuItem;

        // Status strip
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblDateTime;
        private Timer timer;
        
        // Content panel for non-MDI approach
        private Panel mainPanel;
    }
}
