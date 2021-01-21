using System;
using MFGLib;

namespace DisableSogou.BLL
{
	class ConfigData
	{
		public const string ProductName = "DisableSogou";

		public string SogouFolder
		{
			get
			{
				return m_sogouFolder;
			}

			set
			{
				if (string.Compare(m_sogouFolder, value) == 0)
				{
					return;
				}

				m_sogouFolder = value;

				RegistryHelper reg = new RegistryHelper();
				reg.Open("Abin", ProductName, true);
				reg.WriteString("Sogou Directory", m_sogouFolder);
				reg.Close();
			}
		}

		public bool Autorun
		{
			get
			{
				return m_autorun;
			}

			set
			{
				if (m_autorun == value)
				{
					return;
				}

				m_autorun = value;
				if (m_autorun)
				{
					RegistryHelper.AddAutoStartApp(ProductName, System.Windows.Forms.Application.ExecutablePath);
				}
				else
				{
					RegistryHelper.RemoveAutoStartApp(ProductName);
				}
			}
		}

		public bool Valid => FolderHelper.IsImeFolder(m_sogouFolder);

		private string m_sogouFolder;
		private bool m_autorun;

		public ConfigData()
		{
			RegistryHelper reg = new RegistryHelper();
			reg.Open("Abin", ProductName);
			m_sogouFolder = reg.ReadString("Sogou Directory");
			reg.Close();			

			m_autorun = RegistryHelper.CheckAutoStartApp(ProductName) != null;
			CheckFolder();
		}
		
		private void CheckFolder()
		{
			if (Valid)
			{
				return;
			}

			string folder = FolderHelper.GetImeFolder() ?? "";
			if (folder != null)
			{
				SogouFolder = folder;
			}
		}
	}
}
