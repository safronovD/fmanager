namespace FileManager
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Folders", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Files", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Drives", System.Windows.Forms.HorizontalAlignment.Left);
            this.treeView = new System.Windows.Forms.TreeView();
            this.smallIconsImageList = new System.Windows.Forms.ImageList(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconsImageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.groupsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.smallIconsImageList;
            this.treeView.Location = new System.Drawing.Point(160, 28);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(142, 499);
            this.treeView.TabIndex = 0;
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // smallIconsImageList
            // 
            this.smallIconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallIconsImageList.ImageStream")));
            this.smallIconsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallIconsImageList.Images.SetKeyName(0, "Computer.png");
            this.smallIconsImageList.Images.SetKeyName(1, "File.png");
            this.smallIconsImageList.Images.SetKeyName(2, "Folder.png");
            this.smallIconsImageList.Images.SetKeyName(3, "HardDrive.png");
            // 
            // listView
            // 
            this.listView.ContextMenuStrip = this.contextMenuStrip1;
            listViewGroup1.Header = "Folders";
            listViewGroup1.Name = "Folders";
            listViewGroup2.Header = "Files";
            listViewGroup2.Name = "Files";
            listViewGroup3.Header = "Drives";
            listViewGroup3.Name = "Drives";
            this.listView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.largeIconsImageList;
            this.listView.Location = new System.Drawing.Point(308, 28);
            this.listView.Name = "FoldersListView";
            this.listView.Size = new System.Drawing.Size(669, 499);
            this.listView.SmallImageList = this.smallIconsImageList;
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.createFolderToolStripMenuItem,
            this.deleteFolderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(141, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.copyToolStripMenuItem.Text = "copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // createFolderToolStripMenuItem
            // 
            this.createFolderToolStripMenuItem.Name = "createFolderToolStripMenuItem";
            this.createFolderToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.createFolderToolStripMenuItem.Text = "create folder";
            this.createFolderToolStripMenuItem.Click += new System.EventHandler(this.createFolderToolStripMenuItem_Click);
            // 
            // deleteFolderToolStripMenuItem
            // 
            this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
            this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.deleteFolderToolStripMenuItem.Text = "delete folder";
            this.deleteFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteFolderToolStripMenuItem_Click);
            // 
            // largeIconsImageList
            // 
            this.largeIconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeIconsImageList.ImageStream")));
            this.largeIconsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeIconsImageList.Images.SetKeyName(0, "Computer.png");
            this.largeIconsImageList.Images.SetKeyName(1, "File.png");
            this.largeIconsImageList.Images.SetKeyName(2, "Folder.png");
            this.largeIconsImageList.Images.SetKeyName(3, "HardDrive.png");
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripComboBox,
            this.groupsToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(992, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton1.Text = "Back";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripComboBox
            // 
            this.toolStripComboBox.Name = "toolStripComboBox";
            this.toolStripComboBox.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_SelectedIndexChanged);
            // 
            // groupsToolStripButton
            // 
            this.groupsToolStripButton.Checked = true;
            this.groupsToolStripButton.CheckOnClick = true;
            this.groupsToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.groupsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.groupsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("groupsToolStripButton.Image")));
            this.groupsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.groupsToolStripButton.Name = "groupsToolStripButton";
            this.groupsToolStripButton.Size = new System.Drawing.Size(48, 22);
            this.groupsToolStripButton.Text = "groups";
            this.groupsToolStripButton.Click += new System.EventHandler(this.groupsToolStripButton_Click);
            // 
            // listView1
            // 
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.largeIconsImageList;
            this.listView1.Location = new System.Drawing.Point(12, 28);
            this.listView1.Name = "ProfilesListView";
            this.listView1.Size = new System.Drawing.Size(142, 499);
            this.listView1.SmallImageList = this.smallIconsImageList;
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 539);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "FileManager";
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList smallIconsImageList;
        private System.Windows.Forms.ImageList largeIconsImageList;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox;
        private System.Windows.Forms.ToolStripButton groupsToolStripButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.ListView listView1;
    }
}

