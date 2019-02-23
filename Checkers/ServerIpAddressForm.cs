using System.Windows.Forms;

namespace Checkers
{
    public partial class ServerIpAddressForm : Form
    {
        public ServerIpAddressForm()
        {
            InitializeComponent();
        }

        public string IpAddress
        {
            get { return tbIpAddress.Text; }
            set { tbIpAddress.Text = value; }
        }

        private void tbIpAddress_TextChanged(object sender, System.EventArgs e)
        {
            btnOk.Enabled = tbIpAddress.Text.Trim().Length > 0;
        }
    }
}
