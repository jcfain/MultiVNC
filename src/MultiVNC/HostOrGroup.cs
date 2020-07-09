
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

namespace MultiVNC
{
	public class HostOrGroup
	{
		public string hostName { get; set; }
		public int port { get; set; }
		public string password { get; set; }
		public string comment { get; set; }
		public bool readOnly { get; set; }
		public bool inherit { get; set; }
		public string type { get; set; }

		//public void setType()
		//{
		//	if (type == "host")
		//		VNCViewer.hostList.Add(this);
		//	else if (type == "group")
		//		VNCViewer.groupList.Add(this);
		//}
	}
}
