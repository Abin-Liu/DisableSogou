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
		public bool Silent { get; set; }
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

			m_config.Load();
			if (m_config.Valid)
			{
				notifyIcon1.Visible = !Silent;
				m_locker.SogouFolder = m_config.SogouFolder;

				try
				{
					m_locker.Lock(true);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				ShowOption();
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				m_locker.Unlock(true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
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
			Win32API.Window.SetForegroundWindow(Handle);

			if (m_optionOpened)
			{
				return;
			}

			m_optionOpened = true;
			OptionForm form = new OptionForm();
			form.Config = m_config;

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				notifyIcon1.Visible = !m_config.Silent;
				m_locker.SogouFolder = m_config.SogouFolder;				

				try
				{
					m_locker.Lock(true);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			m_optionOpened = false;
		}		
	}
}
