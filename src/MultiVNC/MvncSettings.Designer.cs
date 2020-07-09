namespace MultiVNC
{
	partial class MvncSettings
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
			this.RemoteRunTabControl = new System.Windows.Forms.TabControl();
			this.general = new System.Windows.Forms.TabPage();
			this.VncGroupLayoutCheckBox = new System.Windows.Forms.CheckBox();
			this.VerboseLoggingCheckBox = new System.Windows.Forms.CheckBox();
			this.VpnCheckTestButton = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.VpnCheckHostNameTextBox = new System.Windows.Forms.TextBox();
			this.VpnCheckBox = new System.Windows.Forms.CheckBox();
			this.PsexecLocButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.PsexEcLocTextBox = new System.Windows.Forms.TextBox();
			this.askToLogout = new System.Windows.Forms.CheckBox();
			this.tools = new System.Windows.Forms.TabPage();
			this.label8 = new System.Windows.Forms.Label();
			this.Groups = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.createStartupShortcuts = new System.Windows.Forms.Button();
			this.logOut = new System.Windows.Forms.Button();
			this.lockMachine = new System.Windows.Forms.Button();
			this.RunBatchFile = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.CheckOutButton = new System.Windows.Forms.Button();
			this.CloneButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.warning = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Hosts = new System.Windows.Forms.ComboBox();
			this.relocate = new System.Windows.Forms.Button();
			this.unlock = new System.Windows.Forms.Button();
			this.clean = new System.Windows.Forms.Button();
			this.revert = new System.Windows.Forms.Button();
			this.update = new System.Windows.Forms.Button();
			this.hostsPinger = new System.Windows.Forms.TabPage();
			this.pingStatus = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.host = new System.Windows.Forms.DataGridViewLinkColumn();
			this.ping = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.refreshAll = new System.Windows.Forms.Button();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.toolTipsSettings = new System.Windows.Forms.ToolTip(this.components);
			this.resetGeneral = new System.Windows.Forms.Button();
			this.RemoteRunTabControl.SuspendLayout();
			this.general.SuspendLayout();
			this.tools.SuspendLayout();
			this.hostsPinger.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// RemoteRunTabControl
			// 
			this.RemoteRunTabControl.Controls.Add(this.general);
			this.RemoteRunTabControl.Controls.Add(this.tools);
			this.RemoteRunTabControl.Controls.Add(this.hostsPinger);
			this.RemoteRunTabControl.Location = new System.Drawing.Point(12, 12);
			this.RemoteRunTabControl.Name = "RemoteRunTabControl";
			this.RemoteRunTabControl.SelectedIndex = 0;
			this.RemoteRunTabControl.Size = new System.Drawing.Size(585, 378);
			this.RemoteRunTabControl.TabIndex = 0;
			this.RemoteRunTabControl.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
			// 
			// general
			// 
			this.general.Controls.Add(this.VncGroupLayoutCheckBox);
			this.general.Controls.Add(this.VerboseLoggingCheckBox);
			this.general.Controls.Add(this.VpnCheckTestButton);
			this.general.Controls.Add(this.label7);
			this.general.Controls.Add(this.VpnCheckHostNameTextBox);
			this.general.Controls.Add(this.VpnCheckBox);
			this.general.Controls.Add(this.PsexecLocButton);
			this.general.Controls.Add(this.label6);
			this.general.Controls.Add(this.PsexEcLocTextBox);
			this.general.Controls.Add(this.askToLogout);
			this.general.Location = new System.Drawing.Point(4, 22);
			this.general.Name = "general";
			this.general.Padding = new System.Windows.Forms.Padding(3);
			this.general.Size = new System.Drawing.Size(577, 352);
			this.general.TabIndex = 0;
			this.general.Text = "General";
			this.general.UseVisualStyleBackColor = true;
			// 
			// VncGroupLayoutCheckBox
			// 
			this.VncGroupLayoutCheckBox.AutoSize = true;
			this.VncGroupLayoutCheckBox.Location = new System.Drawing.Point(120, 329);
			this.VncGroupLayoutCheckBox.Name = "VncGroupLayoutCheckBox";
			this.VncGroupLayoutCheckBox.Size = new System.Drawing.Size(200, 17);
			this.VncGroupLayoutCheckBox.TabIndex = 9;
			this.VncGroupLayoutCheckBox.Text = "Vnc Group Layout (Requires Restart)";
			this.VncGroupLayoutCheckBox.UseVisualStyleBackColor = true;
			this.VncGroupLayoutCheckBox.CheckedChanged += new System.EventHandler(this.VncGroupLayoutCheckBox_CheckedChanged);
			this.VncGroupLayoutCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VncGroupLayoutCheckBox_MouseClick);
			// 
			// VerboseLoggingCheckBox
			// 
			this.VerboseLoggingCheckBox.AutoSize = true;
			this.VerboseLoggingCheckBox.Location = new System.Drawing.Point(8, 329);
			this.VerboseLoggingCheckBox.Name = "VerboseLoggingCheckBox";
			this.VerboseLoggingCheckBox.Size = new System.Drawing.Size(106, 17);
			this.VerboseLoggingCheckBox.TabIndex = 8;
			this.VerboseLoggingCheckBox.Text = "Verbose Logging";
			this.VerboseLoggingCheckBox.UseVisualStyleBackColor = true;
			this.VerboseLoggingCheckBox.CheckedChanged += new System.EventHandler(this.VerboseLoggingCheckBox_CheckedChanged);
			// 
			// VpnCheckTestButton
			// 
			this.VpnCheckTestButton.Enabled = false;
			this.VpnCheckTestButton.Location = new System.Drawing.Point(496, 120);
			this.VpnCheckTestButton.Name = "VpnCheckTestButton";
			this.VpnCheckTestButton.Size = new System.Drawing.Size(75, 23);
			this.VpnCheckTestButton.TabIndex = 7;
			this.VpnCheckTestButton.Text = "Test Ping";
			this.VpnCheckTestButton.UseVisualStyleBackColor = true;
			this.VpnCheckTestButton.Click += new System.EventHandler(this.VpnCheckTestButton_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(215, 107);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(149, 13);
			this.label7.TabIndex = 6;
			this.label7.Text = "Internal Hostname/IP Address";
			// 
			// VpnCheckHostNameTextBox
			// 
			this.VpnCheckHostNameTextBox.Enabled = false;
			this.VpnCheckHostNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.VpnCheckHostNameTextBox.Location = new System.Drawing.Point(94, 121);
			this.VpnCheckHostNameTextBox.Name = "VpnCheckHostNameTextBox";
			this.VpnCheckHostNameTextBox.Size = new System.Drawing.Size(391, 20);
			this.VpnCheckHostNameTextBox.TabIndex = 5;
			this.VpnCheckHostNameTextBox.TextChanged += new System.EventHandler(this.VpnCheckHostNameTextBox_TextChanged);
			// 
			// VpnCheckBox
			// 
			this.VpnCheckBox.AutoSize = true;
			this.VpnCheckBox.Location = new System.Drawing.Point(6, 123);
			this.VpnCheckBox.Name = "VpnCheckBox";
			this.VpnCheckBox.Size = new System.Drawing.Size(82, 17);
			this.VpnCheckBox.TabIndex = 4;
			this.VpnCheckBox.Text = "VPN Check";
			this.VpnCheckBox.UseVisualStyleBackColor = true;
			this.VpnCheckBox.CheckedChanged += new System.EventHandler(this.VpnCheckBox_CheckedChanged);
			// 
			// PsexecLocButton
			// 
			this.PsexecLocButton.Location = new System.Drawing.Point(496, 67);
			this.PsexecLocButton.Name = "PsexecLocButton";
			this.PsexecLocButton.Size = new System.Drawing.Size(75, 23);
			this.PsexecLocButton.TabIndex = 3;
			this.PsexecLocButton.Text = "Browse";
			this.PsexecLocButton.UseVisualStyleBackColor = true;
			this.PsexecLocButton.Click += new System.EventHandler(this.PsexecLocButton_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(107, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "PsExec.exe Location";
			// 
			// PsexEcLocTextBox
			// 
			this.PsexEcLocTextBox.Location = new System.Drawing.Point(7, 68);
			this.PsexEcLocTextBox.Name = "PsexEcLocTextBox";
			this.PsexEcLocTextBox.Size = new System.Drawing.Size(478, 20);
			this.PsexEcLocTextBox.TabIndex = 1;
			this.PsexEcLocTextBox.TextChanged += new System.EventHandler(this.PsexEcLocTextBox_TextChanged);
			// 
			// askToLogout
			// 
			this.askToLogout.AutoSize = true;
			this.askToLogout.Location = new System.Drawing.Point(7, 18);
			this.askToLogout.Name = "askToLogout";
			this.askToLogout.Size = new System.Drawing.Size(161, 17);
			this.askToLogout.TabIndex = 0;
			this.askToLogout.Text = "Ask for logout on disconnect";
			this.askToLogout.UseVisualStyleBackColor = true;
			this.askToLogout.CheckedChanged += new System.EventHandler(this.askToLogout_CheckedChanged);
			// 
			// tools
			// 
			this.tools.Controls.Add(this.label8);
			this.tools.Controls.Add(this.Groups);
			this.tools.Controls.Add(this.label2);
			this.tools.Controls.Add(this.createStartupShortcuts);
			this.tools.Controls.Add(this.logOut);
			this.tools.Controls.Add(this.lockMachine);
			this.tools.Controls.Add(this.RunBatchFile);
			this.tools.Controls.Add(this.label5);
			this.tools.Controls.Add(this.CheckOutButton);
			this.tools.Controls.Add(this.CloneButton);
			this.tools.Controls.Add(this.label4);
			this.tools.Controls.Add(this.warning);
			this.tools.Controls.Add(this.label3);
			this.tools.Controls.Add(this.label1);
			this.tools.Controls.Add(this.Hosts);
			this.tools.Controls.Add(this.relocate);
			this.tools.Controls.Add(this.unlock);
			this.tools.Controls.Add(this.clean);
			this.tools.Controls.Add(this.revert);
			this.tools.Controls.Add(this.update);
			this.tools.Location = new System.Drawing.Point(4, 22);
			this.tools.Name = "tools";
			this.tools.Padding = new System.Windows.Forms.Padding(3);
			this.tools.Size = new System.Drawing.Size(577, 352);
			this.tools.TabIndex = 1;
			this.tools.Text = "Tools";
			this.tools.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 37);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(36, 13);
			this.label8.TabIndex = 21;
			this.label8.Text = "Group";
			// 
			// Groups
			// 
			this.Groups.FormattingEnabled = true;
			this.Groups.Location = new System.Drawing.Point(51, 34);
			this.Groups.Name = "Groups";
			this.Groups.Size = new System.Drawing.Size(121, 21);
			this.Groups.TabIndex = 20;
			this.Groups.SelectedIndexChanged += new System.EventHandler(this.Groups_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 161);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Account";
			// 
			// createStartupShortcuts
			// 
			this.createStartupShortcuts.Enabled = false;
			this.createStartupShortcuts.Location = new System.Drawing.Point(150, 177);
			this.createStartupShortcuts.Name = "createStartupShortcuts";
			this.createStartupShortcuts.Size = new System.Drawing.Size(236, 23);
			this.createStartupShortcuts.TabIndex = 14;
			this.createStartupShortcuts.Text = "Create Startup Shortcuts on Host Machine(s)";
			this.createStartupShortcuts.UseVisualStyleBackColor = true;
			this.createStartupShortcuts.Visible = false;
			this.createStartupShortcuts.Click += new System.EventHandler(this.createStartupShortcuts_Click);
			// 
			// logOut
			// 
			this.logOut.Location = new System.Drawing.Point(6, 177);
			this.logOut.Name = "logOut";
			this.logOut.Size = new System.Drawing.Size(66, 23);
			this.logOut.TabIndex = 11;
			this.logOut.Text = "Logout";
			this.logOut.UseVisualStyleBackColor = true;
			this.logOut.Click += new System.EventHandler(this.logOut_Click);
			// 
			// lockMachine
			// 
			this.lockMachine.Location = new System.Drawing.Point(78, 177);
			this.lockMachine.Name = "lockMachine";
			this.lockMachine.Size = new System.Drawing.Size(66, 23);
			this.lockMachine.TabIndex = 10;
			this.lockMachine.Text = "Lock";
			this.lockMachine.UseVisualStyleBackColor = true;
			this.lockMachine.Click += new System.EventHandler(this.lockMachine_Click);
			// 
			// RunBatchFile
			// 
			this.RunBatchFile.Location = new System.Drawing.Point(6, 228);
			this.RunBatchFile.Name = "RunBatchFile";
			this.RunBatchFile.Size = new System.Drawing.Size(89, 23);
			this.RunBatchFile.TabIndex = 19;
			this.RunBatchFile.Text = "Run Batch File";
			this.RunBatchFile.UseVisualStyleBackColor = true;
			this.RunBatchFile.Click += new System.EventHandler(this.RunBatchFile_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 212);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(33, 13);
			this.label5.TabIndex = 18;
			this.label5.Text = "Other";
			// 
			// CheckOutButton
			// 
			this.CheckOutButton.Location = new System.Drawing.Point(78, 126);
			this.CheckOutButton.Name = "CheckOutButton";
			this.CheckOutButton.Size = new System.Drawing.Size(66, 23);
			this.CheckOutButton.TabIndex = 17;
			this.CheckOutButton.Text = "Checkout";
			this.CheckOutButton.UseVisualStyleBackColor = true;
			this.CheckOutButton.Click += new System.EventHandler(this.CheckOutButton_Click);
			// 
			// CloneButton
			// 
			this.CloneButton.Location = new System.Drawing.Point(6, 126);
			this.CloneButton.Name = "CloneButton";
			this.CloneButton.Size = new System.Drawing.Size(66, 23);
			this.CloneButton.TabIndex = 16;
			this.CloneButton.Text = "Clone";
			this.CloneButton.UseVisualStyleBackColor = true;
			this.CloneButton.Click += new System.EventHandler(this.CloneButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 110);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 13);
			this.label4.TabIndex = 15;
			this.label4.Text = "GIT";
			// 
			// warning
			// 
			this.warning.AutoSize = true;
			this.warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.warning.Location = new System.Drawing.Point(9, 324);
			this.warning.Name = "warning";
			this.warning.Size = new System.Drawing.Size(494, 17);
			this.warning.TabIndex = 13;
			this.warning.Text = "Be careful here. Once asked for your password you will NOT be asked again.";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(193, 37);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Host(s)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "SVN";
			// 
			// Hosts
			// 
			this.Hosts.FormattingEnabled = true;
			this.Hosts.Location = new System.Drawing.Point(239, 34);
			this.Hosts.Name = "Hosts";
			this.Hosts.Size = new System.Drawing.Size(121, 21);
			this.Hosts.TabIndex = 7;
			// 
			// relocate
			// 
			this.relocate.Location = new System.Drawing.Point(222, 74);
			this.relocate.Name = "relocate";
			this.relocate.Size = new System.Drawing.Size(66, 23);
			this.relocate.TabIndex = 6;
			this.relocate.Text = "Relocate";
			this.relocate.UseVisualStyleBackColor = true;
			this.relocate.Click += new System.EventHandler(this.relocate_Click);
			// 
			// unlock
			// 
			this.unlock.Location = new System.Drawing.Point(150, 74);
			this.unlock.Name = "unlock";
			this.unlock.Size = new System.Drawing.Size(66, 23);
			this.unlock.TabIndex = 5;
			this.unlock.Text = "Unlock";
			this.unlock.UseVisualStyleBackColor = true;
			this.unlock.Click += new System.EventHandler(this.unlock_Click);
			// 
			// clean
			// 
			this.clean.Location = new System.Drawing.Point(294, 74);
			this.clean.Name = "clean";
			this.clean.Size = new System.Drawing.Size(66, 23);
			this.clean.TabIndex = 4;
			this.clean.Text = "Clean";
			this.clean.UseVisualStyleBackColor = true;
			this.clean.Click += new System.EventHandler(this.clean_Click);
			// 
			// revert
			// 
			this.revert.Location = new System.Drawing.Point(78, 74);
			this.revert.Name = "revert";
			this.revert.Size = new System.Drawing.Size(66, 23);
			this.revert.TabIndex = 3;
			this.revert.Text = "Revert";
			this.revert.UseVisualStyleBackColor = true;
			this.revert.Click += new System.EventHandler(this.revert_Click);
			// 
			// update
			// 
			this.update.Location = new System.Drawing.Point(6, 74);
			this.update.Name = "update";
			this.update.Size = new System.Drawing.Size(66, 23);
			this.update.TabIndex = 2;
			this.update.Text = "Update";
			this.update.UseVisualStyleBackColor = true;
			this.update.Click += new System.EventHandler(this.update_Click);
			// 
			// hostsPinger
			// 
			this.hostsPinger.Controls.Add(this.pingStatus);
			this.hostsPinger.Controls.Add(this.dataGridView1);
			this.hostsPinger.Controls.Add(this.refreshAll);
			this.hostsPinger.Location = new System.Drawing.Point(4, 22);
			this.hostsPinger.Name = "hostsPinger";
			this.hostsPinger.Size = new System.Drawing.Size(577, 352);
			this.hostsPinger.TabIndex = 2;
			this.hostsPinger.Text = "Hosts";
			this.hostsPinger.UseVisualStyleBackColor = true;
			// 
			// pingStatus
			// 
			this.pingStatus.AutoSize = true;
			this.pingStatus.Location = new System.Drawing.Point(4, 326);
			this.pingStatus.Name = "pingStatus";
			this.pingStatus.Size = new System.Drawing.Size(0, 13);
			this.pingStatus.TabIndex = 3;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.host,
            this.ping});
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(581, 319);
			this.dataGridView1.TabIndex = 2;
			this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			// 
			// host
			// 
			this.host.HeaderText = "Host";
			this.host.Name = "host";
			this.host.ReadOnly = true;
			this.host.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.host.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.host.Width = 438;
			// 
			// ping
			// 
			this.ping.HeaderText = "Ping";
			this.ping.Name = "ping";
			this.ping.ReadOnly = true;
			// 
			// refreshAll
			// 
			this.refreshAll.Location = new System.Drawing.Point(499, 325);
			this.refreshAll.Name = "refreshAll";
			this.refreshAll.Size = new System.Drawing.Size(75, 23);
			this.refreshAll.TabIndex = 1;
			this.refreshAll.Text = "Refresh All";
			this.refreshAll.UseVisualStyleBackColor = true;
			this.refreshAll.Click += new System.EventHandler(this.refreshAll_Click);
			// 
			// ApplyButton
			// 
			this.ApplyButton.Enabled = false;
			this.ApplyButton.Location = new System.Drawing.Point(437, 392);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(75, 23);
			this.ApplyButton.TabIndex = 1;
			this.ApplyButton.Text = "Apply";
			this.ApplyButton.UseVisualStyleBackColor = true;
			this.ApplyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(518, 392);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 2;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(356, 392);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// resetGeneral
			// 
			this.resetGeneral.Location = new System.Drawing.Point(12, 392);
			this.resetGeneral.Name = "resetGeneral";
			this.resetGeneral.Size = new System.Drawing.Size(75, 23);
			this.resetGeneral.TabIndex = 0;
			this.resetGeneral.Text = "Reset";
			this.resetGeneral.UseVisualStyleBackColor = true;
			this.resetGeneral.Click += new System.EventHandler(this.resetGeneral_Click);
			// 
			// MvncSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(609, 421);
			this.Controls.Add(this.resetGeneral);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.ApplyButton);
			this.Controls.Add(this.RemoteRunTabControl);
			this.Name = "MvncSettings";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.RemoteRunTabControl.ResumeLayout(false);
			this.general.ResumeLayout(false);
			this.general.PerformLayout();
			this.tools.ResumeLayout(false);
			this.tools.PerformLayout();
			this.hostsPinger.ResumeLayout(false);
			this.hostsPinger.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl RemoteRunTabControl;
		private System.Windows.Forms.TabPage general;
		private System.Windows.Forms.TabPage tools;
		private System.Windows.Forms.Button update;
		private System.Windows.Forms.Button ApplyButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button revert;
		private System.Windows.Forms.Button clean;
		private System.Windows.Forms.Button relocate;
		private System.Windows.Forms.Button unlock;
		private System.Windows.Forms.ComboBox Hosts;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button logOut;
		private System.Windows.Forms.Button lockMachine;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label warning;
		private System.Windows.Forms.TabPage hostsPinger;
		private System.Windows.Forms.Button refreshAll;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label pingStatus;
		private System.Windows.Forms.DataGridViewLinkColumn host;
		private System.Windows.Forms.DataGridViewTextBoxColumn ping;
		private System.Windows.Forms.Button createStartupShortcuts;
		private System.Windows.Forms.ToolTip toolTipsSettings;
		private System.Windows.Forms.Button resetGeneral;
		private System.Windows.Forms.CheckBox askToLogout;
		private System.Windows.Forms.Button CheckOutButton;
		private System.Windows.Forms.Button CloneButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button RunBatchFile;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button PsexecLocButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox PsexEcLocTextBox;
		private System.Windows.Forms.TextBox VpnCheckHostNameTextBox;
		private System.Windows.Forms.CheckBox VpnCheckBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button VpnCheckTestButton;
		private System.Windows.Forms.CheckBox VerboseLoggingCheckBox;
		private System.Windows.Forms.CheckBox VncGroupLayoutCheckBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox Groups;
	}
}