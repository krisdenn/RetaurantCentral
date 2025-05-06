using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public class Table
    {
        public string TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsOccupied { get; set; }
        public string CurrentCustomer { get; set; }
        public DateTime? SeatedTime { get; set; }
        public List<Order> Orders { get; set; }

        public Table(string tableNumber, int capacity, bool isOccupied = false, 
                    string currentCustomer = null, DateTime? seatedTime = null)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            IsOccupied = isOccupied;
            CurrentCustomer = currentCustomer;
            SeatedTime = seatedTime;
            Orders = new List<Order>();
        }
    }

    public class TablesForm : Form
    {
        private string waiterName;
        private List<Table> tables;
        
        // Controls
        private FlowLayoutPanel flpTables;
        private GroupBox gbSelectedTable;
        private Label lblTableNumber;
        private Label lblCapacity;
        private Label lblStatus;
        private Label lblCustomer;
        private Label lblSeatedTime;
        private Button btnOccupy;
        private Button btnClear;
        private Button btnTakeOrder;
        private Button btnViewOrders;
        private ListView lvwTableOrders;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus1;
        private ToolStripStatusLabel lblTableCount;

        public TablesForm(string waiterName)
        {
            this.waiterName = waiterName;
            InitializeSampleTables();
            InitializeComponent();
            PopulateTableView();
        }

        private void InitializeSampleTables()
        {
            // Sample menu items for orders
            FoodMenuItem bruschetta = new FoodMenuItem(1, "Bruschetta", "Toasted bread topped with tomatoes, garlic, and basil", 7.99m, "Appetizers", 10, true);
            FoodMenuItem salmonFillet = new FoodMenuItem(4, "Grilled Salmon", "Fresh salmon fillet with herbs, served with roasted vegetables", 18.99m, "Main Courses", 25, true);
            FoodMenuItem tiramisu = new FoodMenuItem(11, "Tiramisu", "Coffee-flavored Italian dessert with mascarpone cheese", 7.99m, "Desserts", 5, true);
            FoodMenuItem margheritaPizza = new FoodMenuItem(8, "Margherita Pizza", "Classic pizza with tomato sauce, mozzarella, and basil", 13.99m, "Pizzas", 20, true);

            // Create sample tables
            tables = new List<Table>
            {
                new Table("1", 2),
                new Table("2", 2, true, "James Wilson", DateTime.Now.AddMinutes(-45)),
                new Table("3", 4, true, "Taylor Family", DateTime.Now.AddMinutes(-30)),
                new Table("4", 4),
                new Table("5", 6, true, "Johnson Party", DateTime.Now.AddMinutes(-15)),
                new Table("6", 2),
                new Table("7", 4, true, "Sarah & Mike", DateTime.Now.AddMinutes(-60)),
                new Table("8", 6, true, "Birthday Group", DateTime.Now.AddMinutes(-90)),
                new Table("9", 2),
                new Table("10", 8)
            };

            // Add sample orders to tables
            Order order1 = new Order(
                2001, 
                "James Wilson", 
                new List<FoodMenuItem> { bruschetta, salmonFillet },
                DateTime.Now.AddMinutes(-35),
                DateTime.Now.AddMinutes(15),
                "In Preparation",
                "No cilantro on the salmon",
                "2"
            );
            tables[1].Orders.Add(order1);

            Order order2 = new Order(
                2002, 
                "Taylor Family", 
                new List<FoodMenuItem> { margheritaPizza, margheritaPizza, tiramisu },
                DateTime.Now.AddMinutes(-20),
                DateTime.Now.AddMinutes(10),
                "Pending",
                "Extra cheese on pizzas",
                "3"
            );
            tables[2].Orders.Add(order2);

            Order order3 = new Order(
                2003, 
                "Johnson Party", 
                new List<FoodMenuItem> { bruschetta, bruschetta, salmonFillet, tiramisu, tiramisu },
                DateTime.Now.AddMinutes(-10),
                DateTime.Now.AddMinutes(25),
                "Pending",
                "No special instructions",
                "5"
            );
            tables[4].Orders.Add(order3);

            Order order4 = new Order(
                2004, 
                "Sarah & Mike", 
                new List<FoodMenuItem> { margheritaPizza, tiramisu },
                DateTime.Now.AddMinutes(-50),
                DateTime.Now.AddMinutes(-10),
                "Ready",
                "No special instructions",
                "7"
            );
            tables[6].Orders.Add(order4);
        }
		private void InitializeComponent()
        {
            this.Text = "Table Management";
            this.Size = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Dock = DockStyle.Fill;

            // Tables layout panel
            flpTables = new FlowLayoutPanel();
            flpTables.Size = new Size(670, 600);
            flpTables.Location = new Point(10, 10);
            flpTables.AutoScroll = true;
            flpTables.BorderStyle = BorderStyle.FixedSingle;

            // Selected table details
            gbSelectedTable = new GroupBox();
            gbSelectedTable.Text = "Selected Table";
            gbSelectedTable.Size = new Size(300, 400);
            gbSelectedTable.Location = new Point(690, 10);

            lblTableNumber = new Label();
            lblTableNumber.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTableNumber.AutoSize = true;
            lblTableNumber.Location = new Point(10, 30);

            lblCapacity = new Label();
            lblCapacity.AutoSize = true;
            lblCapacity.Location = new Point(10, 60);

            lblStatus = new Label();
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(10, 90);

            lblCustomer = new Label();
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(10, 120);

            lblSeatedTime = new Label();
            lblSeatedTime.AutoSize = true;
            lblSeatedTime.Location = new Point(10, 150);

            // Orders list for the table
            Label lblOrdersTitle = new Label();
            lblOrdersTitle.Text = "Current Orders:";
            lblOrdersTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            lblOrdersTitle.AutoSize = true;
            lblOrdersTitle.Location = new Point(10, 180);

            lvwTableOrders = new ListView();
            lvwTableOrders.View = View.Details;
            lvwTableOrders.FullRowSelect = true;
            lvwTableOrders.Columns.Add("Order #", 60);
            lvwTableOrders.Columns.Add("Time", 70);
            lvwTableOrders.Columns.Add("Status", 70);
            lvwTableOrders.Size = new Size(280, 100);
            lvwTableOrders.Location = new Point(10, 200);

            // Action buttons
            btnOccupy = new Button();
            btnOccupy.Text = "Seat Customers";
            btnOccupy.Size = new Size(130, 30);
            btnOccupy.Location = new Point(10, 310);
            btnOccupy.Click += BtnOccupy_Click;
            btnOccupy.Enabled = false;

            btnClear = new Button();
            btnClear.Text = "Clear Table";
            btnClear.Size = new Size(130, 30);
            btnClear.Location = new Point(150, 310);
            btnClear.Click += BtnClear_Click;
            btnClear.Enabled = false;

            btnTakeOrder = new Button();
            btnTakeOrder.Text = "Take Order";
            btnTakeOrder.Size = new Size(130, 30);
            btnTakeOrder.Location = new Point(10, 350);
            btnTakeOrder.Click += BtnTakeOrder_Click;
            btnTakeOrder.Enabled = false;

            btnViewOrders = new Button();
            btnViewOrders.Text = "View Order Details";
            btnViewOrders.Size = new Size(130, 30);
            btnViewOrders.Location = new Point(150, 350);
            btnViewOrders.Click += BtnViewOrders_Click;
            btnViewOrders.Enabled = false;

            // Add controls to group box
            gbSelectedTable.Controls.Add(lblTableNumber);
            gbSelectedTable.Controls.Add(lblCapacity);
            gbSelectedTable.Controls.Add(lblStatus);
            gbSelectedTable.Controls.Add(lblCustomer);
            gbSelectedTable.Controls.Add(lblSeatedTime);
            gbSelectedTable.Controls.Add(lblOrdersTitle);
            gbSelectedTable.Controls.Add(lvwTableOrders);
            gbSelectedTable.Controls.Add(btnOccupy);
            gbSelectedTable.Controls.Add(btnClear);
            gbSelectedTable.Controls.Add(btnTakeOrder);
            gbSelectedTable.Controls.Add(btnViewOrders);

            // Status strip
            statusStrip = new StatusStrip();
            lblStatus1 = new ToolStripStatusLabel();
            lblStatus1.Text = $"Waiter: {waiterName}";
            
            lblTableCount = new ToolStripStatusLabel();
            lblTableCount.Alignment = ToolStripItemAlignment.Right;
            
            statusStrip.Items.Add(lblStatus1);
            statusStrip.Items.Add(lblTableCount);

            // Add controls to form
            this.Controls.Add(flpTables);
            this.Controls.Add(gbSelectedTable);
            this.Controls.Add(statusStrip);
        }

        private void PopulateTableView()
        {
            // Clear existing table buttons
            flpTables.Controls.Clear();

            int occupiedCount = 0;
            int availableCount = 0;

            // Create buttons for each table
            foreach (Table table in tables)
            {
                Button btnTable = new Button();
                btnTable.Size = new Size(120, 100);
                btnTable.Text = $"Table {table.TableNumber}\n{table.Capacity} seats";
                btnTable.Tag = table;
                btnTable.Click += BtnTable_Click;

                // Set button color based on table status
                if (table.IsOccupied)
                {
                    btnTable.BackColor = Color.LightCoral;
                    occupiedCount++;
                    
                    // Check if there are ready orders for this table
                    bool hasReadyOrders = table.Orders.Exists(o => o.Status == "Ready");
                    if (hasReadyOrders)
                    {
                        btnTable.BackColor = Color.Gold;
                        btnTable.Text += "\n[READY]";
                    }
                }
                else
                {
                    btnTable.BackColor = Color.LightGreen;
                    availableCount++;
                }

                flpTables.Controls.Add(btnTable);
            }

            // Update status strip
            lblTableCount.Text = $"Occupied: {occupiedCount} | Available: {availableCount}";

            // Clear selected table details
            ClearTableDetails();
        }

        private void BtnTable_Click(object sender, EventArgs e)
        {
            Button btnTable = sender as Button;
            if (btnTable != null)
            {
                Table selectedTable = btnTable.Tag as Table;
                if (selectedTable != null)
                {
                    DisplayTableDetails(selectedTable);
                }
            }
        }

        private void DisplayTableDetails(Table table)
        {
            // Display table info
            lblTableNumber.Text = $"Table {table.TableNumber}";
            lblCapacity.Text = $"Capacity: {table.Capacity} persons";
            lblStatus.Text = table.IsOccupied ? "Status: Occupied" : "Status: Available";
            
            if (table.IsOccupied)
            {
                lblCustomer.Text = $"Customer: {table.CurrentCustomer}";
                lblSeatedTime.Text = $"Seated at: {table.SeatedTime:hh:mm tt}";
                
                // Display orders
                lvwTableOrders.Items.Clear();
                foreach (Order order in table.Orders)
                {
                    ListViewItem lvi = new ListViewItem(order.OrderId.ToString());
                    lvi.SubItems.Add(order.OrderTime.ToString("hh:mm"));
                    lvi.SubItems.Add(order.Status);
                    lvi.Tag = order;
                    lvwTableOrders.Items.Add(lvi);
                }
                
                // Enable/disable buttons
                btnOccupy.Enabled = false;
                btnClear.Enabled = true;
                btnTakeOrder.Enabled = true;
                btnViewOrders.Enabled = table.Orders.Count > 0;
            }
            else
            {
                lblCustomer.Text = "Customer: N/A";
                lblSeatedTime.Text = "Seated at: N/A";
                lvwTableOrders.Items.Clear();
                
                // Enable/disable buttons
                btnOccupy.Enabled = true;
                btnClear.Enabled = false;
                btnTakeOrder.Enabled = false;
                btnViewOrders.Enabled = false;
            }
            
            // Store selected table for button click handlers
            gbSelectedTable.Tag = table;
        }

        private void ClearTableDetails()
        {
            lblTableNumber.Text = "No Table Selected";
            lblCapacity.Text = "";
            lblStatus.Text = "";
            lblCustomer.Text = "";
            lblSeatedTime.Text = "";
            lvwTableOrders.Items.Clear();
            
            btnOccupy.Enabled = false;
            btnClear.Enabled = false;
            btnTakeOrder.Enabled = false;
            btnViewOrders.Enabled = false;
            
            gbSelectedTable.Tag = null;
        }

        private void BtnOccupy_Click(object sender, EventArgs e)
        {
            Table selectedTable = gbSelectedTable.Tag as Table;
            if (selectedTable != null && !selectedTable.IsOccupied)
            {
                // Show dialog to enter customer information
                using (Form inputForm = new Form())
                {
                    inputForm.Text = $"Seat Customer at Table {selectedTable.TableNumber}";
                    inputForm.Size = new Size(300, 150);
                    inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    inputForm.StartPosition = FormStartPosition.CenterParent;
                    inputForm.MaximizeBox = false;
                    inputForm.MinimizeBox = false;

                    Label lblPrompt = new Label();
                    lblPrompt.Text = "Customer name or group:";
                    lblPrompt.AutoSize = true;
                    lblPrompt.Location = new Point(10, 20);
                    inputForm.Controls.Add(lblPrompt);

                    TextBox txtCustomer = new TextBox();
                    txtCustomer.Location = new Point(10, 45);
                    txtCustomer.Size = new Size(270, 20);
                    inputForm.Controls.Add(txtCustomer);

                    Button btnOK = new Button();
                    btnOK.Text = "OK";
                    btnOK.DialogResult = DialogResult.OK;
                    btnOK.Location = new Point(120, 80);
                    inputForm.Controls.Add(btnOK);

                    inputForm.AcceptButton = btnOK;

                    if (inputForm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txtCustomer.Text))
                    {
                        // Update table status
                        selectedTable.IsOccupied = true;
                        selectedTable.CurrentCustomer = txtCustomer.Text;
                        selectedTable.SeatedTime = DateTime.Now;
                        
                        // Show confirmation
                        MessageBox.Show($"Table {selectedTable.TableNumber} has been occupied by {selectedTable.CurrentCustomer}.", 
                            "Table Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Refresh table view
                        PopulateTableView();
                        DisplayTableDetails(selectedTable);
                    }
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Table selectedTable = gbSelectedTable.Tag as Table;
            if (selectedTable != null && selectedTable.IsOccupied)
            {
                // Check if there are any pending or in-preparation orders
                bool hasActiveOrders = selectedTable.Orders.Exists(o => o.Status == "Pending" || o.Status == "In Preparation");
                
                if (hasActiveOrders)
                {
                    MessageBox.Show("This table has active orders in progress. Please wait until all orders are completed.", 
                        "Cannot Clear Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm table clearing
                if (MessageBox.Show($"Are you sure you want to clear Table {selectedTable.TableNumber}?", 
                    "Confirm Clear Table", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Update table status
                    selectedTable.IsOccupied = false;
                    selectedTable.CurrentCustomer = null;
                    selectedTable.SeatedTime = null;
                    selectedTable.Orders.Clear();
                    
                    // Show confirmation
                    MessageBox.Show($"Table {selectedTable.TableNumber} has been cleared and is now available.", 
                        "Table Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh table view
                    PopulateTableView();
                }
            }
        }

        private void BtnTakeOrder_Click(object sender, EventArgs e)
        {
            Table selectedTable = gbSelectedTable.Tag as Table;
            if (selectedTable != null && selectedTable.IsOccupied)
            {
                MessageBox.Show($"Opening menu to take order for Table {selectedTable.TableNumber}. This would open the food menu in a real application.", 
                    "Take Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // In a real application, this would open the menu selection form
                // For this demo, we'll just create a sample order
                
                // Sample menu items
                FoodMenuItem salad = new FoodMenuItem(3, "Caesar Salad", "Fresh romaine lettuce with Caesar dressing", 8.99m, "Appetizers", 5, true);
                FoodMenuItem steak = new FoodMenuItem(6, "Beef Tenderloin", "8oz beef tenderloin with mushroom sauce", 24.99m, "Main Courses", 30, true);
                
                // Create a new order with a unique ID
                int newOrderId = 2000 + (new Random()).Next(100, 999);
                Order newOrder = new Order(
                    newOrderId,
                    selectedTable.CurrentCustomer,
                    new List<FoodMenuItem> { salad, steak },
                    DateTime.Now,
                    DateTime.Now.AddMinutes(35),
                    "Pending",
                    "Medium-rare steak",
                    selectedTable.TableNumber
                );
                
                // Add order to table
                selectedTable.Orders.Add(newOrder);
                
                // Show confirmation
                MessageBox.Show($"Order #{newOrderId} has been created for Table {selectedTable.TableNumber}.", 
                    "Order Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Refresh table details
                DisplayTableDetails(selectedTable);
            }
        }

        private void BtnViewOrders_Click(object sender, EventArgs e)
        {
            if (lvwTableOrders.SelectedItems.Count > 0)
            {
                Order selectedOrder = lvwTableOrders.SelectedItems[0].Tag as Order;
                if (selectedOrder != null)
                {
                    // Display order details
                    string orderDetails = $"Order #{selectedOrder.OrderId}\n";
                    orderDetails += $"Customer: {selectedOrder.CustomerName}\n";
                    orderDetails += $"Time: {selectedOrder.OrderTime:hh:mm tt}\n";
                    orderDetails += $"Status: {selectedOrder.Status}\n\n";
                    orderDetails += "Items:\n";
                    
                    foreach (FoodMenuItem item in selectedOrder.Items)
                    {
                        orderDetails += $"- {item.Name} (${item.Price:F2})\n";
                    }
                    
                    if (!string.IsNullOrEmpty(selectedOrder.Notes))
                    {
                        orderDetails += $"\nNotes: {selectedOrder.Notes}";
                    }
                    
                    MessageBox.Show(orderDetails, "Order Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // If order is ready, ask if it should be delivered
                    if (selectedOrder.Status == "Ready")
                    {
                        if (MessageBox.Show("This order is ready for delivery. Mark as delivered?", 
                            "Deliver Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            selectedOrder.Status = "Delivered";
                            MessageBox.Show("Order has been marked as delivered.", 
                                "Order Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DisplayTableDetails(gbSelectedTable.Tag as Table);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order to view its details.", 
                    "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}