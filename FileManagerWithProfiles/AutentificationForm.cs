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

            //XmlElement xRoot = xDoc.DocumentElement;
            //// обход всех узлов в корневом элементе
            //foreach (XmlNode xnode in xRoot)
            //{
            //    // получаем атрибут name
            //    if (xnode.Attributes.Count > 0)
            //    {
            //        XmlNode attr = xnode.Attributes.GetNamedItem("name");
            //        if (attr != null)
            //            Console.WriteLine(attr.Value);
            //    }
            //    // обходим все дочерние узлы элемента user
            //    foreach (XmlNode childnode in xnode.ChildNodes)
            //    {
            //        // если узел - company
            //        if (childnode.Name == "password")
            //        {
            //            Console.WriteLine($"Компания: {childnode.InnerText}");
            //        }

            //    }
            //    Console.WriteLine();
            //}
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
                        Properties.Settings.Default.userName = textBox1.Text;
                        Properties.Settings.Default.Save();
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
            XmlElement loginElem = _xDoc.CreateElement("login");
            XmlElement passwordElem = _xDoc.CreateElement("password");

            XmlText loginText = _xDoc.CreateTextNode(textBox1.Text);
            XmlText passwordText = _xDoc.CreateTextNode(BCrypt.Net.BCrypt.HashPassword(textBox2.Text + "YYYYY", BCrypt.Net.BCrypt.GenerateSalt()));
            
            loginElem.AppendChild(loginText);
            passwordElem.AppendChild(passwordText);
            userElem.AppendChild(loginElem);
            userElem.AppendChild(passwordElem);
            xRoot.AppendChild(userElem);

            _xDoc.Save(Properties.Settings.Default.xmlPath);

            MessageBox.Show("User created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
