using System;
using System.Windows.Forms;

namespace RestaurantSystem
{
    internal class CustomerOrdersForm : Form
    {
        private string username;

        public CustomerOrdersForm(string username)
        {
            this.username = username;
            this.Text = "Customer Orders";
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CustomerOrdersForm
            // 
            this.ClientSize = new System.Drawing.Size(428, 349);
            this.Name = "CustomerOrdersForm";
            this.ResumeLayout(false);

        }
    }
}