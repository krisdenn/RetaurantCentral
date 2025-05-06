using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public class FoodMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int PreparationTime { get; set; } // in minutes
        public bool IsAvailable { get; set; }

        public FoodMenuItem(int id, string name, string description, decimal price, string category, int prepTime, bool isAvailable)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            PreparationTime = prepTime;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"{Name} - ${Price:F2}";
        }
    }

    public class FoodMenuForm : Form
    {
        private string username;
        private List<FoodMenuItem> menuItems;
        private List<FoodMenuItem> cartItems;

        // Controls
        private TabControl tabCategories;
        private ListView lvwMenu;
        private GroupBox gbDetails;
        private Label lblItemName;
        private Label lblDescription;
        private Label lblPrice;
        private Label lblPrepTime;
        private NumericUpDown nudQuantity;
        private Button btnAddToCart;
        private GroupBox gbCart;
        private ListView lvwCart;
        private Label lblTotal;
        private Button btnPlaceOrder;
        private Button btnClearCart;
        private MonthCalendar calDeliveryDate;
        private DateTimePicker dtpDeliveryTime;
        private TextBox txtSpecialInstructions;

        public FoodMenuForm(string username)
        {
            this.username = username;
            this.cartItems = new List<FoodMenuItem>();
            InitializeMenu();
            InitializeComponent();
            PopulateMenuCategories();
        }

        private void InitializeMenu()
        {
            // Sample menu data - in a real application, this would come from a database
            menuItems = new List<FoodMenuItem>
            {
                // Appetizers
                new FoodMenuItem(1, "Bruschetta", "Toasted bread topped with tomatoes, garlic, and basil", 7.99m, "Appetizers", 10, true),
                new FoodMenuItem(2, "Mozzarella Sticks", "Breaded and deep-fried mozzarella with marinara sauce", 8.99m, "Appetizers", 12, true),
                new FoodMenuItem(3, "Spinach Artichoke Dip", "Creamy spinach dip with artichoke hearts and chips", 9.99m, "Appetizers", 15, true),
                
                // Main Courses
                new FoodMenuItem(4, "Grilled Salmon", "Fresh salmon fillet with herbs, served with roasted vegetables", 18.99m, "Main Courses", 25, true),
                new FoodMenuItem(5, "Chicken Parmesan", "Breaded chicken topped with marinara and mozzarella, served with pasta", 16.99m, "Main Courses", 22, true),
                new FoodMenuItem(6, "Beef Tenderloin", "8oz beef tenderloin with mushroom sauce and mashed potatoes", 24.99m, "Main Courses", 30, true),
                new FoodMenuItem(7, "Vegetable Stir Fry", "Mixed vegetables in soy-ginger sauce with rice", 14.99m, "Main Courses", 18, true),
                
                // Pizzas
                new FoodMenuItem(8, "Margherita Pizza", "Classic pizza with tomato sauce, mozzarella, and basil", 13.99m, "Pizzas", 20, true),
                new FoodMenuItem(9, "Pepperoni Pizza", "Pizza with tomato sauce, mozzarella, and pepperoni", 15.99m, "Pizzas", 20, true),
                new FoodMenuItem(10, "Vegetarian Pizza", "Pizza with tomato sauce, mozzarella, and assorted vegetables", 14.99m, "Pizzas", 22, true),
                
                // Desserts
                new FoodMenuItem(11, "Tiramisu", "Coffee-flavored Italian dessert with mascarpone cheese", 7.99m, "Desserts", 5, true),
                new FoodMenuItem(12, "Chocolate Lava Cake", "Warm chocolate cake with a molten chocolate center", 8.99m, "Desserts", 15, true),
                new FoodMenuItem(13, "Cheesecake", "New York style cheesecake with berry compote", 7.99m, "Desserts", 5, true),
                
                // Beverages
                new FoodMenuItem(14, "Soda", "Assorted soft drinks", 2.99m, "Beverages", 2, true),
                new FoodMenuItem(15, "Iced Tea", "Freshly brewed unsweetened or sweet tea", 2.99m, "Beverages", 3, true),
                new FoodMenuItem(16, "Coffee", "Regular or decaf coffee", 3.49m, "Beverages", 4, true)
            };
        }

        private void InitializeComponent()
        {
            this.Text = "Food Menu";
            this.Size = new Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Dock = DockStyle.Fill;

            // Initialize tab control for categories
            tabCategories = new TabControl();
            tabCategories.Location = new Point(10, 10);
            tabCategories.Size = new Size(650, 300);
            tabCategories.SelectedIndexChanged += TabCategories_SelectedIndexChanged;

            // Initialize menu listview
            lvwMenu = new ListView();
            lvwMenu.View = View.Details;
            lvwMenu.FullRowSelect = true;
            lvwMenu.Columns.Add("Name", 200);
            lvwMenu.Columns.Add("Price", 100);
            lvwMenu.Columns.Add("Prep Time", 100);
            lvwMenu.Size = new Size(630, 250);
            lvwMenu.Location = new Point(10, 20);
            lvwMenu.SelectedIndexChanged += LvwMenu_SelectedIndexChanged;

            // Item details group
            gbDetails = new GroupBox();
            gbDetails.Text = "Item Details";
            gbDetails.Size = new Size(650, 150);
            gbDetails.Location = new Point(10, 320);

            lblItemName = new Label();
            lblItemName.AutoSize = true;
            lblItemName.Font = new Font("Arial", 12, FontStyle.Bold);
            lblItemName.Location = new Point(10, 25);

            lblDescription = new Label();
            lblDescription.Size = new Size(630, 40);
            lblDescription.Location = new Point(10, 50);

            lblPrice = new Label();
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(10, 95);

            lblPrepTime = new Label();
            lblPrepTime.AutoSize = true;
            lblPrepTime.Location = new Point(200, 95);

            Label lblQuantity = new Label();
            lblQuantity.Text = "Quantity:";
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(380, 95);

            nudQuantity = new NumericUpDown();
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 10;
            nudQuantity.Value = 1;
            nudQuantity.Location = new Point(440, 93);
            nudQuantity.Size = new Size(60, 20);

            btnAddToCart = new Button();
            btnAddToCart.Text = "Add to Cart";
            btnAddToCart.Size = new Size(120, 30);
            btnAddToCart.Location = new Point(520, 90);
            btnAddToCart.Click += BtnAddToCart_Click;
            btnAddToCart.Enabled = false;

            gbDetails.Controls.Add(lblItemName);
            gbDetails.Controls.Add(lblDescription);
            gbDetails.Controls.Add(lblPrice);
            gbDetails.Controls.Add(lblPrepTime);
            gbDetails.Controls.Add(lblQuantity);
            gbDetails.Controls.Add(nudQuantity);
            gbDetails.Controls.Add(btnAddToCart);

            // Cart group
            gbCart = new GroupBox();
            gbCart.Text = "Your Order";
            gbCart.Size = new Size(310, 460);
            gbCart.Location = new Point(670, 10);

            lvwCart = new ListView();
            lvwCart.View = View.Details;
            lvwCart.FullRowSelect = true;
            lvwCart.Columns.Add("Item", 150);
            lvwCart.Columns.Add("Qty", 40);
            lvwCart.Columns.Add("Price", 100);
            lvwCart.Size = new Size(290, 200);
            lvwCart.Location = new Point(10, 20);

            lblTotal = new Label();
            lblTotal.Text = "Total: $0.00";
            lblTotal.Font = new Font("Arial", 10, FontStyle.Bold);
            lblTotal.Size = new Size(150, 20);
            lblTotal.Location = new Point(10, 230);

            Label lblDeliveryDate = new Label();
            lblDeliveryDate.Text = "Select Delivery Date:";
            lblDeliveryDate.Size = new Size(150, 20);
            lblDeliveryDate.Location = new Point(10, 260);

            calDeliveryDate = new MonthCalendar();
            calDeliveryDate.Location = new Point(10, 280);
            calDeliveryDate.MaxSelectionCount = 1;
            calDeliveryDate.MinDate = DateTime.Today;

            Label lblDeliveryTime = new Label();
            lblDeliveryTime.Text = "Select Delivery Time:";
            lblDeliveryTime.Size = new Size(150, 20);
            lblDeliveryTime.Location = new Point(10, 340 + calDeliveryDate.Size.Height);

            dtpDeliveryTime = new DateTimePicker();
            dtpDeliveryTime.Format = DateTimePickerFormat.Time;
            dtpDeliveryTime.ShowUpDown = true;
            dtpDeliveryTime.Size = new Size(100, 20);
            dtpDeliveryTime.Location = new Point(150, 340 + calDeliveryDate.Size.Height);

            Label lblSpecialInstructions = new Label();
            lblSpecialInstructions.Text = "Special Instructions:";
            lblSpecialInstructions.Size = new Size(150, 20);
            lblSpecialInstructions.Location = new Point(10, 370 + calDeliveryDate.Size.Height);

            txtSpecialInstructions = new TextBox();
            txtSpecialInstructions.Multiline = true;
            txtSpecialInstructions.Size = new Size(290, 60);
            txtSpecialInstructions.Location = new Point(10, 390 + calDeliveryDate.Size.Height);

            btnPlaceOrder = new Button();
            btnPlaceOrder.Text = "Place Order";
            btnPlaceOrder.Size = new Size(120, 30);
            btnPlaceOrder.Location = new Point(180, 460 + calDeliveryDate.Size.Height);
            btnPlaceOrder.Click += BtnPlaceOrder_Click;
            btnPlaceOrder.Enabled = false;

            btnClearCart = new Button();
            btnClearCart.Text = "Clear Cart";
            btnClearCart.Size = new Size(120, 30);
            btnClearCart.Location = new Point(10, 460 + calDeliveryDate.Size.Height);
			
			btnClearCart.Text = "Clear Cart";
            btnClearCart.Size = new Size(120, 30);
            btnClearCart.Location = new Point(10, 460 + calDeliveryDate.Size.Height);
            btnClearCart.Click += BtnClearCart_Click;
            btnClearCart.Enabled = false;

            gbCart.Controls.Add(lvwCart);
            gbCart.Controls.Add(lblTotal);
            gbCart.Controls.Add(lblDeliveryDate);
            gbCart.Controls.Add(calDeliveryDate);
            gbCart.Controls.Add(lblDeliveryTime);
            gbCart.Controls.Add(dtpDeliveryTime);
            gbCart.Controls.Add(lblSpecialInstructions);
            gbCart.Controls.Add(txtSpecialInstructions);
            gbCart.Controls.Add(btnPlaceOrder);
            gbCart.Controls.Add(btnClearCart);

            // Adjust the size of the groupbox based on the controls
            gbCart.Size = new Size(310, 510 + calDeliveryDate.Size.Height);

            // Add controls to form
            this.Controls.Add(tabCategories);
            this.Controls.Add(gbDetails);
            this.Controls.Add(gbCart);
        }

        private void PopulateMenuCategories()
        {
            // Get distinct categories
            HashSet<string> categories = new HashSet<string>();
            foreach (var item in menuItems)
            {
                categories.Add(item.Category);
            }

            // Create a tab for each category
            foreach (string category in categories)
            {
                TabPage tab = new TabPage(category);
                
                // Create a ListView for this category
                ListView lvw = new ListView();
                lvw.View = View.Details;
                lvw.FullRowSelect = true;
                lvw.Columns.Add("Name", 200);
                lvw.Columns.Add("Price", 100);
                lvw.Columns.Add("Prep Time", 100);
                lvw.Dock = DockStyle.Fill;
                lvw.Tag = category; // Store category in Tag property
                lvw.SelectedIndexChanged += LvwMenu_SelectedIndexChanged;

                // Populate with items for this category
                foreach (var item in menuItems.FindAll(x => x.Category == category && x.IsAvailable))
                {
                    ListViewItem lvi = new ListViewItem(item.Name);
                    lvi.SubItems.Add($"${item.Price:F2}");
                    lvi.SubItems.Add($"{item.PreparationTime} min");
                    lvi.Tag = item; // Store the menu item in the Tag property
                    lvw.Items.Add(lvi);
                }

                tab.Controls.Add(lvw);
                tabCategories.TabPages.Add(tab);
            }
        }

        private void TabCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This would handle category changes if needed
        }

        private void LvwMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lvw = sender as ListView;
            if (lvw != null && lvw.SelectedItems.Count > 0)
            {
                FoodMenuItem item = lvw.SelectedItems[0].Tag as FoodMenuItem;
                if (item != null)
                {
                    // Display item details
                    lblItemName.Text = item.Name;
                    lblDescription.Text = item.Description;
                    lblPrice.Text = $"Price: ${item.Price:F2}";
                    lblPrepTime.Text = $"Preparation Time: {item.PreparationTime} min";
                    btnAddToCart.Enabled = true;
                    btnAddToCart.Tag = item; // Store selected item in button's Tag
                }
            }
            else
            {
                ClearItemDetails();
            }
        }

        private void ClearItemDetails()
        {
            lblItemName.Text = "";
            lblDescription.Text = "";
            lblPrice.Text = "";
            lblPrepTime.Text = "";
            btnAddToCart.Enabled = false;
            btnAddToCart.Tag = null;
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (btnAddToCart.Tag != null)
            {
                FoodMenuItem item = btnAddToCart.Tag as FoodMenuItem;
                int quantity = (int)nudQuantity.Value;

                // Add to cart ListView
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add(quantity.ToString());
                lvi.SubItems.Add($"${item.Price * quantity:F2}");
                lvi.Tag = new { Item = item, Quantity = quantity }; // Store item and quantity
                lvwCart.Items.Add(lvi);

                // Add to cart items list
                for (int i = 0; i < quantity; i++)
                {
                    cartItems.Add(item);
                }

                // Update total
                UpdateCartTotal();

                // Enable place order and clear cart buttons
                btnPlaceOrder.Enabled = true;
                btnClearCart.Enabled = true;

                // Reset quantity
                nudQuantity.Value = 1;
            }
        }

        private void BtnClearCart_Click(object sender, EventArgs e)
        {
            // Clear cart
            lvwCart.Items.Clear();
            cartItems.Clear();
            UpdateCartTotal();
            
            btnPlaceOrder.Enabled = false;
            btnClearCart.Enabled = false;
        }

        private void UpdateCartTotal()
        {
            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.Price;
            }
            lblTotal.Text = $"Total: ${total:F2}";
        }

        private void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add items to your cart before placing an order.", 
                    "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get delivery date and time
            DateTime deliveryDate = calDeliveryDate.SelectionStart.Date;
            DateTime deliveryTime = dtpDeliveryTime.Value;
            DateTime deliveryDateTime = deliveryDate.Add(deliveryTime.TimeOfDay);

            // Check if delivery time is valid (at least 30 minutes from now)
            if (deliveryDateTime < DateTime.Now.AddMinutes(30))
            {
                MessageBox.Show("Please select a delivery time at least 30 minutes from now.", 
                    "Invalid Delivery Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get special instructions
            string specialInstructions = txtSpecialInstructions.Text;

            // Calculate total
            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.Price;
            }

            // Create order summary
            string orderSummary = "Order Summary:\n\n";
            
            // Group items by name and count quantities
            Dictionary<string, int> itemCounts = new Dictionary<string, int>();
            foreach (var item in cartItems)
            {
                if (itemCounts.ContainsKey(item.Name))
                    itemCounts[item.Name]++;
                else
                    itemCounts[item.Name] = 1;
            }

            // Add each item to the summary
            foreach (var kvp in itemCounts)
            {
                FoodMenuItem item = menuItems.Find(x => x.Name == kvp.Key);
                orderSummary += $"{kvp.Value}x {kvp.Key} - ${item.Price * kvp.Value:F2}\n";
            }
            
            orderSummary += $"\nTotal: ${total:F2}";
            orderSummary += $"\nDelivery Time: {deliveryDateTime:MM/dd/yyyy hh:mm tt}";
            
            if (!string.IsNullOrWhiteSpace(specialInstructions))
                orderSummary += $"\nSpecial Instructions: {specialInstructions}";

            // Show order confirmation dialog
            if (MessageBox.Show(orderSummary + "\n\nConfirm your order?", "Order Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // In a real system, this would save the order to a database
                MessageBox.Show("Your order has been placed successfully!\nYou will receive a notification when your food is ready.", 
                    "Order Placed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Clear cart after successful order
                BtnClearCart_Click(sender, e);
            }
        }
    }
}