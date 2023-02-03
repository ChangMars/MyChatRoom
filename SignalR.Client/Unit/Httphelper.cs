using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace SignalR.Client.Unit
{
	public class Httphelper
	{
		/// <summary>Call API by Post</summary>
		public async Task<string> HttpPost(string url, string body, string token = "", string proxy = "", int proxyport = 0, string proxyuser = "", string proxypassword = "")
		{
			return await Task.Run(() => {
				try
				{
					//ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
					Encoding encoding = Encoding.UTF8;
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
					request.Proxy = null;
					request.Method = "POST";
					if (proxy != "" && proxyport != 0)
					{
						IWebProxy pp = new WebProxy(proxy, proxyport);
						if (proxyuser != "")
						{
							ICredentials credentials = new NetworkCredential(proxyuser, proxypassword);
							pp.Credentials = credentials;
						}
						request.Proxy = pp;
					}
					if (token != "")
						request.Headers.Add("Authorization", "Bearer " + token);
					//request.Timeout = 60000;
					//request.Accept = "text/html, application/xhtml+xml, */*";
					request.ContentType = "application/json";

					byte[] buffer = encoding.GetBytes(body);
					request.ContentLength = buffer.Length;
					request.GetRequestStream().Write(buffer, 0, buffer.Length);
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
					{
						return reader.ReadToEnd();
					}
				}
				catch (Exception e)
				{
					//MessageBox.Show(e.Message);
					Console.WriteLine(e.Message);
					return null;
				}
			});
		}

		/// <summary>Call API by Get</summary>
		public async Task<string> HttpGet(string url,string token = "", string proxy = "", int proxyport = 0, string proxyuser = "", string proxypassword = "")
		{
			return await Task.Run(() =>
			{
				try
				{
					//ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
					Encoding encoding = Encoding.UTF8;
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
					if (proxy != "" && proxyport != 0)
					{
						IWebProxy pp = new WebProxy(proxy, proxyport);
						if (proxyuser != "")
						{
							ICredentials credentials = new NetworkCredential(proxyuser, proxypassword);
							pp.Credentials = credentials;
						}
						request.Proxy = pp;
					}
					if (token != "")
						request.Headers.Add("Authorization", "Bearer " + token);
					request.Method = "GET";
					request.Timeout = 5000;
					request.Accept = "text/html, application/xhtml+xml, */*";
					request.ContentType = "application/json";
					Stopwatch ss = new Stopwatch();
					ss.Start();
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					ss.Stop();
					Console.WriteLine(ss.ElapsedMilliseconds);
					using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
					{
						return reader.ReadToEnd();
					}
				}
				catch (Exception ex)
				{
					//MessageBox.Show(e.Message);
					Console.WriteLine(ex.Message);
					return null;
				}
			});
		}
	}
}
