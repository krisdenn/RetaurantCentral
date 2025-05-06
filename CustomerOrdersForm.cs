internal class CustomerOrdersForm : Form
{
    private string username;

    public CustomerOrdersForm(string username)
    {
        this.username = username;
        this.Text = $"Welcome, {username}";
    }
}
