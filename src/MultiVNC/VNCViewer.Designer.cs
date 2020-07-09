using System.Windows.Forms;
namespace MultiVNC
{
	public partial class VncViewer : Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VncViewer));
			this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reloadHostsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.hosts = new System.Windows.Forms.TreeView();
			this.infoBox = new System.Windows.Forms.GroupBox();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.portTextBox = new System.Windows.Forms.TextBox();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.editButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.richNotebox = new System.Windows.Forms.RichTextBox();
			this.connectButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.inheritCheckbox = new System.Windows.Forms.CheckBox();
			this.readOnlyCheckbox = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.thumbNailsAreReadOnly = new System.Windows.Forms.CheckBox();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.infoBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// printPreviewDialog1
			// 
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.Visible = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1066, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.reloadHostsFileToolStripMenuItem,
            this.toolStripSeparator2,
            this.settingsToolStripMenuItem,
            this.resetToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupToolStripMenuItem,
            this.hostToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
			this.toolStripMenuItem1.Text = "Add";
			// 
			// groupToolStripMenuItem
			// 
			this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
			this.groupToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.groupToolStripMenuItem.Text = "Group";
			this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
			// 
			// hostToolStripMenuItem
			// 
			this.hostToolStripMenuItem.Name = "hostToolStripMenuItem";
			this.hostToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.hostToolStripMenuItem.Text = "Host";
			this.hostToolStripMenuItem.Click += new System.EventHandler(this.hostToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.saveToolStripMenuItem.Text = "Save Hosts File";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click_1);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.saveAsToolStripMenuItem.Text = "Save Hosts File As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click_1);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.loadToolStripMenuItem.Text = "Load Hosts File...";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
			// 
			// reloadHostsFileToolStripMenuItem
			// 
			this.reloadHostsFileToolStripMenuItem.Name = "reloadHostsFileToolStripMenuItem";
			this.reloadHostsFileToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.reloadHostsFileToolStripMenuItem.Text = "Reload Hosts File";
			this.reloadHostsFileToolStripMenuItem.Click += new System.EventHandler(this.reloadHostsFileToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(174, 6);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.settingsToolStripMenuItem.Text = "Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.resetToolStripMenuItem.Text = "Reset Vnc Pass";
			this.resetToolStripMenuItem.Visible = false;
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Size = new System.Drawing.Size(1066, 717);
			this.splitContainer1.SplitterDistance = 201;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 4;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.hosts);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.infoBox);
			this.splitContainer2.Panel2MinSize = 27;
			this.splitContainer2.Size = new System.Drawing.Size(191, 707);
			this.splitContainer2.SplitterDistance = 483;
			this.splitContainer2.TabIndex = 0;
			// 
			// hosts
			// 
			this.hosts.AllowDrop = true;
			this.hosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hosts.Location = new System.Drawing.Point(3, 3);
			this.hosts.Name = "hosts";
			this.hosts.Size = new System.Drawing.Size(181, 473);
			this.hosts.TabIndex = 0;
			this.hosts.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.hosts_ItemDrag);
			this.hosts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.hosts_AfterSelect);
			this.hosts.DragDrop += new System.Windows.Forms.DragEventHandler(this.hosts_DragDrop);
			this.hosts.DragEnter += new System.Windows.Forms.DragEventHandler(this.hosts_DragEnter);
			this.hosts.DragOver += new System.Windows.Forms.DragEventHandler(this.hosts_DragOver);
			this.hosts.DoubleClick += new System.EventHandler(this.hosts_DoubleClick);
			this.hosts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hosts_KeyDown);
			this.hosts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hosts_MouseUp);
			// 
			// infoBox
			// 
			this.infoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.infoBox.Controls.Add(this.passwordTextBox);
			this.infoBox.Controls.Add(this.label2);
			this.infoBox.Controls.Add(this.portTextBox);
			this.infoBox.Controls.Add(this.nameTextBox);
			this.infoBox.Controls.Add(this.editButton);
			this.infoBox.Controls.Add(this.saveButton);
			this.infoBox.Controls.Add(this.richNotebox);
			this.infoBox.Controls.Add(this.connectButton);
			this.infoBox.Controls.Add(this.label5);
			this.infoBox.Controls.Add(this.inheritCheckbox);
			this.infoBox.Controls.Add(this.readOnlyCheckbox);
			this.infoBox.Controls.Add(this.label3);
			this.infoBox.Controls.Add(this.Label1);
			this.infoBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.infoBox.Location = new System.Drawing.Point(3, 6);
			this.infoBox.Name = "infoBox";
			this.infoBox.Size = new System.Drawing.Size(184, 207);
			this.infoBox.TabIndex = 1;
			this.infoBox.TabStop = false;
			this.infoBox.Text = "Info";
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.passwordTextBox.Enabled = false;
			this.passwordTextBox.Location = new System.Drawing.Point(0, 75);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(178, 20);
			this.passwordTextBox.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(-3, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Password";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// portTextBox
			// 
			this.portTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.portTextBox.Enabled = false;
			this.portTextBox.Location = new System.Drawing.Point(131, 32);
			this.portTextBox.Name = "portTextBox";
			this.portTextBox.Size = new System.Drawing.Size(47, 20);
			this.portTextBox.TabIndex = 13;
			this.portTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// nameTextBox
			// 
			this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTextBox.Enabled = false;
			this.nameTextBox.Location = new System.Drawing.Point(0, 32);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(131, 20);
			this.nameTextBox.TabIndex = 12;
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.editButton.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.editButton.Location = new System.Drawing.Point(6, 183);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(17, 19);
			this.editButton.TabIndex = 11;
			this.editButton.Text = "!";
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.saveButton.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.saveButton.Location = new System.Drawing.Point(28, 183);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(17, 19);
			this.saveButton.TabIndex = 10;
			this.saveButton.Text = "<";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// richNotebox
			// 
			this.richNotebox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richNotebox.Enabled = false;
			this.richNotebox.Location = new System.Drawing.Point(0, 124);
			this.richNotebox.Name = "richNotebox";
			this.richNotebox.Size = new System.Drawing.Size(181, 54);
			this.richNotebox.TabIndex = 7;
			this.richNotebox.Text = "";
			this.richNotebox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richNotebox_KeyPress);
			// 
			// connectButton
			// 
			this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.connectButton.Enabled = false;
			this.connectButton.Location = new System.Drawing.Point(103, 180);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(75, 23);
			this.connectButton.TabIndex = 8;
			this.connectButton.Text = "Connect";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(-3, 105);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Note";
			// 
			// inheritCheckbox
			// 
			this.inheritCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.inheritCheckbox.AutoSize = true;
			this.inheritCheckbox.Enabled = false;
			this.inheritCheckbox.Location = new System.Drawing.Point(123, 101);
			this.inheritCheckbox.Name = "inheritCheckbox";
			this.inheritCheckbox.Size = new System.Drawing.Size(55, 17);
			this.inheritCheckbox.TabIndex = 5;
			this.inheritCheckbox.Text = "Inherit";
			this.inheritCheckbox.UseVisualStyleBackColor = true;
			this.inheritCheckbox.CheckStateChanged += new System.EventHandler(this.inheritCheckbox_CheckStateChanged);
			// 
			// readOnlyCheckbox
			// 
			this.readOnlyCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.readOnlyCheckbox.AutoSize = true;
			this.readOnlyCheckbox.Enabled = false;
			this.readOnlyCheckbox.Location = new System.Drawing.Point(41, 101);
			this.readOnlyCheckbox.Name = "readOnlyCheckbox";
			this.readOnlyCheckbox.Size = new System.Drawing.Size(76, 17);
			this.readOnlyCheckbox.TabIndex = 4;
			this.readOnlyCheckbox.Text = "Read Only";
			this.readOnlyCheckbox.UseVisualStyleBackColor = true;
			this.readOnlyCheckbox.CheckStateChanged += new System.EventHandler(this.readOnlyCheckbox_CheckStateChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(155, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Port";
			// 
			// Label1
			// 
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(-1, 16);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(35, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Name";
			// 
			// thumbNailsAreReadOnly
			// 
			this.thumbNailsAreReadOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.thumbNailsAreReadOnly.AutoSize = true;
			this.thumbNailsAreReadOnly.Checked = true;
			this.thumbNailsAreReadOnly.CheckState = System.Windows.Forms.CheckState.Checked;
			this.thumbNailsAreReadOnly.Location = new System.Drawing.Point(933, 7);
			this.thumbNailsAreReadOnly.Name = "thumbNailsAreReadOnly";
			this.thumbNailsAreReadOnly.Size = new System.Drawing.Size(133, 17);
			this.thumbNailsAreReadOnly.TabIndex = 5;
			this.thumbNailsAreReadOnly.Text = "Read Only Thumbnails";
			this.thumbNailsAreReadOnly.UseVisualStyleBackColor = true;
			this.thumbNailsAreReadOnly.CheckedChanged += new System.EventHandler(this.thumbNailsAreReadOnly_CheckedChanged);
			// 
			// VncViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1066, 741);
			this.Controls.Add(this.thumbNailsAreReadOnly);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "VncViewer";
			this.Text = "MultiVNC";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VNCViewer_FormClosing);
			this.ResizeEnd += new System.EventHandler(this.VncViewer_ResizeEnd);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.infoBox.ResumeLayout(false);
			this.infoBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView hosts;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem groupToolStripMenuItem;
		private ToolStripMenuItem hostToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem saveToolStripMenuItem;
		private ToolStripMenuItem saveAsToolStripMenuItem;
		private ToolStripMenuItem loadToolStripMenuItem;
		private ToolStripMenuItem settingsToolStripMenuItem;
		private CheckBox thumbNailsAreReadOnly;
		private ToolStripSeparator toolStripSeparator2;
		private GroupBox infoBox;
		private RichTextBox richNotebox;
		private Label label5;
		private CheckBox readOnlyCheckbox;
		private Label label3;
		private Label Label1;
		private SplitContainer splitContainer2;
		private Button connectButton;
		private CheckBox inheritCheckbox;
		private Button saveButton;
		private Button editButton;
		private TextBox portTextBox;
		private TextBox nameTextBox;
		private TextBox passwordTextBox;
		private Label label2;
		private ToolStripMenuItem resetToolStripMenuItem;
		private ToolStripMenuItem reloadHostsFileToolStripMenuItem;
	}
}