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
		static void Main()
		{
			bool createdNew = false;
			Mutex mutex = new Mutex(true, "{43039718-C810-42F4-980E-8C6710422880}", out createdNew);
			if (createdNew)
			{				
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}			
		}		
	}
}
