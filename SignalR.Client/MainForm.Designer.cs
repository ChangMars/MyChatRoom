
namespace SignalR.Client
{
	partial class MainForm
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.btnSendMessage = new System.Windows.Forms.Button();
			this.txtToid = new System.Windows.Forms.TextBox();
			this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.showMTSM = new System.Windows.Forms.ToolStripMenuItem();
			this.hidenotifyMTSM = new System.Windows.Forms.ToolStripMenuItem();
			this.exitMTSM = new System.Windows.Forms.ToolStripMenuItem();
			this.niTask = new System.Windows.Forms.NotifyIcon(this.components);
			this.chkAutoStart = new System.Windows.Forms.CheckBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.txtname = new System.Windows.Forms.TextBox();
			this.txtempno = new System.Windows.Forms.TextBox();
			this.lbTitle = new System.Windows.Forms.Label();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnSetting = new System.Windows.Forms.Button();
			this.btnMinsize = new System.Windows.Forms.Button();
			this.lbCon = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lbSideline = new System.Windows.Forms.Label();
			this.lbOnCount = new System.Windows.Forms.Label();
			this.lvMember = new System.Windows.Forms.ListView();
			this.lvimglist = new System.Windows.Forms.ImageList(this.components);
			this.txtMessage = new System.Windows.Forms.RichTextBox();
			this.btnSendPic = new System.Windows.Forms.Button();
			this.rtxtMessageBlock = new System.Windows.Forms.RichTextBox();
			this.tmrScan = new System.Windows.Forms.Timer(this.components);
			this.cmsMenu.SuspendLayout();
			this.tlpMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label1.Location = new System.Drawing.Point(102, 32);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 30);
			this.label1.TabIndex = 10;
			this.label1.Text = "工號:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSendMessage
			// 
			this.tlpMain.SetColumnSpan(this.btnSendMessage, 2);
			this.btnSendMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSendMessage.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.btnSendMessage.Location = new System.Drawing.Point(502, 342);
			this.btnSendMessage.Margin = new System.Windows.Forms.Padding(2);
			this.btnSendMessage.Name = "btnSendMessage";
			this.btnSendMessage.Size = new System.Drawing.Size(60, 56);
			this.btnSendMessage.TabIndex = 8;
			this.btnSendMessage.Text = "發送";
			this.btnSendMessage.UseVisualStyleBackColor = true;
			this.btnSendMessage.Click += new System.EventHandler(this.btnFunction_Click);
			// 
			// txtToid
			// 
			this.txtToid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtToid.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.txtToid.Location = new System.Drawing.Point(342, 35);
			this.txtToid.Margin = new System.Windows.Forms.Padding(2);
			this.txtToid.Name = "txtToid";
			this.txtToid.ReadOnly = true;
			this.txtToid.Size = new System.Drawing.Size(76, 23);
			this.txtToid.TabIndex = 6;
			// 
			// cmsMenu
			// 
			this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMTSM,
            this.hidenotifyMTSM,
            this.exitMTSM});
			this.cmsMenu.Name = "contextMenuStrip1";
			this.cmsMenu.Size = new System.Drawing.Size(123, 70);
			// 
			// showMTSM
			// 
			this.showMTSM.Name = "showMTSM";
			this.showMTSM.Size = new System.Drawing.Size(122, 22);
			this.showMTSM.Text = "顯示";
			this.showMTSM.Click += new System.EventHandler(this.showMenuItem_Click);
			// 
			// hidenotifyMTSM
			// 
			this.hidenotifyMTSM.Name = "hidenotifyMTSM";
			this.hidenotifyMTSM.Size = new System.Drawing.Size(122, 22);
			this.hidenotifyMTSM.Text = "關閉通知";
			this.hidenotifyMTSM.Click += new System.EventHandler(this.hideMenuItem_Click);
			// 
			// exitMTSM
			// 
			this.exitMTSM.Name = "exitMTSM";
			this.exitMTSM.Size = new System.Drawing.Size(122, 22);
			this.exitMTSM.Text = "退出";
			this.exitMTSM.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// niTask
			// 
			this.niTask.ContextMenuStrip = this.cmsMenu;
			this.niTask.Text = "聊天室";
			this.niTask.Visible = true;
			this.niTask.DoubleClick += new System.EventHandler(this.niTask_Click);
			// 
			// chkAutoStart
			// 
			this.chkAutoStart.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkAutoStart.AutoSize = true;
			this.chkAutoStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkAutoStart.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.chkAutoStart.Location = new System.Drawing.Point(423, 65);
			this.chkAutoStart.Name = "chkAutoStart";
			this.chkAutoStart.Size = new System.Drawing.Size(74, 24);
			this.chkAutoStart.TabIndex = 12;
			this.chkAutoStart.Text = "開機自動啟動";
			this.chkAutoStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkAutoStart.UseVisualStyleBackColor = true;
			this.chkAutoStart.CheckedChanged += new System.EventHandler(this.chkAutoStart_CheckedChanged);
			// 
			// btnLogin
			// 
			this.tlpMain.SetColumnSpan(this.btnLogin, 3);
			this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLogin.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.btnLogin.Location = new System.Drawing.Point(503, 65);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(94, 24);
			this.btnLogin.TabIndex = 13;
			this.btnLogin.Text = "登入";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnFunction_Click);
			// 
			// tlpMain
			// 
			this.tlpMain.BackColor = System.Drawing.SystemColors.Control;
			this.tlpMain.ColumnCount = 10;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tlpMain.Controls.Add(this.label3, 2, 2);
			this.tlpMain.Controls.Add(this.txtname, 3, 2);
			this.tlpMain.Controls.Add(this.txtempno, 3, 1);
			this.tlpMain.Controls.Add(this.label1, 2, 1);
			this.tlpMain.Controls.Add(this.lbTitle, 2, 0);
			this.tlpMain.Controls.Add(this.btnExit, 9, 0);
			this.tlpMain.Controls.Add(this.btnSetting, 8, 0);
			this.tlpMain.Controls.Add(this.btnMinsize, 7, 0);
			this.tlpMain.Controls.Add(this.btnSendMessage, 7, 4);
			this.tlpMain.Controls.Add(this.lbCon, 7, 1);
			this.tlpMain.Controls.Add(this.label4, 4, 1);
			this.tlpMain.Controls.Add(this.txtToid, 5, 1);
			this.tlpMain.Controls.Add(this.chkAutoStart, 6, 2);
			this.tlpMain.Controls.Add(this.btnLogin, 7, 2);
			this.tlpMain.Controls.Add(this.lbSideline, 1, 0);
			this.tlpMain.Controls.Add(this.lbOnCount, 0, 0);
			this.tlpMain.Controls.Add(this.lvMember, 0, 1);
			this.tlpMain.Controls.Add(this.txtMessage, 2, 4);
			this.tlpMain.Controls.Add(this.btnSendPic, 9, 4);
			this.tlpMain.Controls.Add(this.rtxtMessageBlock, 2, 3);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 5;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tlpMain.Size = new System.Drawing.Size(600, 400);
			this.tlpMain.TabIndex = 14;
			this.tlpMain.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tlpMain_CellPaint);
			this.tlpMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
			this.tlpMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
			this.tlpMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label3.Location = new System.Drawing.Point(102, 62);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 30);
			this.label3.TabIndex = 14;
			this.label3.Text = "姓名:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtname
			// 
			this.txtname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtname.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.txtname.Location = new System.Drawing.Point(182, 65);
			this.txtname.Margin = new System.Windows.Forms.Padding(2);
			this.txtname.Name = "txtname";
			this.txtname.ReadOnly = true;
			this.txtname.Size = new System.Drawing.Size(76, 23);
			this.txtname.TabIndex = 17;
			// 
			// txtempno
			// 
			this.txtempno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtempno.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.txtempno.Location = new System.Drawing.Point(182, 35);
			this.txtempno.Margin = new System.Windows.Forms.Padding(2);
			this.txtempno.Name = "txtempno";
			this.txtempno.ReadOnly = true;
			this.txtempno.Size = new System.Drawing.Size(76, 23);
			this.txtempno.TabIndex = 16;
			// 
			// lbTitle
			// 
			this.lbTitle.AutoSize = true;
			this.lbTitle.BackColor = System.Drawing.Color.SkyBlue;
			this.tlpMain.SetColumnSpan(this.lbTitle, 5);
			this.lbTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbTitle.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.lbTitle.Location = new System.Drawing.Point(100, 0);
			this.lbTitle.Margin = new System.Windows.Forms.Padding(0);
			this.lbTitle.Name = "lbTitle";
			this.lbTitle.Size = new System.Drawing.Size(400, 32);
			this.lbTitle.TabIndex = 20;
			this.lbTitle.Text = "聊天室";
			this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbTitle.DoubleClick += new System.EventHandler(this.lbTitle_DoubleClick);
			this.lbTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbTitle_MouseDown);
			this.lbTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbTitle_MouseMove);
			this.lbTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbTitle_MouseUp);
			// 
			// btnExit
			// 
			this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnExit.Image = global::SignalR.Client.Properties.Resources.close;
			this.btnExit.Location = new System.Drawing.Point(564, 0);
			this.btnExit.Margin = new System.Windows.Forms.Padding(0);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(36, 32);
			this.btnExit.TabIndex = 19;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnFunction_Click);
			// 
			// btnSetting
			// 
			this.btnSetting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSetting.Image = global::SignalR.Client.Properties.Resources.setting;
			this.btnSetting.Location = new System.Drawing.Point(532, 0);
			this.btnSetting.Margin = new System.Windows.Forms.Padding(0);
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new System.Drawing.Size(32, 32);
			this.btnSetting.TabIndex = 21;
			this.btnSetting.UseVisualStyleBackColor = true;
			this.btnSetting.Click += new System.EventHandler(this.btnFunction_Click);
			// 
			// btnMinsize
			// 
			this.btnMinsize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnMinsize.Image = global::SignalR.Client.Properties.Resources.small;
			this.btnMinsize.Location = new System.Drawing.Point(500, 0);
			this.btnMinsize.Margin = new System.Windows.Forms.Padding(0);
			this.btnMinsize.Name = "btnMinsize";
			this.btnMinsize.Size = new System.Drawing.Size(32, 32);
			this.btnMinsize.TabIndex = 22;
			this.btnMinsize.UseVisualStyleBackColor = true;
			this.btnMinsize.Click += new System.EventHandler(this.btnFunction_Click);
			// 
			// lbCon
			// 
			this.lbCon.AutoSize = true;
			this.lbCon.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lbCon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tlpMain.SetColumnSpan(this.lbCon, 3);
			this.lbCon.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbCon.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.lbCon.ForeColor = System.Drawing.SystemColors.Info;
			this.lbCon.Location = new System.Drawing.Point(503, 32);
			this.lbCon.Name = "lbCon";
			this.lbCon.Size = new System.Drawing.Size(94, 30);
			this.lbCon.TabIndex = 18;
			this.lbCon.Text = "連線狀態";
			this.lbCon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label4.Location = new System.Drawing.Point(262, 32);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 30);
			this.label4.TabIndex = 15;
			this.label4.Text = "傳送給:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbSideline
			// 
			this.lbSideline.AutoSize = true;
			this.lbSideline.BackColor = System.Drawing.Color.SkyBlue;
			this.lbSideline.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbSideline.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.lbSideline.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.lbSideline.Location = new System.Drawing.Point(80, 0);
			this.lbSideline.Margin = new System.Windows.Forms.Padding(0);
			this.lbSideline.Name = "lbSideline";
			this.tlpMain.SetRowSpan(this.lbSideline, 5);
			this.lbSideline.Size = new System.Drawing.Size(20, 400);
			this.lbSideline.TabIndex = 23;
			this.lbSideline.Text = "線上人員列表";
			this.lbSideline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbSideline.Click += new System.EventHandler(this.lbSideline_Click);
			// 
			// lbOnCount
			// 
			this.lbOnCount.AutoSize = true;
			this.lbOnCount.BackColor = System.Drawing.Color.SkyBlue;
			this.lbOnCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbOnCount.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.lbOnCount.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbOnCount.Location = new System.Drawing.Point(0, 0);
			this.lbOnCount.Margin = new System.Windows.Forms.Padding(0);
			this.lbOnCount.Name = "lbOnCount";
			this.lbOnCount.Size = new System.Drawing.Size(80, 32);
			this.lbOnCount.TabIndex = 24;
			this.lbOnCount.Text = "線上人數:";
			this.lbOnCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lvMember
			// 
			this.lvMember.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMember.GridLines = true;
			this.lvMember.HideSelection = false;
			this.lvMember.Location = new System.Drawing.Point(3, 35);
			this.lvMember.MultiSelect = false;
			this.lvMember.Name = "lvMember";
			this.tlpMain.SetRowSpan(this.lvMember, 4);
			this.lvMember.Size = new System.Drawing.Size(74, 362);
			this.lvMember.SmallImageList = this.lvimglist;
			this.lvMember.TabIndex = 25;
			this.lvMember.UseCompatibleStateImageBehavior = false;
			this.lvMember.View = System.Windows.Forms.View.Details;
			this.lvMember.SelectedIndexChanged += new System.EventHandler(this.lvMember_SelectedIndexChanged);
			// 
			// lvimglist
			// 
			this.lvimglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lvimglist.ImageStream")));
			this.lvimglist.TransparentColor = System.Drawing.Color.Transparent;
			this.lvimglist.Images.SetKeyName(0, "male.ico");
			this.lvimglist.Images.SetKeyName(1, "female.ico");
			this.lvimglist.Images.SetKeyName(2, "info.ico");
			// 
			// txtMessage
			// 
			this.txtMessage.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tlpMain.SetColumnSpan(this.txtMessage, 5);
			this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtMessage.EnableAutoDragDrop = true;
			this.txtMessage.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.txtMessage.Location = new System.Drawing.Point(103, 343);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(394, 54);
			this.txtMessage.TabIndex = 26;
			this.txtMessage.Text = "";
			this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
			// 
			// btnSendPic
			// 
			this.btnSendPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendPic.BackColor = System.Drawing.Color.SkyBlue;
			this.btnSendPic.BackgroundImage = global::SignalR.Client.Properties.Resources.upload1;
			this.btnSendPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnSendPic.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
			this.btnSendPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSendPic.Location = new System.Drawing.Point(564, 358);
			this.btnSendPic.Margin = new System.Windows.Forms.Padding(0);
			this.btnSendPic.Name = "btnSendPic";
			this.btnSendPic.Size = new System.Drawing.Size(36, 23);
			this.btnSendPic.TabIndex = 27;
			this.btnSendPic.UseVisualStyleBackColor = false;
			this.btnSendPic.Click += new System.EventHandler(this.btnFunction_Click);
			// 
			// rtxtMessageBlock
			// 
			this.rtxtMessageBlock.BackColor = System.Drawing.SystemColors.Menu;
			this.rtxtMessageBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tlpMain.SetColumnSpan(this.rtxtMessageBlock, 8);
			this.rtxtMessageBlock.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.rtxtMessageBlock.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtMessageBlock.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.rtxtMessageBlock.Location = new System.Drawing.Point(103, 95);
			this.rtxtMessageBlock.Name = "rtxtMessageBlock";
			this.rtxtMessageBlock.Size = new System.Drawing.Size(494, 242);
			this.rtxtMessageBlock.TabIndex = 28;
			this.rtxtMessageBlock.Text = "";
			this.rtxtMessageBlock.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtMessageBlock_LinkClicked);
			this.rtxtMessageBlock.TextChanged += new System.EventHandler(this.rtxtMessageBlock_TextChanged);
			// 
			// tmrScan
			// 
			this.tmrScan.Enabled = true;
			this.tmrScan.Interval = 2000;
			this.tmrScan.Tick += new System.EventHandler(this.tmrScan_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 400);
			this.Controls.Add(this.tlpMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "聊天室";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.SizeChanged += new System.EventHandler(this.Form1_MinimumSizeChanged);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
			this.cmsMenu.ResumeLayout(false);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSendMessage;
		private System.Windows.Forms.TextBox txtToid;
		private System.Windows.Forms.ContextMenuStrip cmsMenu;
		private System.Windows.Forms.ToolStripMenuItem exitMTSM;
		private System.Windows.Forms.ToolStripMenuItem hidenotifyMTSM;
		private System.Windows.Forms.ToolStripMenuItem showMTSM;
		private System.Windows.Forms.NotifyIcon niTask;
		private System.Windows.Forms.CheckBox chkAutoStart;
		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.TextBox txtname;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtempno;
		private System.Windows.Forms.Timer tmrScan;
		private System.Windows.Forms.Label lbCon;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lbTitle;
		private System.Windows.Forms.Button btnSetting;
		private System.Windows.Forms.Button btnMinsize;
		private System.Windows.Forms.Label lbSideline;
		private System.Windows.Forms.Label lbOnCount;
		private System.Windows.Forms.ListView lvMember;
		private System.Windows.Forms.ImageList lvimglist;
		private System.Windows.Forms.RichTextBox txtMessage;
		private System.Windows.Forms.Button btnSendPic;
		private System.Windows.Forms.RichTextBox rtxtMessageBlock;
	}
}

