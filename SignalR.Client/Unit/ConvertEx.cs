using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SignalR.Client.Unit
{
	public static class ConvertEx
	{
		/// <summary>JSON To Dic</summary>
		public static Dictionary<string, string> ToDictionary(string jsonSrting)
		{
			if (jsonSrting == null || jsonSrting == "")
				return null;
			var jsonObject = JObject.Parse(jsonSrting);
			var jTokens = jsonObject.Descendants().Where(p => !p.Any());
			var tmpKeys = jTokens.Aggregate(new Dictionary<string, string>(),
				(properties, jToken) =>
				{
					properties.Add(jToken.Path, jToken.ToString());
					return properties;
				});
			return tmpKeys;
		}

		/// <summary>Dic To JSON string</summary>
		public static string MyDictionaryToJson(Dictionary<string, string> dict)
		{
			var entries = dict.Select(d =>
				string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
			return "{" + string.Join(",", entries) + "}";
		}

	}
}
