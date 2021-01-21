using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DisableSogou.BLL
{
	class AdLocker
	{
		private ADData[] DataList =
		{
			//new SougouJunk("SGTool", "SGTool.exe"),
			new ADData("SogouCloud", "SogouCloud.exe"),
			new ADData("SohuNews", "SohuNews.exe"),
		};

		public string SogouFolder { get; set; }

		public void Lock(bool backup)
		{
			foreach (ADData data in DataList)
			{
				if (backup)
				{
					data.Backup(SogouFolder);
				}

				data.Lock(SogouFolder);
			}
		}

		public void Unlock(bool restore)
		{
			foreach (ADData data in DataList)
			{
				data.Unlock();
				
				if (restore)
				{
					data.Restore(SogouFolder);
				}
			}
		}		
	}

	class ADData
	{
		public string ProcessName { get; set; }
		public string FileName { get; set; }
		public bool Locked => m_fs != null;
		private FileStream m_fs;

		public ADData(string processName, string fileName)
		{
			ProcessName = processName;
			FileName = fileName;
		}

		public void Lock(string sogouFolder)
		{
			Unlock();

			if (!KillProcessByName(ProcessName))
			{
				throw new Exception("无法杀死进程：" + ProcessName);
			}			

			string filePath = sogouFolder + '\\' + FileName;

			try
			{
				File.Delete(filePath);
			}
			catch
			{
				throw new Exception("无法删除文件：" + filePath);
			}

			try
			{
				m_fs = File.OpenWrite(filePath);
			}
			catch
			{
				m_fs = null;
				throw new Exception("无法锁定文件：" + filePath);
			}
		}

		public void Unlock()
		{
			if (m_fs == null)
			{
				return;
			}

			try
			{
				m_fs.Close();
			}
			catch
			{				
			}
			
			m_fs = null;			
		}

		public void Backup(string sogouFolder)
		{
			string filePath = sogouFolder + '\\' + FileName;
			if (!File.Exists(filePath))
			{
				return;
			}

			FileInfo fi = new FileInfo(filePath);
			if (fi.Length < 1000)
			{
				return;
			}

			string backupDir = sogouFolder + "\\BackupFiles";
			try
			{
				Directory.CreateDirectory(backupDir);
				File.Copy(filePath, backupDir + '\\' + FileName, true);
			}
			catch
			{
			}
		}

		public void Restore(string sogouFolder)
		{
			string backupDir = sogouFolder + "\\BackupFiles";
			try
			{
				Directory.CreateDirectory(backupDir);
				File.Copy(backupDir + '\\' + FileName, sogouFolder + '\\' + FileName, true);
			}
			catch
			{
			}
		}

		static bool KillProcessByName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return true;
			}

			try
			{
				foreach (Process proc in Process.GetProcessesByName(name))
				{
					proc.Kill();
				}
			}
			catch
			{
				return false;
			}

			return true;
		}
	}
}
