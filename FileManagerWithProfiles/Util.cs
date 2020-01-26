using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace FileManagerWithProfiles
{
    static class Util
    {
        static public void initXMLComponents(ref XmlDocument xDoc, ref XmlNode userNode)
        {
            xDoc = new XmlDocument();
            xDoc.Load(Properties.Settings.Default.xmlPath);

            XmlElement xRoot = xDoc.DocumentElement;

            List<XmlNode> list = xRoot.ChildNodes.Cast<XmlNode>()
                           .Where(user => user["login"].InnerText.Equals(Properties.Settings.Default.userName))
                           .ToList();

            if (list.Count != 1)
            {
                throw new ArgumentException("Users.xml is corrupted!", "XmlDocument");
            }

            userNode = list[0];
        }

        static public TreeNode createNodeForView(string path)
        {
            return new TreeNode("1", 0, 0, GetTreeNodeDirectories(path).ToArray())
            {
                ImageIndex = 2,
                SelectedImageIndex = 2,
                Name = "Root",
                Text = path,
                Tag = path,
                ToolTipText = "Folder"
            };
        }

        static public TreeNode createNodeForSave(string path)
        {
            return new TreeNode("1", 0, 0, GetTreeNodeDirectoriesAndFiles(path).ToArray())
            {
                ImageIndex = 2,
                SelectedImageIndex = 2,
                Name = "Root",
                Text = path,
                Tag = path,
                ToolTipText = "Folder"
            };
        }

        static public List<TreeNode> GetTreeNodeDirectories(string fullPath)
        {
            List<TreeNode> newTreeNodeList = new List<TreeNode>(GetDirectories(fullPath).Count);
            foreach (var directory in GetDirectories(fullPath))
            {
                TreeNode newTreeNode = new TreeNode(Path.GetFileName(directory), 2, 2)
                {
                    Name = Path.GetFileName(directory),
                    Tag = directory,
                    ToolTipText = "Folder"
                };
                newTreeNode.Nodes.AddRange(GetTreeNodeDirectories(directory).ToArray());
                newTreeNodeList.Add(newTreeNode);
            }
            return newTreeNodeList;
        }

        static public List<TreeNode> GetTreeNodeDirectoriesAndFiles(string fullPath)
        {
            List<TreeNode> newTreeNodeList = new List<TreeNode>(GetDirectories(fullPath).Count);
            foreach (var directory in GetDirectories(fullPath))
            {
                TreeNode newTreeNode = new TreeNode(Path.GetFileName(directory), 2, 2)
                {
                    Name = Path.GetFileName(directory),
                    Tag = directory,
                    ToolTipText = "Folder"
                };
                newTreeNode.Nodes.AddRange(GetTreeNodeDirectoriesAndFiles(directory).ToArray());
                newTreeNodeList.Add(newTreeNode);
            }
            foreach (var file in GetFiles(fullPath))
            {
                TreeNode newTreeNode = new TreeNode(Path.GetFileName(file), 1, 1)
                {
                    Name = Path.GetFileName(file),
                    Tag = file,
                    ToolTipText = "File"
                };
                newTreeNodeList.Add(newTreeNode);
            }
            return newTreeNodeList;
        }

        static public List<TreeNode> GetTreeNodeDrives()
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

        static public String GetFullPathForSelectedNode(TreeNode node)
        {
            return node.Tag.ToString();
        }

        static public List<String> GetLogicalDrives()
        {
            List<String> drivesList = new List<string>();
            drivesList.AddRange(Directory.GetLogicalDrives());
            return drivesList;
        }

        static public List<String> GetDirectories(String fullPath)
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

        static public List<String> GetFiles(String fullPath)
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

        public static TreeNode getFilteredNode(TreeNode node)
        {
            TreeNode viewNode = (TreeNode)node.Clone();
            viewNode.Nodes.Clear();
            viewNode.ImageIndex = 2;
            viewNode.SelectedImageIndex = 2;
            foreach (TreeNode inode in node.Nodes)
            {
                if (inode.ToolTipText.Equals("Folder"))
                {
                    inode.ImageIndex = 2;
                    inode.SelectedImageIndex = 2;
                    viewNode.Nodes.Add(getFilteredNode(inode));
                }
            }
            return viewNode;
        }

        public static TreeNode findInTreeNode(TreeNode treeNode, String fullPath)
        {
            var nodeLevels = fullPath.Split('\\').ToList();

            if (nodeLevels.Count == 2)
            {
                return treeNode;
            }

            if (nodeLevels.Count > 2)
            {
                TreeNode node = treeNode;
                for (int i = 2; i < nodeLevels.Count; i++)
                {
                    node = node.Nodes.Find(nodeLevels[i], false)[0];
                }
                return node;
            }

            return new TreeNode();
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }
            
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static void DirectoryDelete(string sourceDirName, bool delSubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Delete();
            }

            if (delSubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(sourceDirName, subdir.Name);
                    DirectoryDelete(temppath, delSubDirs);
                }
            }
        }

        public static void DirectoryMove(string sourceDirName, string destDirName, bool moveSubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.MoveTo(temppath);
            }

            if (moveSubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryMove(subdir.FullName, temppath, moveSubDirs);
                }
            }

            dir.Delete();
        }
    }
}
