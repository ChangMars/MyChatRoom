using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SignalR.Client.Unit
{
	public class RegAutoStart
	{
		/// 將本程式設為開啟自啟
		/// </summary>
		/// <param name="onOff">自啟開關</param>
		/// <returns></returns>
		public static bool SetMeStart(bool onOff)
		{
			bool isOk;
			string appName = Process.GetCurrentProcess().MainModule.ModuleName;
			Console.WriteLine(appName);
			string appPath = Process.GetCurrentProcess().MainModule.FileName;
			Console.WriteLine(appPath);
			isOk = SetAutoStart(onOff, appName, appPath);
			return isOk;
		}

		/// <summary>
		/// 將應用程式設為或不設為開機啟動
		/// </summary>
		/// <param name="onOff">自啟開關</param>
		/// <param name="appName">應用程式名</param>
		/// <param name="appPath">應用程式完全路徑</param>
		public static bool SetAutoStart(bool onOff, string appName, string appPath)
		{
			bool isOk = true;
			//如果從沒有設為開機啟動設定到要設為開機啟動
			if (!IsExistKey(appName) && onOff)
			{
				Console.WriteLine("if");
				isOk = SelfRunning(onOff, appName, @appPath);
			}
			//如果從設為開機啟動設定到不要設為開機啟動
			else if (IsExistKey(appName) && !onOff)
			{
				Console.WriteLine("else if");
				isOk = SelfRunning(onOff, appName, @appPath);
			}
			return isOk;
		}

		/// <summary>
		/// 判斷註冊鍵值對是否存在，即是否處於開機啟動狀態
		/// </summary>
		/// <param name="keyName">鍵值名</param>
		/// <returns></returns>
		private static bool IsExistKey(string keyName)
		{
			try
			{
				Console.WriteLine("判斷Regedit key是否存在");
				bool _exist = false;
				RegistryKey local = Registry.LocalMachine;
				RegistryKey runs = local.OpenSubKey(@"SOFTWAREMicrosoftWindowsCurrentVersionRun", true);
				if (runs == null)
				{
					RegistryKey key2 = local.CreateSubKey("SOFTWARE");
					RegistryKey key3 = key2.CreateSubKey("Microsoft");
					RegistryKey key4 = key3.CreateSubKey("Windows");
					RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
					RegistryKey key6 = key5.CreateSubKey("Run");
					runs = key6;
				}
				string[] runsName = runs.GetValueNames();
				foreach (string strName in runsName)
				{
					if (strName.ToUpper() == keyName.ToUpper())
					{
						_exist = true;
						Console.WriteLine("Regedit key 是否存在 = {0}", _exist);
						return _exist;
					}
				}
				Console.WriteLine("Regedit key 是否存在 = {0}", _exist);
				return _exist;

			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 獲取Run註冊表key
		/// </summary>
		/// <returns></returns>
		private static RegistryKey getRunKey()
		{
			RegistryKey rk1 = Registry.LocalMachine;
			RegistryKey rk2 = rk1.CreateSubKey("SOFTWARE");
			RegistryKey rk3 = rk2.CreateSubKey("Microsoft");
			RegistryKey rk4 = rk3.CreateSubKey("Windows");
			RegistryKey rk5 = rk4.CreateSubKey("CurrentVersion");
			RegistryKey rk6 = rk5.CreateSubKey("Run");
			return rk6;
		}

		/// <summary>
		/// 寫入或刪除登錄檔鍵值對,即設為開機啟動或開機不啟動
		/// </summary>
		/// <param name="isStart">是否開機啟動</param>
		/// <param name="exeName">應用程式名</param>
		/// <param name="path">應用程式路徑帶程式名</param>
		/// <returns></returns>
		private static bool SelfRunning(bool isStart, string exeName, string path)
		{
			try
			{
				Console.WriteLine("設置Regedit Key");
				RegistryKey local = Registry.LocalMachine;
				//RegistryKey key = local.OpenSubKey(@"SOFTWAREMicrosoftWindowsCurrentVersionRun", true);
				RegistryKey key = getRunKey();
				Console.WriteLine(key.ToString());
				if (key == null)
				{
					Console.WriteLine("key == null");
					key = local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
				}
				//若開機自啟動則新增鍵值對
				if (isStart)
				{
					Console.WriteLine("設置Regedit Key2 {0},{1}", exeName, path);
					key.SetValue(exeName, path);
					key.Close();
				}
				else//否則刪除鍵值對
				{
					Console.WriteLine("刪除Regedit Key");
					string[] keyNames = key.GetValueNames();
					Console.WriteLine(keyNames.Aggregate((i, j) => i + "," + j));
					foreach (string keyName in keyNames)
					{
						if (keyName.ToUpper() == exeName.ToUpper())
						{
							key.DeleteValue(exeName);
							key.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				string ss = ex.Message;
				Console.WriteLine(ss);
				return false;
				//throw;
			}

			return true;
		}
	}
}
