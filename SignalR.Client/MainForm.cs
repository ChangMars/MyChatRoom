using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using SignalR.Client.SubForm;
using SignalR.Client.Unit;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using Windows.Foundation.Collections;

namespace SignalR.Client
{
	public partial class MainForm : Form
	{
		#region Private 
		private int curr_x; // 拖拉視窗使用
		private int curr_y; // 拖拉視窗使用
		private bool isWndMove; // 拖拉視窗使用

		private Confighelper confighelper = new Confighelper();
		private string _host = "";
		private string _hubname = "";
		private string _token = "";
		private string _empno = "";
		private string _name = "";
		private string _verifyapi = "";
		private string _onlineapi = "";
		private string _proxy = "";
		private string _proxyauth = "";
		private string _proxypassword = "";
		private string _cloudinarykey = "";
		private string _cloudinarysecret = "";
		private string _cloudinarycloud = "";
		private string _b2query = "";
		private string _b2key = "";
		private bool _bisnotify = true;
		private HubConnection connection;
		private List<Color> lstConBK = new List<Color>() { Color.DarkRed, Color.LightGreen, Color.LightCyan, Color.DarkOrange };
		private Dictionary<string, string> dicMember = new Dictionary<string, string>();
		private B2Unit b2obj;

		private bool isuseProxy = true;
		#endregion

		#region Construct
		public MainForm()
		{
			InitializeComponent();
			confighelper.additem(confighelper.existkeyname(SystemCofing.lstConfigkey)); // 檢查Config是否有參數
			_host = ConfigurationManager.AppSettings["host"] == "" ? SystemCofing.Host : ConfigurationManager.AppSettings["host"];
			_hubname = ConfigurationManager.AppSettings["hubname"] == "" ? SystemCofing.HubName : ConfigurationManager.AppSettings["hubname"];
			_token = ConfigurationManager.AppSettings["token"];
			_empno = ConfigurationManager.AppSettings["empno"];
			_name = ConfigurationManager.AppSettings["name"];
			_verifyapi = ConfigurationManager.AppSettings["verifytokenapi"] == "" ? SystemCofing.Host + SystemCofing.VerifyTokenApi : ConfigurationManager.AppSettings["verifytokenapi"];
			_onlineapi = ConfigurationManager.AppSettings["onlineapi"] == "" ? SystemCofing.Host + SystemCofing.OnlineApi : ConfigurationManager.AppSettings["onlineapi"];
			confighelper.modifyitem("host", _host);
			confighelper.modifyitem("hubname", _hubname);
			confighelper.modifyitem("verifytokenapi", _verifyapi);
			confighelper.modifyitem("onlineapi", _onlineapi);
			loadkey();
		}
		#endregion

		#region Control Event
		private async void Form1_Load(object sender, EventArgs e)
		{

			ToastNotificationManagerCompat.OnActivated += toastArgs =>
			{

				// Obtain the arguments from the notification
				ToastArguments args = ToastArguments.Parse(toastArgs.Argument);
				string strAction = args.Get("action");
				if (strAction == "viewConversation")
				{
					this.BeginInvoke(new Action(() =>
					{
						showMenuItem_Click(showMTSM, null);
					}));
				}		
				else if(strAction == "btnSend")
				{
					// Obtain any user input (text boxes, menu selections) from the notification
					this.BeginInvoke(new Action(() =>
					{
						ValueSet userInput = toastArgs.UserInput;
						userInput.TryGetValue("txtReply", out object oTime);
						txtMessage.Text = oTime.ToString();
						btnFunction_Click(btnSendMessage, null);
					}));
				}

				// Need to dispatch to UI thread if performing UI operations
				//Application.CurrentCulture.Dispatcher.Invoke(delegate
				//{
				//	// TODO: Show the corresponding content
				//	MessageBox.Show("Toast activated. Args: " + toastArgs.Argument);
				//});
			};


			FileVersionInfo version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			this.lbTitle.Text = "Single Chat_" + version.FileVersion.ToString();

			txtempno.Text = _empno;
			txtname.Text = _name;

			// 設定taskbar icon
			Bitmap bmp = Properties.Resources.taskbar;
			niTask.Icon = Icon.FromHandle(bmp.GetHicon());

			// 初始化ListView
			this.lvMember.Columns.Add("工號", 70, HorizontalAlignment.Left);
			this.lvMember.Columns[0].ImageIndex = 2;

			// 測試用 需要proxy api
			//string _api = "https://gms.auo.com/webapi/pvreturndata/getdata";
			//string body = "{\"plantNo\":\"BDL222010019\",\"userKey\":\"R4%2fwm3E4PzhBUb0gZ0Q3%2bJGl328hkcb3roIrr1cqxC69JIBZFcOHwO74LAOVbT%2b3W27gOkjzfCY%3d\",\"startTimestamp\":\"20220712130101\",\"endTimestamp\":\"20220712130201\",\"timeType\":\"createTime\",\"timezone\":\"8\"}";
			//string s = new Httphelper().HttpPost(_api,body,"",_proxy,80,_proxyauth,_proxypassword);
			//MessageBox.Show(s);

			// 自動登入
			Httphelper httphelper = new Httphelper();
			string str = await httphelper.HttpGet(_verifyapi, _token);
			b2obj = new B2Unit(_b2query, _b2key);
			b2obj.settingProxy(_proxy, 80, _proxyauth, _proxypassword);
			b2obj.authorize_account(isuseProxy);
			b2obj.getuploadurl(isuseProxy);
			//MessageBox.Show(b2obj.getuploadurl(true));
			if (str == null) // 判斷伺服器是否斷線
			{
				MessageBox.Show("無法連線至遠端伺服器!!");
				return;
			}
			if (ConvertEx.ToDictionary(str) == null)
				btnLogin.Text = "登入";
			else
			{
				btnLogin.Text = "登出";
				connectionhub(_token);
			}
		}

		/// <summary>所有按鈕事件</summary>
		private async void btnFunction_Click(object sender,EventArgs e)
		{
			Button btn = (Button)sender;
			//Thread m_thShowForm;
			switch (btn.Name)
			{
				case "btnLogin":
					if (btnLogin.Text == "登入")
					{
						frmLogin frm = new frmLogin();
						frm.ShowDialog();
						_token = ConfigurationManager.AppSettings["token"];
						if (_token != "")
						{
							txtempno.Text = _empno = ConfigurationManager.AppSettings["empno"];
							txtname.Text = _name = ConfigurationManager.AppSettings["name"];
							connectionhub(_token);
							btnLogin.Text = "登出";
						}
					}
					else
					{
						_token = "";
						confighelper.modifyitem("token", _token);
						btnLogin.Text = "登入";
						await connection.StopAsync();
						await connection.DisposeAsync();
					}
					break;
				case "btnSendMessage":
					try
					{
						// 先發送文字
						if (txtMessage.Text.TrimStart().TrimEnd() != "")
						{
							if (txtToid.Text == "")
								await this.connection.InvokeAsync("SendMessageToAll", txtMessage.Text.TrimStart().TrimEnd());
							else
							{
								await this.connection.InvokeAsync("SendMessageToUser", txtToid.Text, txtMessage.Text.TrimStart().TrimEnd());
								this.rtxtMessageBlock.AppendText($"傳送給{ txtToid.Text }:{ txtMessage.Text.TrimStart().TrimEnd() }\r\n");
							}
						}

						// 有圖片的話再發送圖片
						if (txtMessage.Rtf.IndexOf(@"{\pict\") > -1)
						{
							Clipboard.Clear();
							txtMessage.SelectAll();
							txtMessage.Copy();
							string _RtfText = txtMessage.SelectedRtf;
							List<string> lstPic = new List<string>();
							while(true) // RTF 字串切割 獲取每張圖片RTF字串
							{
								int _Index = _RtfText.IndexOf("pict");
								if (_Index == -1) break;
								_RtfText = _RtfText.Remove(0, _Index -1);
								_Index = _RtfText.IndexOf("}") + 1;
								lstPic.Add(_RtfText.Substring(0, _Index));
								_RtfText = _RtfText.Remove(0, _Index);
							}
							// 將每張圖RTF字串 轉成 BYTE 儲存成 BMP
							foreach(string rtfTxt in lstPic)
							{
								Clipboard.Clear();
								Match mat = Regex.Match(rtfTxt, @"picw[\d]+");
								mat = Regex.Match(rtfTxt, @"picwgoal[\d]+");
								//width of image
								int width = int.Parse(mat.Value.Replace("picwgoal", "")) / 15;
								mat = Regex.Match(rtfTxt, @"pichgoal[\d]+");

								//height of image
								int hight = int.Parse(mat.Value.Replace("pichgoal", "")) / 15;
								string content = rtfTxt.Substring(mat.Index + mat.Length, rtfTxt.IndexOf("}", (mat.Index + mat.Length)) - (mat.Index + mat.Length));
								string text = content.Replace("\r\n", "").Replace(" ", "");

								int _Count = text.Length / 2;
								byte[] buffer = new byte[_Count];
								for (int z = 0; z != _Count; z++)
								{
									string _TempText = text[z * 2].ToString() + text[(z * 2) + 1].ToString();
									buffer[z] = Convert.ToByte(_TempText, 16);
								}
								MemoryStream m = new MemoryStream(buffer);
								using (Bitmap bmp = new Bitmap(m, true))
								{
									using (Bitmap bmp1 = new Bitmap(bmp, width, hight))
									{
										Clipboard.SetImage(bmp1);
										string path = Application.StartupPath + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".bmp";
										bmp1.Save(path);
										string str = UploadimgToCloudinary(path); // 上傳至Cloudinary
										if(str != "")
											await this.connection.InvokeAsync("SendMessageToAll", str); // 傳網址到SignalR hub
									}
								}
							}		
						}

						this.txtMessage.Text = "";
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
					break;
				case "btnSendPic":
					OpenFileDialog openJpgDialog = new OpenFileDialog();
					//openJpgDialog.Filter = "JPG圖片(*.jpg,*.jpeg)|*.jpg|BMP圖片(*.bmp)|*.bmp|GIF圖片(*.gif)|*.gif|PNG圖片|*.png";
					//openJpgDialog.FilterIndex = 0;
					openJpgDialog.RestoreDirectory = true;
					openJpgDialog.Multiselect = false;
					if (openJpgDialog.ShowDialog() != DialogResult.OK)
						return;
					try
					{
						string filePath = "", pichttp = "";
						filePath = openJpgDialog.FileName;
						pichttp = await b2obj.uploadfiletob2(filePath, isuseProxy);
						//Image img = Image.FromFile(filePath);
						//Clipboard.SetDataObject(img);
						//txtMessage.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
						//pichttp = UploadimgToCloudinary(filePath); // 上傳圖片
						Console.WriteLine(pichttp);
						if (pichttp != "")
						{
							await this.connection.InvokeAsync("SendMessageToAll", pichttp);
							this.txtMessage.Text = "";
						}
						else
						{
							MessageBox.Show("檔案上傳失敗!!");
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
					break;
				case "btnMinsize":
					this.WindowState = FormWindowState.Minimized;
					break;
				case "btnSetting":
					//var bm = new Bitmap(Width, Height);
					//this.DrawToBitmap(bm, this.Bounds);
					//bm.Save(@"d:\你的圖檔名稱.gif", ImageFormat.Gif);
					//MessageBox.Show("功能尚未上線，敬請期待!!");
					frmAbout frmd = new frmAbout();
					frmd.ShowDialog();
					break;
				case "btnExit":
					this.niTask.Visible = false;
					this.Close();
					this.Dispose();
					break;
			}
		}

		#region 判斷縮小視窗則隱藏到Task bar
		private void Form1_MinimumSizeChanged(object sender, EventArgs e)
		{
			// 判斷縮小隱藏視窗
			if (this.WindowState == FormWindowState.Minimized)
			{
				this.Hide(); //或者是this.Visible = false;
				this.niTask.Visible = true;
			}
		}

		private void niTask_Click(object sender, EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
			this.Activate();
		}

		private void hideMenuItem_Click(object sender, EventArgs e)
		{
			if(hidenotifyMTSM.Text == "關閉通知")
			{
				hidenotifyMTSM.Text = "開啟通知";
				_bisnotify = false;
			}
			else
			{
				hidenotifyMTSM.Text = "關閉通知";
				_bisnotify = true;
			}
		}

		private void showMenuItem_Click(object sender, EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
			this.Activate();
		}

		private void exitMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("你確定要退出程序嗎？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
			{
				niTask.Visible = false;
				this.Close();
				this.Dispose();
				Application.Exit();
			}
		}
		#endregion

		#region 拖拉放大縮小
		public enum MouseDirection
		{
			Herizontal,//水平方向拖动，只改变窗体的宽度
			Vertical,//垂直方向拖动，只改变窗体的高度
			Declining,//倾斜方向，同时改变窗体的宽度和高度
			None//不做标志，即不拖动窗体改变大小
		}
		bool isMouseDown = false; //表示鼠标当前是否处于按下状态，初始值为否 
		MouseDirection direction = MouseDirection.None;//表示拖动的方向，起始为None，表示不拖动

		private void Main_MouseMove(object sender, MouseEventArgs e)
		{
			//如果鼠标按下，同时有方向箭头那么直接调整大小,这里是改进的地方，不然斜角拉的过程中，会有问题
			if (isMouseDown && direction != MouseDirection.None)
			{
				//设定好方向后，调用下面方法，改变窗体大小  
				ResizeWindow();
				return;
			}

			//鼠标移动过程中，坐标时刻在改变 
			//当鼠标移动时横坐标距离窗体右边缘5像素以内且纵坐标距离下边缘也在5像素以内时，要将光标变为倾斜的箭头形状，同时拖拽方向direction置为MouseDirection.Declining 
			if (e.Location.X >= this.Width - 5 && e.Location.Y > this.Height - 5)
			{
				this.Cursor = Cursors.SizeNWSE;
				direction = MouseDirection.Declining;
			}
			//当鼠标移动时横坐标距离窗体右边缘5像素以内时，要将光标变为倾斜的箭头形状，同时拖拽方向direction置为MouseDirection.Herizontal
			else if (e.Location.X >= this.Width - 5)
			{
				this.Cursor = Cursors.SizeWE;
				direction = MouseDirection.Herizontal;
			}
			//同理当鼠标移动时纵坐标距离窗体下边缘5像素以内时，要将光标变为倾斜的箭头形状，同时拖拽方向direction置为MouseDirection.Vertical
			else if (e.Location.Y >= this.Height - 5)
			{
				this.Cursor = Cursors.SizeNS;
				direction = MouseDirection.Vertical;

			}
			//否则，以外的窗体区域，鼠标星座均为单向箭头（默认）             
			else
				this.Cursor = Cursors.Arrow;

		}

		private void ResizeWindow()
		{
			//这个判断很重要，只有在鼠标按下时才能拖拽改变窗体大小，如果不作判断，那么鼠标弹起和按下时，窗体都可以改变 
			if (!isMouseDown)
				return;
			//MousePosition的参考点是屏幕的左上角，表示鼠标当前相对于屏幕左上角的坐标this.left和this.top的参考点也是屏幕，属性MousePosition是该程序的重点
			if (direction == MouseDirection.Declining)
			{
				//此行代码在mousemove事件中已经写过，在此再写一遍，并不多余，一定要写
				this.Cursor = Cursors.SizeNWSE;
				//下面是改变窗体宽和高的代码，不明白的可以仔细思考一下
				this.Width = MousePosition.X - this.Left;
				this.Height = MousePosition.Y - this.Top;
			}
			//以下同理
			if (direction == MouseDirection.Herizontal)
			{
				this.Cursor = Cursors.SizeWE;
				this.Width = MousePosition.X - this.Left;
			}
			else if (direction == MouseDirection.Vertical)
			{
				this.Cursor = Cursors.SizeNS;
				this.Height = MousePosition.Y - this.Top;
			}
			//即使鼠标按下，但是不在窗口右和下边缘，那么也不能改变窗口大小
			else
				this.Cursor = Cursors.Arrow;
		}

		private void Main_MouseDown(object sender, MouseEventArgs e)
		{
			isMouseDown = true;
		}

		private void Main_MouseUp(object sender, MouseEventArgs e)
		{
			isMouseDown = false;
			direction = MouseDirection.None;
		}
		#endregion

		#region 第一欄拖拉視窗事件
		private void lbTitle_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.curr_x = e.X;
				this.curr_y = e.Y;
				this.isWndMove = true;
			}
		}

		private void lbTitle_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.isWndMove)
				this.Location = new System.Drawing.Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
		}

		private void lbTitle_MouseUp(object sender, MouseEventArgs e)
		{
			this.isWndMove = false;
		}

		/// <summary>點兩下放大</summary>
		private void lbTitle_DoubleClick(object sender, EventArgs e)
		{
			// 判斷視窗大小
			if (this.WindowState == FormWindowState.Normal)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.WindowState = FormWindowState.Normal;
			}
		}
		#endregion

		/// <summary>richtextbox點http開啟網頁</summary>
		private void rtxtMessageBlock_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText, "chrome.exe");
		}

		/// <summary>勾選開機自動啟動</summary>
		private void chkAutoStart_CheckedChanged(object sender, EventArgs e)
		{
			RegAutoStart.SetMeStart(chkAutoStart.Checked);
		}

		/// <summary>選擇ListView使用者</summary>
		private void lvMember_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvMember.SelectedItems.Count > 0 && lvMember.SelectedItems[0].Text != txtempno.Text)
			{
				txtToid.Text = lvMember.SelectedItems[0].Text;
			}
			else
			{
				txtToid.Text = "";
			}
		}

		/// <summary>richTextBox收到訊息自動滾動到最下方</summary>
		private void rtxtMessageBlock_TextChanged(object sender, EventArgs e)
		{
			rtxtMessageBlock.ScrollToCaret();
		}

		/// <summary>Enter送出訊息</summary>
		private void txtMessage_KeyDown(object sender, KeyEventArgs e)
		{
			// 按Shift + Enter 換行 有圖片不能換行
			if (e.Shift == true && e.KeyCode == Keys.Enter)
			{
				if(txtMessage.Rtf.IndexOf(@"{\pict\") < -1)
				{
					txtMessage.Text = txtMessage.Text + System.Environment.NewLine;
					txtMessage.Select(txtMessage.Text.Length, -1);
				}	
			}
			// 按Enter送出訊息
			else if (e.KeyCode == Keys.Enter)
			{
				btnSendMessage.Focus();
				btnFunction_Click(btnSendMessage, null);
				txtMessage.Focus();
			}
		}

		/// <summary>TablePanel第一欄塗色</summary>
		private void tlpMain_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			if (e.Row == 0)
				e.Graphics.FillRectangle(Brushes.SkyBlue, e.CellBounds);
		}

		/// <summary>側邊欄縮放事件</summary>
		private void lbSideline_Click(object sender, EventArgs e)
		{
			if (tlpMain.ColumnStyles[0].Width != 0)
			{
				tlpMain.SuspendLayout();
				tlpMain.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0F);
				tlpMain.ResumeLayout();
			}
			else
			{
				tlpMain.SuspendLayout();
				tlpMain.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20F);
				tlpMain.ResumeLayout();
			}
		}

		#endregion

		#region Timer
		private async void tmrScan_Tick(object sender, EventArgs e)
		{
			if (connection != null)
			{
				lbCon.Text = connection.State.ToString();
				lbCon.BackColor = lstConBK[(int)connection.State];
			}
			string str = await new Httphelper().HttpGet(_onlineapi);
			if (str == null)
			{
				//Console.WriteLine("伺服器錯誤");
				return;
			}
			str = str.Replace(" ", "");
			if (str != "")
			{
				Dictionary<string, string> dic = new Dictionary<string, string>();
				foreach (string s in str.Split(','))
				{
					string[] temp = s.Split('=');
					dic.Add(temp[0], temp[1]);
				}
				dicMember = dic;
			}
			this.lvMember.BeginUpdate();
			this.lvMember.Items.Clear();
			foreach (KeyValuePair<string, string> item in dicMember)
			{
				this.lvMember.Items.Add(item.Key,1);
			}
			this.lvMember.EndUpdate();  //結束數據處理，UI界面一次性繪製。
			lbOnCount.Text = string.Format("線上人數:{0}", this.lvMember.Items.Count);
		}
		#endregion

		#region Private Method
		/// <summary>
		/// 讀取cloudinary和b2 api key
		/// </summary>
		private void loadkey()
		{
			string m_strParamPath = @"apikey.ini";
			string m_strCloudinary = "Cloudinary";
			string m_strB2 = "B2";
			string m_strProxy = "Proxy";
			IniFile objConfiger = new IniFile(m_strParamPath);
			if (File.Exists(m_strParamPath) == false ||
					objConfiger.checkSectionInCollection(m_strProxy) == false ||
					objConfiger.checkSectionInCollection(m_strCloudinary) == false ||
					objConfiger.checkSectionInCollection(m_strB2) == false)
			{
				MessageBox.Show("尚未找到Cloudinary&B2 API Key值");
			}
			_proxy = objConfiger.getString(m_strProxy, "Proxy", "");
			_proxyauth = objConfiger.getString(m_strProxy, "Auth", "");
			_proxypassword = objConfiger.getString(m_strProxy, "Password", "");
			_cloudinarykey = objConfiger.getString(m_strCloudinary, "Key", "");
			_cloudinarysecret = objConfiger.getString(m_strCloudinary, "Secret", "");
			_cloudinarycloud = objConfiger.getString(m_strCloudinary, "Cloud", "");
			_b2query = objConfiger.getString(m_strB2, "Query", "");
			_b2key = objConfiger.getString(m_strB2, "Key", "");
		}


		/// <summary>連線Signal Hub</summary> 
		private bool connectionhub(string token)
		{
			// New object
			connection = new HubConnectionBuilder()
			  .WithUrl(_host + _hubname, option =>
			  {
				  option.AccessTokenProvider = () => Task.FromResult(token);
			  })
			  .Build();

			// Connection server
			connection.Closed += async (error) =>
			{
				await Task.Delay(new Random().Next(0, 5) * 1000);
				MessageBox.Show("斷線重新連接");
				await connection.StartAsync();
			};

			// 註冊給伺服端呼叫的方
			connection.On("ReceiveMessage", (string s1) => OnSend(s1));

			// 連線到 SignalR 伺服器
			//this.hubConnection.Start().Wait();
			try
			{
				connection.StartAsync();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		/// <summary>監聽Server Signal Hub回傳事件</summary> 
		private void OnSend(string message)
		{
			this.rtxtMessageBlock.InvokeIfNecessary(() =>
			{
				// 接受到訊息為http解讀為圖片
				if(message.Contains("http"))
				{
					bool isImage = false;
					foreach(string s in new List<string> { ".png" , ".jpg" , ".jpeg" , ".svg" , ".webp" , ".gif" , ".bmp" })
						isImage = (!isImage) ? message.EndsWith(s) : isImage;
					if(isImage)
					{
						// 圖片解讀
						this.rtxtMessageBlock.AppendText($"{message.Substring(0, message.IndexOf('h'))}\r\n");
						Clipboard.SetDataObject(GetPicFromUrl(message.Substring(message.IndexOf('h')), false, 0, 0, _proxy, 80, _proxyauth, _proxypassword));
					}
					else
					{
						this.rtxtMessageBlock.AppendText($"{message}\r\n");
					}
				}
				else
				{
					this.rtxtMessageBlock.AppendText($"{message}\r\n");
				}
				// 跳出通知訊息
				if (this.WindowState == FormWindowState.Minimized && _bisnotify == true && !message.StartsWith(_empno) && !message.StartsWith("傳送給"))
				{
					//new ToastContentBuilder()
					//	.AddText("聊天室")
					//	.AddText(message).SetToastDuration(ToastDuration.Long).Show();
					new ToastContentBuilder()
						.AddArgument("action", "viewConversation")
						.AddArgument("conversationId", 9813)
						.AddText("密客室")
						.AddText(message)
						.AddInputTextBox("txtReply", placeHolderContent: "快速回覆")
						.AddButton(new ToastButton()
							.SetContent("發送")
							.AddArgument("action", "btnSend")
							.SetBackgroundActivation())
						.Show();
				}

			});
		}

		/// <summary>圖片重新壓縮大小</summary> 
		private Image ReSizeImage(Image img, System.Drawing.Size size)
		{
			Bitmap bitmap = new Bitmap(size.Width, size.Height);
			Graphics g = Graphics.FromImage(bitmap);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.DrawImage(img, 0, 0, bitmap.Width, bitmap.Height);
			g.Dispose();
			return bitmap;
		}

		/// <summary>上傳圖片到Cloudinary</summary> 
		private string UploadimgToCloudinary(string picpath)
		{
			var myAccount = new Account { ApiKey = _cloudinarykey, ApiSecret = _cloudinarysecret, Cloud = _cloudinarycloud };
			Cloudinary _cloudinary = new Cloudinary(myAccount);

			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(picpath),
				//Transformation = new Transformation().Width(200).Height(200).Crop("thumb").Gravity("face"),
			};
			var uploadResult = _cloudinary.Upload(uploadParams);

			return (uploadResult != null) ? uploadResult.SecureUri.AbsoluteUri : "";
		}

		/// <summary>從url獲取圖片並更新MessageBox</summary>
		private async Task<Image> GetPicFromUrl(string url, bool isReSize = false, int width = 0, int height = 0, string proxy = "", int proxyport = 0, string proxyuser = "", string proxypassword = "")
		{
			return await Task.Run(() => {
				try
				{
					// 從網址抓取圖片餵入RichTextBox
					WebRequest wb = System.Net.WebRequest.Create(url);
					if (proxy != "" && proxyport != 0)
					{
						IWebProxy pp = new WebProxy(proxy, proxyport);
						if (proxyuser != "")
						{
							ICredentials credentials = new NetworkCredential(proxyuser, proxypassword);
							pp.Credentials = credentials;
						}
						wb.Proxy = pp;
					}
					Image img = Image.FromStream(wb.GetResponse().GetResponseStream());
					if (isReSize && width != 0 & height != 0)
					{
						img = ReSizeImage(img, new System.Drawing.Size(width, height));
					}
					this.BeginInvoke(new Action(() =>
					{
						rtxtMessageBlock.SelectionStart = rtxtMessageBlock.TextLength;
						Clipboard.SetDataObject(img);
						rtxtMessageBlock.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
						rtxtMessageBlock.AppendText("\r\n");
					}));
					return img;
				}
				catch (Exception e)
				{
					MessageBox.Show(string.Format("獲取圖片失敗!! error :{0}", e.Message));
					this.BeginInvoke(new Action(() =>
					{
						this.rtxtMessageBlock.AppendText($"{url}\r\n");
					}));
					return null;
				}
			});
		}
		#endregion
	}
}
