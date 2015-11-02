﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADB.net;
using System.IO;

namespace ADB_Helper
{
    public partial class frmFileExplorer : Form
    {
        public frmFileExplorer()
        {
            InitializeComponent();
        }

        private void UpdateFileTree()
        {
            List<string> entries = new List<string>();
            entries.AddRange(FileSystem.GetAllEntries("/"));

            tvFileTree.Nodes.Clear();

            progressBar.Maximum = entries.Count;

            foreach(string entry in entries)
            {
                TreeNode node = tvFileTree.Nodes.Add(entry, entry);
                if (FileSystem.IsDirectory(entry))
                    node.Nodes.Add("Loading...");
                progressBar.Value++;
            }

            progressBar.Value = 0;
        }

        private void ExpandNode(TreeNode nodeE)
        {
            string fullpath = "/" + nodeE.FullPath.Replace("\\", "/");

            List<string> entries = new List<string>();
            entries.AddRange(FileSystem.GetAllEntries(fullpath));

            int m = entries.Count;

            nodeE.Nodes.Clear();

            progressBar.Maximum = entries.Count;

            foreach (string item in entries)
            {
                TreeNode n = new TreeNode(item);
                if (FileSystem.IsDirectory(fullpath + "/" + item))
                    n.Nodes.Add("Loading...");
                nodeE.Nodes.Add(n);
                progressBar.Value++;
            }
            progressBar.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateFileTree();
        }

        private void tvFileTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ExpandNode(e.Node);
        }

        private void btnPull_Click(object sender, EventArgs e)
        {
            if (tvFileTree.SelectedNode != null || tvFileTree.SelectedNode.Nodes.Count > 0)
            {
                string fullpath = "/" + tvFileTree.SelectedNode.FullPath.Replace("\\", "/");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileSystem.PullFile(fullpath, Path.GetDirectoryName(saveFileDialog.FileName));
                }
            }
        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            if (tvFileTree.SelectedNode != null)
            {
                string fullpath;
                if (tvFileTree.SelectedNode.Nodes.Count == 0)
                    fullpath = "/" + Path.GetDirectoryName(tvFileTree.SelectedNode.FullPath).Replace("\\", "/");
                else
                    fullpath = "/" + tvFileTree.SelectedNode.FullPath.Replace("\\", "/");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileSystem.PushFile(openFileDialog.FileName.Substring(2), fullpath);
                }
            }
        }
    }
}
