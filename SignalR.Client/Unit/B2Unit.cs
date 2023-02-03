using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Windows.Media.AppBroadcasting;
using Windows.UI.Xaml;
using Windows.Web.Syndication;

namespace SignalR.Client.Unit
{
	public class B2Unit
	{
		private string _accountId = "";
		private string _accountKey = "";

		private string _bucketId = "";
		private string _bucketName = "";
		private string _apiUrl = "";
		private string _authorizationToken = "";
		private string _downloadUrl = "";
		private string _s3ApiUrl = "";

		private string _uploadauthorizationToken = "";
		private string _uploadurl = "";

		private IWebProxy _proxy = null;
		private ICredentials _proxy_credentials = null;

		public B2Unit(string accountId,string accountKey)
		{
			_accountId = accountId;
			_accountKey = accountKey;
		}

		/// <summary>
		/// 設定Proxy
		/// </summary>
		/// <param name="proxy"></param>
		/// <param name="port"></param>
		/// <param name="account"></param>
		/// <param name="password"></param>
		public void settingProxy(string proxy, int port, string account = "", string password = "")
		{
			_proxy = new WebProxy(proxy, port);
			if (account != "" || password != "")
			{
				_proxy_credentials = new NetworkCredential(account, password);
				_proxy.Credentials = _proxy_credentials;
			}
		}

		/// <summary>
		/// 獲取b2帳戶驗證訊息
		/// </summary>
		/// <param name="isProxy">是否啟用proxy</param>
		/// <returns></returns>
		public bool authorize_account(bool isProxy = false)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.backblazeb2.com/b2api/v2/b2_authorize_account");
			String credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(_accountId + ":" + _accountKey));
			webRequest.Headers.Add("Authorization", "Basic " + credentials);
			webRequest.ContentType = "application/json; charset=utf-8";
			if (isProxy && _proxy != null)	
				webRequest.Proxy = _proxy;
			WebResponse response = (HttpWebResponse)webRequest.GetResponse();
			String json = new StreamReader(response.GetResponseStream()).ReadToEnd();
			Dictionary<string,string> dic = ConvertEx.ToDictionary(json);
			_apiUrl = dic["apiUrl"];
			_authorizationToken = dic["authorizationToken"];
			_downloadUrl = dic["downloadUrl"];
			_s3ApiUrl = dic["s3ApiUrl"];
			_bucketId = dic["allowed.bucketId"];
			_bucketName = dic["allowed.bucketName"];
			response.Close();
			Console.WriteLine(json);
			return true;
		}

		/// <summary>
		/// 獲取上傳檔案url
		/// </summary>
		/// <param name="isProxy">是否啟用proxy</param>
		/// <returns></returns>
		public string getuploadurl(bool isProxy = false)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_apiUrl + "/b2api/v2/b2_get_upload_url");
			string body = "{\"bucketId\":\"" + _bucketId + "\"}";
			var data = Encoding.UTF8.GetBytes(body);
			webRequest.Method = "POST";
			webRequest.Headers.Add("Authorization", _authorizationToken);
			webRequest.ContentType = "application/json; charset=utf-8";
			webRequest.ContentLength = data.Length;
			if (isProxy && _proxy != null)
				webRequest.Proxy = _proxy;
			using (var stream = webRequest.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
				stream.Close();
			}
			WebResponse response = (HttpWebResponse)webRequest.GetResponse();
			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			Dictionary<string, string> dic = ConvertEx.ToDictionary(responseString);
			_uploadauthorizationToken = dic["authorizationToken"];
			_uploadurl = dic["uploadUrl"];
			response.Close();
			Console.WriteLine(responseString);
			return responseString;
		}

		/// <summary>
		/// 上傳檔案至b2
		/// </summary>
		/// <param name="filepath">檔案路徑</param>
		/// <param name="isProxy">是否啟用proxy</param>
		/// <returns></returns>
		public async Task<string> uploadfiletob2(string filepath, bool isProxy = false)
		{
			return await Task.Run(() => {
				try
				{
					String fileName = Path.GetFileName(filepath); //Desired name for the file
					String ext = Path.GetExtension(fileName);
					String contentType = MimeMapping.GetMimeMapping(fileName); //Type of file i.e. image/jpeg, audio/mpeg...
					String sha1Str = ""; //Sha1 verification for the file
			
					// Read the file into memory and take a sha1 of the data.
					byte[] bytes = File.ReadAllBytes(filepath);
					
					if((bytes.Length / 1024 / 1024) >= 20)
					{
						MessageBox.Show(string.Format("檔案大小:{0}MB > 20MB，如要上傳請付費!!!", bytes.Length / 1024 / 1024));
						return string.Format("檔案上傳失敗!原因:{0}", "超過基本上傳大小");
					}
					
					using (FileStream fs = new FileStream(filepath, FileMode.Open))
					using (BufferedStream bs = new BufferedStream(fs))
					{
						using (SHA1Managed sha1 = new SHA1Managed())
						{
							byte[] hash = sha1.ComputeHash(bs);
							StringBuilder formatted = new StringBuilder(2 * hash.Length);
							foreach (byte b in hash)
							{
								formatted.AppendFormat("{0:X2}", b);
							}
							sha1Str = formatted.ToString();
						}
					}

					// NOTE: Loss of precision. You may need to change this code if the file size is larger than 32-bits.
					//FileInfo fileInfo = new FileInfo(filepath);
					//SHA1 sha1 = SHA1.Create();
					//byte[] hashData = sha1.ComputeHash(bytes, 0, (int)fileInfo.Length);
					//MessageBox.Show(string.Format("檔案訊息大小:{0}Byte", hashData.Length));
					//StringBuilder sb = new StringBuilder();
					//foreach (byte b in hashData)
					//{
					//	sb.Append(b.ToString("x2"));
					//}
					//sha1Str = sb.ToString();


					//排除HttpWebRequest發送請求的發生“基礎連接已關閉" 錯誤問題
					//System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
					//System.Net.ServicePointManager.DefaultConnectionLimit = 256;
					//ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
					// 这里设置了协议类型。
					//ServicePointManager.SecurityProtocol =SecurityProtocolType.Tls12;// (SecurityProtocolType)3072;// SecurityProtocolType.Tls1.2; 
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //(SecurityProtocolType)768 | (SecurityProtocolType)3072
					ServicePointManager.CheckCertificateRevocationList = true;
					ServicePointManager.DefaultConnectionLimit = 100;
					ServicePointManager.Expect100Continue = false;

					// Send over the wire
					HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_uploadurl);
					webRequest.Timeout = -1;
					webRequest.Method = "POST";
					webRequest.Headers.Add("Authorization", _uploadauthorizationToken);
					webRequest.Headers.Add("X-Bz-File-Name", Guid.NewGuid().ToString() + ext);
					webRequest.Headers.Add("X-Bz-Content-Sha1", sha1Str);
					webRequest.Headers.Add("X-Bz-Info-Author", "unknown");
					webRequest.Headers.Add("X-Bz-Server-Side-Encryption", "AES256");
			
					webRequest.ContentType = contentType;
					if (isProxy && _proxy != null)
						webRequest.Proxy = _proxy;
					using (var stream = webRequest.GetRequestStream())
					{
						stream.Write(bytes, 0, bytes.Length);
						stream.Close();
					}
					WebResponse response = (HttpWebResponse)webRequest.GetResponse();
					var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
					Dictionary<string, string> dic = ConvertEx.ToDictionary(responseString);
					string downloadurl = _s3ApiUrl.Insert(_s3ApiUrl.IndexOf("s3"), _bucketName + ".") + "/" + dic["fileName"];
					response.Close();
					Console.WriteLine(responseString);
					return downloadurl;
				}
				catch (Exception e)
				{
					//MessageBox.Show(e.Message);
					Console.WriteLine(e.Message);
					return string.Format("檔案上傳失敗!原因:{0}", e.Message);
				}
			});
		}

	}
}
