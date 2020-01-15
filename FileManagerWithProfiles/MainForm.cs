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

        public MainForm()
        {
            InitializeComponent();

            foreach (string view in Enum.GetNames(typeof(View)))
            {
                toolStripComboBox.Items.Add(view);
            }

            toolStripComboBox.SelectedIndex = 0;
            
            List<ListViewItem> newListViewItemList = new List<ListViewItem>(2);

            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Name = "My Computer";
            listViewItem.Text = "My Computer";
            newListViewItemList.Add(listViewItem);

            listViewItem = new ListViewItem();
            listViewItem.Name = "Real Folder";
            listViewItem.Text = "Real Folder";
            newListViewItemList.Add(listViewItem);

            listView1.Items.Clear();
            listView1.Items.AddRange(newListViewItemList.ToArray());

            listView1.View = View.List;
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
            if(view != View.Details)
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
            if (listView1.SelectedItems[0].Name == "Real Folder")
            {
                initTopNodeWithPath(@"F:\music");
            }
        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
        }
    }
}
