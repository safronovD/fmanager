using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                    if (Util.PasswordHandler.Validate(textBox2.Text + "YYYYY", node["password"].InnerText))
                    {
                        Properties.Settings.Default.userName = textBox1.Text;
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
            try
            {
                XmlElement xRoot = _xDoc.DocumentElement;

                if (!Util.checkPassOrLogin(textBox1.Text))
                {
                    throw new ArgumentException("Login is incorrect.");
                }

                if (!Util.checkPassOrLogin(textBox2.Text))
                {
                    throw new ArgumentException("Password is incorrect.");
                }

                foreach (XmlNode node in xRoot)
                {
                    if (node["login"].InnerText == textBox1.Text)
                    {
                        throw new ArgumentException("Login already exists.");
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
                XmlText passwordText = _xDoc.CreateTextNode(Util.PasswordHandler.CreatePasswordHash(textBox2.Text + "YYYYY"));
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

                MessageBox.Show("User created.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Properties.Settings.Default.userName = textBox1.Text;
                Properties.Settings.Default.Save();

                if (!Directory.Exists(@"C:\temp"))
                {
                    Directory.CreateDirectory(@"C:\temp");
                }

                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGuest_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.userName = "__guest__";
            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
