using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SignalR.Client.Unit;

namespace SignalR.Client.SubForm
{
	public partial class frmLogin : Form
	{ 
		private int curr_x; // 拖拉視窗使用
		private int curr_y; // 拖拉視窗使用
		private bool isWndMove; // 拖拉視窗使用
		private Confighelper confighelper = new Confighelper();
		private string _apiurl = "";
		private string _token = "";

		public frmLogin()
		{
			InitializeComponent();
			_apiurl = ConfigurationManager.AppSettings["loginapi"] == "" ? SystemCofing.Host + SystemCofing.LoginApi : ConfigurationManager.AppSettings["loginapi"];
			_token = ConfigurationManager.AppSettings["token"];
		}

		#region control event
		private async void btnLogin_Click(object sender, EventArgs e)
		{
			if (txtAccount.Text == "" || txtPassword.Text == "")
				return;
			Httphelper httphelper = new Httphelper();
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("username", txtAccount.Text);
			dic.Add("password", txtPassword.Text);
			string str = ConvertEx.MyDictionaryToJson(dic);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			var res = await httphelper.HttpPost(_apiurl, str);
			sw.Stop();
			MessageBox.Show(string.Format("Call login api spend {0}ms", sw.ElapsedMilliseconds));
			if (res == null || res == "")
			{
				MessageBox.Show("登入失敗");
				return;
			}
			Dictionary<string, string> dicres = ConvertEx.ToDictionary(res);
			confighelper.modifyitem("empno", dicres["empno"]);
			confighelper.modifyitem("name", dicres["name"]);
			confighelper.modifyitem("token", dicres["token"]);
			confighelper.modifyitem("loginapi", _apiurl);
			this.Close();
			this.Dispose();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
			this.Dispose();
		}

		private void txtPassword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				btnLogin.Focus();
				btnLogin_Click(sender, e);
				txtPassword.Focus();
			}
		}

		private void txtAccount_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if(txtPassword.Text != "")
				{
					btnLogin.Focus();
					btnLogin_Click(sender, e);
					txtPassword.Focus();
				}
				else
				{
					txtPassword.Focus();
				}
			}
		}
		#endregion

		#region 視窗拖拉功能
		private void frmLogin_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.curr_x = e.X;
				this.curr_y = e.Y;
				this.isWndMove = true;
			}
		}

		private void frmLogin_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.isWndMove)
				this.Location = new Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
		}

		private void frmLogin_MouseUp(object sender, MouseEventArgs e)
		{
			this.isWndMove = false;
		}
		#endregion

	}
}
