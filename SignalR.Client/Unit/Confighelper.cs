using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Client.Unit
{
	public class Confighelper
	{
		/// <summary>
		/// 判斷List陣列中的key是否存在Config中
		/// </summary>
		/// <param name="lst"></param>
		/// <returns></returns>
		public List<string> existkeyname(List<string> lst)
		{
			List<string> retlst = lst;
			foreach (string key in ConfigurationManager.AppSettings)
			{
				if (lst.Exists(p => p == key))
				{
					retlst.Remove(key);
				}
			}
			return retlst;
		}

		/// <summary>
		/// 新增Config Key
		/// </summary>
		/// <param name="lst"></param>
		public void additem(List<string> lst)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			foreach (string key in lst)
				config.AppSettings.Settings.Add(key, "");

			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		/// <summary>
		/// 修改Config key 值
		/// </summary>
		/// <param name="key"></param>
		/// <param name="vale"></param>
		public void modifyitem(string key, string vale)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings[key].Value = vale;
			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		/// <summary>
		/// 取得所有Config key 值
		/// </summary>
		/// <returns></returns>
		public Dictionary<string,string> getitemvalue()
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			Dictionary<string, string> dic = new Dictionary<string, string>();
			foreach (string key in ConfigurationManager.AppSettings)
			{
				dic.Add(key, config.AppSettings.Settings[key].Value);
			}
			return dic;
		}

		/// <summary>
		/// 刪除 指定 list key name 
		/// </summary>
		/// <param name="keyname"></param>
		public void delete(List<string> keyname)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			foreach (string key in keyname)
			{
				config.AppSettings.Settings.Remove(key);
			}
			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}
