using System;
using Microsoft.Win32;

namespace MFGLib
{
	/// <summary>
	/// A helper class providers convenient way to access registry values, the root key is always Registry.CurrentUser
	/// </summary>
	public sealed class RegistryHelper : IDisposable
	{
		#region Public Properties
		/// <summary>
		/// Returns true if the underlying register key is opened, false otherwise
		/// </summary>
		public bool Opened { get { return m_regKey != null; } }

		/// <summary>
		/// Returns true if the underlying register key has write privilege, false if readonly
		/// </summary>
		public bool Writable { get; private set; } = false;
		#endregion

		#region C'tors
		/// <summary>
		/// Default constructor
		/// </summary>
		public RegistryHelper()
		{
			m_rootKey = Registry.CurrentUser;
		}

		/// <summary>
		/// Constructor with root key
		/// </summary>
		public RegistryHelper(RegistryKey rootKey)
		{
			m_rootKey = rootKey;
		}
		#endregion

		#region SubKey operation
		/// <summary>
		/// Open a subkey using path
		/// </summary>
		/// <param name="subKey">Path of the subkey to be opened, e.g. "Software\\Microsoft\\Windows"</param>
		/// <param name="writable">True if the subkey sould be opened writable, false if readonly</param>
		/// <returns>Return true if the subkey is opened successfully, false otherwise</returns>
		public bool Open(string subKey, bool writable = false)
		{
			if (Opened)
			{
				Close();
			}

			try
			{
				if (writable)
				{
					m_regKey = m_rootKey.CreateSubKey(subKey);
				}
				else
				{
					m_regKey = m_rootKey.OpenSubKey(subKey);
				}
			}
			catch
			{
				m_regKey = null;
			}

			if (!Opened)
			{
				return false;
			}

			Writable = writable;
			return true;
		}

		/// <summary>
		/// Open a subkey using company and product name
		/// </summary>
		/// <param name="company">Name of thew company, e.g. "Microsoft"</param>
		/// <param name="product">Name of the product, e.g. "Windows"</param>
		/// <param name="writable">True if the subkey sould be opened writable, false if readonly</param>
		/// <returns></returns>
		public bool Open(string company, string product, bool writable = false)
		{
			return Open(string.Format("Software\\{0}\\{1}", company, product), writable);
		}		

		/// <summary>
		/// Close an opened subkey
		/// </summary>
		public void Close()
		{
			if (Opened)
			{
				m_regKey.Close();
				m_regKey = null;
				Writable = false;
			}
		}

		/// <summary>
		/// Dispose object
		/// </summary>
		public void Dispose()
		{
			Close();
			m_rootKey = null;
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Delete a subkey including all its children
		/// </summary>
		/// <param name="subKey">Path of the subkey to delete</param>
		public void DeleteSubKey(string subKey)
		{
			if (!Writable)
			{
				throw new NullReferenceException("Registry key not writable.");
			}

			try
			{
				m_regKey.DeleteSubKeyTree(subKey);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Delete a key under Registry.CurrentUser
		/// </summary>
		/// <param name="subKey">Path of the subkey to delete</param>
		public static void DeleteKey(string subKey)
		{
			try
			{
				Registry.CurrentUser.DeleteSubKeyTree(subKey);
			}
			catch
			{
			}
		}
		#endregion

		#region Value Access
		/// <summary>
		/// Delete a value under the opened subkey
		/// </summary>
		/// <param name="name">Name of the value to delete</param>
		public void DeleteValue(string name)
		{
			if (!Writable)
			{
				throw new NullReferenceException("Registry key not writable.");
			}

			try
			{
				m_regKey.DeleteValue(name, false);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Read a string value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="defValue">Default value to return if the specified value does not exist</param>
		/// <returns>Value stored in registry</returns>
		public string ReadString(string name, string defValue = "")
		{
			if (!Opened)
			{
				return defValue;
			}

			object result = m_regKey.GetValue(name, defValue);
			if (result == null)
			{
				return null;
			}

			return result.ToString();
		}

		/// <summary>
		/// Read an int32 value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="defValue">Default value to return if the specified value does not exist</param>
		/// <returns>Value stored in registry</returns>
		public int ReadInt(string name, int defValue = 0)
		{
			string value = ReadString(name, "");
			try
			{
				return Convert.ToInt32(value);
			}
			catch
			{
				return defValue;
			}
		}

		/// <summary>
		/// Read a boolean value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="defValue">Default value to return if the specified value does not exist</param>
		/// <returns>Value stored in registry</returns>
		public bool ReadBool(string name, bool defValue = false)
		{
			return ReadInt(name, 0) != 0;
		}

		/// <summary>
		/// Read a double value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="defValue">Default value to return if the specified value does not exist</param>
		/// <returns>Value stored in registry</returns>
		public double ReadDouble(string name, double defValue = 0)
		{
			string value = ReadString(name, "");
			try
			{
				return Convert.ToDouble(value);
			}
			catch
			{
				return defValue;
			}
		}

		/// <summary>
		/// Read a decimal value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="defValue">Default value to return if the specified value does not exist</param>
		/// <returns>Value stored in registry</returns>
		public decimal ReadDecimal(string name, decimal defValue = 0)
		{
			string value = ReadString(name, "");
			try
			{
				return Convert.ToDecimal(value);
			}
			catch
			{
				return defValue;
			}
		}

		/// <summary>
		/// Read a DateTime value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <returns>Value stored in registry</returns>
		public DateTime ReadDateTime(string name)
		{
			string value = ReadString(name, "");
			try
			{
				return Convert.ToDateTime(value);
			}
			catch
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>
		/// Write a string value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		public void WriteString(string name, string value)
		{
			WriteObject(name, value, RegistryValueKind.String);
		}

		/// <summary>
		/// Write an int value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		public void WriteInt(string name, int value)
		{
			WriteObject(name, value, RegistryValueKind.DWord);
		}

		/// <summary>
		/// Write a boolean value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		public void WriteBool(string name, bool value)
		{
			WriteInt(name, value ? 1 : 0);
		}

		/// <summary>
		/// Write a double value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		public void WriteDouble(string name, double value)
		{
			WriteString(name, value.ToString());
		}

		/// <summary>
		/// Write a decimal value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		public void WriteDecimal(string name, decimal value)
		{
			WriteString(name, value.ToString());
		}

		/// <summary>
		/// Write the date component of a DateTime value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		/// <param name="format">Format for convertion</param>
		public void WriteDate(string name, DateTime value, string format = "yyyy-MM-dd")
		{
			WriteString(name, value.ToString(format));
		}

		/// <summary>
		/// Write a DateTime value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		/// <param name="format">Format for convertion</param>
		public void WriteDateTime(string name, DateTime value, string format = "yyyy-MM-dd HH:mm:ss")
		{
			WriteString(name, value.ToString(format));
		}

		/// <summary>
		/// Write an object value to registry
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <param name="value">Value to write into the registry</param>
		/// <param name="type">RegistryValueKind to store</param>
		private void WriteObject(string name, object value, RegistryValueKind type = RegistryValueKind.String)
		{
			if (!Writable)
			{
				throw new NullReferenceException("Registry key not writable.");
			}

			m_regKey.SetValue(name, value, type);
		}
		#endregion

		#region Static members
		/// <summary>
		/// Add an application to Software\\Microsoft\\Windows\\CurrentVersion\\Run
		/// </summary>
		/// <param name="name">Application name</param>
		/// <param name="executablePath">Application executable file path</param>
		/// <param name="currentUser">Only apply to current user</param>
		/// <param name="parameters">Parameters</param>
		public static void AddAutoStartApp(string name, string executablePath, string parameters = null, bool currentUser = true)
		{
			if (!string.IsNullOrEmpty(parameters))
			{
				executablePath = string.Format("\"{0}\" {1}", executablePath, parameters);
			}

			using (RegistryHelper reg = new RegistryHelper(currentUser ? Registry.CurrentUser : Registry.LocalMachine))
			{
				if (reg.Open("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
				{
					reg.WriteString(name, executablePath);
				}				
			}
		}

		/// <summary>
		/// Remove an existing application from Software\\Microsoft\\Windows\\CurrentVersion\\Run
		/// </summary>
		/// <param name="name">Application name</param>
		/// <param name="currentUser">Only apply to current user</param>
		public static void RemoveAutoStartApp(string name, bool currentUser = true)
		{
			using (RegistryHelper reg = new RegistryHelper(currentUser ? Registry.CurrentUser : Registry.LocalMachine))
			{
				if (reg.Open("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
				{
					reg.DeleteValue(name);
				}					
			}
		}

		/// <summary>
		/// Checks whether an application has entry in Software\\Microsoft\\Windows\\CurrentVersion\\Run
		/// </summary>
		/// <param name="name">Application name to be checked</param>
		/// <param name="currentUser">Only check in current user</param>
		/// <returns>Returns executable path if exists, null otherwise.</returns>
		public static string CheckAutoStartApp(string name, bool currentUser = true)
		{
			string result = null;
			using (RegistryHelper reg = new RegistryHelper(currentUser ? Registry.CurrentUser : Registry.LocalMachine))
			{
				if (reg.Open("Software\\Microsoft\\Windows\\CurrentVersion\\Run", false))
				{
					result = reg.ReadString(name, null);
				}
			}

			if (result == "")
			{
				result = null;
			}

			return result;
		}

		#endregion

		#region Private Members
		private RegistryKey m_regKey = null;
		private RegistryKey m_rootKey = null;
		#endregion
	}
}
