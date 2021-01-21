using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DisableSogou.BLL
{
    public static class FolderHelper
    {
		public static bool IsImeFolder(string folder)
		{
			return !string.IsNullOrEmpty(folder) && File.Exists(folder + "\\SogouPY.ime");
		}

		public static string GetImeFolder(string baseFolder = null)
		{
			if (string.IsNullOrEmpty(baseFolder))
			{
				baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\SogouInput";
			}

			if (!Directory.Exists(baseFolder))
			{
				return null;
			}

			string maxVer = null;
			DirectoryInfo[] folderList = new DirectoryInfo(baseFolder).GetDirectories();
			foreach (DirectoryInfo di in folderList)
			{
				if (!IsImeFolder(di.FullName))
				{
					continue;
				}

				if (maxVer == null || CompareVersions(maxVer, di.Name) < 0)
				{
					maxVer = di.Name;
				}
			}

			if (maxVer == null)
			{
				return null;
			}

			return baseFolder + '\\' + maxVer;
		}

		// 子目录格式 10.0.0.4300
		static readonly Regex VersionRegex = new Regex(@"\d+");
		static int[] VersionToArray(string version)
		{
			int[] results = new int[4];
			MatchCollection mc = VersionRegex.Matches(version);
			if (mc.Count != 4)
			{
				return results;
			}

			for (int i = 0; i < 4; i++)
			{
				results[i] = Convert.ToInt32(mc[i].Value);
			}

			return results;
		}

		static int CompareVersions(string v1, string v2)
		{
			int[] lhs = VersionToArray(v1);
			int[] rhs = VersionToArray(v2);
			for (int i = 0; i < 4; i++)
			{
				if (lhs[i] > rhs[i])
				{
					return 1;
				}

				if (lhs[i] < rhs[i])
				{
					return -1;
				}
			}

			return 0;
		}
	}
}
