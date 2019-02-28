using System.Windows.Forms;

namespace Checkers
{
    public partial class DialogForm : Form
    {
        public DialogForm(string message, string caption)
        {
            InitializeComponent();
            Text = caption;
            lbMessage.Text = message;
        }

        public static DialogResult Show(Form owner, string message, string caption)
        {
            var frm = new DialogForm(message, caption);
            return frm.ShowDialog(owner);
        }
    }

}
