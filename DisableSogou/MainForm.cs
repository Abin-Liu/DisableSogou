using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DisableSogou.BLL;

namespace DisableSogou
{
	public partial class MainForm : Form
	{		
		ConfigData m_config = new ConfigData();
		AdLocker m_locker = new AdLocker();
		bool m_optionOpened = false;

		public MainForm()
		{
			InitializeComponent();			
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.Visible = false;
			this.ShowInTaskbar = false;

			if (m_config.Valid)
			{
				m_locker.SogouFolder = m_config.SogouFolder;
				m_locker.Lock();
			}
			else
			{
				ShowOption();
			}
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			m_locker.Unlock(true);
		}

		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			ShowOption();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowOption();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}		
		
		void ShowOption()
		{
			if (m_optionOpened)
			{
				return;
			}

			m_optionOpened = true;
			OptionForm form = new OptionForm();
			form.Config = m_config;

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				m_locker.SogouFolder = m_config.SogouFolder;
				m_locker.Unlock(false);
				m_locker.Lock();
			}

			m_optionOpened = false;
		}		
	}
}
