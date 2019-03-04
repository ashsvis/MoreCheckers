using System.Windows.Forms;

namespace Checkers
{
    public partial class DialogForm : Form
    {
        public DialogForm(string message, string caption, bool question)
        {
            InitializeComponent();
            Text = caption;
            lbMessage.Text = message;
            if (!question)
            {
                btnYes.Visible = false;
                btnNo.Text = "OK";
            }
        }

        public static DialogResult Show(Form owner, string message, string caption, bool question = true)
        {
            var frm = new DialogForm(message, caption, question);
            return frm.ShowDialog(owner);
        }
    }

}
