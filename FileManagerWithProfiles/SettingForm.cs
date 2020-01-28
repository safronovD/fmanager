using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FileManagerWithProfiles
{
    public partial class SettingForm : Form
    {
        private XmlNode _userNode;
        private XmlDocument _xDoc;

        public SettingForm()
        {
            InitializeComponent();

            try
            {
                Util.initXMLComponents(ref _xDoc, ref _userNode);

                addColors(comboBoxFontColors);
                addColors(comboBoxBackColor);

                comboBoxFontColors.SelectedItem = _userNode["fontColor"].InnerText;
                comboBoxBackColor.SelectedItem = _userNode["backColor"].InnerText;

                textBoxRootDirectory.Text = _userNode["root"].InnerText;

            } catch (ArgumentException exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (exp.ParamName == "XmlDocument")
                {
                    Application.Exit();
                }
            } catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.userName.Equals("__guest__"))
                {
                    throw new ArgumentException("Cannot do it in guest mode.");
                }

                if (!Util.checkPassOrLogin(textBoxNewPass.Text))
                {
                    throw new ArgumentException ("Pass is not correct.")
                }

                if (BCrypt.Net.BCrypt.Verify(textBoxCurPass.Text + "YYYYY", _userNode["password"].InnerText))
                {
                    _userNode["password"].InnerText = BCrypt.Net.BCrypt.HashPassword(textBoxNewPass.Text + "YYYYY", BCrypt.Net.BCrypt.GenerateSalt());
                    _xDoc.Save(Properties.Settings.Default.xmlPath);

                    MessageBox.Show("Password changed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Current password is not correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void changeColors(String fontColor, String backColor)
        {
            this.BackColor = Color.FromName(backColor);
            this.ForeColor = Color.FromName(fontColor);
            foreach (TabPage page in tabControl.TabPages)
            {
                page.BackColor = Color.FromName(backColor);
            }
        }

        private void addColors(ComboBox comboBox)
        {
            comboBox.Items.Add(Color.White.Name);
            comboBox.Items.Add(Color.Black.Name);
            comboBox.Items.Add(Color.Blue.Name);
            comboBox.Items.Add(Color.Green.Name);
            comboBox.Items.Add(Color.Red.Name);
            comboBox.Items.Add(Color.Yellow.Name);
            comboBox.Items.Add(Color.Purple.Name);
            comboBox.Items.Add(Color.Pink.Name);
            comboBox.Items.Add(Color.Silver.Name);
            comboBox.Items.Add(Color.Gold.Name);
            comboBox.Items.Add(Color.Thistle.Name);
            comboBox.Items.Add(Color.Violet.Name);
            comboBox.Items.Add(Color.Cyan.Name);
            comboBox.Items.Add(Color.BlanchedAlmond.Name);
            comboBox.Items.Add(Color.Firebrick.Name);
            comboBox.Items.Add(Color.Khaki.Name);
        }

        private void comboBoxFontColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeColors(comboBoxFontColors.SelectedItem.ToString(), this.BackColor.Name);
        }

        private void comboBoxBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeColors(this.ForeColor.Name, comboBoxBackColor.SelectedItem.ToString());
        }

        private void buttonColorsSave_Click(object sender, EventArgs e)
        {
            try
            {
                _userNode["fontColor"].InnerText = comboBoxFontColors.SelectedItem.ToString();
                _userNode["backColor"].InnerText = comboBoxBackColor.SelectedItem.ToString();
                _xDoc.Save(Properties.Settings.Default.xmlPath);
                MessageBox.Show("Settings saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void buttonChangeRootDirectory_Click(object sender, EventArgs e)
        {
            _userNode["root"].InnerText = textBoxRootDirectory.Text;
            _xDoc.Save(Properties.Settings.Default.xmlPath);
            MessageBox.Show("Settings saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
