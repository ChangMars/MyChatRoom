using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Client
{
	public class SystemCofing
	{
		public static readonly List<string> lstConfigkey
			= new List<string>() { "empno", "name", "token", "host", "hubname", "verifytokenapi", "loginapi","onlineapi" };
		public static readonly string Host = "https://ylcwss01.yulon-motor.com.tw/";
		public static readonly string HubName = "chathub";
		public static readonly string VerifyTokenApi = "api/authenticate/showuserrolesbyauth/";
		public static readonly string LoginApi = "api/authenticate/login/";
		public static readonly string OnlineApi = "api/notify/getonlinelist/";
	}
}
