
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using VncSharp;
using CSharpWin32Common;

namespace MultiVNC.XML
{
	class ReadXml
	{
		private readonly string _path;
		readonly TreeNode _rootNode;

		//Create a dictionary of group host pairs///////////////
		readonly List<RDInfo> _allHostList = new List<RDInfo>();
		private string _currentGroup;
		////////////////////////////////////////////////////////
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="rootNode"></param>
		public ReadXml(string path, TreeNode rootNode)
		{
			this._path = path;
			this._rootNode = rootNode;
		}
		public bool LoadXml()
		{
			try
			{
				XmlDocument xmldoc = new XmlDocument();
				FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read);
				xmldoc.Load(fs);
				var xmlnode = xmldoc.ChildNodes[1];
				Log.Debug("RootNode name: " + xmlnode.Attributes["name"].Value);
				_rootNode.Tag = new RDInfo()
				{
					hostName = xmlnode.Attributes["name"].Value,
					comment = xmlnode.Attributes["comment"].Value,
					treeNode = _rootNode,
					type = "group",
					readOnly = XmlConvert.ToBoolean(xmlnode.Attributes["readOnly"].Value),
					port = XmlConvert.ToInt32(xmlnode.Attributes["port"].Value),
					inherit = XmlConvert.ToBoolean(xmlnode.Attributes["inherit"].Value),
					password = xmlnode.Attributes["password"].Value
				};
				Log.Debug("((RDInfo)rootNode.Tag).password: " + ((RDInfo)_rootNode.Tag).password);
				_rootNode.Text = xmlnode.Attributes["name"].Value;
				AddNode(xmlnode, _rootNode);
				VncViewer.HostSet.Add("All", _allHostList);
				fs.Close();
				return true;
			}
			catch(Exception e)
			{
				return false;
			}
		}
		private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
		{
			if (inXmlNode.HasChildNodes)
			{
				XmlNode xNode;
				TreeNode tNode;
				XmlNodeList nodeList = inXmlNode.ChildNodes;
				for (int i = 0; i <= nodeList.Count - 1; i++)
				{
					xNode = inXmlNode.ChildNodes[i];
					string name = xNode.Attributes["name"].Value;
					TreeNode treeNode = new TreeNode(xNode.Attributes["name"].Value);
					if (xNode.Name == "group")
					{
						_currentGroup = name;
						
						treeNode.ImageIndex = 2;
						treeNode.SelectedImageIndex = 2;
						GroupBox groupPanel = new GroupBox();
						groupPanel.Name = "groupPanel";
						FlowLayoutPanel groupFlowPanel = new FlowLayoutPanel();
						groupFlowPanel.AutoScroll = true;
						groupFlowPanel.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);
						groupFlowPanel.Dock = DockStyle.Fill;
						groupFlowPanel.Name = name;
						groupPanel.Controls.Add(groupFlowPanel);
						treeNode.Tag = new RDInfo()
						{
							hostName = name,
							comment = xNode.Attributes["comment"].Value,
							treeNode = treeNode,
							type = "group",
							readOnly = XmlConvert.ToBoolean(xNode.Attributes["readOnly"].Value),
							inherit = XmlConvert.ToBoolean(xNode.Attributes["inherit"].Value),
							port = XmlConvert.ToInt32(inXmlNode.Attributes["port"].Value),
							password = xNode.Attributes["password"].Value,
							groupPanel = groupPanel,
							groupFlowPanel = groupFlowPanel
						};
					}
					inTreeNode.Nodes.Add(treeNode);
					tNode = inTreeNode.Nodes[i];
					//Add the next host or group
					AddNode(xNode, tNode);
				}
			}
			else if (inXmlNode.Name == "host")
			{
				inTreeNode.Tag = new RDInfo()
				{
					hostName = inXmlNode.Attributes["name"].Value,
					comment = inXmlNode.Attributes["comment"].Value,
					treeNode = inTreeNode,
					rd = new RemoteDesktop(),
					type = "host",
					readOnly = XmlConvert.ToBoolean(inXmlNode.Attributes["readOnly"].Value),
					inherit = XmlConvert.ToBoolean(inXmlNode.Attributes["inherit"].Value),
					port = XmlConvert.ToInt32(inXmlNode.Attributes["port"].Value),
					password = inXmlNode.Attributes["password"].Value
				};
				//Add host to global hostlist
				_allHostList.Add((RDInfo) inTreeNode.Tag);

				//Add to a dictionary of group host pairs///////////////
				if (!VncViewer.HostSet.ContainsKey(_currentGroup))
				{
					VncViewer.HostSet.Add(_currentGroup, new List<RDInfo>());
				}
				VncViewer.HostSet[_currentGroup].Add((RDInfo)inTreeNode.Tag);
				////////////////////////////////////////////////////////
				inTreeNode.ImageIndex = 0;
				inTreeNode.SelectedImageIndex = 0;
				inTreeNode.Text = inXmlNode.Attributes["name"].Value;
			}
			else
			{
				//Group does not have children or root
				inTreeNode.Tag = new RDInfo() { hostName = inXmlNode.Attributes["name"].Value, 
												comment = inXmlNode.Attributes["comment"].Value, 
												treeNode = inTreeNode, type = "group",
												readOnly = XmlConvert.ToBoolean(inXmlNode.Attributes["readOnly"].Value),
												inherit = XmlConvert.ToBoolean(inXmlNode.Attributes["inherit"].Value),
												port = XmlConvert.ToInt32(inXmlNode.Attributes["port"].Value),
												password = inXmlNode.Attributes["password"].Value
				};
				inTreeNode.ImageIndex = 2;
				inTreeNode.SelectedImageIndex = 2;
				inTreeNode.Text = inXmlNode.Attributes["name"].Value;
			}
		}
	}
}
