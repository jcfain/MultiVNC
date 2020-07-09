
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


using System.Drawing;
using System.Windows.Forms;
using VncSharp;

namespace MultiVNC
{

	/// <summary>
	/// Holds all the info about every RemoteDesktop cluster and Treenode.
	/// Attached to:
	/// rdInfo.enlarge.Tag
	/// rdInfo.close.Tag
	/// rdInfo.retry.Tag
	/// rdInfo.ctrlAltDel.Tag
	/// rdInfo.rd.Tag
	/// rdInfo.treeNode.Tag
	/// </summary>
	public class RDInfo
	{
		public RemoteDesktop rd { get; set; }
		public Panel panel { get; set; }
		public string hostName { get; set; }
		public int port { get; set; }
		public string password { get; set; }
		public Point rdLocation { get; set; }
		public Size rdSize { get; set; }
		public Size rdSizeEnlarged { get; set; }
		public Label errorLabel { get; set; }
		public Point errorLabelLoc { get; set; }
		public Label hostLabel { get; set; }
		public Point hostLabelLoc { get; set; }
		public Button retry { get; set; }
		public Point retryLoc { get; set; }
		public Button ctrlAltDel { get; set; }
		public Point ctrlAltDelLoc { get; set; }
		public Button enlarge { get; set; }
		public Point enlargeLoc { get; set; }
		public Button close { get; set; }
		public Point closeLoc { get; set; }
		public string comment { get; set; }
		public string type { get; set; }
		public TreeNode treeNode { get; set; }
		public bool readOnly { get; set; }
		public bool inherit { get; set; }
		public FlowLayoutPanel groupFlowPanel { get; set; }
		public GroupBox groupPanel { get; set; }
	}
}
