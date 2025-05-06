using System;
using System.Windows.Forms;

namespace RestaurantSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }

    public class MainForm : Form
    {
        private string username;
        private string userRole;
        private MenuStrip mainMenu;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem viewMenu;
        private ToolStripMenuItem helpMenu;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private Panel mainPanel;

        public MainForm(string username, string role)
        {
            this.username = username;
            this.userRole = role;
            InitializeComponent();
            ConfigureBasedOnRole();
        }

        private void InitializeComponent()
        {
            this.Text = "Restaurant Management System";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Initialize main menu
            mainMenu = new MenuStrip();
            fileMenu = new ToolStripMenuItem("&File");
            viewMenu = new ToolStripMenuItem("&View");
            helpMenu = new ToolStripMenuItem("&Help");

            // File menu items
            var profileMenuItem = new ToolStripMenuItem("My &Profile");
            profileMenuItem.Click += (s, e) => ShowFormInPanel(new UserProfileForm());

            var settingsMenuItem = new ToolStripMenuItem("&Settings");
            settingsMenuItem.Click += (s, e) => OpenSettings();

            var logoutMenuItem = new ToolStripMenuItem("&Logout");
            logoutMenuItem.Click += (s, e) => Logout();

            var exitMenuItem = new ToolStripMenuItem("E&xit");
            exitMenuItem.Click += (s, e) => Application.Exit();

            fileMenu.DropDownItems.Add(profileMenuItem);
            fileMenu.DropDownItems.Add(settingsMenuItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(logoutMenuItem);
            fileMenu.DropDownItems.Add(exitMenuItem);

            // Help menu items
            var aboutMenuItem = new ToolStripMenuItem("&About");
            aboutMenuItem.Click += (s, e) => ShowAboutDialog();

            helpMenu.DropDownItems.Add(aboutMenuItem);

            // Add menus to the menu strip
            mainMenu.Items.Add(fileMenu);
            mainMenu.Items.Add(viewMenu);
            mainMenu.Items.Add(helpMenu);

            // Status strip
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblStatus.Text = $"Logged in as {userRole}: {username}";
            statusStrip.Items.Add(lblStatus);

            // Main content panel
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;

            // Add controls to form
            this.Controls.Add(mainMenu);
            this.Controls.Add(mainPanel);
            this.Controls.Add(statusStrip);
            this.MainMenuStrip = mainMenu;
        }

        private void ConfigureBasedOnRole()
        {
            viewMenu.DropDownItems.Clear();

            switch (userRole)
            {
                case "Administrator":
                    this.Text += " - Administrator Panel";
                    var userMgmtMenuItem = new ToolStripMenuItem("&User Management");
                    userMgmtMenuItem.Click += (s, e) => OpenUserManagement();
                    var reportMenuItem = new ToolStripMenuItem("&Reports");
                    reportMenuItem.Click += (s, e) => ShowNotImplementedMessage("Reports");
                    viewMenu.DropDownItems.Add(userMgmtMenuItem);
                    viewMenu.DropDownItems.Add(reportMenuItem);
                    OpenUserManagement();
                    break;

                case "Waiter":
                    this.Text += " - Waiter Panel";
                    var ordersMenuItem = new ToolStripMenuItem("Active &Orders");
                    ordersMenuItem.Click += (s, e) => OpenWaiterOrders();
                    var tablesMenuItem = new ToolStripMenuItem("&Tables Status");
                    tablesMenuItem.Click += (s, e) => ShowNotImplementedMessage("Tables Status");
                    viewMenu.DropDownItems.Add(ordersMenuItem);
                    viewMenu.DropDownItems.Add(tablesMenuItem);
                    OpenWaiterOrders();
                    break;

                case "Chef":
                    this.Text += " - Kitchen Panel";
                    var kitchenOrdersItem = new ToolStripMenuItem("&Kitchen Orders");
                    kitchenOrdersItem.Click += (s, e) => ShowNotImplementedMessage("Kitchen Orders");
                    var recipesMenuItem = new ToolStripMenuItem("&Recipes");
                    recipesMenuItem.Click += (s, e) => OpenRecipes();
                    var inventoryMenuItem = new ToolStripMenuItem("&Inventory");
                    inventoryMenuItem.Click += (s, e) => ShowNotImplementedMessage("Inventory");
                    viewMenu.DropDownItems.Add(kitchenOrdersItem);
                    viewMenu.DropDownItems.Add(recipesMenuItem);
                    viewMenu.DropDownItems.Add(inventoryMenuItem);
                    OpenRecipes();
                    break;

                case "Customer":
                    this.Text += " - Customer Portal";
                    var myOrdersMenuItem = new ToolStripMenuItem("My &Orders");
                    myOrdersMenuItem.Click += (s, e) => OpenCustomerOrders();
                    var menuMenuItem = new ToolStripMenuItem("&Menu");
                    menuMenuItem.Click += (s, e) => ShowNotImplementedMessage("Menu");
                    var reservationMenuItem = new ToolStripMenuItem("&Reservations");
                    reservationMenuItem.Click += (s, e) => ShowNotImplementedMessage("Reservations");
                    viewMenu.DropDownItems.Add(myOrdersMenuItem);
                    viewMenu.DropDownItems.Add(menuMenuItem);
                    viewMenu.DropDownItems.Add(reservationMenuItem);
                    OpenCustomerOrders();
                    break;
            }
        }

        private void OpenSettings()
        {
            ShowFormInPanel(new SettingsForm());
            lblStatus.Text = "Settings opened";
        }

        private void OpenUserManagement()
        {
            UserManagementForm userMgmtForm = new UserManagementForm(username);
            userMgmtForm.ShowDialog();
        }

        private void OpenWaiterOrders()
        {
            ShowFormInPanel(new WaiterOrdersForm(username));
            lblStatus.Text = "Waiter orders opened";
        }

        private void OpenRecipes()
        {
            ShowFormInPanel(new RecipesForm());
            lblStatus.Text = "Recipes opened";
        }

        private void OpenCustomerOrders()
        {
            ShowFormInPanel(new CustomerOrdersForm(username));
            lblStatus.Text = "Customer orders opened";
        }

        private void ShowFormInPanel(Form form)
        {
            mainPanel.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(form);
            form.Show();
        }

        private void Logout()
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
                "Restaurant Management System\nVersion 1.0\n\nDeveloped for demonstration purposes.",
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
    }

    // Placeholder implementations for missing forms
    public class SettingsForm : Form
    {
        public SettingsForm()
        {
            this.Text = "Settings";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }

    public class WaiterOrdersForm : Form
    {
        public WaiterOrdersForm(string username)
        {
            this.Text = "Waiter Orders";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }

    public class RecipesForm : Form
    {
        public RecipesForm()
        {
            this.Text = "Recipes";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }

    public class CustomerOrdersForm : Form
    {
        public CustomerOrdersForm(string username)
        {
            this.Text = "Customer Orders";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
