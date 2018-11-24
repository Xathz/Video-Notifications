namespace VideoNotifications {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.ChannelsListView = new System.Windows.Forms.ListView();
            this.ChannelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChannelsImageList = new System.Windows.Forms.ImageList(this.components);
            this.VideosListView = new System.Windows.Forms.ListView();
            this.TitleColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.VideoPictureBox = new System.Windows.Forms.PictureBox();
            this.VideoDescriptionLabel = new System.Windows.Forms.Label();
            this.VideoLinkLabel = new System.Windows.Forms.LinkLabel();
            this.ModifierLabel = new System.Windows.Forms.Label();
            this.DismissedColorLabel = new System.Windows.Forms.Label();
            this.IgnoredColorLabel = new System.Windows.Forms.Label();
            this.UnwatchedColorLabel = new System.Windows.Forms.Label();
            this.WatchedColorLabel = new System.Windows.Forms.Label();
            this.DurationLabel = new System.Windows.Forms.Label();
            this.PostedLabel = new System.Windows.Forms.Label();
            this.DurationLabelLabel = new System.Windows.Forms.Label();
            this.PostedLabelLabel = new System.Windows.Forms.Label();
            this.TrayNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.WebsiteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddChannelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteChannelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ForceCheckMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PauseNotificationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MinimizeToTrayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LightIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YouTubeCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.YouTubeCheckWorker = new System.ComponentModel.BackgroundWorker();
            this.GeneralToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.VideoInfoPanel = new System.Windows.Forms.Panel();
            this.VideoStatusPanel = new System.Windows.Forms.Panel();
            this.SetVideoStatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.VideoPictureBox)).BeginInit();
            this.TrayContextMenuStrip.SuspendLayout();
            this.VideoInfoPanel.SuspendLayout();
            this.VideoStatusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChannelsListView
            // 
            this.ChannelsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChannelColumn});
            this.ChannelsListView.FullRowSelect = true;
            this.ChannelsListView.HideSelection = false;
            this.ChannelsListView.Location = new System.Drawing.Point(12, 12);
            this.ChannelsListView.MultiSelect = false;
            this.ChannelsListView.Name = "ChannelsListView";
            this.ChannelsListView.Size = new System.Drawing.Size(194, 269);
            this.ChannelsListView.SmallImageList = this.ChannelsImageList;
            this.ChannelsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ChannelsListView.TabIndex = 0;
            this.ChannelsListView.TileSize = new System.Drawing.Size(1, 1);
            this.ChannelsListView.UseCompatibleStateImageBehavior = false;
            this.ChannelsListView.View = System.Windows.Forms.View.Details;
            this.ChannelsListView.SelectedIndexChanged += new System.EventHandler(this.ChannelsListView_SelectedIndexChanged);
            // 
            // ChannelColumn
            // 
            this.ChannelColumn.Text = "Channel";
            this.ChannelColumn.Width = 170;
            // 
            // ChannelsImageList
            // 
            this.ChannelsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ChannelsImageList.ImageSize = new System.Drawing.Size(24, 24);
            this.ChannelsImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // VideosListView
            // 
            this.VideosListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TitleColumn,
            this.PostedColumn});
            this.VideosListView.FullRowSelect = true;
            this.VideosListView.Location = new System.Drawing.Point(212, 12);
            this.VideosListView.MultiSelect = false;
            this.VideosListView.Name = "VideosListView";
            this.VideosListView.Size = new System.Drawing.Size(560, 269);
            this.VideosListView.TabIndex = 0;
            this.VideosListView.TileSize = new System.Drawing.Size(1, 1);
            this.VideosListView.UseCompatibleStateImageBehavior = false;
            this.VideosListView.View = System.Windows.Forms.View.Details;
            this.VideosListView.SelectedIndexChanged += new System.EventHandler(this.VideosListView_SelectedIndexChanged);
            // 
            // TitleColumn
            // 
            this.TitleColumn.Text = "Title";
            this.TitleColumn.Width = 393;
            // 
            // PostedColumn
            // 
            this.PostedColumn.Text = "Posted";
            this.PostedColumn.Width = 140;
            // 
            // VideoPictureBox
            // 
            this.VideoPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.VideoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VideoPictureBox.ErrorImage = null;
            this.VideoPictureBox.InitialImage = null;
            this.VideoPictureBox.Location = new System.Drawing.Point(12, 287);
            this.VideoPictureBox.Name = "VideoPictureBox";
            this.VideoPictureBox.Size = new System.Drawing.Size(384, 216);
            this.VideoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VideoPictureBox.TabIndex = 16;
            this.VideoPictureBox.TabStop = false;
            this.GeneralToolTip.SetToolTip(this.VideoPictureBox, "Open video in browser");
            this.VideoPictureBox.Visible = false;
            this.VideoPictureBox.Click += new System.EventHandler(this.VideoPictureBox_Click);
            // 
            // VideoDescriptionLabel
            // 
            this.VideoDescriptionLabel.BackColor = System.Drawing.Color.Transparent;
            this.VideoDescriptionLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoDescriptionLabel.Location = new System.Drawing.Point(9, 506);
            this.VideoDescriptionLabel.Name = "VideoDescriptionLabel";
            this.VideoDescriptionLabel.Size = new System.Drawing.Size(387, 46);
            this.VideoDescriptionLabel.TabIndex = 17;
            // 
            // VideoLinkLabel
            // 
            this.VideoLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.VideoLinkLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoLinkLabel.Location = new System.Drawing.Point(9, 4);
            this.VideoLinkLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.VideoLinkLabel.Name = "VideoLinkLabel";
            this.VideoLinkLabel.Size = new System.Drawing.Size(352, 32);
            this.VideoLinkLabel.TabIndex = 20;
            this.VideoLinkLabel.TabStop = true;
            this.VideoLinkLabel.Text = "Video title";
            this.VideoLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GeneralToolTip.SetToolTip(this.VideoLinkLabel, "Open video in browser");
            this.VideoLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.VideoURLLinkLabel_LinkClicked);
            // 
            // ModifierLabel
            // 
            this.ModifierLabel.AutoSize = true;
            this.ModifierLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModifierLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ModifierLabel.Location = new System.Drawing.Point(109, 42);
            this.ModifierLabel.Name = "ModifierLabel";
            this.ModifierLabel.Size = new System.Drawing.Size(152, 13);
            this.ModifierLabel.TabIndex = 23;
            this.ModifierLabel.Text = "Shift + Click to mark all videos";
            // 
            // DismissedColorLabel
            // 
            this.DismissedColorLabel.BackColor = System.Drawing.Color.LemonChiffon;
            this.DismissedColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DismissedColorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DismissedColorLabel.Location = new System.Drawing.Point(188, 21);
            this.DismissedColorLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.DismissedColorLabel.Name = "DismissedColorLabel";
            this.DismissedColorLabel.Size = new System.Drawing.Size(80, 17);
            this.DismissedColorLabel.TabIndex = 20;
            this.DismissedColorLabel.Text = "Dismissed";
            this.DismissedColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DismissedColorLabel.Click += new System.EventHandler(this.DismissedColorLabel_Click);
            // 
            // IgnoredColorLabel
            // 
            this.IgnoredColorLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.IgnoredColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IgnoredColorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.IgnoredColorLabel.Location = new System.Drawing.Point(274, 21);
            this.IgnoredColorLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.IgnoredColorLabel.Name = "IgnoredColorLabel";
            this.IgnoredColorLabel.Size = new System.Drawing.Size(80, 17);
            this.IgnoredColorLabel.TabIndex = 20;
            this.IgnoredColorLabel.Text = "Ignored";
            this.IgnoredColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.IgnoredColorLabel.Click += new System.EventHandler(this.IgnoredColorLabel_Click);
            // 
            // UnwatchedColorLabel
            // 
            this.UnwatchedColorLabel.BackColor = System.Drawing.SystemColors.Window;
            this.UnwatchedColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnwatchedColorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UnwatchedColorLabel.Location = new System.Drawing.Point(102, 21);
            this.UnwatchedColorLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.UnwatchedColorLabel.Name = "UnwatchedColorLabel";
            this.UnwatchedColorLabel.Size = new System.Drawing.Size(80, 17);
            this.UnwatchedColorLabel.TabIndex = 20;
            this.UnwatchedColorLabel.Text = "Unwatched";
            this.UnwatchedColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UnwatchedColorLabel.Click += new System.EventHandler(this.UnwatchedColorLabel_Click);
            // 
            // WatchedColorLabel
            // 
            this.WatchedColorLabel.BackColor = System.Drawing.Color.LightGreen;
            this.WatchedColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WatchedColorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WatchedColorLabel.Location = new System.Drawing.Point(16, 21);
            this.WatchedColorLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.WatchedColorLabel.Name = "WatchedColorLabel";
            this.WatchedColorLabel.Size = new System.Drawing.Size(80, 17);
            this.WatchedColorLabel.TabIndex = 20;
            this.WatchedColorLabel.Text = "Watched";
            this.WatchedColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WatchedColorLabel.Click += new System.EventHandler(this.WatchedColorLabel_Click);
            // 
            // DurationLabel
            // 
            this.DurationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DurationLabel.AutoSize = true;
            this.DurationLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DurationLabel.Location = new System.Drawing.Point(66, 58);
            this.DurationLabel.Name = "DurationLabel";
            this.DurationLabel.Size = new System.Drawing.Size(90, 15);
            this.DurationLabel.TabIndex = 6;
            this.DurationLabel.Text = "Duration length";
            // 
            // PostedLabel
            // 
            this.PostedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostedLabel.AutoSize = true;
            this.PostedLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostedLabel.Location = new System.Drawing.Point(66, 40);
            this.PostedLabel.Name = "PostedLabel";
            this.PostedLabel.Size = new System.Drawing.Size(66, 15);
            this.PostedLabel.TabIndex = 1;
            this.PostedLabel.Text = "Date / time";
            // 
            // DurationLabelLabel
            // 
            this.DurationLabelLabel.AutoSize = true;
            this.DurationLabelLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DurationLabelLabel.Location = new System.Drawing.Point(10, 58);
            this.DurationLabelLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.DurationLabelLabel.Name = "DurationLabelLabel";
            this.DurationLabelLabel.Size = new System.Drawing.Size(57, 15);
            this.DurationLabelLabel.TabIndex = 5;
            this.DurationLabelLabel.Text = "Duration:";
            // 
            // PostedLabelLabel
            // 
            this.PostedLabelLabel.AutoSize = true;
            this.PostedLabelLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostedLabelLabel.Location = new System.Drawing.Point(21, 40);
            this.PostedLabelLabel.Name = "PostedLabelLabel";
            this.PostedLabelLabel.Size = new System.Drawing.Size(46, 15);
            this.PostedLabelLabel.TabIndex = 0;
            this.PostedLabelLabel.Text = "Posted:";
            // 
            // TrayNotifyIcon
            // 
            this.TrayNotifyIcon.ContextMenuStrip = this.TrayContextMenuStrip;
            this.TrayNotifyIcon.Text = "Video Notifications for YouTube";
            this.TrayNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayNotifyIcon_MouseClick);
            // 
            // TrayContextMenuStrip
            // 
            this.TrayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WebsiteMenuItem,
            this.ToolStripSeparator1,
            this.AddChannelMenuItem,
            this.DeleteChannelMenuItem,
            this.ForceCheckMenuItem,
            this.ToolStripSeparator2,
            this.PauseNotificationsMenuItem,
            this.MinimizeToTrayMenuItem,
            this.LightIconMenuItem,
            this.ToolStripSeparator3,
            this.ExitMenuItem});
            this.TrayContextMenuStrip.Name = "TrayContextMenuStrip";
            this.TrayContextMenuStrip.Size = new System.Drawing.Size(218, 198);
            // 
            // WebsiteMenuItem
            // 
            this.WebsiteMenuItem.Image = global::VideoNotifications.Properties.Resources.Website;
            this.WebsiteMenuItem.Name = "WebsiteMenuItem";
            this.WebsiteMenuItem.Size = new System.Drawing.Size(217, 22);
            this.WebsiteMenuItem.Text = "Website";
            this.WebsiteMenuItem.Click += new System.EventHandler(this.WebsiteMenuItem_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(214, 6);
            // 
            // AddChannelMenuItem
            // 
            this.AddChannelMenuItem.Image = global::VideoNotifications.Properties.Resources.Add;
            this.AddChannelMenuItem.Name = "AddChannelMenuItem";
            this.AddChannelMenuItem.Size = new System.Drawing.Size(217, 22);
            this.AddChannelMenuItem.Text = "Add channel";
            this.AddChannelMenuItem.Click += new System.EventHandler(this.AddChannelMenuItem_Click);
            // 
            // DeleteChannelMenuItem
            // 
            this.DeleteChannelMenuItem.Image = global::VideoNotifications.Properties.Resources.Delete;
            this.DeleteChannelMenuItem.Name = "DeleteChannelMenuItem";
            this.DeleteChannelMenuItem.Size = new System.Drawing.Size(217, 22);
            this.DeleteChannelMenuItem.Text = "Delete channel";
            this.DeleteChannelMenuItem.Click += new System.EventHandler(this.DeleteChannelMenuItem_Click);
            // 
            // ForceCheckMenuItem
            // 
            this.ForceCheckMenuItem.Image = global::VideoNotifications.Properties.Resources.Refresh;
            this.ForceCheckMenuItem.Name = "ForceCheckMenuItem";
            this.ForceCheckMenuItem.Size = new System.Drawing.Size(217, 22);
            this.ForceCheckMenuItem.Text = "Force check for new videos";
            this.ForceCheckMenuItem.Click += new System.EventHandler(this.ForceCheckMenuItem_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(214, 6);
            // 
            // PauseNotificationsMenuItem
            // 
            this.PauseNotificationsMenuItem.CheckOnClick = true;
            this.PauseNotificationsMenuItem.Name = "PauseNotificationsMenuItem";
            this.PauseNotificationsMenuItem.Size = new System.Drawing.Size(217, 22);
            this.PauseNotificationsMenuItem.Text = "Pause notifications";
            this.PauseNotificationsMenuItem.CheckedChanged += new System.EventHandler(this.PauseNotificationsMenuItem_CheckedChanged);
            // 
            // MinimizeToTrayMenuItem
            // 
            this.MinimizeToTrayMenuItem.CheckOnClick = true;
            this.MinimizeToTrayMenuItem.Name = "MinimizeToTrayMenuItem";
            this.MinimizeToTrayMenuItem.Size = new System.Drawing.Size(217, 22);
            this.MinimizeToTrayMenuItem.Text = "Minimize to tray";
            this.MinimizeToTrayMenuItem.CheckedChanged += new System.EventHandler(this.MinimizeToTrayMenuItem_CheckedChanged);
            // 
            // LightIconMenuItem
            // 
            this.LightIconMenuItem.CheckOnClick = true;
            this.LightIconMenuItem.Name = "LightIconMenuItem";
            this.LightIconMenuItem.Size = new System.Drawing.Size(217, 22);
            this.LightIconMenuItem.Text = "Use light icon";
            this.LightIconMenuItem.CheckedChanged += new System.EventHandler(this.LightIconMenuItem_CheckedChanged);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(214, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Image = global::VideoNotifications.Properties.Resources.Close;
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(217, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // YouTubeCheckTimer
            // 
            this.YouTubeCheckTimer.Interval = 1200000;
            this.YouTubeCheckTimer.Tick += new System.EventHandler(this.YouTubeCheckTimer_Tick);
            // 
            // YouTubeCheckWorker
            // 
            this.YouTubeCheckWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.YouTubeCheckWorker_DoWork);
            this.YouTubeCheckWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.YouTubeCheckWorker_RunWorkerCompleted);
            // 
            // GeneralToolTip
            // 
            this.GeneralToolTip.BackColor = System.Drawing.SystemColors.Control;
            // 
            // VideoInfoPanel
            // 
            this.VideoInfoPanel.BackColor = System.Drawing.Color.Transparent;
            this.VideoInfoPanel.Controls.Add(this.DurationLabel);
            this.VideoInfoPanel.Controls.Add(this.VideoLinkLabel);
            this.VideoInfoPanel.Controls.Add(this.PostedLabel);
            this.VideoInfoPanel.Controls.Add(this.PostedLabelLabel);
            this.VideoInfoPanel.Controls.Add(this.DurationLabelLabel);
            this.VideoInfoPanel.Location = new System.Drawing.Point(402, 344);
            this.VideoInfoPanel.Name = "VideoInfoPanel";
            this.VideoInfoPanel.Size = new System.Drawing.Size(370, 76);
            this.VideoInfoPanel.TabIndex = 23;
            this.VideoInfoPanel.Visible = false;
            // 
            // VideoStatusPanel
            // 
            this.VideoStatusPanel.BackColor = System.Drawing.Color.Transparent;
            this.VideoStatusPanel.Controls.Add(this.SetVideoStatusLabel);
            this.VideoStatusPanel.Controls.Add(this.ModifierLabel);
            this.VideoStatusPanel.Controls.Add(this.DismissedColorLabel);
            this.VideoStatusPanel.Controls.Add(this.WatchedColorLabel);
            this.VideoStatusPanel.Controls.Add(this.IgnoredColorLabel);
            this.VideoStatusPanel.Controls.Add(this.UnwatchedColorLabel);
            this.VideoStatusPanel.Location = new System.Drawing.Point(402, 287);
            this.VideoStatusPanel.Name = "VideoStatusPanel";
            this.VideoStatusPanel.Size = new System.Drawing.Size(370, 58);
            this.VideoStatusPanel.TabIndex = 24;
            this.VideoStatusPanel.Visible = false;
            // 
            // SetVideoStatusLabel
            // 
            this.SetVideoStatusLabel.AutoSize = true;
            this.SetVideoStatusLabel.Location = new System.Drawing.Point(144, 4);
            this.SetVideoStatusLabel.Name = "SetVideoStatusLabel";
            this.SetVideoStatusLabel.Size = new System.Drawing.Size(88, 13);
            this.SetVideoStatusLabel.TabIndex = 24;
            this.SetVideoStatusLabel.Text = "Set video status";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.VideoStatusPanel);
            this.Controls.Add(this.VideoInfoPanel);
            this.Controls.Add(this.VideoPictureBox);
            this.Controls.Add(this.VideoDescriptionLabel);
            this.Controls.Add(this.VideosListView);
            this.Controls.Add(this.ChannelsListView);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Video Notifications for YouTube";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.VideoPictureBox)).EndInit();
            this.TrayContextMenuStrip.ResumeLayout(false);
            this.VideoInfoPanel.ResumeLayout(false);
            this.VideoInfoPanel.PerformLayout();
            this.VideoStatusPanel.ResumeLayout(false);
            this.VideoStatusPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ChannelsListView;
        private System.Windows.Forms.ColumnHeader ChannelColumn;
        private System.Windows.Forms.ListView VideosListView;
        private System.Windows.Forms.ColumnHeader TitleColumn;
        private System.Windows.Forms.ColumnHeader PostedColumn;
        private System.Windows.Forms.PictureBox VideoPictureBox;
        private System.Windows.Forms.Label VideoDescriptionLabel;
        private System.Windows.Forms.LinkLabel VideoLinkLabel;
        private System.Windows.Forms.Label PostedLabel;
        private System.Windows.Forms.Label PostedLabelLabel;
        private System.Windows.Forms.Label DurationLabel;
        private System.Windows.Forms.Label DurationLabelLabel;
        private System.Windows.Forms.NotifyIcon TrayNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WebsiteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LightIconMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.Timer YouTubeCheckTimer;
        private System.Windows.Forms.ToolStripMenuItem PauseNotificationsMenuItem;
        private System.Windows.Forms.Label DismissedColorLabel;
        private System.Windows.Forms.Label IgnoredColorLabel;
        private System.Windows.Forms.Label UnwatchedColorLabel;
        private System.Windows.Forms.Label WatchedColorLabel;
        private System.Windows.Forms.Label ModifierLabel;
        private System.Windows.Forms.ToolStripMenuItem MinimizeToTrayMenuItem;
        private System.ComponentModel.BackgroundWorker YouTubeCheckWorker;
        private System.Windows.Forms.ImageList ChannelsImageList;
        private System.Windows.Forms.ToolTip GeneralToolTip;
        private System.Windows.Forms.Panel VideoInfoPanel;
        private System.Windows.Forms.Panel VideoStatusPanel;
        private System.Windows.Forms.Label SetVideoStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem AddChannelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteChannelMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ForceCheckMenuItem;
    }
}

