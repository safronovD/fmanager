using System;
using System.Windows.Forms;

namespace FileManagerWithProfiles
{
    public partial class TextBoxDialog : Form
    {
        public TextBoxDialog(string name, string label, string text)
        {
            InitializeComponent();

            this.Text = name;
            this.label.Text = label;
            this.textBox.Text = text;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Text = textBox.Text;
            this.Close();
        }

        private void buttonCancell_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
