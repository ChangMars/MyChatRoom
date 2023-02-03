using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalR.Client
{
	static class Program
	{
		/// <summary>
		/// 應用程式的主要進入點。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// dll 和 exe 打包教學 http://www.luofenming.com/show.aspx?id=ART2021042800001
			//AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
			//{//注意WindowsFormsApplication1 这个是主程序的命名空间
			//	string resourceName = "SignalR." + new AssemblyName(args.Name).Name + ".dll";
			//	using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			//	{
			//		Byte[] assemblyData = new Byte[stream.Length];
			//		stream.Read(assemblyData, 0, assemblyData.Length);
			//		return Assembly.Load(assemblyData);
			//	}
			//};

			/**
			 * * 当前用户是管理员的时候，直接启动应用程序
			 * * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
			 */
			//获得当前登录的Windows用户标示
			System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
			System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
			//判断当前登录用户是否为管理员
			//if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
			{
				//如果是管理员，则直接运行
				Application.Run(new MainForm());
			}
			//else 
			//{
			//	//创建启动对象
			//	System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			//	startInfo.UseShellExecute = true;
			//	startInfo.WorkingDirectory = Environment.CurrentDirectory;
			//	startInfo.FileName = Application.ExecutablePath;
			//	//设置启动动作,确保以管理员身份运行
			//	startInfo.Verb = "runas";
			//	try
			//	{
			//		System.Diagnostics.Process.Start(startInfo);
			//	}
			//	catch
			//	{
			//		return;
			//	}
			//	//退出
			//	Application.Exit();
			//}
		}
	}
}
