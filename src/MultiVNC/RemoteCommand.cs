using System;
using System.Net.Sockets;
using CSharpWin32Common;
using MultiVNC.Properties;

namespace MultiVNC
{
	public static class RemoteCommand
	{
		public static void LockScreen(string hostName)
		{
			string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			//string SID = GetHostSid(hostName);
			startInfo.Arguments = @"\\" + hostName + " /accepteula -u " + userName + " -p " +
					                Encryption.DecryptPlainString(Settings.Default.password) + " -d -i cmd /C rundll32.exe user32.dll, LockWorkStation";
			process.StartInfo = startInfo;
			process.Start();
			//ExecuteCommand(hostName, "cmd /C rundll32.exe user32.dll, LockWorkStation");
		}

		public static void Logout(string hostname)
		{
			//Log.Debug("Starting psexec: logout");
			//string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
			//System.Diagnostics.Process process = new System.Diagnostics.Process();
			//System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			//startInfo.FileName = Settings.Default.psexecLoc;
			////string SID = getHostSID(hostname);
			//startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " + Encryption.DecryptPlainString(Settings.Default.password) + " -d -i cmd /C shutdown /l /f";
			//process.StartInfo = startInfo;
			//process.Start();
			RemoteMachineMgmt.LogOff(hostname, Environment.UserName, Encryption.DecryptPlainString(Settings.Default.password));
		}
		public static void CreateStartupShortcutsOnHosts(string hostname, string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " + password + " -i -d -c -v BatchFiles/CreateStartupShortCuts.bat";
			process.StartInfo = startInfo;
			process.Start();
		}
		public static bool IsOnInternalNetwork(string host)
		{
			if (!Settings.Default.VpnCheck)
			{
				return true;
			}
			if (new Pinger(host).success)
			{
				return true;
			}
			return false;
		}
		/// 
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <returns></returns>
		public static bool IsPortAcceptingConnections(string host, int port)
		{
			using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				try
				{
					socket.Connect(host, port);
					Log.Message("Connection test Succeeded: Host: " + host + ":" + port);
					return true;
				}
				catch (SocketException ex)
				{
					if (ex.SocketErrorCode == SocketError.ConnectionRefused)
					{
						Dialog.showErrorDialog(VncViewer.ActiveForm, "SocketError.ConnectionRefused\nHost: " + host + ":" + port, "SocketError");
					}
					else
					{
						Dialog.showErrorDialog(VncViewer.ActiveForm, "SocketError: " + ex.Message + "\nCode: " + ex.SocketErrorCode + "\nHost:" + host + ":" + port, "SocketError");
					}
					return false;
				}
			}
		}
		//public static string GetHostSid(string hostname)
		//{
		//	if (File.Exists(Settings.Default.hostStatus + "\\SID\\" + hostname + "_SID.txt"))
		//	{
		//		StreamReader reader = new StreamReader(Settings.Default.hostStatus + "\\SID\\" + hostname + "_SID.txt");
		//		return reader.ReadLine();
		//	}
		//	else
		//		return "0";
		//}
	}
}
