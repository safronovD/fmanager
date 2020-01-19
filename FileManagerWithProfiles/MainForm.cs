using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml;

namespace FileManagerWithProfiles
{
    enum TypeItem
    {
        drive,
        folder,
        file
    }

    public partial class MainForm : System.Windows.Forms.Form
    {
        private TreeNode _lastSelectNode;
        private XmlNode _userNode;
        private XmlDocument _xDoc;

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

            initTopNodeWithPath(@"F:\music");

            listView1.View = View.Tile;

            addProfiles(listView1);
            listView1.ShowGroups = true;
        }

        private void initTopNodeWithPath(string path)
        {
            treeView.Nodes.Clear();
            TreeNode topNode = new TreeNode("1", 0, 0, GetTreeNodeDirectories(path).ToArray())
            {
                ImageIndex = 2,
                SelectedImageIndex = 2,
                Name = "Root",
                Text = path,
                Tag = path
            };
            treeView.Nodes.AddRange(new TreeNode[] { topNode });
            treeView.TopNode = topNode;
            _lastSelectNode = treeView.TopNode;
            _lastSelectNode.Expand();
        }

        private TreeView getRealTreeView(string Path)
        {
            return null;
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {

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
            SetListView(_lastSelectNode);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _lastSelectNode = e.Node;
            SetListView(_lastSelectNode);
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count == 1)
                {
                    string path = GetFullPathForSelectedNode(_lastSelectNode) + @"\" + listView.SelectedItems[0].Name;
                    if (listView.SelectedItems[0].Group == listView.Groups["Files"])
                    {
                        if (File.Exists(path))
                        {
                            Process.Start(path);
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
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }


        #region ListView
        private void SetListView(TreeNode node)
        {
            listView.Items.Clear();
            listView.Items.AddRange(GetListViewDirectories(node).ToArray());
            listView.Items.AddRange(GetListViewFiles(node).ToArray());
        }

        private List<ListViewItem> GetListViewFiles(TreeNode node)
        {
            var files = GetFiles(GetFullPathForSelectedNode(node));

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
            var directories = GetDirectories(GetFullPathForSelectedNode(node));

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
            List<ListViewItem> newListViewItemList = new List<ListViewItem>(GetLogicalDrives().Count);
            foreach (var drive in GetLogicalDrives())
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


        #region TreeView

        private List<TreeNode> GetTreeNodeDirectories(string fullPath)
        {
            List<TreeNode> newTreeNodeList = new List<TreeNode>(GetDirectories(fullPath).Count);
            foreach (var directory in GetDirectories(fullPath))
            {
                TreeNode newTreeNode = new TreeNode(Path.GetFileName(directory), 2, 2)
                {
                    Name = Path.GetFileName(directory),
                    Tag = directory
                };
                newTreeNode.Nodes.AddRange(GetTreeNodeDirectories(directory).ToArray());
                newTreeNodeList.Add(newTreeNode);
            }
            return newTreeNodeList;
        }

        private List<TreeNode> GetTreeNodeDirectoriesAndFiles(string fullPath)
        {
            List<TreeNode> newTreeNodeList = new List<TreeNode>(GetDirectories(fullPath).Count);
            foreach (var directory in GetDirectories(fullPath))
            {
                TreeNode newTreeNode = new TreeNode(Path.GetFileName(directory), 2, 2)
                {
                    Name = Path.GetFileName(directory),
                    Tag = directory
                };
                newTreeNode.Nodes.AddRange(GetTreeNodeDirectoriesAndFiles(directory).ToArray());
                newTreeNodeList.Add(newTreeNode);
            }
            foreach (var file in GetFiles(fullPath))
            {
                TreeNode newTreeNode = new TreeNode(Path.GetFileName(file), 1, 1)
                {
                    Name = Path.GetFileName(file),
                    Tag = file
                };
                newTreeNodeList.Add(newTreeNode);
            }
            return newTreeNodeList;
        }

        private List<TreeNode> GetTreeNodeDrives()
        {
            List<TreeNode> newTreeNodeList = new List<TreeNode>(GetLogicalDrives().Count);
            foreach (var drive in GetLogicalDrives())
            {
                TreeNode newTreeNode = new TreeNode(drive, 3, 3);
                newTreeNode.Nodes.AddRange(GetTreeNodeDirectories(newTreeNode.Text).ToArray());
                newTreeNode.Name = drive;
                newTreeNodeList.Add(newTreeNode);
            }
            return newTreeNodeList;
        }

        private String GetFullPathForSelectedNode(TreeNode node)
        {
            return node.Tag.ToString();
        }
        #endregion


        #region AdditionalFuntionsForListViewAndTreeView
        private List<String> GetLogicalDrives()
        {
            List<String> drivesList = new List<string>();
            drivesList.AddRange(System.IO.Directory.GetLogicalDrives());
            return drivesList;
        }

        private List<String> GetDirectories(String fullPath)
        {
            List<String> directories = new List<string>();
            try
            {
                directories.AddRange(Directory.GetDirectories(fullPath.ToString()));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return directories;
        }

        private List<String> GetFiles(String fullPath)
        {
            List<String> files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(fullPath));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
            files.Sort((x, y) => String.Compare(x, y, StringComparison.Ordinal));
            return files;
        }

        #endregion

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItems = listView.SelectedItems;
            foreach (TreeNode node in _lastSelectNode.Nodes)
            {
                if (node.Text == selectedItems[0].Text)
                {
                    string s = GetFullPathForSelectedNode(node);
                }
            }

        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetFullPathForSelectedNode(_lastSelectNode);

            try
            {
                var i = 0;
                while (true)
                {
                    string folderPath = path + @"/" + "NewFolder" + i.ToString();
                    if (!Directory.Exists(folderPath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(folderPath);
                        _lastSelectNode.Collapse();
                        _lastSelectNode.Expand();
                        SetListView(_lastSelectNode);
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
                    string path = GetFullPathForSelectedNode(_lastSelectNode) + "\\" + selectedItem.Text;
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path);
                    }
                    _lastSelectNode.Collapse();
                    _lastSelectNode.Expand();
                    SetListView(_lastSelectNode);
                }
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

        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
            Util.initXMLComponents(ref _xDoc, ref _userNode);
            changeColors(_userNode["fontColor"].InnerText, _userNode["backColor"].InnerText);
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
                int i = 0;
                while (true)
                {
                    string filePath = path + @"/" + i.ToString() + ".xml";
                    if (!File.Exists(filePath))
                    {
                        saveTreeNode(treeView.TopNode, filePath);

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
                        _userNode.AppendChild(profileElem);
                        _xDoc.Save(Properties.Settings.Default.xmlPath);

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

        private void addProfiles(ListView listView)
        {
            List<ListViewItem> newListViewItemList = new List<ListViewItem>(2);

            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Name = "Computer";
            listViewItem.Text = "Computer";
            listViewItem.Group = listView1.Groups["Real"];
            newListViewItemList.Add(listViewItem);

            listViewItem = new ListViewItem();
            listViewItem.Name = "Folder";
            listViewItem.Text = "Folder";
            listViewItem.Group = listView1.Groups["Real"];
            newListViewItemList.Add(listViewItem);

            foreach (XmlNode node in _userNode.ChildNodes)
            {
                if (node.Name == "profile")
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
    }
}
