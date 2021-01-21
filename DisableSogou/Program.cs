using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace DisableSogou
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			bool createdNew = false;
			Mutex mutex = new Mutex(true, "{43039718-C810-42F4-980E-8C6710422880}", out createdNew);
			if (!createdNew)
			{
				return;
			}

			bool silent = args.Length > 0 && string.Compare(args[0], "-silent") == 0;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm form = new MainForm();
			form.Silent = silent;
			Application.Run(form);
		}		
	}
}
