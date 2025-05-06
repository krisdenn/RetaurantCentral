using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public List<FoodMenuItem> Items { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public string Status { get; set; } // Pending, In Preparation, Ready, Delivered
        public string Notes { get; set; }
        public string TableNumber { get; set; } // Null for delivery/takeout orders

        public Order(int orderId, string customerName, List<FoodMenuItem> items, DateTime orderTime, 
                    DateTime? estimatedDeliveryTime, string status, string notes, string tableNumber = null)
        {
            OrderId = orderId;
            CustomerName = customerName;
            Items = items;
            OrderTime = orderTime;
            EstimatedDeliveryTime = estimatedDeliveryTime;
            Status = status;
            Notes = notes;
            TableNumber = tableNumber;
        }
    }

    public class KitchenQueueForm : Form
    {
        private string chefName;
        private List<Order> orders;
        
        // Controls
        private GroupBox gbPendingOrders;
        private ListView lvwPendingOrders;
        private GroupBox gbInProgress;
        private ListView lvwInProgress;
        private GroupBox gbOrderDetails;
        private Label lblOrderId;
        private Label lblCustomer;
        private Label lblOrderTime;
        private Label lblTable;
        private ListView lvwOrderItems;
        private TextBox txtNotes;
        private Button btnStartPreparation;
        private Button btnMarkReady;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblOrdersCount;

        public KitchenQueueForm(string chefName)
        {
            this.chefName = chefName;
            InitializeSampleOrders();
            InitializeComponent();
            PopulateOrderLists();
        }

        private void InitializeSampleOrders()
        {
            // Sample menu items for orders
            FoodMenuItem bruschetta = new FoodMenuItem(1, "Bruschetta", "Toasted bread topped with tomatoes, garlic, and basil", 7.99m, "Appetizers", 10, true);
            FoodMenuItem salmonFillet = new FoodMenuItem(4, "Grilled Salmon", "Fresh salmon fillet with herbs, served with roasted vegetables", 18.99m, "Main Courses", 25, true);
            FoodMenuItem tiramisu = new FoodMenuItem(11, "Tiramisu", "Coffee-flavored Italian dessert with mascarpone cheese", 7.99m, "Desserts", 5, true);
            FoodMenuItem margheritaPizza = new FoodMenuItem(8, "Margherita Pizza", "Classic pizza with tomato sauce, mozzarella, and basil", 13.99m, "Pizzas", 20, true);
            FoodMenuItem chickenParmesan = new FoodMenuItem(5, "Chicken Parmesan", "Breaded chicken topped with marinara and mozzarella, served with pasta", 16.99m, "Main Courses", 22, true);
            
            // Sample orders
            orders = new List<Order>();
            
            // Create a few pending orders
            orders.Add(new Order(
                1001, 
                "John Smith", 
                new List<FoodMenuItem> { bruschetta, salmonFillet, tiramisu },
                DateTime.Now.AddMinutes(-15),
                DateTime.Now.AddMinutes(25),
                "Pending",
                "No cilantro on the salmon",
                "Table 5"
            ));
            
            orders.Add(new Order(
                1002, 
                "Lisa Johnson", 
                new List<FoodMenuItem> { margheritaPizza, margheritaPizza },
                DateTime.Now.AddMinutes(-10),
                DateTime.Now.AddMinutes(20),
                "Pending",
                "Extra cheese on both pizzas",
                "Table 8"
            ));
            
            // Create in-progress orders
            orders.Add(new Order(
                1003, 
                "Michael Wong", 
                new List<FoodMenuItem> { chickenParmesan, bruschetta },
                DateTime.Now.AddMinutes(-20),
                DateTime.Now.AddMinutes(10),
                "In Preparation",
                "Extra sauce on chicken",
                "Table 3"
            ));
            
            orders.Add(new Order(
                1004, 
                "Sarah Davis", 
                new List<FoodMenuItem> { salmonFillet, tiramisu },
                DateTime.Now.AddMinutes(-25),
                DateTime.Now.AddMinutes(5),
                "In Preparation",
                "No special instructions",
                "Table 7"
            ));
        }

        private void InitializeComponent()
        {
            this.Text = "Kitchen Order Queue";
            this.Size = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Dock = DockStyle.Fill;

            // Pending Orders
            gbPendingOrders = new GroupBox();
            gbPendingOrders.Text = "Pending Orders";
            gbPendingOrders.Size = new Size(470, 300);
            gbPendingOrders.Location = new Point(10, 10);

            lvwPendingOrders = new ListView();
            lvwPendingOrders.View = View.Details;
            lvwPendingOrders.FullRowSelect = true;
            lvwPendingOrders.Columns.Add("Order #", 60);
            lvwPendingOrders.Columns.Add("Time Received", 120);
            lvwPendingOrders.Columns.Add("Table", 60);
            lvwPendingOrders.Columns.Add("Items", 180);
            lvwPendingOrders.Size = new Size(450, 270);
            lvwPendingOrders.Location = new Point(10, 20);
            lvwPendingOrders.SelectedIndexChanged += LvwOrders_SelectedIndexChanged;

            gbPendingOrders.Controls.Add(lvwPendingOrders);

            // In Progress Orders
            gbInProgress = new GroupBox();
            gbInProgress.Text = "Orders In Preparation";
            gbInProgress.Size = new Size(470, 300);
            gbInProgress.Location = new Point(490, 10);

            lvwInProgress = new ListView();
            lvwInProgress.View = View.Details;
            lvwInProgress.FullRowSelect = true;
            lvwInProgress.Columns.Add("Order #", 60);
            lvwInProgress.Columns.Add("Time Started", 120);
            lvwInProgress.Columns.Add("Table", 60);
            lvwInProgress.Columns.Add("Items", 180);
            lvwInProgress.Size = new Size(450, 270);
            lvwInProgress.Location = new Point(10, 20);
            lvwInProgress.SelectedIndexChanged += LvwOrders_SelectedIndexChanged;

            gbInProgress.Controls.Add(lvwInProgress);

            // Order Details
            gbOrderDetails = new GroupBox();
            gbOrderDetails.Text = "Order Details";
            gbOrderDetails.Size = new Size(950, 300);
            gbOrderDetails.Location = new Point(10, 320);

            // Order info
            lblOrderId = new Label();
            lblOrderId.Font = new Font("Arial", 12, FontStyle.Bold);
            lblOrderId.AutoSize = true;
            lblOrderId.Location = new Point(10, 25);

            lblCustomer = new Label();
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(10, 50);

            lblOrderTime = new Label();
            lblOrderTime.AutoSize = true;
            lblOrderTime.Location = new Point(10, 75);

            lblTable = new Label();
            lblTable.AutoSize = true;
            lblTable.Location = new Point(10, 100);

            // Order items
            Label lblItemsTitle = new Label();
            lblItemsTitle.Text = "Order Items:";
            lblItemsTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            lblItemsTitle.AutoSize = true;
            lblItemsTitle.Location = new Point(10, 130);

            lvwOrderItems = new ListView();
            lvwOrderItems.View = View.Details;
            lvwOrderItems.Columns.Add("Item", 200);
            lvwOrderItems.Columns.Add("Prep Time", 80);
            lvwOrderItems.Size = new Size(300, 150);
            lvwOrderItems.Location = new Point(10, 155);

            // Notes
            Label lblNotesTitle = new Label();
            lblNotesTitle.Text = "Special Instructions:";
            lblNotesTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            lblNotesTitle.AutoSize = true;
            lblNotesTitle.Location = new Point(320, 130);

            txtNotes = new TextBox();
            txtNotes.Multiline = true;
            txtNotes.ReadOnly = true;
            txtNotes.Size = new Size(300, 100);
            txtNotes.Location = new Point(320, 155);

            // Action buttons
            btnStartPreparation = new Button();
            btnStartPreparation.Text = "Start Preparation";
            btnStartPreparation.Size = new Size(140, 30);
            btnStartPreparation.Location = new Point(650, 155);
            btnStartPreparation.Click += BtnStartPreparation_Click;
            btnStartPreparation.Enabled = false;

            btnMarkReady = new Button();
            btnMarkReady.Text = "Mark as Ready";
            btnMarkReady.Size = new Size(140, 30);
            btnMarkReady.Location = new Point(650, 195);
            btnMarkReady.Click += BtnMarkReady_Click;
            btnMarkReady.Enabled = false;

            // Add controls to the group box
            gbOrderDetails.Controls.Add(lblOrderId);
            gbOrderDetails.Controls.Add(lblCustomer);
            gbOrderDetails.Controls.Add(lblOrderTime);
            gbOrderDetails.Controls.Add(lblTable);
            gbOrderDetails.Controls.Add(lblItemsTitle);
            gbOrderDetails.Controls.Add(lvwOrderItems);
            gbOrderDetails.Controls.Add(lblNotesTitle);
            gbOrderDetails.Controls.Add(txtNotes);
            gbOrderDetails.Controls.Add(btnStartPreparation);
            gbOrderDetails.Controls.Add(btnMarkReady);

            // Status Strip
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblStatus.Text = $"Chef: {chefName}";
            
            lblOrdersCount = new ToolStripStatusLabel();
            lblOrdersCount.Alignment = ToolStripItemAlignment.Right;
            
            statusStrip.Items.Add(lblStatus);
            statusStrip.Items.Add(lblOrdersCount);

            // Add controls to form
            this.Controls.Add(gbPendingOrders);
            this.Controls.Add(gbInProgress);
            this.Controls.Add(gbOrderDetails);
            this.Controls.Add(statusStrip);
        }

        private void PopulateOrderLists()
        {
            // Clear existing items
            lvwPendingOrders.Items.Clear();
            lvwInProgress.Items.Clear();

            int pendingCount = 0;
            int inProgressCount = 0;

            // Populate lists
            foreach (Order order in orders)
            {
                ListViewItem lvi = new ListViewItem(order.OrderId.ToString());
                
                // Add appropriate time
                if (order.Status == "Pending")
                {
                    lvi.SubItems.Add(order.OrderTime.ToString("hh:mm tt"));
                    pendingCount++;
                }
                else
                {
                    // For in-progress orders, show when preparation started
                    // For demo, just use a time between order time and now
                    DateTime startTime = order.OrderTime.AddMinutes((DateTime.Now - order.OrderTime).TotalMinutes / 2);
                    lvi.SubItems.Add(startTime.ToString("hh:mm tt"));
                    inProgressCount++;
                }

                // Add table number
                lvi.SubItems.Add(order.TableNumber ?? "N/A");
                
                // Add item summary
                string itemsSummary = string.Join(", ", order.Items.ConvertAll(i => i.Name));
                if (itemsSummary.Length > 25)
                    itemsSummary = itemsSummary.Substring(0, 22) + "...";
                lvi.SubItems.Add(itemsSummary);

                // Store order in tag
                lvi.Tag = order;

                // Add to appropriate list
                if (order.Status == "Pending")
                {
                    lvwPendingOrders.Items.Add(lvi);
                }
                else if (order.Status == "In Preparation")
                {
                    lvwInProgress.Items.Add(lvi);
                }
            }

            // Update status counts
            lblOrdersCount.Text = $"Pending: {pendingCount} | In Progress: {inProgressCount}";

            // Clear order details
            ClearOrderDetails();
        }

        private void LvwOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Determine which list view was clicked
            ListView lvw = sender as ListView;
            if (lvw != null && lvw.SelectedItems.Count > 0)
            {
                // Get the selected order
                Order order = lvw.SelectedItems[0].Tag as Order;
                if (order != null)
                {
                    DisplayOrderDetails(order);
                    
                    // Enable/disable buttons based on order status
                    if (order.Status == "Pending")
                    {
                        btnStartPreparation.Enabled = true;
                        btnMarkReady.Enabled = false;
                    }
                    else if (order.Status == "In Preparation")
                    {
                        btnStartPreparation.Enabled = false;
                        btnMarkReady.Enabled = true;
                    }
                    else
                    {
                        btnStartPreparation.Enabled = false;
                        btnMarkReady.Enabled = false;
                    }
                }
            }
            else
            {
                ClearOrderDetails();
            }
        }

        private void DisplayOrderDetails(Order order)
        {
            // Display order info
            lblOrderId.Text = $"Order #{order.OrderId}";
            lblCustomer.Text = $"Customer: {order.CustomerName}";
            lblOrderTime.Text = $"Order Time: {order.OrderTime:hh:mm tt}";
            lblTable.Text = order.TableNumber != null ? $"Table: {order.TableNumber}" : "Delivery Order";

            // Display order items
            lvwOrderItems.Items.Clear();
            foreach (FoodMenuItem item in order.Items)
            {
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add($"{item.PreparationTime} min");
                lvwOrderItems.Items.Add(lvi);
            }

            // Display notes
            txtNotes.Text = order.Notes;
        }

        private void ClearOrderDetails()
        {
            lblOrderId.Text = "";
            lblCustomer.Text = "";
            lblOrderTime.Text = "";
            lblTable.Text = "";
            lvwOrderItems.Items.Clear();
            txtNotes.Text = "";
            btnStartPreparation.Enabled = false;
            btnMarkReady.Enabled = false;
        }

        private void BtnStartPreparation_Click(object sender, EventArgs e)
        {
            if (lvwPendingOrders.SelectedItems.Count > 0)
            {
                Order order = lvwPendingOrders.SelectedItems[0].Tag as Order;
                if (order != null)
                {
                    // Update order status
                    order.Status = "In Preparation";
                    
                    // Show confirmation
                    MessageBox.Show($"Order #{order.OrderId} has been moved to preparation.", 
                        "Order Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh lists
                    PopulateOrderLists();
                }
            }
        }

        private void BtnMarkReady_Click(object sender, EventArgs e)
        {
            if (lvwInProgress.SelectedItems.Count > 0)
            {
                Order order = lvwInProgress.SelectedItems[0].Tag as Order;
                if (order != null)
                {
                    // Update order status
                    order.Status = "Ready";
                    
                    // Show confirmation
                    MessageBox.Show($"Order #{order.OrderId} has been marked as ready. The waiter will be notified.", 
                        "Order Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Refresh lists
                    PopulateOrderLists();
                }
            }
        }
    }
}
