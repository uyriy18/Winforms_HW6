using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        DirectoryInfo fileList;
        List<string> fullPath = new List<string>();
        string tmpPath;
        public Form1()
        {
            InitializeComponent();
            lstv_Directories.View = View.LargeIcon;
            treeView1.BeforeSelect += treeView_BeforeSelect;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            FillDriveNodes();                                        //Show all disks
        }

        void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;

            try
            {
                if(Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if(dirs.Length != 0)
                    {
                        for(int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch { }
        }

        void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if(dirs.Length != 0)
                    {
                        for(int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch { }
        }

        void FillDriveNodes()
        {
            try
            {
                foreach(DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode driveNode = new TreeNode { Text = drive.Name };
                    FillTreeNode(driveNode, drive.Name);
                    treeView1.Nodes.Add(driveNode);
                    driveNode.ImageIndex = 1;
                    driveNode.SelectedImageIndex = 1;
                }
            }
            catch { }
        }

        void FillTreeNode(TreeNode driveNode, string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            try
            {
                foreach (var item in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = item.Remove(0, item.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                    driveNode.ImageIndex = 0;
                    driveNode.SelectedImageIndex = 0;
                }
            }
            catch { }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string tmpFilesName = "";
                string tmpDirNames = "";
                string ext = "";

                lstv_Directories.Clear();
                fileList = new DirectoryInfo(e.Node.FullPath);
                tmpPath = e.Node.FullPath;                                                // copy disc name

                String[] directories = Directory.GetDirectories(e.Node.FullPath);
                String[] files = Directory.GetFiles(e.Node.FullPath);
                fullPath.Clear();
                fullPath.Add(e.Node.FullPath);
                foreach (var item in directories)
                {
                    tmpFilesName = item.Substring(item.LastIndexOf("\\") + 1);
                    lstv_Directories.Items.Add(tmpFilesName, 0);
                }
                foreach (var item in files)
                {
                    tmpDirNames = item.Substring(item.LastIndexOf("\\") + 1);
                    ext = item.Substring(item.LastIndexOf(@"."));
                    add_Extension(ext, tmpDirNames);
                   
                }
                fill_txbx_FullPath();
                fullPath.Add(txbx_Path.Text.Substring(txbx_Path.Text.LastIndexOf(@"\")));
                fill_Table();
            }
            catch { }
    
        }

        private void lstv_Directories_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) // Show full (only) directory name in text box
        {
            try
            {

                if (e.Item.ImageIndex == 0)
                {
                    txbx_Path.Text = (tmpPath + @"\" + e.Item.Text);                  //show in tetbox the disk name and selected directory;                   
                }
                else if(e.Item.ImageIndex == 4)
                {
                    txbx_Path.Text = (tmpPath + @"\" + e.Item.Text);
                    show_Picture(e.Item.Text);
                }
            }
            catch { }

        }

        void fill_txbx_FullPath()                  // add directory full name in the text box from fullPath<Strilng>
        {
            txbx_Path.Clear();
            foreach(var item in fullPath)
            {
                txbx_Path.Text += item;
            }
        } 

        private void btn_Back_Click(object sender, EventArgs e)     //back in previous directory
        {
            if (fullPath.Count > 1)
            {
                lstv_Directories.Clear();
                fullPath.RemoveAt((fullPath.Count - 1));
                fill_txbx_FullPath();
                perform_Action();
            }
            else return;
                
        }

        void perform_Action()                                 // show new directories
        {
            pictureBox1.Visible = false;
            try
            {
                String[] directories = Directory.GetDirectories(txbx_Path.Text);
                String[] files = Directory.GetFiles(txbx_Path.Text);
                foreach (var item in directories)
                {
                    string tmp = item.Substring(item.LastIndexOf("\\") + 1);
                    lstv_Directories.Items.Add(tmp, 0);
                }
                foreach (var item in files)
                {
                    string tmp = item.Substring(item.LastIndexOf("\\") + 1);
                    string ext = item.Substring(item.LastIndexOf(@"."));        //put file extension in variable
                    add_Extension(ext, tmp);                                    //define images for files
                    tmpPath = txbx_Path.Text;
                }
                if (lstv_Directories.View == View.Details)                      //espesialy for list view
                {
                    fill_Table();                                               //invoke Method to fill table
                }
            }
            catch { }
            
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)  //large icons
        {
            lstv_Directories.View = View.LargeIcon;
        }

        private void smallIoconToolStripMenuItem_Click(object sender, EventArgs e)   // small icons
        {
            lstv_Directories.View = View.SmallIcon;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)   // list
        {
            lstv_Directories.View = View.List;
        }

        private void titleToolStripMenuItem_Click(object sender, EventArgs e)  //title
        {
            lstv_Directories.View = View.Tile;
        }

        private void dtailToolStripMenuItem_Click(object sender, EventArgs e)             // ListView Style - details
        {

            lstv_Directories.View = View.Details;
            lstv_Directories.Columns.Clear();        
            fill_Table();                                                                 // Invoke Method to fill table 
        }

        void fill_Table()                                                                 // Method performs table filling
        {
            lstv_Directories.Columns.Add("File name");
            lstv_Directories.Columns.Add("Description");
            lstv_Directories.Columns[0].Width = 150;
            lstv_Directories.Columns[1].Width = 150;
            foreach (ListViewItem i in lstv_Directories.Items)
            {
                if (i.ImageIndex == 2)
                {
                    try
                    {
                        FileInfo file = new FileInfo(txbx_Path.Text + i.Text);                 //file size counting
                        i.SubItems.Add(file.Length.ToString() + " Bytes");
                    }
                    catch { }
                }
                else if (i.ImageIndex == 0)
                {
                    i.SubItems.Add("Directory");
                }
                else continue;
            }
        }

        private double  GetDirectorySize(string path)                                                // Count directory size
        {
            try
            {
                double size = 0;
                string[] files = Directory.GetFiles(path);
                foreach (var item in files)
                {
                    size += (new FileInfo(item)).Length;
                }
                string[] dirs = Directory.GetDirectories(path);
                foreach (var item in files)
                {
                    size += GetDirectorySize(item);
                }
                return size;
            }
            catch
            {
                return 0;
            }
        }

        private void lstv_Directories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lstv_Directories.Clear();
            string dirName = "";
            dirName = txbx_Path.Text.Substring(txbx_Path.Text.LastIndexOf(@"\") + 1);   // geting directory name
            fullPath.Add(dirName);                                                      // Adding directory name from text box
            perform_Action();                                                           // open directory
            
        }

        void add_Extension(string ext, string tmp)                                      // Adding spesial icon to file
        {
            switch (ext)
            {
                case ".jpg":
                    lstv_Directories.Items.Add(tmp, 4);
                    break;
                case ".cms":
                    lstv_Directories.Items.Add(tmp, 4);
                    break;
                case ".bmp":
                    lstv_Directories.Items.Add(tmp, 4);
                    break;
                default:
                    lstv_Directories.Items.Add(tmp, 2);
                    break;
            }
        }
        void show_Picture(string name)                                               // add picture yo picture box 
        {          
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Visible = true;         
            pictureBox1.Image = Image.FromFile(txbx_Path.Text);                    
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in lstv_Directories.Items)
            {
                item.Selected = true;
            }
        }

        private void showSIzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtailToolStripMenuItem_Click(sender, e);
            foreach (ListViewItem item in lstv_Directories.Items)
            {
                item.SubItems.Add(GetDirectorySize(item.Name).ToString());
            }
        }
    }
}
