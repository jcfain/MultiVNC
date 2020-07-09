
// MultiVNC - .NET VNC Client
// Copyright (C) 2015 Jason C. Fain
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


using CSharpWin32Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VncSharp;
using MultiVNC.Properties;
using MultiVNC.XML;
using System.IO;

namespace MultiVNC
{
	public partial class VncViewer : Form
	{
		/// <summary>
		/// Stores group names and their childrens host rdinfos
		/// </summary>
		public static readonly Dictionary<string, List<RDInfo>> HostSet = new Dictionary<string, List<RDInfo>>();
		
		/// <summary>
		/// Node that houses all server children
		/// </summary>
		readonly TreeNode _rootNode = new TreeNode();

		/// <summary>
		/// Keeps track of the host file edits.
		/// </summary>
		private bool _hostsChanged = false;

		/// <summary>
		/// Set once to enable _vncPanel group display.
		/// </summary>
		private bool _groupLayout = Settings.Default.VncPanelGroupDisplay;

		/// <summary>
		/// All tool tips
		/// </summary>
		private static readonly ToolTip ToolTips = new ToolTip();
		
		/// <summary>
		/// Main panel to hold the VNC clusters
		/// </summary>
		readonly FlowLayoutPanel _vncPanel = new FlowLayoutPanel();

		/// <summary>
		/// Holds the currently selected currentRdInfo
		/// </summary>
		RDInfo _currentRdInfo;

		private bool IAmClosing;

		/// <summary>
		/// Main constructor
		/// </summary>
		public VncViewer()
		{
			InitializeComponent();
			LoadSettings();
		}

		readonly ImageList _icons = new ImageList();

		/// <summary>
		/// Load all the main windows settings
		/// </summary>
		private void LoadSettings()
		{
			_vncPanel.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);
			_vncPanel.Dock = DockStyle.Fill;
			_vncPanel.AutoScroll = true;
			_vncPanel.SizeChanged += VncPanel_SizeChanged;
			SwitchVncPanelConfig();
			splitContainer1.Panel2.Controls.Add(_vncPanel);

			_icons.Images.Add("disConnected", Resources.disConnectedIcon);
			_icons.Images.Add("connected", Resources.connectedIcon);
			_icons.Images.Add("group", Resources.groupIcon);
			_icons.Images.Add("miniIcon", Resources.mainIcon);
			hosts.ImageList = _icons;
			this.Icon = Resources.My_Workgroup_icon;

			hosts.Nodes.Add(_rootNode);
			_rootNode.ImageIndex = 2;
			_rootNode.SelectedImageIndex = 2;

			//Check for a host config
			if (!File.Exists(Settings.Default.hostsFile))
			{

				Dialog.showInfoDialog(this, "Thank you for choosing MultiVNC,\n"
											+"To get started, add a group/host from either\n"
											+"the menu: File > Add, by right clicking on 'Servers'\n"
											+"or load an existing mvnc file from the menu\n"
											+"'File > Load Hosts File...'",
											"Welcome!"
									  );
				_rootNode.Tag = new RDInfo() { hostName = "Servers", comment = "Add hosts/groups here", treeNode = _rootNode, type = "group", readOnly = false, port = 5900, password = ""};
				_rootNode.Text = "Servers";
				SaveHostFile(Settings.Default.hostsFile);
			}
			else
			{
				while (!loadXml(Settings.Default.hostsFile))
				{
				   Settings.Default.hostsFile = Dialog.showOpenFileDialog(this, Settings.Default.hostsFile, "MVNC files (*.mvnc)|*.mvnc");
					if (Settings.Default.hostsFile == "Cancel")
					{
						Close();
						return;
					}
				}
				_rootNode.ExpandAll();


			}

			//TODO Anoying, make option to turn off warning
			//if (!MvncSettings.PsexecExists())
			//{
			//	Dialog.showErrorDialog(this, "Can not finds PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			//}

			//Set this so after treenode select doesnt think its changed.
			//Is set in two places, Here and The treeview hosts after_Select event.
			_currentRdInfo = (RDInfo)_rootNode.Tag;
			updateInfoPanel(_currentRdInfo);
			ToolTips.SetToolTip(thumbNailsAreReadOnly, "Make all the thumbnail desktops read only.\n(This will not affect enlarged desktop.)");

			SetMenuItemTooptips();
		}
		
		/// <summary>
		/// Sets the file menu tool tips.
		/// </summary>
		private void SetMenuItemTooptips()
		{
			string hostFileMenuToolTipText = @"Current: " + Settings.Default.hostsFile;
			saveToolStripMenuItem.ToolTipText = hostFileMenuToolTipText;
			saveAsToolStripMenuItem.ToolTipText = hostFileMenuToolTipText;
			reloadHostsFileToolStripMenuItem.ToolTipText = hostFileMenuToolTipText;
			loadToolStripMenuItem.ToolTipText = hostFileMenuToolTipText;
		}
		/// <summary>
		/// Switch between group display and all display.
		/// </summary>
		private void SwitchVncPanelConfig()
		{
			if (_groupLayout)
			{
				//Add host to group panel
				_vncPanel.FlowDirection = FlowDirection.TopDown;
				_vncPanel.WrapContents = false;
			}
			else
			{
				_vncPanel.FlowDirection = FlowDirection.LeftToRight;
			}
		}

		/// <summary>
		/// Checks for a host online and service then creates VNC cluster and connect to a host.
		/// </summary>
		/// <param name="rdInfo"></param>
		private void ConnectHost(RDInfo rdInfo)
		{
			if (!rdInfo.rd.IsConnected && rdInfo.panel == null)
			{
				if (new Pinger(rdInfo.hostName).success)
				{
					if (RemoteCommand.IsPortAcceptingConnections(rdInfo.hostName, rdInfo.port))
					{
						rdInfo.treeNode.Text = rdInfo.hostName;
						SetRdControlAndConnect(rdInfo);
						//Log.Debug("VncPanel.IsHandleCreated: " + VncPanel.IsHandleCreated);
						//if (VncPanel.InvokeRequired)
						//{
						//    Log.Debug("VncPanel.InvokeRequired: " + VncPanel.InvokeRequired);
						//    VncPanel.Invoke((MethodInvoker)delegate
						//    {
						//        setRDControlAndConnect(rdInfo);
						//    });
						//    return;
						//}
						//BackgroundWorker worker = new BackgroundWorker();
						//worker.DoWork += (obj, e) => setRDControlAndConnect(rdInfo);
						//worker.RunWorkerAsync();
					}
					else
						rdInfo.treeNode.Text = rdInfo.hostName + " (VNC OFFLINE)";
				}
				else
				{
					rdInfo.treeNode.Text = rdInfo.hostName + " (Ping Failed)";
				}
			}
			else if (!rdInfo.rd.IsConnected)
			{
				RetryConnection(rdInfo);
			}
		}

		/// <summary>
		/// Creates a vnc cluster and connects
		/// </summary>
		/// <param name="rdInfo"></param>private void worker_DoWork(object sender, DoWorkEventArgs e) 
		private void SetRdControlAndConnect(RDInfo rdInfo)
		{
			Panel panel = new Panel();
			rdInfo.rd = new RemoteDesktop();
			panel.Size = new Size(250, 225);
			panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			rdInfo.panel = panel;

			rdInfo.rdLocation = new Point(20, 25);
			rdInfo.rdSize = new Size(200, 150);
			rdInfo.errorLabel = new Label();
			rdInfo.errorLabelLoc = new Point(rdInfo.rdLocation.X, rdInfo.rdLocation.Y + 175);
			rdInfo.hostLabel = new Label();
			rdInfo.hostLabelLoc = new Point(rdInfo.rdLocation.X, rdInfo.rdLocation.Y - 17);
			rdInfo.retry = new Button();
			rdInfo.retryLoc = new Point(rdInfo.rdLocation.X + 75, rdInfo.rdLocation.Y + 150);
			rdInfo.ctrlAltDel = new Button();
			rdInfo.ctrlAltDelLoc = new Point(rdInfo.rdLocation.X, rdInfo.rdLocation.Y + 150);
			rdInfo.enlarge = new Button();
			rdInfo.enlargeLoc = new Point(panel.Size.Width - 45, 0);
			rdInfo.close = new Button();
			rdInfo.closeLoc = new Point(panel.Size.Width - 25, 0);

			////////////////////
			//RD Setup
			////////////////////
			rdInfo.rd.VncPort = rdInfo.port;
			rdInfo.rd.AutoScroll = true;
			rdInfo.rd.Location = rdInfo.rdLocation;
			rdInfo.rd.Name = "rd";
			rdInfo.rd.Tag = rdInfo;
			rdInfo.rd.TabIndex = 1;
			rdInfo.rd.ConnectComplete += rd_ConnectComplete;
			rdInfo.rd.Disposed += rd_Disposed;
			rdInfo.rd.ConnectionLost += rd_ConnectionLost;
			/////////////////////////////////////////////////////


			//Host name Label
			rdInfo.hostLabel.Text = rdInfo.hostName;
			rdInfo.hostLabel.Height = 50;
			rdInfo.hostLabel.AutoSize = true;
			rdInfo.hostLabel.Location = rdInfo.hostLabelLoc;//new Point(Location.X, Location.Y - 17);

			//Error message Label
			rdInfo.errorLabel.Location = rdInfo.errorLabelLoc;//new Point(rdInfo.rd.Location.X, rdInfo.rd.Location.Y + 175);
			rdInfo.errorLabel.Hide();

			//Reconnect button
			rdInfo.retry.Width = 75;
			rdInfo.retry.Click += retry_Click;
			rdInfo.retry.Text = "Retry";
			rdInfo.retry.Tag = rdInfo;
			rdInfo.retry.Location = rdInfo.retryLoc;
			rdInfo.retry.Visible = !rdInfo.rd.IsConnected;
			ToolTips.SetToolTip(rdInfo.retry, "Reconnect to: " + rdInfo.hostName);

			//Send ctrl-alt-del button
			rdInfo.ctrlAltDel.Width = 75;
			rdInfo.ctrlAltDel.Click += ctrlAltDel_Click;
			rdInfo.ctrlAltDel.Text = "Ctrl-Alt-Del";
			rdInfo.ctrlAltDel.Tag = rdInfo;
			rdInfo.ctrlAltDel.Location = rdInfo.ctrlAltDelLoc;
			ToolTips.SetToolTip(rdInfo.ctrlAltDel, "Send a Ctrl-Alt-Del to: "+rdInfo.hostName);

			//Enlarge RD
			rdInfo.enlarge.Size = new System.Drawing.Size(20, 20); ;
			rdInfo.enlarge.Click += enlarge_Click;
			rdInfo.enlarge.Tag = rdInfo;
			rdInfo.enlarge.Image = Resources.enlargeIcon;
			rdInfo.enlarge.UseVisualStyleBackColor = true;
			rdInfo.enlarge.Location = rdInfo.enlargeLoc;
			ToolTips.SetToolTip(rdInfo.enlarge, "Open '" + rdInfo.hostName + "' in new window.");

			//Close RD
			rdInfo.close.Size = new System.Drawing.Size(20, 20); ;
			rdInfo.close.Click += close_Click;
			rdInfo.close.Image = Resources.closeIcon;
			rdInfo.close.Tag = rdInfo;
			rdInfo.close.UseVisualStyleBackColor = true;
			rdInfo.close.Location = rdInfo.closeLoc;
			ToolTips.SetToolTip(rdInfo.close, "Close the connection to: " + rdInfo.hostName);

			panel.Controls.Add(rdInfo.rd);
			panel.Controls.Add(rdInfo.hostLabel);
			panel.Controls.Add(rdInfo.retry);
			panel.Controls.Add(rdInfo.ctrlAltDel);
			panel.Controls.Add(rdInfo.enlarge);
			panel.Controls.Add(rdInfo.close);
			panel.Controls.Add(rdInfo.errorLabel);

			//Add the cluster panel to the VNCPanel
			if (_groupLayout)
			{
				//Add host to group panel
				TreeNode hostParent = rdInfo.treeNode.Parent;
				RDInfo parentRdInfo = (RDInfo)hostParent.Tag;
				//Control[] groupBoxControls = VncPanel.Controls.Find("groupPanel", true);
				Control[] groupFlowControls = _vncPanel.Controls.Find(parentRdInfo.groupFlowPanel.Name, true);

				if (groupFlowControls.Length == 0)
				{
					_vncPanel.Controls.Add(new Label() { Text = parentRdInfo.groupFlowPanel.Name, AutoSize = true});
					_vncPanel.Controls.Add(parentRdInfo.groupPanel);
				}
				ResizeGroupBoxes();
				parentRdInfo.groupFlowPanel.Controls.Add(panel);
			}
			else
			{
				_vncPanel.Controls.Add(panel);
			}


			//Try to connect to the vnc
			try
			{
				rdInfo.rd.GetPassword = new AuthenticateDelegate(GetPass);
				Log.Debug("Connecting to: " + rdInfo.hostName);
				rdInfo.rd.Connect(rdInfo.hostName, thumbNailsAreReadOnly.Checked, true);
				//BackgroundWorker worker = new BackgroundWorker();
				//worker.DoWork += (obj, e) => rdInfo.rd.Connect(rdInfo.hostName, thumbNailsAreReadOnly.Checked, true);
				//worker.RunWorkerAsync();
				//Log.Debug("Setting size");
				//Moved to connectComplete event
				//Rectangle vncRect = ImageHandling.GetScaledRectangle(rdInfo.rd.Desktop, new Rectangle(rdInfo.rdLocation, rdInfo.rdSize));
				//rdInfo.rd.Size = new Size(vncRect.Width, vncRect.Height);
				//rdInfo.rdSize = new Size(vncRect.Width, vncRect.Height);

			}
			catch (Exception ex)
			{
				createErrorInfo(rdInfo, ex);
			}


		}

		/// <summary>
		/// Gets default the vnc password
		/// </summary>
		/// <returns></returns>
		private string GetPass()
		{
			Log.Debug(Settings.Default.encryptPass ? Encryption.DecryptPlainString(_currentRdInfo.password) : _currentRdInfo.password);
			return Settings.Default.encryptPass ? Encryption.DecryptPlainString(_currentRdInfo.password) : _currentRdInfo.password;
		}

		/// <summary>
		/// Resizes a group box in group display mode
		/// </summary>
		private void ResizeGroupBoxes()
		{
			if (_groupLayout)
			{
				Control[] groupBoxControls = _vncPanel.Controls.Find("groupPanel", true);
				for (int i = 0; i < groupBoxControls.Length; i++)
				{
					groupBoxControls[i].Height = _vncPanel.Height / groupBoxControls.Length;
					groupBoxControls[i].Width = _vncPanel.Width - 20;
				}
			}
		}

		private void VncViewer_ResizeEnd(object sender, EventArgs e)
		{
			Log.Debug("Enter VncViewer_ResizeEnd");
			//ResizeGroupBoxes();
		}

		/// <summary>
		/// Close VNC cluster and dispose it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void close_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			RDInfo rdInfo = (RDInfo)button.Tag;
			if (Settings.Default.askToLogout)
			{
				if (MvncSettings.PsexecExists())
				{
					string result = Dialog.showYesNoLockDialog(this, "Do you want to logout?", "Logout?");
					if (result == "logOut")
					{
						if (MvncSettings.GetPassword())
						{
							RemoteCommand.Logout(rdInfo.hostName);
							disposeRDPanel(rdInfo);
						}
					}
					else if (result == "lock")
					{
						if (MvncSettings.GetPassword())
						{
							RemoteCommand.LockScreen(rdInfo.hostName);
							disposeRDPanel(rdInfo);
						}
					}
					else if (result == "no")
					{
						disposeRDPanel(rdInfo);
					}
				}
			}
			else
			{
				disposeRDPanel(rdInfo);
			}
		}

		/// <summary>
		/// Dispose VNC cluster
		/// </summary>
		/// <param name="rdInfo"></param>
		private void disposeRDPanel(RDInfo rdInfo)
		{
			if (rdInfo.rd.IsConnected)
				rdInfo.rd.Disconnect();

			rdInfo.panel.Dispose();

			if (rdInfo.treeNode.Text.Contains("Connected"))
			{
				rdInfo.treeNode.Text = rdInfo.treeNode.Text.Substring(0, rdInfo.treeNode.Text.IndexOf(' '));
			}
		}

		/// <summary>
		/// Open Remote desktop in new window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void enlarge_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			RDInfo rdInfo = (RDInfo)button.Tag;
			RemoteDesktop vncControl = rdInfo.rd;


			rdInfo.enlarge.Enabled = false;
			rdInfo.close.Enabled = false;

			Form vncEnlarged = new Form();
			vncEnlarged.Text = rdInfo.hostName;
			vncEnlarged.FormClosing += vncEnlarged_FormClosing;
			vncEnlarged.Tag = rdInfo;
			vncEnlarged.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			vncEnlarged.MaximizeBox = false;
			vncEnlarged.Name = "vncEnlarged";
			rdInfo.retryLoc = rdInfo.retry.Location;
			rdInfo.ctrlAltDelLoc = rdInfo.ctrlAltDel.Location;
			rdInfo.ctrlAltDel.Location = new Point(0, 0);
			rdInfo.retry.Location = new Point(75, 0);
			vncControl.Location = new Point(0, 25);
			Rectangle vncRect = ImageHandling.GetScaledRectangle(vncControl.Desktop, new Rectangle(vncControl.Location, 
																									new Size(Screen.GetWorkingArea(this).Size.Width - 100, 
																											 Screen.GetWorkingArea(this).Size.Height - 100)));//Screen.GetWorkingArea(this).Size

			vncEnlarged.Size = new Size(vncRect.Width +5, vncRect.Height + 75);

			rdInfo.rdSizeEnlarged = new Size(vncRect.Width, vncRect.Height);
			vncControl.Size = rdInfo.rdSizeEnlarged;
			vncControl.SetScalingMode(true);
			vncControl.SetInputMode(false);
			vncControl.Dock = DockStyle.Bottom;
			vncControl.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
			vncEnlarged.Controls.Add(rdInfo.ctrlAltDel);
			vncEnlarged.Controls.Add(rdInfo.retry);
			vncEnlarged.Controls.Add(vncControl);
			vncEnlarged.Show();
			vncEnlarged.Location = new Point(15, 15);
			//vncEnlarged.WindowState = FormWindowState.Maximized;
		}

		/// <summary>
		/// Adds the Remotedesktop back to its VNC cluster
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void vncEnlarged_FormClosing(object sender, FormClosingEventArgs e)
		{
			Form vncEnlarged = (Form)sender;
			RDInfo rdInfo = (RDInfo)vncEnlarged.Tag;
			rdInfo.rd.Size = rdInfo.rdSize;
			rdInfo.rd.Location = rdInfo.rdLocation;
			rdInfo.ctrlAltDel.Location = rdInfo.ctrlAltDelLoc;
			if (rdInfo.rd.IsConnected)
				rdInfo.rd.SetInputMode(thumbNailsAreReadOnly.Checked);
			rdInfo.retry.Location = rdInfo.retryLoc;
			rdInfo.panel.Controls.Add(rdInfo.rd);
			rdInfo.panel.Controls.Add(rdInfo.ctrlAltDel);
			rdInfo.panel.Controls.Add(rdInfo.retry);
			rdInfo.enlarge.Enabled = true;
			rdInfo.close.Enabled = true;
		}

		/// <summary>
		/// Send a Ctrl Alt Del to the Remote machine
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ctrlAltDel_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			RDInfo rdInfo = (RDInfo)button.Tag;
			RemoteDesktop vncControl = rdInfo.rd;
			vncControl.Focus();
			vncControl.SendSpecialKeys(SpecialKeys.CtrlAltDel);
		}

		/// <summary>
		/// Sends a connect to to the remote.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void retry_Click(object sender, EventArgs e)
		{
			Button retryButton = (Button)sender;
			RDInfo rdInfo = (RDInfo)retryButton.Tag;
			RetryConnection(rdInfo);
		}

		/// <summary>
		/// Retries a connection on a specific Panel(rdInfo)
		/// </summary>
		/// <param name="rdInfo"></param>
		void RetryConnection(RDInfo rdInfo)
		{
			if (rdInfo.rd.Parent != null)
			{
				rdInfo.retry.Enabled = false;
			
				try
				{
					rdInfo.errorLabel.Hide();
					if (!rdInfo.rd.IsConnected)
					{
						bool readOnly = thumbNailsAreReadOnly.Checked;
						if (rdInfo.rd.Parent.Name == "vncEnlarged")
						{
							readOnly = false;
						}
						rdInfo.treeNode.Text = rdInfo.hostName;
						rdInfo.rd.Connect(rdInfo.hostName, readOnly, true);
						rdInfo.ctrlAltDel.Enabled = true;
						if (rdInfo.rd.Parent.Name != "vncEnlarged")
						{
							rdInfo.enlarge.Enabled = true;
						}
						else
						{
							rdInfo.rd.Size = rdInfo.rdSizeEnlarged;
						}
					}
				}
				catch
				(Exception ex)
				{
					createErrorInfo(rdInfo, ex);
				}
			}
			else
			{
				SetRdControlAndConnect(rdInfo);
			}
		}

		/// <summary>
		/// Adds a label to the VNC Cluster to notify error.
		/// </summary>
		/// <param name="rdInfo"></param>
		/// <param name="e"></param>
		private void createErrorInfo(RDInfo rdInfo, Exception e)
		{
			rdInfo.rd.Size = rdInfo.rdSize;
			rdInfo.errorLabel.Text = e.Message;
			ToolTips.SetToolTip(rdInfo.errorLabel, e.Message);
			rdInfo.errorLabel.Show();
			Log.Error("VNC exception: " + e.Message);
			Log.Debug("Stack " + e.StackTrace);
			rdInfo.enlarge.Enabled = false;
			rdInfo.ctrlAltDel.Enabled = false;
			rdInfo.retry.Visible = true;
			rdInfo.retry.Enabled = true;
		}

		/// <summary>
		/// Event listener for VNCCluster dispose 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void rd_Disposed(object sender, EventArgs e)
		{
			RemoteDesktop rd = (RemoteDesktop)sender;
			RDInfo rdInfo = (RDInfo)rd.Tag;
			if (Log.VerboseLogging)
			{
				Log.Debug("Disposed: " + rdInfo.hostName);
			}
		}

		/// <summary>
		/// Event listener for Remote connect
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rd_ConnectComplete(object sender, ConnectEventArgs e)
		{
			// Update the Form to match the geometry of remote desktop (including the height of the menu bar in this form).
			//ClientSize = new Size(e.DesktopWidth, e.DesktopHeight + menuStrip1.Height);
			RemoteDesktop rd = (RemoteDesktop)sender;
			Log.Debug("Connected to " + rd.Name);
			RDInfo rdInfo = (RDInfo)rd.Tag;

			Log.Debug("Setting size");
			Rectangle vncRect = ImageHandling.GetScaledRectangle(rdInfo.rd.Desktop, new Rectangle(rdInfo.rdLocation, rdInfo.rdSize));
			rdInfo.rd.Size = new Size(vncRect.Width, vncRect.Height);
			rdInfo.rdSize = new Size(vncRect.Width, vncRect.Height);

			rdInfo.treeNode.ImageIndex = 1;
			rdInfo.treeNode.SelectedImageIndex = 1;
			rdInfo.retry.Visible = false;
			rdInfo.retry.Enabled = false;

			//FlipMenuOptions();
			Log.Debug("VNC Connected to " + rdInfo.hostName);

			// Give the remote desktop focus now that it's connected
			rd.Focus();
		}

		/// <summary>
		/// Event listener for Remote disconnect
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rd_ConnectionLost(object sender, EventArgs e)
		{
			if (!IAmClosing)
			{
				Log.Debug("Enter rd_ConnectionLost");
				RemoteDesktop rd = (RemoteDesktop) sender;
				Log.Debug("Disconnected from " + rd.Name);
				RDInfo rdInfo = (RDInfo) rd.Tag;
				rdInfo.treeNode.ImageIndex = 0;
				rdInfo.treeNode.SelectedImageIndex = 0;
				rdInfo.retry.Visible = true;
				rdInfo.retry.Enabled = true;
				Log.Debug("Disconnected from " + rdInfo.hostName);
				Log.Debug("Exit rd_ConnectionLost");
			}
		}

		/// <summary>
		/// Event listener for the main form closing.
		/// Asks to logout and save hosts file if needed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VNCViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			IAmClosing = true;
			//try
			//{
			if (_hostsChanged)
				if (Dialog.showYesNoDialog(this, "Save changes to the hosts file?", "Save Changes?"))
				{
					SaveHostFile(Settings.Default.hostsFile);
				}

			Control[] rds = this.Controls.Find("rd", true);
				if (Settings.Default.askToLogout)
				{
					if (MvncSettings.PsexecExists())
					{
						if (rds.Length > 0)
						{
							string result = Dialog.showYesNoLockDialog(this, "Do you want to logout of all hosts?", "Logout?");
							if (result == "logOut")
							{
								if (MvncSettings.GetPassword())
								{
									logoutAll(rds);
								}
							}
							else if (result == "lock")
							{
								if (MvncSettings.GetPassword())
								{
									lockAll(rds);
								}
							}
						}
					}
				}
				//TODO: This is really slow.
				//VNCSharp throws an error if this is not done.
				//disposeAll(rds);

			//}
			//catch(Exception ex)
			//{
			//    Log.writeCatchFile(ex, "VNCViewer_FormClosing");
			//}
		}

		/// <summary>
		/// Log out of all hosts in a control list.
		/// </summary>
		/// <param name="rds"></param>
		private void logoutAll(Control[] rds)
		{
			if (rds.Length > 0)
			{
				if (MvncSettings.GetPassword())
				{
					foreach (Control t in rds)
					{
						RDInfo rdInfo = (RDInfo) t.Tag;
						RemoteCommand.Logout(rdInfo.hostName);
					}
				}
			}
		}

		/// <summary>
		/// Dispose all RemoteDesktop controls.
		/// </summary>
		/// <param name="rds"></param>
		private void disposeAll(Control[] rds)
		{
			if (rds.Length > 0)
			{
				foreach (Control t in rds)
				{
					if (t.IsHandleCreated)
					{
						RDInfo rdInfo = (RDInfo) t.Tag;
						rdInfo.panel.Dispose();
					}
				}
			}
		}

		/// <summary>
		/// Lock all RemoteDesktops in the rds array
		/// </summary>
		/// <param name="rds"></param>
		private void lockAll(Control[] rds)
		{
			if (rds.Length > 0)
			{
				if (MvncSettings.GetPassword())
				{
					foreach (Control t in rds)
					{
						RDInfo rdInfo = (RDInfo) t.Tag;
						RemoteCommand.LockScreen(rdInfo.hostName);
					}
				}
			}
		}

		/// <summary>
		/// Make sure All RemoteDesktop controls are readOnly in thumbnail mode.
		/// Used for automation testing to prevent accedental mouseclicks/keypress
		/// </summary>
		/// <param name="rds"></param>
		/// <param name="readOnly"></param>
		private void togglaAllRdsReadOnly(Control[] rds, bool readOnly)
		{
			if (rds.Length > 0)
			{
				foreach (Control t in rds)
				{
					RDInfo rdInfo = (RDInfo)t.Tag;
					if (rdInfo.rd.IsConnected)
						rdInfo.rd.SetInputMode(readOnly);
				}
			}
		}

		/// <summary>
		/// Connects to a host clicked on in the tree
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_DoubleClick(object sender, EventArgs e)
		{
			TreeView treeView = (TreeView)sender;
			TreeNode treeNode = treeView.SelectedNode;
			if (treeNode != null)
			{
				RDInfo rdInfo = (RDInfo)treeNode.Tag;
				Log.Debug("treeNode.Level: " + treeNode.Level);
				if (treeNode.Level != 0)
				{
					if (!rdInfo.rd.IsConnected)
					{
						ConnectHost(rdInfo);
					}
				}
			}
		}

		/// <summary>
		/// Used for treeview hosts right click menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Log.Debug("Enter mbRight");
				if (hosts.GetNodeAt(e.X, e.Y).GetType() == typeof(TreeNode))
				{
					Log.Debug("Enter hosts.GetNodeAt(e.X, e.Y).GetType().Equals(typeof(TreeNode))");
					hosts.SelectedNode = hosts.GetNodeAt(e.X, e.Y);


					ContextMenu cm = new ContextMenu();
					RDInfo rdInfo = (RDInfo)hosts.SelectedNode.Tag;
					if (hosts.SelectedNode != _rootNode && rdInfo.type == "host")
					{
						Log.Debug("hosts.SelectedNode.Level != 0");
						AddMenuItem(cm, "Connect", TreeViewRightClick, rdInfo).Enabled = !rdInfo.rd.IsConnected;
						cm.MenuItems.Add("-");
						AddMenuItem(cm, "Copy Hostname", TreeViewRightClick, rdInfo, Shortcut.CtrlC);
						AddMenuItem(cm, "Delete", TreeViewRightClick, rdInfo).Enabled = !rdInfo.rd.IsConnected;
					}
					else
					{
						if (groupNodeHasHost(hosts.SelectedNode))
						{
							AddMenuItem(cm, "Connect All", TreeViewRightClick, rdInfo);
						}
						cm.MenuItems.Add("-"); 
						AddMenuItem(cm, "Add Group", TreeViewRightClick, rdInfo);
						AddMenuItem(cm, "Add Host", TreeViewRightClick, rdInfo);//.Enabled = true;
						AddMenuItem(cm, "Copy Groupname", TreeViewRightClick, rdInfo, Shortcut.CtrlC);
						if (hosts.SelectedNode != _rootNode)
						{
							AddMenuItem(cm, "Delete", TreeViewRightClick, rdInfo).Enabled = !IsNodesChildrenConnected(hosts.SelectedNode);
						}
					}

					cm.Show(hosts, e.Location);
				}
			}
		}

		/// <summary>
		/// Seaeches a Groups nodes children for a connected host.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		bool IsNodesChildrenConnected(TreeNode parent)
		{
			foreach (TreeNode child in parent.Nodes)
			{
				RDInfo rdInfo = (RDInfo)child.Tag;
				if (rdInfo.rd != null && rdInfo.rd.IsConnected)
					return true;
			}
			return false;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cm"></param>
		/// <param name="text"></param>
		/// <param name="handler"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		private MenuItem AddMenuItem(ContextMenu cm, string text, EventHandler handler, RDInfo rdInfo)
		{
			MenuItem item = new MenuItem(text, handler) {Tag = rdInfo};
			cm.MenuItems.Add(item);
			return item;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cm"></param>
		/// <param name="text"></param>
		/// <param name="handler"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		private MenuItem AddMenuItem(ContextMenu cm, string text, EventHandler handler, RDInfo rdInfo, Shortcut shortcut)
		{
			MenuItem item = new MenuItem(text, handler)
			{
				Shortcut = shortcut,
				ShowShortcut = true,
				Tag = rdInfo
			};
			cm.MenuItems.Add(item);
			return item;
		}

		/// <summary>
		/// Check for group node children
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private bool groupNodeHasHost(TreeNode node)
		{
			foreach(TreeNode child in node.Nodes)
			{
				RDInfo rdInfo = (RDInfo)child.Tag;
				if (rdInfo.type == "host")
				{
					return true;
				}
				if (rdInfo.type == "group")
				{
					if (groupNodeHasHost(child))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Handles rightclick commands.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeViewRightClick(object sender, EventArgs e)
		{
			MenuItem host = (MenuItem)sender;
			RDInfo rdInfo = (RDInfo)host.Tag;
			if (host.Text == "Connect")
			{
				if (!rdInfo.rd.IsConnected)
				{
					ConnectHost(rdInfo);
				}
			}
			else if (host.Text == "Connect All")
			{
				ConnectAllChildren(rdInfo);
			}
			else if (host.Text == "Copy Hostname" || host.Text == "Copy Groupname")
			{
				Clipboard.SetText(rdInfo.hostName);
			}
			else if (host.Text == "Add Group")
			{
				AddNewTreeNode("group");
			}
			else if (host.Text == "Add Host")
			{
				AddNewTreeNode("host");
			}
			else if (host.Text == "Delete")
			{
				DeleteNode(rdInfo.treeNode);
			}
		}

		/// <summary>
		/// Connects every child host node in all child groups.
		/// </summary>
		/// <param name="rdInfo"></param>
		private void ConnectAllChildren(RDInfo rdInfo)
		{
			this.Enabled = false;
			TreeNodeCollection productHostsTreeNodes = rdInfo.treeNode.Nodes;
			foreach (TreeNode node in productHostsTreeNodes)
			{
				rdInfo = (RDInfo) node.Tag;
				if (rdInfo.type.Equals("host"))
				{
					ConnectHost(rdInfo);
				}
				else if (rdInfo.type.Equals("group"))
				{
					ConnectAllChildren(rdInfo);
				}
			}
			this.Enabled = true;
		}
		private void ResumeRdClusterLayout(RDInfo rdInfo)
		{
			 foreach (Control c in rdInfo.panel.Controls)
			 {
					c.ResumeLayout();
			 }
		}
		private void SuspendRdClusterLayout(RDInfo rdInfo)
		{
			foreach (Control c in rdInfo.panel.Controls)
			{
				c.SuspendLayout();
			}
		}


		/// <summary>
		/// Checks file, saves it in settings and Loads a XML hosts file into the treeview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string hostsFile = Dialog.showOpenFileDialog(this, Settings.Default.hostsFile, "MVNC files (*.mvnc)|*.mvnc");
			if (hostsFile != Settings.Default.hostsFile && hostsFile != "Cancel")
			{
				if (MvncSettings.CheckForValidHostFile(hostsFile))
				{
					if (loadXml(hostsFile))
					{
						Settings.Default.hostsFile = hostsFile;
						Settings.Default.Save();
						_hostsChanged = false;
						SetMenuItemTooptips();
					}
					else
					{
						bool result = Dialog.showYesNoDialog(this, "File is not a valid hosts file format. Reload default?", "Invalid format!");
						if (result)
						{
							loadXml(Settings.Default.hostsFile);
						}
					}
				}
				else
				{
					Dialog.showErrorDialog(this, "File is not a valid hosts file.", "Invalid file!");
				}
			}
			else
			{
				Log.Debug("hostsFile: " + hostsFile);
			}
		}

		/// <summary>
		/// Saves the hosts XML file
		/// </summary>
		/// <param name="path"></param>
		private void SaveHostFile(string path)
		{
			new WriteXML().All(path, _rootNode);
			_hostsChanged = false;
		}

		/// <summary>
		/// Loads a XML hosts file
		/// </summary>
		/// <param name="path"></param>
		private bool loadXml(string path)
		{
			_rootNode.Nodes.Clear();
			bool load = new ReadXml(path, _rootNode).LoadXml();
			_rootNode.ExpandAll();
			return load;
		}

		/// <summary>
		/// Deletes an item and all its children from the hosts treenode
		/// </summary>
		/// <param name="treeNode"></param>
		private void DeleteNode(TreeNode treeNode)
		{
			if (Dialog.showYesNoDialog(this, "Are you sure you want to delete " + treeNode.Text + " and any children hosts?", "Confirm Delete?"))
			{
				_hostsChanged = true;
				hosts.Nodes.Remove(treeNode);
			}
		}

	   
		/// <summary>
		/// Adds a new host to the hosts treeview
		/// </summary>
		private void AddNewTreeNode(string TreeNodeType)
		{
			TreeNode selectedNode = hosts.SelectedNode;
			RDInfo selectedRdInfo = (RDInfo)selectedNode.Tag;
			if (selectedRdInfo.type == "group")
				selectedNode = hosts.SelectedNode;
			else
			{
				selectedNode = hosts.SelectedNode.Parent;
				selectedRdInfo = (RDInfo)selectedNode.Tag;
			}
			HostOrGroup newTreeNode = MvncDialog.ShowNewHostOrGroupPrompt(this,
				"Add a " + TreeNodeType + " to: '" + selectedNode.Text + "'",
				new RDInfo() { port = 5900, inherit = true, type = TreeNodeType },
				selectedRdInfo
			);
			if (newTreeNode != null)
			{
				_hostsChanged = true;
				Log.Debug("Saving host: " + newTreeNode.hostName);
				TreeNode newNode = new TreeNode(newTreeNode.hostName);
				newNode.Tag = new RDInfo() { 
					hostName = newTreeNode.hostName, 
					comment = newTreeNode.comment, 
					treeNode = newNode, 
					type = TreeNodeType, 
					rd = new RemoteDesktop(), 
					readOnly = false, 
					port = newTreeNode.port, 
					password = newTreeNode.password, 
					inherit = newTreeNode.inherit 
				};
				selectedNode.Nodes.Add(newNode);
				_rootNode.ExpandAll();
			}
		}


		private void SaveHost(RDInfo rdInfo, HostOrGroup editHostObj)
		{
			//Changed in memory but not in xml yet
			_hostsChanged = true;
			rdInfo.treeNode.Text = editHostObj.hostName;
			rdInfo.hostName = editHostObj.hostName;
			rdInfo.comment = editHostObj.comment;
			rdInfo.readOnly = editHostObj.readOnly;
			rdInfo.port = editHostObj.port;
			rdInfo.inherit = editHostObj.inherit;
			rdInfo.password = Settings.Default.encryptPass ? Encryption.EncryptPlainString(editHostObj.password) : editHostObj.password;
			if (rdInfo.rd == null)
				rdInfo.rd = new RemoteDesktop();
			if (rdInfo.inherit)
				SetChildToParentSettings(rdInfo);
		}

		private void SaveGroup(RDInfo rdInfo, HostOrGroup editGroupObj)
		{
			if (editGroupObj != null)
			{
				_hostsChanged = true;
				rdInfo.treeNode.Text = editGroupObj.hostName;
				rdInfo.hostName = editGroupObj.hostName;
				rdInfo.comment = editGroupObj.comment;
				Log.Debug("rdInfo.inherit: " + rdInfo.inherit);
				Log.Debug("editGroup.inherit: " + editGroupObj.inherit);
				if (rdInfo.inherit != editGroupObj.inherit)
				{
					if (Dialog.showYesNoDialog(this, "Change group: " + rdInfo.hostName + " and all it's childs settings to it's parents settings?", "Inherit Change!"))
					{
						if (editGroupObj.inherit)
						{
							rdInfo.password = GetParentRdInfo(rdInfo).password;
							rdInfo.readOnly = GetParentRdInfo(rdInfo).readOnly;
						}
						else
						{
							rdInfo.password = Settings.Default.encryptPass ? Encryption.EncryptPlainString(editGroupObj.password) : editGroupObj.password;
							rdInfo.readOnly = editGroupObj.readOnly;
						}
						rdInfo.inherit = editGroupObj.inherit;
						ChangeAllChildrenInheritedPermissions(rdInfo);
					}
				}

				//Change readOnly if its not inherited
				if (rdInfo.readOnly != editGroupObj.readOnly && !editGroupObj.inherit)
				{
					if (Dialog.showYesNoDialog(this, "Change group: " + rdInfo.hostName + " and all it's childs readOnly settings?", "Read Only Change!"))
					{
						rdInfo.readOnly = editGroupObj.readOnly;
						ChangeGroupPermissions(rdInfo);
					}
				}

				//Change password only if its not inherited
				if (rdInfo.password != editGroupObj.password && !editGroupObj.inherit)
				{
					if (Dialog.showYesNoDialog(this, "Change group: " + rdInfo.hostName + " and all it's childs password settings?", "Password Change!"))
					{
						rdInfo.password = editGroupObj.password;
						ChangeGroupPassword(rdInfo);
					}
				}
			}
		}
		public void ResetAllPasswords()
		{
			RDInfo rootNodeRdinfo = GetRdInfo(_rootNode);
			rootNodeRdinfo.password = "ldc";//Encryption.encryptPlainString("ldc");
			ChangeGroupPassword(rootNodeRdinfo);
		}
		/// <summary>
		/// Sets a childs inherited settings to its parents.
		/// </summary>
		/// <param name="ParentRdInfo"></param>
		/// <param name="ChildRdInfo"></param>
		private void SetChildToParentSettings(RDInfo ChildRdInfo)
		{
			RDInfo parentRdinfo = GetParentRdInfo(ChildRdInfo);
			ChildRdInfo.readOnly = parentRdinfo.readOnly;
			ChildRdInfo.password = parentRdinfo.password;
			ChildRdInfo.port = parentRdinfo.port;
			Log.Debug("setChildToParentSettings parentRdinfo.password: " + parentRdinfo.password);
			Log.Debug("setChildToParentSettings childInfo.password: " + ChildRdInfo.password);
		}

		/// <summary>
		/// Changes all child permissions to the group permissions if thier inherit property is set to true.
		/// </summary>
		/// <param name="rdInfo"></param>
		private void ChangeGroupPassword(RDInfo rdInfo)
		{
			//change children nodes in parent group permissions
			foreach (TreeNode child in rdInfo.treeNode.Nodes)
			{
				RDInfo childInfo = (RDInfo)child.Tag;
				if (childInfo.inherit)
				{
					Log.Debug("changeGroupPassword rdInfo.hostName: " + rdInfo.hostName);
					Log.Debug("changeGroupPassword rdInfo.password: " + rdInfo.password);
					childInfo.password = rdInfo.password;
					if (childInfo.type == "group")
						ChangeGroupPassword(childInfo);
				}
			}
		}
		/// <summary>
		/// Changes all child permissions to the group permissions.
		/// </summary>
		/// <param name="rdInfo"></param>
		private void ChangeGroupPermissions(RDInfo rdInfo)
		{
			//change children nodes in parent group permissions
			foreach (TreeNode child in rdInfo.treeNode.Nodes)
			{
				RDInfo childInfo = (RDInfo)child.Tag;
				if (childInfo.inherit)
				{
					Log.Debug("childInfo.hostName: " + childInfo.hostName);
					Log.Debug("childInfo.readOnly: " + childInfo.readOnly);
					childInfo.readOnly = rdInfo.readOnly;
					if (childInfo.type == "group")
						ChangeGroupPermissions(childInfo);
				}
			}
		}

		/// <summary>
		/// Changes all child permissions to the group permissions.
		/// </summary>
		/// <param name="rdInfo"></param>
		private void ChangeAllChildrenInheritedPermissions(RDInfo rdInfo)
		{
			//Change all of the children nodes under the current node to it's parents settings.
			foreach (TreeNode child in rdInfo.treeNode.Nodes)
			{
				RDInfo childInfo = (RDInfo)child.Tag;
				if (childInfo.inherit)
				{
					SetChildToParentSettings(childInfo);
					Log.Debug("childInfo.hostName: " + childInfo.hostName);
					Log.Debug("childInfo.password: " + childInfo.password);
					if (childInfo.type == "group")
						ChangeAllChildrenInheritedPermissions(childInfo);
				}
			}
		}

		/// <summary>
		/// Gets the TreeNodes tag cast to rdInfo.
		/// </summary>
		/// <param name="node"></param>
		public static RDInfo GetRdInfo(TreeNode node)
		{
			return (RDInfo)node.Tag;
		}

		/// <summary>
		/// Gets the parents RdInfo.
		/// </summary>
		/// <param name="ChildRdInfo"></param>
		private RDInfo GetParentRdInfo(RDInfo ChildRdInfo)
		{
			//Find the parent node of the current node.
			TreeNode parent = ChildRdInfo.treeNode.Parent;
			RDInfo parentRdInfo = (RDInfo)parent.Tag;
			return parentRdInfo;
		}

		/// <summary>
		/// Saves the current treeview config to the currently loaded hosts XML
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			SaveHostFile(Settings.Default.hostsFile);
		}

		/// <summary>
		/// Saves the current treeview config to a new hosts XML and saves the file path in the settings
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			string newFile = Dialog.showFileSaveDialog(this, Settings.Default.hostsFile);
			if (newFile != null)
			{
				SaveHostFile(newFile);
				Settings.Default.hostsFile = newFile;
				Settings.Default.Save();
				SetMenuItemTooptips();
			}
		}

		/// <summary>
		/// Deprecated: Locks all the logged in RemoteDesktops.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lockAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lockAll(this.Controls.Find("rd", true));
		}

		/// <summary>
		/// Adds a group to the currently selected treenode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void groupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewTreeNode("group");
		}

		/// <summary>
		/// Adds a host to the currently selected treenode group
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddNewTreeNode("host");
		}

		/// <summary>
		/// Not really sure
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		/// <summary>
		/// Checks if the node exists as a child
		/// </summary>
		/// <param name="original"></param>
		/// <param name="child"></param>
		private bool TreenodeContainsNode(TreeNode node, TreeNode containsNode)
		{
			//RDInfo originalRdInfo = (RDInfo)node.Tag;
			var nodes = node.Nodes;
			foreach (TreeNode childNode in nodes)
			{
				RDInfo childNodeRdInfo = (RDInfo)childNode.Tag;
				Log.Debug(childNodeRdInfo.type);
				Log.Debug(childNodeRdInfo.hostName);
				if (childNode == containsNode)
				{
					return true;
				}
				if (childNodeRdInfo.type.Equals("group"))
				{
					if(TreenodeContainsNode(childNode, containsNode))
					{
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// Changes the mouse effect on drag.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
			{
				var originalNode = (TreeNode) e.Data.GetData("System.Windows.Forms.TreeNode");
				Point dropPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
				TreeNode destinationNode = ((TreeView)sender).GetNodeAt(dropPoint);

				if (originalNode != _rootNode && !TreenodeContainsNode(originalNode, destinationNode))
				{
					e.Effect = DragDropEffects.Move;
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}
		}

		/// <summary>
		/// Changes the mouse effect on drag.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
			{
				var originalNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
				Point dropPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
				TreeNode destinationNode = ((TreeView)sender).GetNodeAt(dropPoint);
				if (originalNode != _rootNode && destinationNode != null && !TreenodeContainsNode(originalNode, destinationNode))
				{
					RDInfo destinationRdInfo = (RDInfo) destinationNode.Tag;
					e.Effect = DragDropEffects.Move;
					//Log.Debug("destinationRdInfo.hostName: " + destinationRdInfo.hostName);
					//Clear placeholders above and below
					hosts.Refresh();
					//Draw the placeholders
					var leftPos = destinationNode.Bounds.Left;
					var rightPos = hosts.Width - 4;
					var g = hosts.CreateGraphics();


					int destinationNodeCenter = destinationNode.Bounds.Y + (destinationNode.Bounds.Height/2);
					//Log.Debug("Over dropPoint.Y: " + dropPoint.Y + " destinationNodeCenter: " + destinationNodeCenter);
					if (destinationRdInfo.type == "group")
					{
						//if (dropPoint.Y < destinationNodeCenter)
						//{
						//	Point[] leftTriangle = new Point[5]
						//	{
						//	new Point(leftPos, destinationNode.Bounds.Top - 4),
						//	new Point(leftPos, destinationNode.Bounds.Top + 4),
						//	new Point(leftPos + 4, destinationNode.Bounds.Y),
						//	new Point(leftPos + 4, destinationNode.Bounds.Top - 1),
						//	new Point(leftPos, destinationNode.Bounds.Top - 5)
						//	};

						//	Point[] rightTriangle = new Point[5]
						//	{
						//	new Point(rightPos, destinationNode.Bounds.Top - 4),
						//	new Point(rightPos, destinationNode.Bounds.Top + 4),
						//	new Point(rightPos - 4, destinationNode.Bounds.Y),
						//	new Point(rightPos - 4, destinationNode.Bounds.Top - 1),
						//	new Point(rightPos, destinationNode.Bounds.Top - 5)
						//	};

						//	g.FillPolygon(System.Drawing.Brushes.Black, leftTriangle);
						//	g.FillPolygon(System.Drawing.Brushes.Black, rightTriangle);

						//	g.DrawLine(new System.Drawing.Pen(Color.Black, 2),
						//		new Point(leftPos, destinationNode.Bounds.Top),
						//		new Point(rightPos, destinationNode.Bounds.Top));
						//}
						//else if (dropPoint.Y > destinationNodeCenter)
						//{
						//	Point[] leftTriangle = new Point[5]
						//	{
						//	new Point(leftPos, destinationNode.Bounds.Bottom - 4),
						//	new Point(leftPos, destinationNode.Bounds.Bottom + 4),
						//	new Point(leftPos + 4, destinationNode.Bounds.Y),
						//	new Point(leftPos + 4, destinationNode.Bounds.Bottom - 1),
						//	new Point(leftPos, destinationNode.Bounds.Bottom - 5)
						//	};

						//	Point[] rightTriangle = new Point[5]
						//	{
						//	new Point(rightPos, destinationNode.Bounds.Bottom - 4),
						//	new Point(rightPos, destinationNode.Bounds.Bottom + 4),
						//	new Point(rightPos - 4, destinationNode.Bounds.Y),
						//	new Point(rightPos - 4, destinationNode.Bounds.Bottom - 1),
						//	new Point(rightPos, destinationNode.Bounds.Bottom - 5)
						//	};

						//	g.FillPolygon(System.Drawing.Brushes.Black, leftTriangle);
						//	g.FillPolygon(System.Drawing.Brushes.Black, rightTriangle);


						//	g.DrawLine(new System.Drawing.Pen(Color.Black, 2),
						//		new Point(leftPos, destinationNode.Bounds.Bottom),
						//		new Point(rightPos, destinationNode.Bounds.Bottom));
						//}
						//else
						//{
						g.DrawRectangle(new System.Drawing.Pen(Color.Black, 2), destinationNode.Bounds);
						//}
					}
					else if (dropPoint.Y <= destinationNodeCenter)
					{
						Point[] leftTriangle = new Point[5]
						{
							new Point(leftPos, destinationNode.Bounds.Top - 4),
							new Point(leftPos, destinationNode.Bounds.Top + 4),
							new Point(leftPos + 4, destinationNode.Bounds.Y),
							new Point(leftPos + 4, destinationNode.Bounds.Top - 1),
							new Point(leftPos, destinationNode.Bounds.Top - 5)
						};

						Point[] rightTriangle = new Point[5]
						{
							new Point(rightPos, destinationNode.Bounds.Top - 4),
							new Point(rightPos, destinationNode.Bounds.Top + 4),
							new Point(rightPos - 4, destinationNode.Bounds.Y),
							new Point(rightPos - 4, destinationNode.Bounds.Top - 1),
							new Point(rightPos, destinationNode.Bounds.Top - 5)
						};

						g.FillPolygon(System.Drawing.Brushes.Black, leftTriangle);
						g.FillPolygon(System.Drawing.Brushes.Black, rightTriangle);

						g.DrawLine(new System.Drawing.Pen(Color.Black, 2),
							new Point(leftPos, destinationNode.Bounds.Top),
							new Point(rightPos, destinationNode.Bounds.Top));
					}
					else if (dropPoint.Y > destinationNodeCenter)
					{
						Point[] leftTriangle = new Point[5]
						{
							new Point(leftPos, destinationNode.Bounds.Bottom - 4),
							new Point(leftPos, destinationNode.Bounds.Bottom + 4),
							new Point(leftPos + 4, destinationNode.Bounds.Y),
							new Point(leftPos + 4, destinationNode.Bounds.Bottom - 1),
							new Point(leftPos, destinationNode.Bounds.Bottom - 5)
						};

						Point[] rightTriangle = new Point[5]
						{
							new Point(rightPos, destinationNode.Bounds.Bottom - 4),
							new Point(rightPos, destinationNode.Bounds.Bottom + 4),
							new Point(rightPos - 4, destinationNode.Bounds.Y),
							new Point(rightPos - 4, destinationNode.Bounds.Bottom - 1),
							new Point(rightPos, destinationNode.Bounds.Bottom - 5)
						};

						g.FillPolygon(System.Drawing.Brushes.Black, leftTriangle);
						g.FillPolygon(System.Drawing.Brushes.Black, rightTriangle);


						g.DrawLine(new System.Drawing.Pen(Color.Black, 2),
							new Point(leftPos, destinationNode.Bounds.Bottom),
							new Point(rightPos, destinationNode.Bounds.Bottom));
					}
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}
		}
		
		/// <summary>
		/// Rearranges the host tree.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_DragDrop(object sender, DragEventArgs e)
		{
			Log.Debug("Enter Dragdrop");

			if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
			{
				Log.Debug("Enter e.Data.GetDataPresent");
				Point dropPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
				TreeNode destinationNode = ((TreeView)sender).GetNodeAt(dropPoint);
				if (destinationNode != null)
				{
					RDInfo destinationRdInfo = (RDInfo) destinationNode.Tag;
					//Get the node being dragged
					var originalNode = (TreeNode) e.Data.GetData("System.Windows.Forms.TreeNode");
					var containsNode = TreenodeContainsNode(originalNode, destinationNode);
					Log.Debug("containsNode: " + containsNode);
					if (originalNode != destinationNode && originalNode != _rootNode && !containsNode)
					{
						RDInfo originalNodeRdInfo = (RDInfo) originalNode.Tag;
						Log.Debug("originalNode.rdInfo.type: " + originalNodeRdInfo.type);
						//If group or root
						if (destinationNode != originalNode.Parent && destinationRdInfo.type != "host" && !destinationRdInfo.readOnly)
						{
							Log.Debug("Enter Drop");
							TreeNode newNodeClone = (TreeNode) originalNode.Clone();
							RDInfo newNodeCloneRdInfo = (RDInfo) newNodeClone.Tag;
							newNodeCloneRdInfo.treeNode = newNodeClone;

							//Reorganize the groups
							//I do not believe this is needed any more but I am not sure.
							//int destinationNodeCenter = destinationNode.Bounds.Y + (destinationNode.Bounds.Height / 2);
							//int insertionIndex = 0;
							//TreeNode destinationParent = destinationNode.Parent;
							//if (dropPoint.Y < destinationNodeCenter)
							//{
							//	insertionIndex = destinationNode.Index;
							//	destinationParent.Nodes.Insert(insertionIndex, originalNode);
							//}
							//else if (dropPoint.Y > destinationNodeCenter)
							//{
							//	insertionIndex = destinationNode.Index + 1;
							//	destinationParent.Nodes.Insert(insertionIndex, originalNode);
							//}
							//else
							//{
							destinationNode.Nodes.Add(newNodeClone);
							destinationNode.ExpandAll();
							//}
							//Remove Original Node
							originalNode.Remove();
							_hostsChanged = true;
						}
						else if (destinationRdInfo.type == "host")
						{
							TreeNode sourceParent = originalNode.Parent;
							RDInfo sourceParentRdInfo = (RDInfo) sourceParent.Tag;
							TreeNode destinationParent = destinationNode.Parent;
							RDInfo destinationParentRdInfo = (RDInfo) destinationParent.Tag;
							if (!destinationParentRdInfo.readOnly)
							{
								if (sourceParentRdInfo.readOnly)
								{
									Dialog.showErrorDialog(this, "Source group is read only.", "Read only!");
								}
								else if (destinationParentRdInfo.readOnly)
								{
									Dialog.showErrorDialog(this, "Destination group is read only.", "Read only!");
								}
								else
								{
									//Point destinationNodePoint = new Point(destinationNode.Bounds.X, destinationNode.Bounds.Y);
									int destinationNodeCenter = destinationNode.Bounds.Y + (destinationNode.Bounds.Height/2);
									sourceParent.Nodes.RemoveAt(originalNode.Index);
									Log.Debug("Drop dropPoint.Y: " + dropPoint.Y + " destinationNodeCenter: " + destinationNodeCenter);
									int insertionIndex = 0;
									if (dropPoint.Y <= destinationNodeCenter)
									{
										insertionIndex = destinationNode.Index;
									}
									else
									{
										insertionIndex = destinationNode.Index + 1;
									}
									destinationParent.Nodes.Insert(insertionIndex, originalNode);
									originalNodeRdInfo.treeNode = originalNode;
									_hostsChanged = true;
								}
							}
						}
						else if (destinationRdInfo.type != "host" && destinationRdInfo.readOnly)
						{
							Dialog.showErrorDialog(this, "Destination group is read only.", "Read only");
						}
					}
				}
				hosts.Refresh();
			}
		}

		/// <summary>
		/// Resizes the group box in group view mode when the main window is resized.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VncPanel_SizeChanged(object sender, EventArgs e)
		{
			Log.Debug("VncPanel_SizeChanged");
			ResizeGroupBoxes();
		}

		/// <summary>
		/// Opens the Settings window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new MvncSettings(HostSet).Show();
		}

		/// <summary>
		/// Check box to make thumbnails readonly or not.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void thumbNailsAreReadOnly_CheckedChanged(object sender, EventArgs e)
		{
			Control[] rds = this.Controls.Find("rd", true);
			togglaAllRdsReadOnly(rds, thumbNailsAreReadOnly.Checked);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_AfterSelect(object sender, TreeViewEventArgs e)
		{
			HostOrGroup hostOrGroupObj = getHostOrGroup();

			//Log.Debug("checkRdInfoChanged(currentRdInfo, hostOrGroupObj): " + checkRdInfoChanged(_currentRdInfo, hostOrGroupObj));
			if (checkRdInfoChanged(_currentRdInfo, hostOrGroupObj))
			{
				if (Dialog.showYesNoDialog(this, "Save changes?", "Save"))
				{
					_hostsChanged = true;
					if (_currentRdInfo.type == "group")
					{
						SaveGroup(_currentRdInfo, hostOrGroupObj);
					}
					else if (_currentRdInfo.type == "host")
					{
						SaveHost(_currentRdInfo, hostOrGroupObj);
					}
				}
			}
			toggleInfoPanel(false);
			_currentRdInfo = (RDInfo)e.Node.Tag;

			if (_currentRdInfo.type == "group")
			{
				infoBox.Text = "Group Info";
				connectButton.Text = "Connect All";
			}
			else if (_currentRdInfo.type == "host")
			{
				infoBox.Text = "Host Info";
				//infoBox.BackgroundImage = icons.Images[currentRdInfo.treeNode.ImageKey];
				connectButton.Text = "Connect";
			}
			connectButton.Enabled = true;
			updateInfoPanel(_currentRdInfo);
		}

		/// <summary>
		/// Can only be used for treenode select changed untill i combine host and group.
		/// </summary>
		/// <param name="rdInfo"></param>
		/// <returns></returns>
		private bool checkRdInfoChanged(RDInfo rdInfo, HostOrGroup editedHostRDInfo)
		{
			Log.Debug("rdInfo.comment: " + rdInfo.comment + ", editedHostRDInfo.comment: " + editedHostRDInfo.comment);
			Log.Debug("rdInfo.port: " + rdInfo.port + ", editedHostRDInfo.port: " + editedHostRDInfo.port);
			Log.Debug("rdInfo.hostName: " + rdInfo.hostName + ", editedHostRDInfo.hostName: " + editedHostRDInfo.hostName);
			Log.Debug("rdInfo.inherit: " + rdInfo.inherit + ", editedHostRDInfo.inherit: " + editedHostRDInfo.inherit);
			Log.Debug("rdInfo.readOnly: " + rdInfo.readOnly + ", editedHostRDInfo.readOnly: " + editedHostRDInfo.readOnly);
			Log.Debug("rdInfo.password: " + rdInfo.password + ", editedHostRDInfo.password: " + editedHostRDInfo.password);
			return !rdInfo.comment.Equals(editedHostRDInfo.comment) ||
					!rdInfo.port.ToString().Equals(editedHostRDInfo.port.ToString()) ||
					!rdInfo.hostName.Equals(editedHostRDInfo.hostName) ||
					!rdInfo.inherit.Equals(editedHostRDInfo.inherit) ||
					!rdInfo.readOnly.Equals(editedHostRDInfo.readOnly) ||
					!rdInfo.password.Equals(editedHostRDInfo.password);
		}

		/// <summary>
		/// Gets the current info from the hostsInfo panel
		/// </summary>
		/// <returns></returns>
		private HostOrGroup getHostOrGroup()
		{
			HostOrGroup hostOrGroupObj = new HostOrGroup()
			{
				hostName = nameTextBox.Text,
				comment = richNotebox.Text,
				inherit = inheritCheckbox.Checked,
				readOnly = readOnlyCheckbox.Checked,
				password = passwordTextBox.Text,
				port = Convert.ToInt32(portTextBox.Text)
			};
			if (_currentRdInfo.type == "group")
			{
				hostOrGroupObj.type = "group";
			}
			else if (_currentRdInfo.type == "host")
			{
				hostOrGroupObj.type = "host";
			}
			//hostOrGroupObj.setType();
			return hostOrGroupObj;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="rdInfo"></param>
		private void updateInfoPanel(RDInfo rdInfo)
		{
			nameTextBox.Text = rdInfo.hostName;
			portTextBox.Text = rdInfo.port.ToString();
			//passwordTextBox.Text = Encryption.ToInsecureString(Encryption.DecryptString(rdInfo.password));
			passwordTextBox.Text = rdInfo.password;
			readOnlyCheckbox.Checked = rdInfo.readOnly;
			inheritCheckbox.Checked = rdInfo.inherit;
			richNotebox.Text = rdInfo.comment;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enabled"></param>
		private void toggleInfoPanel(bool enabled)
		{
			nameTextBox.Enabled = enabled;
			portTextBox.Enabled = enabled;
			passwordTextBox.Enabled = enabled;
			readOnlyCheckbox.Enabled = enabled;
			inheritCheckbox.Enabled = enabled;
			richNotebox.Enabled = enabled;
		}

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////infoPanel listeners////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void connectButton_Click(object sender, EventArgs e)
		{
			RDInfo rdInfo = (RDInfo)hosts.SelectedNode.Tag;
			if (rdInfo.type == "group")
			{
				ConnectAllChildren(rdInfo);
			}
			else if (rdInfo.type == "host")
			{
				ConnectHost(rdInfo);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editButton_Click(object sender, EventArgs e)
		{
			toggleInfoPanel(true);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void richNotebox_KeyPress(object sender, KeyPressEventArgs e)
		{
			//Log.Debug("richNotebox_KeyPress");
			//HostsChanged = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void readOnlyCheckbox_CheckStateChanged(object sender, EventArgs e)
		{
			//Log.Debug("readOnlyCheckbox_CheckStateChanged");
			//HostsChanged = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void inheritCheckbox_CheckStateChanged(object sender, EventArgs e)
		{
			//Log.Debug("inheritCheckbox_CheckStateChanged");
			//HostsChanged = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, EventArgs e)
		{
			RDInfo rdInfo = (RDInfo)hosts.SelectedNode.Tag;
			toggleInfoPanel(false);
			HostOrGroup hostOrGroupObj = getHostOrGroup();
			if (hostOrGroupObj.type == "host")
				SaveHost(rdInfo, hostOrGroupObj);
			else if (hostOrGroupObj.type == "group")
				SaveGroup(rdInfo, hostOrGroupObj);
		}
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////infoPanel listeners////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Hidden not for user use.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void resetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ResetAllPasswords();
		}

		/// <summary>
		/// Copies the last selected hostname to the clipboard.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hosts_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
			{
				Clipboard.SetText(_currentRdInfo.hostName);
			}
		}

		private void resetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void reloadHostsFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_hostsChanged && Dialog.showYesNoDialog(this, "Reload default hosts mvnc file? All changes will be lost!", "Warning!"))
			{
				if(loadXml(Settings.Default.hostsFile))
				{
					_hostsChanged = false;
				}
			}
		}

	}
}
