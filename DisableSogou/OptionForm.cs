using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DisableSogou.BLL;

namespace DisableSogou
{
	partial class OptionForm : Form
	{
		public ConfigData Config { get; set; }

		public OptionForm()
		{
			InitializeComponent();
		}

		private void OptionForm_Load(object sender, EventArgs e)
		{
			txtPath.Text = Config.SogouFolder;
			chkAutorun.Checked = Config.Autorun;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description = "请选择搜狗拼音输入法的安装目录：";
			dlg.ShowNewFolderButton = false;
			dlg.SelectedPath = txtPath.Text;
			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}			

			if (!FolderHelper.IsImeFolder(dlg.SelectedPath))
			{
				dlg.SelectedPath = FolderHelper.GetImeFolder(dlg.SelectedPath);
			}			

			txtPath.Text = dlg.SelectedPath;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!FolderHelper.IsImeFolder(txtPath.Text))
			{
				MessageBox.Show(this, "请正确选择搜狗输入法安装目录（SogouPY.ime所在目录）。");
				return;
			}

			Config.SogouFolder = txtPath.Text;
			Config.Autorun = chkAutorun.Checked;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
