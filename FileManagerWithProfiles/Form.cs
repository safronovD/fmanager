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

namespace FileManager
{
    enum TypeItem
    {
        drive,
        folder,
        file
    }

    public partial class Form : System.Windows.Forms.Form
    {
        private TreeNode _lastSelectNode;
        public Form()
        {
            InitializeComponent();
            foreach (string view in Enum.GetNames(typeof(View)))
                toolStripComboBox.Items.Add(view);

            toolStripComboBox.SelectedIndex = 0;

            TreeNode topNode = new TreeNode("That computer", 0, 0, GetTreeNodeDrives().ToArray());
            topNode.ImageIndex = 0;
            topNode.Name = "Computer";
            topNode.SelectedImageIndex = 0;
            topNode.Text = "My computer";

            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {topNode});
            treeView.TopNode = topNode;
            _lastSelectNode = treeView.TopNode;
            _lastSelectNode.Expand();
       }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetTreeViewExpandList(e.Node);
        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            node.Nodes.Clear();
            List<TreeNode> emptyTreeNode = new List<TreeNode>(1);
            emptyTreeNode.Add(new TreeNode("Node"));
            node.Nodes.AddRange(emptyTreeNode.ToArray());
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
                    string path = GetFullPathForSelectedNode(_lastSelectNode) + listView.SelectedItems[0].Name;
                    if (listView.SelectedItems[0].Group == listView.Groups["Files"])
                    {
                        if (File.Exists(path))
                        {
                            Process.Start(GetFullPathForSelectedNode(_lastSelectNode) + listView.SelectedItems[0].Name);
                        }
                    }
                    else if (listView.SelectedItems[0].Group == listView.Groups["Folders"])
                    {
                        foreach (TreeNode node in _lastSelectNode.Nodes)
                        {
                            TreeNode[] treeNode = _lastSelectNode.Nodes.Find(listView.SelectedItems[0].Name, false);
                            if (node.Text == listView.SelectedItems[0].Name)
                            {
                                node.Expand();
                                treeView.SelectedNode = node;
                                break;
                            }
                            else if (node.Text == "Node")
                            {
                                _lastSelectNode.Expand();
                                listView_DoubleClick(sender, e);
                                break;
                            }

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
        private void SetListView(TreeNode _node)
        {
            listView.Items.Clear();
            var node = _node;
            if (treeView.TopNode == node)
            {
                listView.Items.AddRange(GetListViewDrives().ToArray());
            }
            else
            {
                listView.Items.AddRange(GetListViewDirectories(node).ToArray());
                listView.Items.AddRange(GetListViewFiles(node).ToArray());
            }
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
        private void SetTreeViewExpandList(TreeNode node)
        {
            if (node.Nodes.Count == 1)
            {
                node.Nodes.Clear();
            }
            else
            {
                node.Nodes.AddRange(GetTreeNodeDirectories(node.Text).ToArray());
            }
        }

        private List<TreeNode> GetTreeNodeDirectories(string fullPath)
        {   
            List<TreeNode> newTreeNodeList = new List<TreeNode>(GetDirectories(fullPath).Count);
            foreach (var directory in GetDirectories(fullPath))
            {
                TreeNode newTreeNode = new TreeNode(Path.GetFileName(directory), 2, 2)
                {
                    Name = Path.GetFileName(directory),
                    Tag = fullPath
                };
               // newTreeNode.Nodes.AddRange(GetTreeNodeDirectories(fullPath + @"\" + newTreeNode.Name).ToArray());
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
            StringBuilder fullPath = new StringBuilder(node.Text);
            TreeNode prevNode = node.Parent;
            if (prevNode.Text != null)
            {
                while (prevNode != treeView.TopNode)
                {
                    fullPath = new StringBuilder(prevNode.Text + @"\" + fullPath);
                    prevNode = prevNode.Parent;
                }
            } else
            {
                MessageBox.Show("Cannot create new folder here.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fullPath.ToString();
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
            string path = GetFullPathForSelectedNode(_lastSelectNode) + "\\NewFolder";

            try
            {
                if (Directory.Exists(path))
                {
                    MessageBox.Show("New Folder exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                DirectoryInfo di = Directory.CreateDirectory(path);
                _lastSelectNode.Collapse();
                _lastSelectNode.Expand();
                SetListView(_lastSelectNode);
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
    }
}
