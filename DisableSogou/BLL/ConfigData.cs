using System;
using MFGLib;

namespace DisableSogou.BLL
{
	class ConfigData
	{
		public const string ProductName = "DisableSogou";

		public string SogouFolder { get; set; }
		public bool Autorun { get; set; }
		public bool Silent { get; set; }
		public bool Valid => FolderHelper.IsImeFolder(SogouFolder);		

		public void Load()
		{
			RegistryHelper reg = new RegistryHelper();
			reg.Open("Abin", ProductName);
			SogouFolder = reg.ReadString("Sogou Directory");
			Silent = reg.ReadBool("Silent");
			reg.Close();

			Autorun = RegistryHelper.CheckAutoStartApp(ProductName) != null;

			if (!Valid)
			{
				SogouFolder = FolderHelper.GetImeFolder() ?? "";
			}
		}

		public void Save()
		{
			RegistryHelper reg = new RegistryHelper();
			reg.Open("Abin", ProductName, true);
			reg.WriteString("Sogou Directory", SogouFolder);
			reg.WriteBool("Silent", Silent);
			reg.Close();

			if (Autorun)
			{
				string param = Silent ? "-silent" : null;
				RegistryHelper.AddAutoStartApp(ProductName, System.Windows.Forms.Application.ExecutablePath, param);
			}
			else
			{
				RegistryHelper.RemoveAutoStartApp(ProductName);
			}
		}		
	}
}
