using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpWin32Common;
using MultiVNC.Properties;

namespace MultiVNC
{
	public static class GitTools
	{
		public static void Clone(string hostname)
		{
			Log.Debug("Starting psexec: Clone");
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
			//string SID = getHostSID(hostname);
			//startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + MvncSettings.UserNameDomain + " -p " + getDecryptedPassword() + @" -d -i \\crp40ppfs07.medassets.com\XMD21PPSHARE01\J\TCfolders\Automation\GitBats\GitClone.bat " + MvncSettings.UserName + " " + getDecryptedPassword();
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " +
				                    Encryption.DecryptPlainString(Settings.Default.password) +
				                    @" -d -i -c -v BatchFiles\GitClone.bat " + MvncSettings.UserName + " " +
				                    Encryption.DecryptPlainString(Settings.Default.password);
			process.StartInfo = startInfo;
			process.Start();
			//ExecuteCommand(hostname, "cmd /C shutdown /l /f");
		}
		public static void CheckOut(string hostname)
		{
			Log.Debug("Starting psexec: CheckOut");
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			string userName = MvncSettings.UserNameDomain ?? MvncSettings.UserName;
			//string SID = getHostSID(hostname);
			//startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + MvncSettings.UserNameDomain + " -p " + getDecryptedPassword() + " -d -i " + SID + " cmd / C git checkout HEAD -- " + Settings.Default.gitDirectory;
			//startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + MvncSettings.UserNameDomain + " -p " + getDecryptedPassword() + " -d -i cmd /C git checkout HEAD -- " + Settings.Default.gitDirectory;
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " +
				                    Encryption.DecryptPlainString(Settings.Default.password) +
				                    @" -d -i -c -v BatchFiles\GitCheckout.bat";
			process.StartInfo = startInfo;
			process.Start();
		}
	}
}
