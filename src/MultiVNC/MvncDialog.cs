
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
using System.Windows.Forms;
using CSharpWin32Common;

namespace MultiVNC
{
	public static class MvncDialog
	{
		public static HostOrGroup ShowNewHostOrGroupPrompt(Form parent, string FormCaption, RDInfo rdInfo, RDInfo parentRDInfo)
		{
			HostOrGroup newHost = new HostOrGroup();
			Form dlg = new Form();
			dlg.ControlBox = false;
			dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
			dlg.Width = 230;
			dlg.Height = 230;
			dlg.Text = FormCaption;
			dlg.StartPosition = FormStartPosition.CenterParent;
			Label nameHeading = new Label();
			nameHeading.AutoSize = true;
			nameHeading.Left = 20;
			nameHeading.Top = 10;
			if(rdInfo.type == "host")
				nameHeading.Text = "Hostname \\ IP";
			else
				nameHeading.Text = "Group Name";
			TextBox name = new TextBox();
			name.Left = 20;
			name.Top = 25;
			name.Width = 125;
			name.Text = rdInfo.hostName;
			Label portHeading = new Label();
			portHeading.AutoSize = true;
			portHeading.Left = 145;
			portHeading.Top = 10;
			portHeading.Text = "Port";
			TextBox port = new TextBox();
			port.Left = 145;
			port.Top = 25;
			port.Width = 50;
			port.Enabled = !rdInfo.inherit;
			Label passwordHeading = new Label();
			passwordHeading.AutoSize = true;
			passwordHeading.Left = 20;
			passwordHeading.Top = 50;
			passwordHeading.Text = "Password";
			TextBox password = new TextBox();
			password.Left = 20;
			password.Top = 65;
			password.Width = 175;
			password.PasswordChar = '*';
			password.Enabled = !rdInfo.inherit;
			Label commentHeading = new Label();
			commentHeading.AutoSize = true;
			commentHeading.Left = 20;
			commentHeading.Top = 90;
			commentHeading.Text = "Note";
			TextBox comment = new TextBox();
			comment.Left = 20;
			comment.Top = 105;
			comment.Width = 175;
			comment.Text = rdInfo.comment;
			CheckBox readOnly = new CheckBox();
			readOnly.Left = 25;
			readOnly.Top = 130;
			readOnly.Width = 100;
			readOnly.Text = "Read Only";
			readOnly.Checked = rdInfo.readOnly;
			readOnly.Enabled = !rdInfo.inherit;
			CheckBox inHerit = new CheckBox();
			inHerit.Left = 130;
			inHerit.Top = 130;
			inHerit.Width = 100;
			inHerit.Text = "Inherit";
			inHerit.Checked = rdInfo.inherit;
			inHerit.CheckedChanged += (sender, evt) =>
			{
				readOnly.Enabled = !inHerit.Checked;
				password.Enabled = !inHerit.Checked;
				port.Enabled = !inHerit.Checked;
			};
			Button cancelButton = new Button();
			dlg.CancelButton = cancelButton;
			cancelButton.Text = "Cancel";
			cancelButton.Left = 20;
			cancelButton.Top = 155;
			cancelButton.Click += (sender, evt) =>
			{
				newHost = null;
				dlg.Close();
			};
			Button Confirm = new Button();
			dlg.AcceptButton = Confirm;
			Confirm.Text = "Confirm";
			Confirm.Left = 120;
			Confirm.Top = 155;
			Confirm.Click += (sender, evt) =>
			{
				if (name.Text != String.Empty)
				{
					newHost.hostName = name.Text;
					newHost.comment = comment.Text;
					newHost.readOnly = readOnly.Checked;
					newHost.port = Convert.ToInt32(port.Text);
					newHost.inherit = inHerit.Checked;
					newHost.password = password.Text;
					dlg.Close();
				}
				else
					Dialog.showErrorDialog(dlg, "Name/Ip must not be empty", "Missing Required");
			};
			if (rdInfo.inherit)
			{
				password.Text = parentRDInfo.password;
				port.Text = parentRDInfo.port.ToString();
				readOnly.Checked = parentRDInfo.readOnly;
			}
			else
			{
				password.Text = rdInfo.password;
				port.Text = rdInfo.port.ToString();
				readOnly.Checked = rdInfo.readOnly;
			}
			dlg.Controls.Add(Confirm);
			dlg.Controls.Add(cancelButton);
			dlg.Controls.Add(nameHeading);
			dlg.Controls.Add(passwordHeading);
			dlg.Controls.Add(password);
			dlg.Controls.Add(commentHeading);
			dlg.Controls.Add(comment);
			dlg.Controls.Add(name);
			dlg.Controls.Add(portHeading);
			dlg.Controls.Add(port);
			dlg.Controls.Add(readOnly);
			dlg.Controls.Add(inHerit);
			dlg.ShowDialog(parent);
			name.Focus();
			return newHost;
		}

	}
}