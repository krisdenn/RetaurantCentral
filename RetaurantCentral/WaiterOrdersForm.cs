using System;
using System.Windows.Forms;

namespace RestaurantSystem
{
    internal class WaiterOrdersForm : Form
    {
        private string username;

        public WaiterOrdersForm(string username)
        {
            this.username = username;
        }

        // Use the 'new' keyword to explicitly hide the inherited member
        public new Action<object, object> FormClosed { get; internal set; }

        public new void Show()
        {
            base.Show();
            // Additional logic for showing the form can be added here if needed
        }

        public static implicit operator Form(WaiterOrdersForm v)
        {
            throw new NotImplementedException();
        }
    }
}