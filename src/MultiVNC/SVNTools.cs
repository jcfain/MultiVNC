
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


using MultiVNC.Properties;

namespace MultiVNC
{
	class SVNTools
	{
		//private static string sharedBatDir = @"\\crp40ppfs07.medassets.com\XMD21PPSHARE01\J\TCfolders\TestExecuteBats\";
		//private static string localBatDir = @"C:\QA.TestComplete\RCT\Src\TestComplete_Projects\Base\App\RunTestSuitBats\";
		private static string sharedBatDir = @"BatchFiles\";
		private static string localBatDir = @"BatchFiles\";
		//-c -v BatchFiles\
		//Local
		public static void updateSvn(string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = sharedBatDir + "UpdateSVN.bat";
			startInfo.Arguments = userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
			process.WaitForExit();
		}
		public static void cleanSvn(string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = sharedBatDir + "CleanSVN.bat";
			startInfo.Arguments = userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
			process.WaitForExit();
		}
		public static void unlockSvn(string userName, string password)
		{
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
				startInfo.FileName = sharedBatDir + "UnlockSVN.bat";
				startInfo.Arguments = userName + " " + password;
				process.StartInfo = startInfo;
				process.Start();
				process.WaitForExit();
		}
		public static void revertSvn(string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = localBatDir + "RevertSVN.bat";
			startInfo.Arguments = userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
			process.WaitForExit();
		}
		public static void relocateSvn(string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = localBatDir + "RelocateSVN.bat";
			startInfo.Arguments = userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
			process.WaitForExit();
		}

		//Remote
		public static void updateSvnRemote(string hostname, string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " + Settings.Default.password + " -i -d -c -v " + sharedBatDir + "UpdateSVN.bat" + " " + userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
		}
		public static void cleanSvnRemote(string hostname, string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " + Settings.Default.password + " -i -d -c -v " + sharedBatDir + "CleanSVN.bat" + " " + userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
		}
		public static void unlockSvnRemote(string hostname, string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " + Settings.Default.password + " -i -d -c -v " + sharedBatDir + "UnlockSVN.bat" + " " + userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
		}
		public static void revertSvnRemote(string hostname, string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " + Settings.Default.password + " -i -d -c -v " + localBatDir + "RevertSVN.bat" + " " + userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
		}
		public static void relocateSvnRemote(string hostname, string userName, string password)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.FileName = Settings.Default.psexecLoc;
			startInfo.Arguments = @"\\" + hostname + " /accepteula -u " + userName + " -p " + Settings.Default.password + " -i -d -c -v " + localBatDir + "RelocateSVN.bat" + " " + userName + " " + password;
			process.StartInfo = startInfo;
			process.Start();
		}
	}
}
