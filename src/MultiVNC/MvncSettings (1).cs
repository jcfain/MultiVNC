
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

//TODO: Fix retry in enlarged mode makes enlarged view readonly.
using MultiVNC.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CSharpWin32Common;

namespace MultiVNC
{


	[System.Runtime.InteropServices.Guid("DE250CC5-FA67-45E7-9447-0D727FDBC5F4")]
	public partial class MvncSettings : Form
	{
		public static readonly string UserNameDomain = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
		public static readonly string UserName = Environment.UserName;

		private readonly Dictionary<string, List<RDInfo>> _hostSet;

		private List<RDInfo> _hostList; 

		private static bool _passwordIsValid;
		

		public MvncSettings(Dictionary<string, List<RDInfo>> hostSet)
		{
			InitializeComponent();
			_hostSet = hostSet;
			this.Icon = Resources.My_Workgroup_icon;

			pingStatus.Text = "Hosts Ping Time. Click to browse the host shares.";
			PopulateGroups(_hostSet);
			//PopulateHosts(_hostSet["All"]);
			toolTipsSettings.SetToolTip(createStartupShortcuts, "This calls CreateStartupShortCuts.bat create a shortcut to getSid.bat and presscontrol.exe on the specified host.");
			
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.StartPosition = FormStartPosition.CenterParent;
			LoadSettings();
		}

		private void LoadSettings()
		{
			toolTipsSettings.SetToolTip(revert, @"Calls BatchFiles\RevertSVN.bat on the selected remote machine.");
			toolTipsSettings.SetToolTip(update, @"Calls BatchFiles\UpdateSVN.bat on the selected remote machine.");
			toolTipsSettings.SetToolTip(unlock, @"Calls BatchFiles\UnlockSVN.bat on the selected remote machine.");
			toolTipsSettings.SetToolTip(clean, @"Calls BatchFiles\CleanSVN.bat on the selected remote machine.");
			toolTipsSettings.SetToolTip(CloneButton, @"Calls BatchFiles\GitClone.bat on the selected remote machine.");
			toolTipsSettings.SetToolTip(CheckOutButton, @"Calls BatchFiles\GitCheckout.bat on the selected remote machine.");
			toolTipsSettings.SetToolTip(logOut, "Calls 'cmd /C shutdown /l /f' on the selected remote machine.");
			toolTipsSettings.SetToolTip(lockMachine, "Calls 'cmd /C rundll32.exe user32.dll, LockWorkStation' on the selected remote machine.");
			toolTipsSettings.SetToolTip(RunBatchFile, "Allows you to select a bat file to run on the selected host via psexec.");
			toolTipsSettings.SetToolTip(VpnCheckBox, "Enables the Ping of an internal host before network operations to make sure you are on a VPN.");
			toolTipsSettings.SetToolTip(VpnCheckHostNameTextBox, "Internal host to ping before network operations to make sure you are on a VPN.");
			toolTipsSettings.SetToolTip(VerboseLoggingCheckBox, "If checked, the app will output all logs to a file in the MultiVNC directory.\nThis option is not persistent");
			toolTipsSettings.SetToolTip(VncGroupLayoutCheckBox, "If checked, the Vnc thumbnails will be sorted by thier respective groups in panels.");

			VpnCheckBox.Checked = Settings.Default.VpnCheck;
			VpnCheckHostNameTextBox.Text = Settings.Default.internalHost;
			VpnCheckTestButton.Enabled = VpnCheckBox.Checked;
			askToLogout.Checked = Settings.Default.askToLogout;
			PsexEcLocTextBox.Text = Settings.Default.psexecLoc;
			VncGroupLayoutCheckBox.Checked = Settings.Default.VncPanelGroupDisplay;
			//Apply Enabled false always goes last
			ApplyButton.Enabled = false;
		}

		private void PopulateGroups(Dictionary<string, List<RDInfo>> hostSet)
		{
			Groups.Items.Clear();
			if (hostSet.Count > 0)
			{
				hostSet.Reverse();
				Groups.Items.Add("All");
				foreach (var groupkv in hostSet)
				{
					Log.Message("groupkv: "+ groupkv.Key);
					if (groupkv.Key != "All")
					{
						Groups.Items.Add(groupkv.Key);
					}
				}

				Groups.SelectedItem = "All";
			}
			else
			{
				Groups.Items.Add("No Groups to display.");
			}
		}
		private void PopulateHosts(List<RDInfo> hostList)
		{
			Hosts.Items.Clear();
			Log.Debug("hostList.Count: " +hostList.Count);
			if (hostList.Count > 0)
			{
				Hosts.Items.Add("All");
				foreach (RDInfo hostObj in hostList)
				{
					Hosts.Items.Add(hostObj.hostName);
				}

				Hosts.SelectedItem = "All";
			}
			else
			{
				Hosts.Items.Add("No Hosts to display.");
			}
		}

		//Tools section/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//public static bool CheckForValidInputDir(string path)
		//{
		//	if (!Directory.Exists(path))
		//		return false;

		//	string[] files = Directory.GetFiles(path);
		//	for (int i = 0; i < files.Length; i++)
		//	{
		//		if (!files[i].Contains("json"))
		//		{
		//			MessageBox.Show("Chosen directory needs jsons only.");
		//			return false;
		//		}
		//	}
		//	return true;
		//}
		public static bool CheckForValidHostFile(string path)
		{
			if (path != "" && File.Exists(path) && path.EndsWith(".mvnc"))
			{
				return true;
			}
			Dialog.showErrorDialog(ActiveForm, "Chosen hosts file does not have the extension: 'mvnc' or does not exist.", "Invalid Path!");
			return false;

		}
		private void ApplySettings()
		{
			Settings.Default.askToLogout = askToLogout.Checked;
			Settings.Default.VpnCheck = VpnCheckBox.Checked;
			Settings.Default.internalHost = VpnCheckHostNameTextBox.Text;
			Settings.Default.VncPanelGroupDisplay = VncGroupLayoutCheckBox.Checked;

			if (PsexEcLocTextBox.Text.ToLower().EndsWith("psexec.exe") && File.Exists(PsexEcLocTextBox.Text))
			{
				Settings.Default.psexecLoc = PsexEcLocTextBox.Text;
			}
			else if(!PsexEcLocTextBox.Text.ToLower().EndsWith("psexec.exe"))
			{
				Dialog.showErrorDialog(this, "PsExec location should end with 'psexec.exe' (case insensitive).", "Invalid Path!");
			}
			else if (!File.Exists(PsexEcLocTextBox.Text))
			{
				Dialog.showErrorDialog(this, "PsExec location path does not exist in an absolute or relative path.", "Invalid Path!");
			}
			if (Settings.Default.internalHost.Equals(String.Empty))
			{
				VpnCheckBox.Checked = false;
				Settings.Default.VpnCheck = false;
			}
			Settings.Default.Save();
			ApplyButton.Enabled = false;
		}

		public static bool PsexecExists()
		{
			return File.Exists(Settings.Default.psexecLoc);
		}
		///// <summary>
		///// Converts a JSON dynamic hosts object to a string array.
		///// </summary>
		//public static string[] ConvertDynHostArrToStringArr(dynamic dynHostArr)
		//{
		//	List<string> Temp = new List<string>();
		//	for (int i = 0; i < dynHostArr.Count; i++)
		//		if (new Pinger(dynHostArr[i].hostName).success)//hostsArr[i].info.isGood)
		//			Temp.Add(dynHostArr[i].hostName);
		//	return Temp.ToArray<string>();
		//}
		/// <summary>
		/// <summary>
		/// Sets the password from the users input
		/// </summary>
		/// <returns></returns>
		public static bool GetPassword()
		{
			if (!_passwordIsValid)
			{
				Log.Debug("Settings.Default.password: " + Settings.Default.password);
				//Check to see if it has been saved.
				if (Settings.Default.password.Equals("password"))
				{
					//Password Encrypted in dialog.
					var packet = Dialog.ShowLoginPrompt(ActiveForm, "System Password", "Login");
					if (packet.pass == "cancelDialog")
					{
						return false;
					}
					//Check to see if password is good.
					Log.Debug("System.Environment.MachineName: " + System.Environment.MachineName);
					_passwordIsValid = AccountMgmt.IsPassWordValid(UserName, packet.pass);
					if (_passwordIsValid)
					{
						Settings.Default.password = packet.pass;
						if (packet.savePass)
							Settings.Default.Save();
						return true;
					}
					else
					{
						Dialog.showErrorDialog(VncViewer.ActiveForm, "Invalid Password.", "ERROR");
						Settings.Default.password = "password";
						Settings.Default.Save();
						return false;
					}

				}
				//Make sure it hasnt changed;
				_passwordIsValid = AccountMgmt.IsPassWordValid(UserName, Settings.Default.password);
				if (_passwordIsValid)
				{
					return true;
				}
				//Its not good any more, delete it from registry
				Dialog.showErrorDialog(VncViewer.ActiveForm, "Invalid Password.", "ERROR");
				Settings.Default.password = "password";
				Settings.Default.Save();
				return false;
			}
			return true;

		}
		//Hosts section////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void GetPings()
		{
			//Dialog.showPleaseWaitForm(this, "Please wait while I get the hosts ping times.");
			//Application.DoEvents();
			dataGridView1.Rows.Clear();
			List<RDInfo> selectedGroup = _hostSet["All"];
			foreach (RDInfo hostObj in selectedGroup)
			{
				//if (Dialog.cancel)
				//    break;
				var pinger = new Pinger(hostObj.hostName);
				string[] row;
				if (pinger.reply != null)
				{
					row = new string[] {hostObj.hostName, pinger.reply.RoundtripTime + "ms"};
				}
				else
				{
					row = new string[] {hostObj.hostName, "FAILED"};
				}

				dataGridView1.Rows.Add(row);
				if (pinger.success)
				{
					dataGridView1.Rows[selectedGroup.IndexOf(hostObj)].Cells[1].Style.BackColor = Color.Green;
				}
				else
				{
					dataGridView1.Rows[selectedGroup.IndexOf(hostObj)].Cells[1].Style.BackColor = Color.Red;
				}
			}
			//Dialog.hideDialog();
		}

		private void SendAndExecuteBatOnRemote(string hostName, string batLocation, string userName, string password, string batArgs = "")
		{
			if (PsexecExists())
			{
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
				startInfo.FileName = Settings.Default.psexecLoc;
				if (batArgs == "")
				{
					startInfo.Arguments = @"\\" + hostName + " /accepteula -u " + userName + " -p " + password + " -i -d -c -v " +
					                      batLocation;
				}
				else
				{
					startInfo.Arguments = @"\\" + hostName + " /accepteula -u " + userName + " -p " + password + " -i -d -c -v " +
					                      batLocation + " " + batArgs;
				}
				process.StartInfo = startInfo;
				process.Start();
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}
		///GUI listeners////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		#region GUI listeners
		private void tab_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (RemoteRunTabControl.SelectedIndex)
			{
				case 0:
					Log.Debug("Selected tab: " + RemoteRunTabControl.SelectedIndex);
					break;
				case 1:
					Log.Debug("Selected tab: " + RemoteRunTabControl.SelectedIndex);
					if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
						Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.", "VPN Check Failed!");
					break;
				case 2:
					Log.Debug("Selected tab: " + RemoteRunTabControl.SelectedIndex);
					//if (isOnInternalNetwork())
					//	GetPings();
					//else
					//	Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.", "VPN Check Failed!");
					break;
			}

		}


		private void PsexecLocButton_Click(object sender, EventArgs e)
		{
			string location = Dialog.showOpenFileDialog(ActiveForm, Environment.CurrentDirectory,
				"Psexec.exe|psexec.exe");
			if (location != Environment.CurrentDirectory && location.ToLower().EndsWith("psexec.exe"))
			{
				PsexEcLocTextBox.Text = location;
			}
		}

		private void PsexEcLocTextBox_TextChanged(object sender, EventArgs e)
		{
			ApplyButton.Enabled = true;
		}

		private void refreshAll_Click(object sender, EventArgs e)
		{
			if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
			{
				Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.", "VPN Check Failed!");
			}
			else
			{
				GetPings();
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex > -1)
			{
				Log.Debug("dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value: " + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
				//startInfo.FileName = @"\\crp40ppfs07.medassets.com\XMD21PPSHARE01\J\TCfolders\Automation\LabMachines\" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + ".vnc";
				startInfo.FileName = @"\\" + dataGridView1.Rows[e.RowIndex].Cells[0].Value+"\\";
				process.StartInfo = startInfo;
				process.Start();
			}
		}

		private void update_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								SVNTools.updateSvnRemote(hostObj.hostName, userName,
									Encryption.DecryptPlainString(Settings.Default.password));
							}
						}
						else
						{
							SVNTools.updateSvnRemote(Hosts.SelectedItem.ToString(), userName,
								Encryption.DecryptPlainString(Settings.Default.password));
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}

		private void revert_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								SVNTools.revertSvnRemote(hostObj.hostName, userName, Encryption.DecryptPlainString(Settings.Default.password));
							}
						}
						else
						{
							SVNTools.revertSvnRemote(Hosts.SelectedItem.ToString(), userName,
								Encryption.DecryptPlainString(Settings.Default.password));
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}

		private void unlock_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								SVNTools.unlockSvnRemote(hostObj.hostName, userName, Encryption.DecryptPlainString(Settings.Default.password));
							}
						}
						else
						{
							SVNTools.unlockSvnRemote(Hosts.SelectedItem.ToString(), userName,
								Encryption.DecryptPlainString(Settings.Default.password));
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}

		private void clean_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								SVNTools.cleanSvnRemote(hostObj.hostName, userName,
									Encryption.DecryptPlainString(Settings.Default.password));
							}
						}
						else
						{
							SVNTools.cleanSvnRemote(Hosts.SelectedItem.ToString(), userName,
								Encryption.DecryptPlainString(Settings.Default.password));
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}

		private void relocate_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								SVNTools.relocateSvnRemote(hostObj.hostName, userName,
									Encryption.DecryptPlainString(Settings.Default.password));
							}
						}
						else
						{
							SVNTools.relocateSvnRemote(Hosts.SelectedItem.ToString(), userName,
								Encryption.DecryptPlainString(Settings.Default.password));
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}

		private void lockMachine_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								RemoteCommand.LockScreen(hostObj.hostName);
							}
						}
						else
						{
							RemoteCommand.LockScreen(Hosts.SelectedItem.ToString());
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}
		private void logOut_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								RemoteCommand.Logout(hostObj.hostName);
							}
						}
						else
						{
							RemoteCommand.Logout(Hosts.SelectedItem.ToString());
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
			//"shutdown /l /f" 
		}
		
		private void closeButton_Click(object sender, EventArgs e)
		{
			if (ApplyButton.Enabled)
			{
				bool saveChanges = Dialog.showYesNoDialog(ActiveForm, "Save changes?", "Changes Detected!");
				if (saveChanges)
				{
					ApplySettings();
				}
			}
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			if (ApplyButton.Enabled)
			{
				bool saveChanges = Dialog.showYesNoDialog(ActiveForm, "Save changes?", "Changes Detected!");
				if (saveChanges)
				{
					ApplySettings();
				}
			}
			this.Close();
		}


		//This is to be used if we need to create shortcuts on all the hosts start up folder.
		//it will create a shortcut to press control and getSID.bat
		//Will hide the control once executed.
		private void createStartupShortcuts_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								RemoteCommand.CreateStartupShortcutsOnHosts(hostObj.hostName, userName,
									Encryption.DecryptPlainString(Settings.Default.password));
							}
						}
						else
						{
							RemoteCommand.CreateStartupShortcutsOnHosts(Hosts.SelectedItem.ToString(), userName,
								Encryption.DecryptPlainString(Settings.Default.password));
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}

		}
		
		private void applyButton_Click(object sender, EventArgs e)
		{
			ApplySettings();
		}


		private void resetGeneral_Click(object sender, EventArgs e)
		{
			if (Dialog.showYesNoDialog(this, "Are you sure you wish to reset all General Settings including any saved passwords?\nSettings will NOT be saved until you click 'Apply'", "Confirm"))
			{
				Settings.Default.askToLogout = false;
				askToLogout.Checked = Settings.Default.askToLogout;
				Settings.Default.psexecLoc = @"PSTools\PsExec.exe";
				PsexEcLocTextBox.Text = Settings.Default.psexecLoc;
				Settings.Default.password = "password";
				Settings.Default.VpnCheck = false;
				VpnCheckBox.Checked = false;
				VpnCheckHostNameTextBox.Text = String.Empty;
				Settings.Default.internalHost = String.Empty;
				ApplyButton.Enabled = true;
			}
		}

		//private void mailTest_Click(object sender, EventArgs e)
		//{
		//	String mBody = "Your QA assisted test is complete for the script:<br>"
		//				+ "(Screen shot attached)<br><br>"
		//				+ "<table>"
		//				+ "<tr><td>Host name:</td><td>" + System.Environment.MachineName + "</td></tr>"
		//				+ "<tr><td>Environment:</td><td>Environment</td></tr>"
		//				+ "<tr><td>Facility Id:</td><td>FacilityName</td></tr>"
		//				+ "<tr><td>Facility:</td><td>CBO</td></tr>"
		//				+ "<tr><td>Sub Facility:</td><td>Facility</td></tr>"
		//				+ "</table><br><br>";
		//	//Mailer.send(VNCViewer.userName + "@medassets.com", VNCViewer.userName + "@medassets.com", "Success", mBody);
		//}

		private void askToLogout_CheckedChanged(object sender, EventArgs e)
		{
			ApplyButton.Enabled = true;
		}



		private void CloneButton_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
				{
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				}
				else
				{
					if (GetPassword())
					{
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								GitTools.Clone(hostObj.hostName);
							}
						}
						else
						{
							GitTools.Clone(Hosts.SelectedItem.ToString());
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}

		private void CheckOutButton_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
						"VPN Check Failed!");
				else
				{
					if (GetPassword())
					{
						if (Hosts.SelectedItem.ToString() == "All")
						{
							foreach (RDInfo hostObj in _hostList)
							{
								GitTools.CheckOut(hostObj.hostName);
							}
						}
						else
						{
							GitTools.CheckOut(Hosts.SelectedItem.ToString());
						}
					}
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}
		private void RunBatchFile_Click(object sender, EventArgs e)
		{
			if (PsexecExists())
			{
				string fileLocation = Dialog.showOpenFileDialog(ActiveForm, Environment.CurrentDirectory, "Bat files (*.bat)|*.bat");
				if (fileLocation.ToLower().EndsWith("bat") && File.Exists(fileLocation))
				{
					if (!RemoteCommand.IsOnInternalNetwork(Settings.Default.internalHost))
					{
						Dialog.showErrorDialog(this, "You are not on the internal network.\nPlease connect and try again.",
							"VPN Check Failed!");
					}
					else
					{
						if (GetPassword())
						{
							string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
							Log.Message("Settings.Default.password: " + Settings.Default.password);
							string args = Dialog.ShowGetTextPrompt(ActiveForm, "Batch Arguments?", "Enter your batch file arguments below");
							if (args == "cancelDialog" || Regex.Match(args, "/s+").Success)
							{
								args = "";
							}
							if (Hosts.SelectedItem.ToString() == "All")
							{
								foreach (RDInfo hostObj in _hostList)
								{
									SendAndExecuteBatOnRemote(hostObj.hostName, fileLocation, userName,
										Encryption.DecryptPlainString(Settings.Default.password), args);
								}
							}
							else
							{
								SendAndExecuteBatOnRemote(Hosts.SelectedItem.ToString(), fileLocation, userName,
									Encryption.DecryptPlainString(Settings.Default.password), args);
							}
						}
					}
				}
				else if (fileLocation == Environment.CurrentDirectory)
				{
					//Do Nothing
				}
				else
				{
					Dialog.showErrorDialog(ActiveForm, "Chosen file does not have the extension 'bat' or does not exist.",
						"Invalid batch file");
				}
			}
			else
			{
				Dialog.showErrorDialog(Form.ActiveForm,
					"Can not fins PsExec.exe. Some functions will not be available until specified in Settings.", "Missing PsExec.");
			}
		}

		private void VpnCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ApplyButton.Enabled = true;
			VpnCheckHostNameTextBox.Enabled = VpnCheckBox.Checked;
			if (!VpnCheckBox.Checked)
			{
				VpnCheckTestButton.Enabled = false;
			}
		}

		private void VpnCheckHostNameTextBox_TextChanged(object sender, EventArgs e)
		{
			ApplyButton.Enabled = true;
			if (VpnCheckHostNameTextBox.Text != String.Empty)
			{
				VpnCheckTestButton.Enabled = true;
			}
			else
			{
				VpnCheckTestButton.Enabled = false;
			}
		}

		private void VpnCheckTestButton_Click(object sender, EventArgs e)
		{
			VpnCheckTestButton.Enabled = false;
			if (!new Pinger(VpnCheckHostNameTextBox.Text).success)
			{
				Dialog.showErrorDialog(ActiveForm,
					"Invalid host or you are not corrently connected to the VPN. Connect and try again.", "Ping Failed!");
			}
			else
			{
				Dialog.showInfoDialog(ActiveForm, "Ping Success!", "Success!");
			}
			VpnCheckTestButton.Enabled = true;
		}

		private void VerboseLoggingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Log.VerboseLogging = VerboseLoggingCheckBox.Checked;
		}

		private void VncGroupLayoutCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			ApplyButton.Enabled = true;
		}

		private void Groups_SelectedIndexChanged(object sender, EventArgs e)
		{
			_hostList = _hostSet[Groups.Text];
			PopulateHosts(_hostList);
		}

		private void VncGroupLayoutCheckBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (VncGroupLayoutCheckBox.Checked)
			{
				Dialog.showInfoDialog(ActiveForm, "This is an experemental feature. Use at your own risk.\nRequires MultiVNC Restart for this setting to take place.", "Warning!");
			}
		}

		#endregion region
		
	}
}
