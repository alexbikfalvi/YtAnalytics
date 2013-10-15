﻿namespace YtAnalytics.Forms
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabelLeft = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabelMiddle = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.sideMenu = new DotNetApi.Windows.Controls.SideMenu();
			this.controlSideLog = new YtAnalytics.Controls.ControlSideCalendar();
			this.controlSideComments = new DotNetApi.Windows.Controls.SideTreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.controlSideConfiguration = new DotNetApi.Windows.Controls.SideTreeView();
			this.controlSideTesting = new DotNetApi.Windows.Controls.SideTreeView();
			this.controlSidePlanetLab = new DotNetApi.Windows.Controls.SideTreeView();
			this.controlSideSpiders = new DotNetApi.Windows.Controls.SideTreeView();
			this.controlSideDatabase = new DotNetApi.Windows.Controls.SideTreeView();
			this.controlSideBrowser = new DotNetApi.Windows.Controls.SideTreeView();
			this.sideMenuItemBrowser = new DotNetApi.Windows.Controls.SideMenuItem();
			this.sideMenuItemDatabase = new DotNetApi.Windows.Controls.SideMenuItem();
			this.sideMenuItemSpiders = new DotNetApi.Windows.Controls.SideMenuItem();
			this.sideMenuItemPlanetLab = new DotNetApi.Windows.Controls.SideMenuItem();
			this.sideMenuItemTesting = new DotNetApi.Windows.Controls.SideMenuItem();
			this.sideMenuItemConfiguration = new DotNetApi.Windows.Controls.SideMenuItem();
			this.sideMenuItemLog = new DotNetApi.Windows.Controls.SideMenuItem();
			this.sideMenuItemComments = new DotNetApi.Windows.Controls.SideMenuItem();
			this.labelNotAvailable = new System.Windows.Forms.Label();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.menuViewVideo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemApi2 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemApiV2Related = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemApiV2Responses = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemApi3 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemWeb = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemYouTube = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.sideMenu.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.menuViewVideo.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1008, 684);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(1008, 730);
			this.toolStripContainer.TabIndex = 0;
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
			// 
			// statusStrip
			// 
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelLeft,
            this.statusLabelMiddle,
            this.statusLabelRight});
			this.statusStrip.Location = new System.Drawing.Point(0, 0);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1008, 22);
			this.statusStrip.TabIndex = 0;
			// 
			// statusLabelLeft
			// 
			this.statusLabelLeft.Image = global::YtAnalytics.Resources.Information_16;
			this.statusLabelLeft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.statusLabelLeft.Name = "statusLabelLeft";
			this.statusLabelLeft.Size = new System.Drawing.Size(55, 17);
			this.statusLabelLeft.Text = "Ready";
			// 
			// statusLabelMiddle
			// 
			this.statusLabelMiddle.Name = "statusLabelMiddle";
			this.statusLabelMiddle.Size = new System.Drawing.Size(938, 17);
			this.statusLabelMiddle.Spring = true;
			// 
			// statusLabelRight
			// 
			this.statusLabelRight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.statusLabelRight.Name = "statusLabelRight";
			this.statusLabelRight.Size = new System.Drawing.Size(0, 17);
			// 
			// splitContainer
			// 
			this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.sideMenu);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.labelNotAvailable);
			this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(4);
			this.splitContainer.Size = new System.Drawing.Size(1008, 684);
			this.splitContainer.SplitterDistance = 246;
			this.splitContainer.TabIndex = 0;
			// 
			// sideMenu
			// 
			this.sideMenu.Controls.Add(this.controlSideLog);
			this.sideMenu.Controls.Add(this.controlSideComments);
			this.sideMenu.Controls.Add(this.controlSideConfiguration);
			this.sideMenu.Controls.Add(this.controlSideTesting);
			this.sideMenu.Controls.Add(this.controlSidePlanetLab);
			this.sideMenu.Controls.Add(this.controlSideSpiders);
			this.sideMenu.Controls.Add(this.controlSideDatabase);
			this.sideMenu.Controls.Add(this.controlSideBrowser);
			this.sideMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sideMenu.ItemHeight = 48;
			this.sideMenu.Items.AddRange(new DotNetApi.Windows.Controls.SideMenuItem[] {
            this.sideMenuItemBrowser,
            this.sideMenuItemDatabase,
            this.sideMenuItemSpiders,
            this.sideMenuItemPlanetLab,
            this.sideMenuItemTesting,
            this.sideMenuItemConfiguration,
            this.sideMenuItemLog,
            this.sideMenuItemComments});
			this.sideMenu.Location = new System.Drawing.Point(0, 0);
			this.sideMenu.MinimizedItemWidth = 25;
			this.sideMenu.MinimumPanelHeight = 50;
			this.sideMenu.Name = "sideMenu";
			this.sideMenu.Padding = new System.Windows.Forms.Padding(0, 28, 0, 392);
			this.sideMenu.SelectedIndex = 0;
			this.sideMenu.SelectedItem = this.sideMenuItemBrowser;
			this.sideMenu.Size = new System.Drawing.Size(244, 682);
			this.sideMenu.TabIndex = 0;
			this.sideMenu.VisibleItems = 7;
			// 
			// controlSideLog
			// 
			this.controlSideLog.AutoScroll = true;
			this.controlSideLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSideLog.Location = new System.Drawing.Point(0, 28);
			this.controlSideLog.Name = "controlSideLog";
			this.controlSideLog.Size = new System.Drawing.Size(244, 262);
			this.controlSideLog.TabIndex = 2;
			this.controlSideLog.Visible = false;
			this.controlSideLog.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.OnLogDateChanged);
			this.controlSideLog.DateRefresh += new System.Windows.Forms.DateRangeEventHandler(this.OnLogDateRefresh);
			this.controlSideLog.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// controlSideComments
			// 
			this.controlSideComments.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.controlSideComments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSideComments.FullRowSelect = true;
			this.controlSideComments.HideSelection = false;
			this.controlSideComments.ImageIndex = 0;
			this.controlSideComments.ImageList = this.imageList;
			this.controlSideComments.ItemHeight = 20;
			this.controlSideComments.Location = new System.Drawing.Point(0, 28);
			this.controlSideComments.Name = "controlSideComments";
			this.controlSideComments.SelectedImageIndex = 0;
			this.controlSideComments.ShowLines = false;
			this.controlSideComments.ShowRootLines = false;
			this.controlSideComments.Size = new System.Drawing.Size(244, 262);
			this.controlSideComments.TabIndex = 3;
			this.controlSideComments.Visible = false;
			this.controlSideComments.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "ServerBrowse");
			this.imageList.Images.SetKeyName(1, "ServerDatabase");
			this.imageList.Images.SetKeyName(2, "ServersDatabase");
			this.imageList.Images.SetKeyName(3, "ServerCube");
			this.imageList.Images.SetKeyName(4, "ServersCube");
			this.imageList.Images.SetKeyName(5, "ServersGlobe");
			this.imageList.Images.SetKeyName(6, "FolderClosed");
			this.imageList.Images.SetKeyName(7, "FolderClosedXml");
			this.imageList.Images.SetKeyName(8, "FolderClosedVideo");
			this.imageList.Images.SetKeyName(9, "FolderClosedUser");
			this.imageList.Images.SetKeyName(10, "FolderClosedComment");
			this.imageList.Images.SetKeyName(11, "FolderClosedPlay");
			this.imageList.Images.SetKeyName(12, "FolderClosedGlobe");
			this.imageList.Images.SetKeyName(13, "FolderOpen");
			this.imageList.Images.SetKeyName(14, "FolderOpenXml");
			this.imageList.Images.SetKeyName(15, "FolderOpenVideo");
			this.imageList.Images.SetKeyName(16, "FolderOpenUser");
			this.imageList.Images.SetKeyName(17, "FolderOpenComment");
			this.imageList.Images.SetKeyName(18, "FolderOpenPlay");
			this.imageList.Images.SetKeyName(19, "FolderOpenGlobe");
			this.imageList.Images.SetKeyName(20, "File");
			this.imageList.Images.SetKeyName(21, "FileXml");
			this.imageList.Images.SetKeyName(22, "FileVideo");
			this.imageList.Images.SetKeyName(23, "FileUser");
			this.imageList.Images.SetKeyName(24, "FileComment");
			this.imageList.Images.SetKeyName(25, "FileGraphLine");
			this.imageList.Images.SetKeyName(26, "GlobeBrowse");
			this.imageList.Images.SetKeyName(27, "Categories");
			this.imageList.Images.SetKeyName(28, "Comments");
			this.imageList.Images.SetKeyName(29, "CommentVideo");
			this.imageList.Images.SetKeyName(30, "CommentUser");
			this.imageList.Images.SetKeyName(31, "CommentPlay");
			this.imageList.Images.SetKeyName(32, "Settings");
			this.imageList.Images.SetKeyName(33, "ServerDown");
			this.imageList.Images.SetKeyName(34, "ServerUp");
			this.imageList.Images.SetKeyName(35, "ServerWarning");
			this.imageList.Images.SetKeyName(36, "ServerBusy");
			this.imageList.Images.SetKeyName(37, "Log");
			this.imageList.Images.SetKeyName(38, "QueryDatabase");
			this.imageList.Images.SetKeyName(39, "Cube");
			this.imageList.Images.SetKeyName(40, "Cubes");
			this.imageList.Images.SetKeyName(41, "GlobeSettings");
			this.imageList.Images.SetKeyName(42, "GlobeSchema");
			this.imageList.Images.SetKeyName(43, "GlobeObject");
			this.imageList.Images.SetKeyName(44, "GlobeConsole");
			this.imageList.Images.SetKeyName(45, "TestGlobeGoto");
			this.imageList.Images.SetKeyName(46, "TestConnectGoto");
			// 
			// controlSideConfiguration
			// 
			this.controlSideConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.controlSideConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSideConfiguration.FullRowSelect = true;
			this.controlSideConfiguration.HideSelection = false;
			this.controlSideConfiguration.ImageIndex = 0;
			this.controlSideConfiguration.ImageList = this.imageList;
			this.controlSideConfiguration.ItemHeight = 20;
			this.controlSideConfiguration.Location = new System.Drawing.Point(0, 28);
			this.controlSideConfiguration.Name = "controlSideConfiguration";
			this.controlSideConfiguration.SelectedImageIndex = 0;
			this.controlSideConfiguration.ShowLines = false;
			this.controlSideConfiguration.ShowRootLines = false;
			this.controlSideConfiguration.Size = new System.Drawing.Size(244, 262);
			this.controlSideConfiguration.TabIndex = 1;
			this.controlSideConfiguration.Visible = false;
			this.controlSideConfiguration.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// controlSideTesting
			// 
			this.controlSideTesting.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.controlSideTesting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSideTesting.FullRowSelect = true;
			this.controlSideTesting.HideSelection = false;
			this.controlSideTesting.ImageIndex = 0;
			this.controlSideTesting.ImageList = this.imageList;
			this.controlSideTesting.ItemHeight = 20;
			this.controlSideTesting.Location = new System.Drawing.Point(0, 28);
			this.controlSideTesting.Name = "controlSideTesting";
			this.controlSideTesting.SelectedImageIndex = 0;
			this.controlSideTesting.ShowLines = false;
			this.controlSideTesting.ShowRootLines = false;
			this.controlSideTesting.Size = new System.Drawing.Size(244, 262);
			this.controlSideTesting.TabIndex = 6;
			this.controlSideTesting.Visible = false;
			this.controlSideTesting.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// controlSidePlanetLab
			// 
			this.controlSidePlanetLab.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.controlSidePlanetLab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSidePlanetLab.FullRowSelect = true;
			this.controlSidePlanetLab.HideSelection = false;
			this.controlSidePlanetLab.ImageIndex = 0;
			this.controlSidePlanetLab.ImageList = this.imageList;
			this.controlSidePlanetLab.ItemHeight = 20;
			this.controlSidePlanetLab.Location = new System.Drawing.Point(0, 28);
			this.controlSidePlanetLab.Name = "controlSidePlanetLab";
			this.controlSidePlanetLab.SelectedImageIndex = 0;
			this.controlSidePlanetLab.ShowLines = false;
			this.controlSidePlanetLab.ShowRootLines = false;
			this.controlSidePlanetLab.Size = new System.Drawing.Size(244, 262);
			this.controlSidePlanetLab.TabIndex = 7;
			this.controlSidePlanetLab.Visible = false;
			this.controlSidePlanetLab.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// controlSideSpiders
			// 
			this.controlSideSpiders.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.controlSideSpiders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSideSpiders.FullRowSelect = true;
			this.controlSideSpiders.HideSelection = false;
			this.controlSideSpiders.ImageIndex = 0;
			this.controlSideSpiders.ImageList = this.imageList;
			this.controlSideSpiders.ItemHeight = 20;
			this.controlSideSpiders.Location = new System.Drawing.Point(0, 28);
			this.controlSideSpiders.Name = "controlSideSpiders";
			this.controlSideSpiders.SelectedImageIndex = 0;
			this.controlSideSpiders.ShowLines = false;
			this.controlSideSpiders.ShowRootLines = false;
			this.controlSideSpiders.Size = new System.Drawing.Size(244, 262);
			this.controlSideSpiders.TabIndex = 5;
			this.controlSideSpiders.Visible = false;
			this.controlSideSpiders.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// controlSideDatabase
			// 
			this.controlSideDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.controlSideDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSideDatabase.FullRowSelect = true;
			this.controlSideDatabase.HideSelection = false;
			this.controlSideDatabase.ImageIndex = 0;
			this.controlSideDatabase.ImageList = this.imageList;
			this.controlSideDatabase.ItemHeight = 20;
			this.controlSideDatabase.Location = new System.Drawing.Point(0, 28);
			this.controlSideDatabase.Name = "controlSideDatabase";
			this.controlSideDatabase.SelectedImageIndex = 0;
			this.controlSideDatabase.ShowLines = false;
			this.controlSideDatabase.ShowRootLines = false;
			this.controlSideDatabase.Size = new System.Drawing.Size(244, 262);
			this.controlSideDatabase.TabIndex = 4;
			this.controlSideDatabase.Visible = false;
			this.controlSideDatabase.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// controlSideBrowser
			// 
			this.controlSideBrowser.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.controlSideBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSideBrowser.FullRowSelect = true;
			this.controlSideBrowser.HideSelection = false;
			this.controlSideBrowser.ImageIndex = 0;
			this.controlSideBrowser.ImageList = this.imageList;
			this.controlSideBrowser.ItemHeight = 20;
			this.controlSideBrowser.Location = new System.Drawing.Point(0, 28);
			this.controlSideBrowser.Name = "controlSideBrowser";
			this.controlSideBrowser.SelectedImageIndex = 0;
			this.controlSideBrowser.ShowLines = false;
			this.controlSideBrowser.ShowRootLines = false;
			this.controlSideBrowser.Size = new System.Drawing.Size(244, 262);
			this.controlSideBrowser.TabIndex = 0;
			this.controlSideBrowser.Visible = false;
			this.controlSideBrowser.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// sideMenuItemBrowser
			// 
			this.sideMenuItemBrowser.Control = this.controlSideBrowser;
			this.sideMenuItemBrowser.ImageLarge = global::YtAnalytics.Resources.ServersBrowse_32;
			this.sideMenuItemBrowser.ImageSmall = global::YtAnalytics.Resources.ServersBrowse_16;
			this.sideMenuItemBrowser.Index = -1;
			this.sideMenuItemBrowser.Text = "Browser";
			// 
			// sideMenuItemDatabase
			// 
			this.sideMenuItemDatabase.Control = this.controlSideDatabase;
			this.sideMenuItemDatabase.ImageLarge = global::YtAnalytics.Resources.ServersDatabase_32;
			this.sideMenuItemDatabase.ImageSmall = global::YtAnalytics.Resources.ServersDatabase_16;
			this.sideMenuItemDatabase.Index = -1;
			this.sideMenuItemDatabase.Text = "Database";
			// 
			// sideMenuItemSpiders
			// 
			this.sideMenuItemSpiders.Control = this.controlSideSpiders;
			this.sideMenuItemSpiders.ImageLarge = global::YtAnalytics.Resources.ServersCube_32;
			this.sideMenuItemSpiders.ImageSmall = global::YtAnalytics.Resources.ServersCube_16;
			this.sideMenuItemSpiders.Index = -1;
			this.sideMenuItemSpiders.Text = "Spiders";
			// 
			// sideMenuItemPlanetLab
			// 
			this.sideMenuItemPlanetLab.Control = this.controlSidePlanetLab;
			this.sideMenuItemPlanetLab.ImageLarge = global::YtAnalytics.Resources.GlobeLab_32;
			this.sideMenuItemPlanetLab.ImageSmall = global::YtAnalytics.Resources.GlobeLab_16;
			this.sideMenuItemPlanetLab.Index = -1;
			this.sideMenuItemPlanetLab.Text = "PlanetLab";
			// 
			// sideMenuItemTesting
			// 
			this.sideMenuItemTesting.Control = this.controlSideTesting;
			this.sideMenuItemTesting.ImageLarge = global::YtAnalytics.Resources.TestsLarge_32;
			this.sideMenuItemTesting.ImageSmall = global::YtAnalytics.Resources.TestsLarge_16;
			this.sideMenuItemTesting.Index = -1;
			this.sideMenuItemTesting.Text = "Testing";
			// 
			// sideMenuItemConfiguration
			// 
			this.sideMenuItemConfiguration.Control = this.controlSideConfiguration;
			this.sideMenuItemConfiguration.ImageLarge = global::YtAnalytics.Resources.ConfigurationSettings_32;
			this.sideMenuItemConfiguration.ImageSmall = global::YtAnalytics.Resources.ConfigurationSettings_16;
			this.sideMenuItemConfiguration.Index = -1;
			this.sideMenuItemConfiguration.Text = "Configuration";
			// 
			// sideMenuItemLog
			// 
			this.sideMenuItemLog.Control = this.controlSideLog;
			this.sideMenuItemLog.ImageLarge = global::YtAnalytics.Resources.Log_32;
			this.sideMenuItemLog.ImageSmall = global::YtAnalytics.Resources.Log_16;
			this.sideMenuItemLog.Index = -1;
			this.sideMenuItemLog.Text = "Log";
			// 
			// sideMenuItemComments
			// 
			this.sideMenuItemComments.Control = this.controlSideComments;
			this.sideMenuItemComments.ImageLarge = global::YtAnalytics.Resources.Comments_32;
			this.sideMenuItemComments.ImageSmall = global::YtAnalytics.Resources.Comments_16;
			this.sideMenuItemComments.Index = -1;
			this.sideMenuItemComments.Text = "Comments";
			// 
			// labelNotAvailable
			// 
			this.labelNotAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelNotAvailable.Location = new System.Drawing.Point(4, 4);
			this.labelNotAvailable.Name = "labelNotAvailable";
			this.labelNotAvailable.Size = new System.Drawing.Size(748, 674);
			this.labelNotAvailable.TabIndex = 0;
			this.labelNotAvailable.Text = "Feature not available";
			this.labelNotAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// menuStrip
			// 
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemHelp});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(1008, 24);
			this.menuStrip.TabIndex = 0;
			// 
			// menuItemFile
			// 
			this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
			this.menuItemFile.Name = "menuItemFile";
			this.menuItemFile.Size = new System.Drawing.Size(37, 20);
			this.menuItemFile.Text = "&File";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Name = "menuItemExit";
			this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.menuItemExit.Size = new System.Drawing.Size(134, 22);
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.OnExit);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
			this.menuItemHelp.Name = "menuItemHelp";
			this.menuItemHelp.Size = new System.Drawing.Size(44, 20);
			this.menuItemHelp.Text = "&Help";
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Name = "menuItemAbout";
			this.menuItemAbout.Size = new System.Drawing.Size(116, 22);
			this.menuItemAbout.Text = "&About...";
			this.menuItemAbout.Click += new System.EventHandler(this.OpenAboutForm);
			// 
			// menuViewVideo
			// 
			this.menuViewVideo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemApi2,
            this.menuItemApiV2Related,
            this.menuItemApiV2Responses,
            this.menuItemApi3,
            this.menuItemWeb,
            this.toolStripSeparator1,
            this.menuItemYouTube});
			this.menuViewVideo.Name = "viewMenu";
			this.menuViewVideo.Size = new System.Drawing.Size(192, 142);
			// 
			// menuItemApi2
			// 
			this.menuItemApi2.Image = global::YtAnalytics.Resources.ServerBrowse_16;
			this.menuItemApi2.Name = "menuItemApi2";
			this.menuItemApi2.Size = new System.Drawing.Size(191, 22);
			this.menuItemApi2.Text = "APIv2 information";
			// 
			// menuItemApiV2Related
			// 
			this.menuItemApiV2Related.Image = global::YtAnalytics.Resources.ServerBrowse_16;
			this.menuItemApiV2Related.Name = "menuItemApiV2Related";
			this.menuItemApiV2Related.Size = new System.Drawing.Size(191, 22);
			this.menuItemApiV2Related.Text = "APIv2 related videos";
			// 
			// menuItemApiV2Responses
			// 
			this.menuItemApiV2Responses.Image = global::YtAnalytics.Resources.ServerBrowse_16;
			this.menuItemApiV2Responses.Name = "menuItemApiV2Responses";
			this.menuItemApiV2Responses.Size = new System.Drawing.Size(191, 22);
			this.menuItemApiV2Responses.Text = "APIv2 response videos";
			// 
			// menuItemApi3
			// 
			this.menuItemApi3.Image = global::YtAnalytics.Resources.ServerBrowse_16;
			this.menuItemApi3.Name = "menuItemApi3";
			this.menuItemApi3.Size = new System.Drawing.Size(191, 22);
			this.menuItemApi3.Text = "APIv3 information";
			// 
			// menuItemWeb
			// 
			this.menuItemWeb.Image = global::YtAnalytics.Resources.GlobeBrowse_16;
			this.menuItemWeb.Name = "menuItemWeb";
			this.menuItemWeb.Size = new System.Drawing.Size(191, 22);
			this.menuItemWeb.Text = "Web statistics";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
			// 
			// menuItemYouTube
			// 
			this.menuItemYouTube.Image = global::YtAnalytics.Resources.Globe_16;
			this.menuItemYouTube.Name = "menuItemYouTube";
			this.menuItemYouTube.Size = new System.Drawing.Size(191, 22);
			this.menuItemYouTube.Text = "Open in YouTube";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 730);
			this.Controls.Add(this.toolStripContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "FormMain";
			this.Text = "YouTube Analytics";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.sideMenu.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.menuViewVideo.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.SplitContainer splitContainer;
		private DotNetApi.Windows.Controls.SideMenu sideMenu;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolStripMenuItem menuItemFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemExit;
		private System.Windows.Forms.Label labelNotAvailable;
		private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private Controls.ControlSideCalendar controlSideLog;
		private DotNetApi.Windows.Controls.SideTreeView controlSideConfiguration;
		private DotNetApi.Windows.Controls.SideTreeView controlSideBrowser;
		private System.Windows.Forms.ContextMenuStrip menuViewVideo;
		private System.Windows.Forms.ToolStripMenuItem menuItemApi2;
		private System.Windows.Forms.ToolStripMenuItem menuItemApiV2Related;
		private System.Windows.Forms.ToolStripMenuItem menuItemApiV2Responses;
		private System.Windows.Forms.ToolStripMenuItem menuItemApi3;
		private System.Windows.Forms.ToolStripMenuItem menuItemWeb;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menuItemYouTube;
		private DotNetApi.Windows.Controls.SideTreeView controlSideComments;
		private DotNetApi.Windows.Controls.SideTreeView controlSideDatabase;
		private DotNetApi.Windows.Controls.SideTreeView controlSideSpiders;
		private DotNetApi.Windows.Controls.SideTreeView controlSideTesting;
		private DotNetApi.Windows.Controls.SideTreeView controlSidePlanetLab;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemBrowser;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemDatabase;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemSpiders;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemPlanetLab;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemTesting;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemConfiguration;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemLog;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuItemComments;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelLeft;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelMiddle;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelRight;
	}
}

