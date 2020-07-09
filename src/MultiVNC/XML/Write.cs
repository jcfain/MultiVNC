
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


using System.IO;
using System.Xml;
using System.Windows.Forms;
using CSharpWin32Common;

namespace MultiVNC.XML
{
	class WriteXML
	{
		XmlWriter xmlWriter;
		public void All(string path, TreeNode rootNode)
		{
			var pathRoot = Path.GetDirectoryName(path);

			if (pathRoot != null && !Directory.Exists(pathRoot))
			{
				Directory.CreateDirectory(pathRoot);
			}
			xmlWriter = XmlWriter.Create(path);
			writeXMLRoot(rootNode);
			if (rootNode.GetNodeCount(true) > 0)
			{
				foreach (TreeNode node in rootNode.Nodes)
				{
					Log.Debug("node: " + node.Text);
					RDInfo tag = (RDInfo)node.Tag;
					if (tag != null && tag.type == "group")
					{
						writeGroup(tag, node);
					}
					else if (tag != null && tag.type == "host")
					{
						writeHosts(tag);
					}
				}
			}
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}
		private void writeXMLRoot(TreeNode rootNode)
		{
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("hosts");

			RDInfo tag = (RDInfo)rootNode.Tag;
			xmlWriter.WriteAttributeString("name", tag.hostName);
			xmlWriter.WriteAttributeString("port", XmlConvert.ToString(tag.port));
			xmlWriter.WriteAttributeString("comment", tag.comment);
			xmlWriter.WriteAttributeString("readOnly", XmlConvert.ToString(tag.readOnly));
			xmlWriter.WriteAttributeString("inherit", XmlConvert.ToString(tag.inherit));

			//KeepEvery thing above pass because it is so long.
			//Do not encrypt when writing
			xmlWriter.WriteAttributeString("password", tag.password);
			//Used for changing existing xml
			//xmlWriter.WriteAttributeString("password", Encryption.EncryptString(Encryption.ToSecureString(tag.password)));
		}
		private void writeGroup(RDInfo tag, TreeNode node)
		{
			xmlWriter.WriteStartElement("group");
			xmlWriter.WriteAttributeString("name", tag.hostName);
			xmlWriter.WriteAttributeString("port", XmlConvert.ToString(tag.port));
			xmlWriter.WriteAttributeString("comment", tag.comment);
			xmlWriter.WriteAttributeString("readOnly", XmlConvert.ToString(tag.readOnly));
			xmlWriter.WriteAttributeString("inherit", XmlConvert.ToString(tag.inherit));
			//Keep Every thing above pass because it is so long.
			xmlWriter.WriteAttributeString("password", tag.password);
			foreach (TreeNode host in node.Nodes)
			{
				RDInfo recursivTag = (RDInfo)host.Tag;
				if (recursivTag != null && recursivTag.type == "group")
				{
					writeGroup(recursivTag, host);
				}
				else if (recursivTag != null && recursivTag.type == "host")
				{
					writeHosts(recursivTag);
				}
			}
			xmlWriter.WriteEndElement();
		}
		private void writeHosts(RDInfo tag)
		{
			xmlWriter.WriteStartElement("host");
			xmlWriter.WriteAttributeString("name", tag.hostName);
			xmlWriter.WriteAttributeString("port", XmlConvert.ToString(tag.port));
			xmlWriter.WriteAttributeString("comment", tag.comment);
			xmlWriter.WriteAttributeString("readOnly", XmlConvert.ToString(tag.readOnly));
			xmlWriter.WriteAttributeString("inherit", XmlConvert.ToString(tag.inherit));
			//Keep Every thing above pass because it is so long.
			xmlWriter.WriteAttributeString("password", tag.password);
			xmlWriter.WriteEndElement();
		}
	}
}
