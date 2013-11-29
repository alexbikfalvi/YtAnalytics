﻿namespace InetTools.Controls
{
	partial class ControlCdnFinder
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlCdnFinder));
			this.splitContainer = new DotNetApi.Windows.Controls.ToolSplitContainer();
			this.panelTool = new DotNetApi.Windows.Controls.ThemeControl();
			this.panelDomains = new System.Windows.Forms.Panel();
			this.splitContainerDomains = new DotNetApi.Windows.Controls.ToolSplitContainer();
			this.themeTabControl1 = new DotNetApi.Windows.Controls.ThemeTabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.listViewSites = new System.Windows.Forms.ListView();
			this.columnHeaderIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderResources = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.controlSite = new InetTools.Controls.ControlCdnFinderSite();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.labelServer = new System.Windows.Forms.ToolStripLabel();
			this.textBoxUrl = new System.Windows.Forms.ToolStripTextBox();
			this.buttonSettings = new System.Windows.Forms.ToolStripButton();
			this.separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.buttonOpen = new System.Windows.Forms.ToolStripButton();
			this.separator2 = new System.Windows.Forms.ToolStripSeparator();
			this.buttonStart = new System.Windows.Forms.ToolStripButton();
			this.buttonStop = new System.Windows.Forms.ToolStripButton();
			this.separator3 = new System.Windows.Forms.ToolStripSeparator();
			this.buttonSave = new System.Windows.Forms.ToolStripDropDownButton();
			this.menuItemSaveSites = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSaveResources = new System.Windows.Forms.ToolStripMenuItem();
			this.controlLog = new InetAnalytics.Controls.Log.ControlLogList();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panelTool.SuspendLayout();
			this.panelDomains.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerDomains)).BeginInit();
			this.splitContainerDomains.Panel1.SuspendLayout();
			this.splitContainerDomains.Panel2.SuspendLayout();
			this.splitContainerDomains.SuspendLayout();
			this.themeTabControl1.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.panelTool);
			this.splitContainer.Panel1Border = false;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.controlLog);
			this.splitContainer.Panel2Border = false;
			this.splitContainer.Size = new System.Drawing.Size(800, 600);
			this.splitContainer.SplitterDistance = 425;
			this.splitContainer.SplitterWidth = 5;
			this.splitContainer.TabIndex = 3;
			// 
			// panelTool
			// 
			this.panelTool.Controls.Add(this.panelDomains);
			this.panelTool.Controls.Add(this.toolStrip);
			this.panelTool.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelTool.Location = new System.Drawing.Point(0, 0);
			this.panelTool.Name = "panelTool";
			this.panelTool.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
			this.panelTool.ShowBorder = true;
			this.panelTool.ShowTitle = true;
			this.panelTool.Size = new System.Drawing.Size(800, 425);
			this.panelTool.TabIndex = 0;
			this.panelTool.Title = "Content Delivery Networks Finder";
			// 
			// panelDomains
			// 
			this.panelDomains.Controls.Add(this.splitContainerDomains);
			this.panelDomains.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDomains.Location = new System.Drawing.Point(1, 47);
			this.panelDomains.Name = "panelDomains";
			this.panelDomains.Padding = new System.Windows.Forms.Padding(4);
			this.panelDomains.Size = new System.Drawing.Size(798, 377);
			this.panelDomains.TabIndex = 3;
			// 
			// splitContainerDomains
			// 
			this.splitContainerDomains.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerDomains.Location = new System.Drawing.Point(4, 4);
			this.splitContainerDomains.Name = "splitContainerDomains";
			// 
			// splitContainerDomains.Panel1
			// 
			this.splitContainerDomains.Panel1.Controls.Add(this.themeTabControl1);
			this.splitContainerDomains.Panel1.Controls.Add(this.listViewSites);
			this.splitContainerDomains.Panel1.Padding = new System.Windows.Forms.Padding(1);
			// 
			// splitContainerDomains.Panel2
			// 
			this.splitContainerDomains.Panel2.Controls.Add(this.controlSite);
			this.splitContainerDomains.Panel2.Padding = new System.Windows.Forms.Padding(1);
			this.splitContainerDomains.Size = new System.Drawing.Size(790, 369);
			this.splitContainerDomains.SplitterDistance = 390;
			this.splitContainerDomains.SplitterWidth = 5;
			this.splitContainerDomains.TabIndex = 2;
			this.splitContainerDomains.UseTheme = false;
			// 
			// themeTabControl1
			// 
			this.themeTabControl1.Controls.Add(this.tabPage1);
			this.themeTabControl1.Controls.Add(this.tabPage2);
			this.themeTabControl1.Location = new System.Drawing.Point(72, 85);
			this.themeTabControl1.Name = "themeTabControl1";
			this.themeTabControl1.SelectedIndex = 0;
			this.themeTabControl1.Size = new System.Drawing.Size(200, 100);
			this.themeTabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(192, 74);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(192, 74);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// listViewSites
			// 
			this.listViewSites.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewSites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIndex,
            this.columnHeaderSite,
            this.columnHeaderUrl,
            this.columnHeaderResources});
			this.listViewSites.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewSites.FullRowSelect = true;
			this.listViewSites.GridLines = true;
			this.listViewSites.HideSelection = false;
			this.listViewSites.Location = new System.Drawing.Point(1, 1);
			this.listViewSites.MultiSelect = false;
			this.listViewSites.Name = "listViewSites";
			this.listViewSites.Size = new System.Drawing.Size(388, 367);
			this.listViewSites.SmallImageList = this.imageList;
			this.listViewSites.TabIndex = 0;
			this.listViewSites.UseCompatibleStateImageBehavior = false;
			this.listViewSites.View = System.Windows.Forms.View.Details;
			this.listViewSites.SelectedIndexChanged += new System.EventHandler(this.OnSiteSelectionChanged);
			// 
			// columnHeaderIndex
			// 
			this.columnHeaderIndex.Text = "Index";
			// 
			// columnHeaderSite
			// 
			this.columnHeaderSite.Text = "Site";
			this.columnHeaderSite.Width = 150;
			// 
			// columnHeaderUrl
			// 
			this.columnHeaderUrl.Text = "URL";
			this.columnHeaderUrl.Width = 150;
			// 
			// columnHeaderResources
			// 
			this.columnHeaderResources.Text = "Resources";
			this.columnHeaderResources.Width = 80;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "GlobeQuestion");
			this.imageList.Images.SetKeyName(1, "GlobeSuccess");
			this.imageList.Images.SetKeyName(2, "GlobeWarning");
			this.imageList.Images.SetKeyName(3, "GlobeError");
			// 
			// controlSite
			// 
			this.controlSite.AutoScroll = true;
			this.controlSite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlSite.Location = new System.Drawing.Point(1, 1);
			this.controlSite.Name = "controlSite";
			this.controlSite.Size = new System.Drawing.Size(393, 367);
			this.controlSite.TabIndex = 0;
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelServer,
            this.textBoxUrl,
            this.buttonSettings,
            this.separator1,
            this.buttonOpen,
            this.separator2,
            this.buttonStart,
            this.buttonStop,
            this.separator3,
            this.buttonSave});
			this.toolStrip.Location = new System.Drawing.Point(1, 22);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(798, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// labelServer
			// 
			this.labelServer.Name = "labelServer";
			this.labelServer.Size = new System.Drawing.Size(42, 22);
			this.labelServer.Text = "Server:";
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(200, 25);
			this.textBoxUrl.TextChanged += new System.EventHandler(this.OnInputChanged);
			// 
			// buttonSettings
			// 
			this.buttonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonSettings.Image = global::InetTools.Properties.Resources.Settings_16;
			this.buttonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonSettings.Name = "buttonSettings";
			this.buttonSettings.Size = new System.Drawing.Size(23, 22);
			this.buttonSettings.Text = "&Settings";
			this.buttonSettings.Click += new System.EventHandler(this.OnSettingsClick);
			// 
			// separator1
			// 
			this.separator1.Name = "separator1";
			this.separator1.Size = new System.Drawing.Size(6, 25);
			// 
			// buttonOpen
			// 
			this.buttonOpen.Image = global::InetTools.Properties.Resources.Open_16;
			this.buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(65, 22);
			this.buttonOpen.Text = "&Open...";
			this.buttonOpen.Click += new System.EventHandler(this.OnOpen);
			// 
			// separator2
			// 
			this.separator2.Name = "separator2";
			this.separator2.Size = new System.Drawing.Size(6, 25);
			// 
			// buttonStart
			// 
			this.buttonStart.Enabled = false;
			this.buttonStart.Image = global::InetTools.Properties.Resources.PlayStart_16;
			this.buttonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(51, 22);
			this.buttonStart.Text = "&Start";
			this.buttonStart.Click += new System.EventHandler(this.OnStart);
			// 
			// buttonStop
			// 
			this.buttonStop.Enabled = false;
			this.buttonStop.Image = global::InetTools.Properties.Resources.PlayStop_16;
			this.buttonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(51, 22);
			this.buttonStop.Text = "St&op";
			this.buttonStop.Click += new System.EventHandler(this.OnStop);
			// 
			// separator3
			// 
			this.separator3.Name = "separator3";
			this.separator3.Size = new System.Drawing.Size(6, 25);
			// 
			// buttonSave
			// 
			this.buttonSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSaveSites,
            this.menuItemSaveResources});
			this.buttonSave.Image = global::InetTools.Properties.Resources.Save_16;
			this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(69, 22);
			this.buttonSave.Text = "Sa&ve...";
			// 
			// menuItemSaveSites
			// 
			this.menuItemSaveSites.Name = "menuItemSaveSites";
			this.menuItemSaveSites.Size = new System.Drawing.Size(136, 22);
			this.menuItemSaveSites.Text = "Sites...";
			this.menuItemSaveSites.Click += new System.EventHandler(this.OnSaveSites);
			// 
			// menuItemSaveResources
			// 
			this.menuItemSaveResources.Name = "menuItemSaveResources";
			this.menuItemSaveResources.Size = new System.Drawing.Size(136, 22);
			this.menuItemSaveResources.Text = "Resources...";
			this.menuItemSaveResources.Click += new System.EventHandler(this.OnSaveResources);
			// 
			// controlLog
			// 
			this.controlLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.controlLog.Location = new System.Drawing.Point(0, 0);
			this.controlLog.Name = "controlLog";
			this.controlLog.Padding = new System.Windows.Forms.Padding(1, 22, 1, 1);
			this.controlLog.ShowBorder = true;
			this.controlLog.ShowTitle = true;
			this.controlLog.Size = new System.Drawing.Size(800, 170);
			this.controlLog.TabIndex = 0;
			this.controlLog.Title = "Event Log";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "XML files (*.xml)|*.xml";
			this.saveFileDialog.Title = "Save Sites";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Alexa ranking XML files (*.xml)|*.xml";
			this.openFileDialog.Title = "Open Sites List";
			// 
			// ControlCdnFinder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "ControlCdnFinder";
			this.Size = new System.Drawing.Size(800, 600);
			this.Controls.SetChildIndex(this.splitContainer, 0);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.panelTool.ResumeLayout(false);
			this.panelTool.PerformLayout();
			this.panelDomains.ResumeLayout(false);
			this.splitContainerDomains.Panel1.ResumeLayout(false);
			this.splitContainerDomains.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerDomains)).EndInit();
			this.splitContainerDomains.ResumeLayout(false);
			this.themeTabControl1.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DotNetApi.Windows.Controls.ToolSplitContainer splitContainer;
		private DotNetApi.Windows.Controls.ThemeControl panelTool;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton buttonStart;
		private System.Windows.Forms.ToolStripButton buttonStop;
		private System.Windows.Forms.ToolStripSeparator separator1;
		private System.Windows.Forms.ToolStripLabel labelServer;
		private System.Windows.Forms.ListView listViewSites;
		private System.Windows.Forms.ColumnHeader columnHeaderSite;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripTextBox textBoxUrl;
		private System.Windows.Forms.ToolStripSeparator separator2;
		private System.Windows.Forms.ToolStripSeparator separator3;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private InetAnalytics.Controls.Log.ControlLogList controlLog;
		private System.Windows.Forms.ToolStripButton buttonSettings;
		private System.Windows.Forms.ToolStripButton buttonOpen;
		private System.Windows.Forms.ColumnHeader columnHeaderResources;
		private DotNetApi.Windows.Controls.ToolSplitContainer splitContainerDomains;
		private System.Windows.Forms.Panel panelDomains;
		private System.Windows.Forms.ColumnHeader columnHeaderIndex;
		private ControlCdnFinderSite controlSite;
		private System.Windows.Forms.ToolStripDropDownButton buttonSave;
		private System.Windows.Forms.ToolStripMenuItem menuItemSaveSites;
		private System.Windows.Forms.ToolStripMenuItem menuItemSaveResources;
		private System.Windows.Forms.ColumnHeader columnHeaderUrl;
		private DotNetApi.Windows.Controls.ThemeTabControl themeTabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
	}
}
