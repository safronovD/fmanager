using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BCrypt.Net;

namespace FileManagerWithProfiles
{
    public partial class AutentificationForm : Form
    {
        private XmlDocument _xDoc;

        public AutentificationForm()
        {
            InitializeComponent();

            _xDoc = new XmlDocument();
            _xDoc.Load(Properties.Settings.Default.xmlPath);
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            XmlElement xRoot = _xDoc.DocumentElement;

            foreach (XmlNode node in xRoot)
            {
                if (node["login"].InnerText == textBox1.Text)
                {
                    if (BCrypt.Net.BCrypt.Verify(textBox2.Text + "YYYYY", node["password"].InnerText))
                    {
                        //Properties.Settings.Default.userName = textBox1.Text;
                        Properties.Settings.Default.Save();
                        
                        this.Close();

                        return;
                    } else
                    {
                        MessageBox.Show("Password is not correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            MessageBox.Show("Login is not correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            XmlElement xRoot = _xDoc.DocumentElement;

            foreach (XmlNode node in xRoot)
            {
                if (node["login"].InnerText == textBox1.Text)
                {
                    MessageBox.Show("Login already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            XmlElement userElem = _xDoc.CreateElement("user");
            XmlAttribute nameAttr = _xDoc.CreateAttribute("name");
            XmlElement loginElem = _xDoc.CreateElement("login");
            XmlElement passwordElem = _xDoc.CreateElement("password");
            XmlElement rootElem = _xDoc.CreateElement("root");
            XmlElement fontElem = _xDoc.CreateElement("fontColor");
            XmlElement backElem = _xDoc.CreateElement("backColor");
            XmlElement profilesElem = _xDoc.CreateElement("profiles");

            XmlText loginText = _xDoc.CreateTextNode(textBox1.Text);
            XmlText passwordText = _xDoc.CreateTextNode(BCrypt.Net.BCrypt.HashPassword(textBox2.Text + "YYYYY", BCrypt.Net.BCrypt.GenerateSalt()));
            XmlText rootText = _xDoc.CreateTextNode(@"C:\temp");
            XmlText fontText = _xDoc.CreateTextNode("Black");
            XmlText backText = _xDoc.CreateTextNode("White");

            nameAttr.AppendChild(loginText);
            loginElem.AppendChild(loginText);
            passwordElem.AppendChild(passwordText);
            rootElem.AppendChild(rootText);
            fontElem.AppendChild(fontText);
            backElem.AppendChild(backText);
            userElem.AppendChild(loginElem);
            userElem.AppendChild(passwordElem);
            userElem.AppendChild(rootElem);
            userElem.AppendChild(fontElem);
            userElem.AppendChild(backElem);
            userElem.AppendChild(profilesElem);
            xRoot.AppendChild(userElem);

            _xDoc.Save(Properties.Settings.Default.xmlPath);

            MessageBox.Show("User created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
