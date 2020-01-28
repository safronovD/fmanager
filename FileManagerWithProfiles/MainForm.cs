using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;
using System.Xml;
using System.IO.Compression;

namespace FileManagerWithProfiles
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private TreeNode _lastSelectNode;
        private TreeNode _fullNode;

        private XmlNode _userNode;
        private XmlDocument _xDoc;

        private String _mode = "Null";
        private ListViewItem _selectedProfile = null;

        public MainForm()
        {
            InitializeComponent();

            Util.initXMLComponents(ref _xDoc, ref _userNode);
            changeColors(_userNode["fontColor"].InnerText, _userNode["backColor"].InnerText);

            foreach (string view in Enum.GetNames(typeof(View)))
            {
                toolStripComboBox.Items.Add(view);
            }

            toolStripComboBox.SelectedIndex = 0;

            listView1.View = View.Tile;

            addProfiles(listView1);
            listView1.ShowGroups = true;
        }

        private void initTopNodeWithPath(string path)
        {
            treeView.Nodes.Clear();
            TreeNode topNode = Util.createNodeForView(path);
            treeView.Nodes.AddRange(new TreeNode[] { topNode });
            treeView.TopNode = topNode;
            _lastSelectNode = treeView.TopNode;
            _lastSelectNode.Expand();
            setListView(treeView.TopNode);
        }

        private void initTopNodeWithNode(TreeNode topNode)
        {
            treeView.Nodes.Clear();
            treeView.Nodes.AddRange(new TreeNode[] { topNode });
            treeView.TopNode = topNode;
            _lastSelectNode = treeView.TopNode;
            _lastSelectNode.Expand();
            setListView(treeView.TopNode);
        }

        private void toolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string viewName = toolStripComboBox.SelectedItem.ToString();
            View view = (View)Enum.Parse(typeof(View), viewName);
            if (view != View.Details)
                listView.View = view;
        }

        private void groupsToolStripButton_Click(object sender, EventArgs e)
        {
            listView.ShowGroups = !listView.ShowGroups;
            setListView(_lastSelectNode);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _lastSelectNode = e.Node;
            setListView(_lastSelectNode);
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count == 1)
                {
                    if (listView.SelectedItems[0].Group == listView.Groups["Files"])
                    {
                        string path = "";
                        if (_mode.Equals("Real"))
                        {
                            path = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + listView.SelectedItems[0].Name;
                        }
                        else if (_mode.Equals("Virtual"))
                        {
                            path = Util.GetFullPathForSelectedNode(Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.Find(listView.SelectedItems[0].Name, false)[0]);
                        }

                        if (File.Exists(path))
                        {
                            Process.Start(path);
                        } else
                        {
                            throw new ArgumentException("File " + listView.SelectedItems[0].Name + " moved or renamed");
                        }
                    }
                    else if (listView.SelectedItems[0].Group == listView.Groups["Folders"])
                    {
                        TreeNode[] treeNode = _lastSelectNode.Nodes.Find(listView.SelectedItems[0].Name, false);
                        if (treeNode.Count() == 1)
                        {
                            treeNode[0].Expand();
                            treeView.SelectedNode = treeNode[0];
                        }
                    }
                    else if (listView.SelectedItems[0].Group == listView.Groups["System"])
                    {
                        TreeNode treeNode = _lastSelectNode.Parent;
                        treeView.SelectedNode = treeNode;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }


        #region ListView
        private void setListView(TreeNode node)
        {
            listView.Items.Clear();
            if (_mode.Equals("Real"))
            {
                listView.Items.AddRange(GetListViewDirectories(node).ToArray());
                listView.Items.AddRange(GetListViewFiles(node).ToArray());
            }
            if (_mode.Equals("Virtual"))
            {
                listView.Items.AddRange(getVirtualListView(Util.findInTreeNode(_fullNode, node.FullPath)).ToArray());
            }

            var listViewItem = new ListViewItem();
            listViewItem.Name = "...";
            listViewItem.Text = "...";
            listViewItem.ImageIndex = 2;
            listViewItem.Group = listView.Groups["System"];

            listView.Items.Add(listViewItem);
        }

        private List<ListViewItem> getVirtualListView(TreeNode node)
        {
            List<ListViewItem> newListViewItemList = new List<ListViewItem>(node.Nodes.Count);

            foreach (TreeNode childNode in node.Nodes)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Name = Path.GetFileName(childNode.Name);
                listViewItem.Text = Path.GetFileName(childNode.Text);

                switch (childNode.ToolTipText) {
                    case "File":
                    {
                        listViewItem.ImageIndex = 1;
                        listViewItem.Group = listView.Groups["Files"];
                        break;
                    }
                    case "Folder":
                    {
                        listViewItem.ImageIndex = 2;
                        listViewItem.Group = listView.Groups["Folders"];
                        break;
                    }
                }

                newListViewItemList.Add(listViewItem);
            }
            return newListViewItemList;
        }

        private List<ListViewItem> GetListViewFiles(TreeNode node)
        {
            var files = Util.GetFiles(Util.GetFullPathForSelectedNode(node));

            List<ListViewItem> newListViewItemList = new List<ListViewItem>(files.Count);
            foreach (var file in files)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Name = Path.GetFileName(file);
                listViewItem.Text = Path.GetFileName(file);
                listViewItem.ImageIndex = 1;
                listViewItem.Group = listView.Groups["Files"];
                newListViewItemList.Add(listViewItem);
            }
            return newListViewItemList;
        }

        private List<ListViewItem> GetListViewDirectories(TreeNode node)
        {
            var directories = Util.GetDirectories(Util.GetFullPathForSelectedNode(node));

            List<ListViewItem> newListViewItemList = new List<ListViewItem>(directories.Count);
            foreach (var directory in directories)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Name = Path.GetFileName(directory);
                listViewItem.Text = Path.GetFileName(directory);
                listViewItem.ImageIndex = 2;
                listViewItem.Group = listView.Groups["Folders"];
                newListViewItemList.Add(listViewItem);
            }
            return newListViewItemList;
        }

        private List<ListViewItem> GetListViewDrives()
        {
            List<ListViewItem> newListViewItemList = new List<ListViewItem>(Util.GetLogicalDrives().Count);
            foreach (var drive in Util.GetLogicalDrives())
            {
                var listViewItem = new ListViewItem();
                listViewItem.Name = drive;
                listViewItem.Text = drive;
                listViewItem.ImageIndex = 3;
                listViewItem.Group = listView.Groups["Drives"];
                newListViewItemList.Add(listViewItem);
            }
            return newListViewItemList;
        }
        #endregion

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count == 1)
                {
                    var selectedItem = listView.SelectedItems[0];
                    string path = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + selectedItem.Name;

                    if (selectedItem.Group == listView.Groups["Files"])
                    {
                        if (File.Exists(path))
                        {
                            int i = 0;
                            while (true)
                            {
                                string newPath = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + Path.GetFileNameWithoutExtension(path) + i.ToString() + Path.GetExtension(path);
                                if (!File.Exists(newPath))
                                {
                                    File.Copy(path, newPath);
                                    break;
                                }
                                i++;
                            }
                        }
                    }
                    else if (selectedItem.Group == listView.Groups["Folders"])
                    {
                        if (Directory.Exists(path))
                        {
                            int i = 0;
                            while (true)
                            {
                                string newPath = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + selectedItem.Name + i.ToString();
                                if (!Directory.Exists(newPath))
                                {
                                    Util.DirectoryCopy(path, newPath, true);
                                    break;
                                }
                                i++;
                            }
                        }
                    }
                }

                _lastSelectNode.Nodes.Clear();
                _lastSelectNode.Nodes.AddRange(Util.GetTreeNodeDirectories(Util.GetFullPathForSelectedNode(_lastSelectNode)).ToArray());
                setListView(_lastSelectNode);

            } catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Util.GetFullPathForSelectedNode(_lastSelectNode);

            try
            {
                var i = 0;
                while (true)
                {
                    string folderPath = path + @"/" + "NewFolder" + i.ToString();
                    if (!Directory.Exists(folderPath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(folderPath);
                        _lastSelectNode.Nodes.Clear();
                        _lastSelectNode.Nodes.AddRange(Util.GetTreeNodeDirectories(Util.GetFullPathForSelectedNode(_lastSelectNode)).ToArray());
                        setListView(_lastSelectNode);
                        break;
                    }
                    i++;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = listView.SelectedItems;
                foreach (ListViewItem selectedItem in listView.SelectedItems)
                {
                    string path = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + selectedItem.Name;

                    if (selectedItem.Group == listView.Groups["Files"])
                    {
                        DialogResult result = MessageBox.Show("Delete real file?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Yes)
                        {
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                    }
                    else if (selectedItem.Group == listView.Groups["Folders"])
                    {
                        if (Directory.Exists(path))
                        {
                            if (Directory.GetFileSystemEntries(path).Length == 0)
                            {
                                Directory.Delete(path);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Folder is not empty. Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                                if (result == DialogResult.Yes)
                                {
                                    Util.DirectoryDelete(path, true);
                                }
                            }
                        }
                    }
                }

                _lastSelectNode.Nodes.Clear();
                _lastSelectNode.Nodes.AddRange(Util.GetTreeNodeDirectories(Util.GetFullPathForSelectedNode(_lastSelectNode)).ToArray());
                setListView(_lastSelectNode);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            treeView.SelectedNode = _lastSelectNode.Parent;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    var selectedItem = listView1.SelectedItems[0];

                    if (_mode.Equals("Virtual"))
                    {
                        DialogResult result = MessageBox.Show("Save profile changes in " + _selectedProfile.Text +"?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Yes)
                        {
                            saveTreeNode(_fullNode, Properties.Settings.Default.profilesPath + @"/" + _selectedProfile.Name + ".xml");
                        }
                    }

                    if (selectedItem.Group == listView1.Groups["Virtual"])
                    {
                        _mode = "Virtual";
                        _fullNode = loadTreeNode(Properties.Settings.Default.profilesPath + @"/" + selectedItem.Name + ".xml");
                        listView.ContextMenuStrip = contextMenuStrip2;
                        initTopNodeWithNode(Util.getFilteredNode(_fullNode));
                        _selectedProfile = selectedItem;
                    } else
                    {
                        _mode = "Real";
                        _fullNode = null;
                        listView.ContextMenuStrip = contextMenuStrip1;
                        initTopNodeWithPath(_userNode["root"].InnerText);
                        _selectedProfile = null;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
            Util.initXMLComponents(ref _xDoc, ref _userNode);
            changeColors(_userNode["fontColor"].InnerText, _userNode["backColor"].InnerText);
            addProfiles(listView1);
            treeView.Nodes.Clear();
            listView.Clear();
        }

        private void saveTreeNode(TreeNode treeNode, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            SoapFormatter sf = new SoapFormatter();
            sf.Serialize(fs, treeNode);
            fs.Close();
        }

        private TreeNode loadTreeNode(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            SoapFormatter sf = new SoapFormatter();
            TreeNode treeNode = (TreeNode)sf.Deserialize(fs);
            fs.Close();

            return treeNode;
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            string path = Properties.Settings.Default.profilesPath;

            try
            {
                if (Properties.Settings.Default.userName.Equals("__guest__"))
                {
                    throw new ArgumentException("Cannot do it in guest mode.");
                }

                int i = 0;
                while (true)
                {
                    string filePath = path + @"/" + i.ToString() + ".xml";
                    if (!File.Exists(filePath))
                    {
                        saveTreeNode(Util.createNodeForSave(_userNode["root"].InnerText), filePath);

                        XmlElement profileElem = _xDoc.CreateElement("profile");
                        XmlElement profileName = _xDoc.CreateElement("name");
                        profileName.AppendChild(_xDoc.CreateTextNode("NewProfile" + i.ToString()));
                        XmlElement profileID = _xDoc.CreateElement("id");
                        profileID.AppendChild(_xDoc.CreateTextNode(i.ToString()));
                        XmlElement profileFolder = _xDoc.CreateElement("folder");
                        profileFolder.AppendChild(_xDoc.CreateTextNode(_userNode["root"].InnerText));
                        profileElem.AppendChild(profileName);
                        profileElem.AppendChild(profileID);
                        profileElem.AppendChild(profileFolder);
                        _userNode["profiles"].AppendChild(profileElem);
                        _xDoc.Save(Properties.Settings.Default.xmlPath);

                        break;
                    }
                    i++;
                }

                addProfiles(listView1);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void addProfiles(ListView listView)
        {
            List<ListViewItem> newListViewItemList = new List<ListViewItem>(2);

            ListViewItem listViewItem = new ListViewItem();
            //listViewItem.Name = "Computer";
            //listViewItem.Text = "Computer";
            //listViewItem.Group = listView1.Groups["Real"];
            //newListViewItemList.Add(listViewItem);

            listViewItem = new ListViewItem();
            listViewItem.Name = "Folder";
            listViewItem.Text = "Folder";
            listViewItem.Group = listView1.Groups["Real"];
            newListViewItemList.Add(listViewItem);

            foreach (XmlNode node in _userNode["profiles"].ChildNodes)
            {
                if (node["folder"].InnerText == _userNode["root"].InnerText)
                {
                    listViewItem = new ListViewItem();
                    listViewItem.Name = node["id"].InnerText;
                    listViewItem.Text = node["name"].InnerText;
                    listViewItem.Group = listView1.Groups["Virtual"];
                    newListViewItemList.Add(listViewItem);
                }
            }

            listView1.Items.Clear();
            listView1.Items.AddRange(newListViewItemList.ToArray());
        }

        private void changeColors(String fontColor, String backColor)
        {
            this.BackColor = Color.FromName(backColor);
            this.ForeColor = Color.FromName(fontColor);

            this.listView.BackColor = Color.FromName(backColor);
            this.listView1.BackColor = Color.FromName(backColor);
            this.treeView.BackColor = Color.FromName(backColor);

            this.listView.ForeColor = Color.FromName(fontColor);
            this.listView1.ForeColor = Color.FromName(fontColor);
            this.treeView.ForeColor = Color.FromName(fontColor);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    var selectedItem = listView1.SelectedItems[0];
                    string filePath = Properties.Settings.Default.profilesPath + @"/" + selectedItem.Name + ".xml";

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    List<XmlNode> list = _userNode["profiles"].ChildNodes.Cast<XmlNode>()
                    .Where(profile => profile["id"].InnerText.Equals(selectedItem.Name))
                    .ToList();

                    if (list.Count != 1)
                    {
                        throw new ArgumentException("Users.xml is corrupted!", "XmlDocument");
                    }

                    _userNode["profiles"].RemoveChild(list[0]);
                    _xDoc.Save(Properties.Settings.Default.xmlPath);

                    listView1.Items.Remove(selectedItem);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count == 1)
                {
                    TreeNode[] treeNodeList = _lastSelectNode.Nodes.Find(listView.SelectedItems[0].Name, false);
                    if (treeNodeList.Count() == 1)
                    {
                        TreeNode treeNode = Util.findInTreeNode(_fullNode, treeNodeList[0].FullPath);
                        TreeNode newNode = (TreeNode)treeNode.Clone();

                        int i = 0;

                        while (true)
                        {
                            string newName = treeNode.Name + "Copy" + i.ToString();
                            if (_lastSelectNode.Nodes.Find(newName, false).ToList().Count == 0)
                            {
                                newNode.Name = newName;
                                newNode.Text = newName;
                                break;
                            }
                            i++;
                        }

                        Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.Add(newNode);
                        _lastSelectNode.Nodes.Add(Util.getFilteredNode(newNode));

                        setListView(_lastSelectNode);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode newNode =new TreeNode();
                int i = 0;

                while (true)
                {
                    string newName = "NewFolder" + i.ToString();
                    if (_lastSelectNode.Nodes.Find(newName, false).ToList().Count == 0)
                    {
                        newNode = new TreeNode
                        {
                            ImageIndex = 2,
                            SelectedImageIndex = 2,
                            Name = newName,
                            Text = newName,
                            Tag = "Virtual",
                            ToolTipText = "Folder"
                        };
                        break;
                    }
                    i++;
                }

                Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.Add(newNode);
                _lastSelectNode.Nodes.Add(Util.getFilteredNode(newNode));

                setListView(_lastSelectNode);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem selectedItem in listView.SelectedItems)
                {
                    TreeNode[] treeNodeList = Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.Find(selectedItem.Name, false);

                    if (treeNodeList.Count() == 1)
                    {
                        TreeNode node = treeNodeList[0];

                        Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.RemoveByKey(node.Name);
                        _lastSelectNode.Nodes.RemoveByKey(node.Name);
                    }
                }
                setListView(_lastSelectNode);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        void listView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        void listView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection dragItems =
                    (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));

                Point loc = listView.PointToClient(new Point(e.X, e.Y));
                ListViewItem endItem = listView.GetItemAt(loc.X, loc.Y);

                TreeNode treeNode = new TreeNode();

                if (endItem is null)
                {
                    return;
                }
                else if (endItem.Group == listView.Groups["Files"])
                {
                    return;
                }
                else if (endItem.Group == listView.Groups["System"])
                {
                    if (endItem.Name.Equals("..."))
                    {
                        if (_lastSelectNode != treeView.TopNode)
                        {
                            treeNode = _lastSelectNode.Parent;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (endItem.Group == listView.Groups["Folders"])
                {
                    TreeNode[] tempList = _lastSelectNode.Nodes.Find(endItem.Name, false);
                    if (tempList.Count() == 1)
                    {
                        treeNode = tempList[0];
                    }
                    else
                    {
                        return;
                    }
                }

                if (_mode.Equals("Virtual"))
                {
                    foreach (ListViewItem item in dragItems)
                    {
                        TreeNode node = Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.Find(item.Name, false)[0];

                        if (treeNode.Nodes.Find(node.Name, false).Count() != 0)
                        {
                            throw new ArgumentException("Folder with name " + node.Name + " already exists in " + treeNode.Name);
                        }

                        Util.findInTreeNode(_fullNode, treeNode.FullPath).Nodes.Add((TreeNode)node.Clone());
                        Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.RemoveByKey(node.Name);

                        if (node.ToolTipText.Equals("Folder"))
                        {
                            treeNode.Nodes.Add(Util.getFilteredNode((TreeNode)node.Clone()));
                            _lastSelectNode.Nodes.RemoveByKey(node.Name);
                        }
                    }
                }
                else if (_mode.Equals("Real"))
                {
                    foreach (ListViewItem item in dragItems)
                    {
                        string path = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + item.Name;

                        if (item.Group == listView.Groups["Files"])
                        {
                            if (File.Exists(path))
                            {
                                string newPath = Util.GetFullPathForSelectedNode(treeNode) + @"\" + item.Name;
                                if (!File.Exists(newPath))
                                {
                                    File.Move(path, newPath);
                                }
                                else
                                {
                                    MessageBox.Show(item.Name + " already exists in " + treeNode.Name + '.', "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if (item.Group == listView.Groups["Folders"])
                        {
                            if (Directory.Exists(path))
                            {
                                string newPath = Util.GetFullPathForSelectedNode(treeNode) + @"\" + item.Name;
                                if (!Directory.Exists(newPath))
                                {
                                    Util.DirectoryMove(path, newPath, true);
                                }
                                else
                                {
                                    MessageBox.Show(item.Name + " already exists in " + treeNode.Name + '.', "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }

                    if (endItem.Group == listView.Groups["System"])
                    {
                        if (endItem.Name.Equals("..."))
                        {
                            TreeNode curNode = _lastSelectNode;
                            _lastSelectNode = _lastSelectNode.Parent;
                            _lastSelectNode.Nodes.Clear();
                            _lastSelectNode.Nodes.AddRange(Util.GetTreeNodeDirectories(Util.GetFullPathForSelectedNode(_lastSelectNode)).ToArray());

                            curNode.Expand();
                            treeView.SelectedNode = curNode;
                            _lastSelectNode = curNode;
                        }
                    }
                    else
                    {
                        _lastSelectNode.Nodes.Clear();
                        _lastSelectNode.Nodes.AddRange(Util.GetTreeNodeDirectories(Util.GetFullPathForSelectedNode(_lastSelectNode)).ToArray());
                    }
                }

                setListView(_lastSelectNode);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView.DoDragDrop(listView.SelectedItems, DragDropEffects.Move);
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProfile is null)
                {
                    throw new ArgumentException("Chouse profile.");
                }

                saveTreeNode(_fullNode, Properties.Settings.Default.profilesPath + @"/" + _selectedProfile.Name + ".xml");
                MessageBox.Show("Profile changes save.", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

            private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProfile is null)
                {
                    throw new Exception("Chouse profile.");
                }

                TextBoxDialog dialog = new TextBoxDialog("AA", "Change profile name or cancell:", _selectedProfile.Text);
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    string newName = dialog.Text;

                    XmlNode xNode = _userNode["profiles"];
                    List<XmlNode> list = xNode.ChildNodes.Cast<XmlNode>()
                   .Where(user => user["id"].InnerText.Equals(_selectedProfile.Name))
                   .ToList();

                    if (list.Count == 1)
                    {
                        list[0]["name"].InnerText = newName;
                        _xDoc.Save(Properties.Settings.Default.xmlPath);
                    }

                    addProfiles(listView1);
                }
            } catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count == 1)
                {
                    var selectedItem = listView.SelectedItems[0];

                    if (selectedItem.Group == listView.Groups["System"])
                    {
                        return;
                    }

                    TextBoxDialog dialog = new TextBoxDialog("Rename", "Change profile name or cancell:", selectedItem.Name);
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        string path = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + selectedItem.Name;
                        string newPath = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + dialog.Text;
                        
                        if (selectedItem.Group == listView.Groups["Files"])
                        {
                            if (File.Exists(path))
                            {
                                if (!File.Exists(newPath))
                                {
                                    File.Move(path, newPath);
                                } else
                                {
                                    throw new ArgumentException("File " + dialog.Text + " already exists.");
                                }
                            }
                        }
                        else if (selectedItem.Group == listView.Groups["Folders"])
                        {
                            if (Directory.Exists(path))
                            {
                                if (!Directory.Exists(newPath))
                                {
                                    Util.DirectoryMove(path, newPath, true);
                                } else
                                {
                                    throw new ArgumentException("Folder " + dialog.Text + " already exists.");
                                }
                            }
                        }
                    }
                }
                _lastSelectNode.Nodes.Clear();
                _lastSelectNode.Nodes.AddRange(Util.GetTreeNodeDirectories(Util.GetFullPathForSelectedNode(_lastSelectNode)).ToArray());
                setListView(_lastSelectNode);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }


        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count == 1)
                {
                    var selectedItem = listView.SelectedItems[0];

                    if (selectedItem.Group == listView.Groups["System"])
                    {
                        return;
                    }

                    TextBoxDialog dialog = new TextBoxDialog("Rename", "Change profile name or cancell:", selectedItem.Name);
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        TreeNode node = Util.findInTreeNode(_fullNode, _lastSelectNode.FullPath).Nodes.Find(selectedItem.Name, false)[0];

                        node.Text = dialog.Text;
                        node.Name = dialog.Text;

                        if (node.ToolTipText.Equals("Folder"))
                        {
                            node = _lastSelectNode.Nodes.Find(selectedItem.Name, false)[0];

                            node.Text = dialog.Text;
                            node.Name = dialog.Text;
                        }

                    }
                }
                setListView(_lastSelectNode);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }

        private void archivateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count == 1)
                {
                    var selectedItem = listView.SelectedItems[0];

                    string path = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + selectedItem.Name;
                    string newPath = path + ".zip";

                    if (selectedItem.Group == listView.Groups["System"])
                    {
                        return;
                    }

                    if (selectedItem.Group == listView.Groups["Files"])
                    {
                        if (File.Exists(path))
                        {
                            string tempPath = Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + "temp" + @"\" + selectedItem.Name;

                            Directory.CreateDirectory(Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + "temp");
                            File.Move(path, tempPath);
                            ZipFile.CreateFromDirectory(Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + "temp", newPath);

                            File.Move(tempPath, path);
                            Directory.Delete(Util.GetFullPathForSelectedNode(_lastSelectNode) + @"\" + "temp");
                        }
                    }
                    
                    if (selectedItem.Group == listView.Groups["Folders"])
                    {
                        if (Directory.Exists(path))
                        {
                            ZipFile.CreateFromDirectory(path, newPath);
                        }
                    }
                }

                _lastSelectNode.Nodes.Clear();
                _lastSelectNode.Nodes.AddRange(Util.GetTreeNodeDirectories(Util.GetFullPathForSelectedNode(_lastSelectNode)).ToArray());
                setListView(_lastSelectNode);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }
    }
}
