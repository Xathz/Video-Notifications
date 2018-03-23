namespace VideoNotifications.Forms {
    partial class AddChannelForm {
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
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchInfoLabel = new System.Windows.Forms.Label();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.ChannelsListView = new System.Windows.Forms.ListView();
            this.ChannelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DoubleClickLabel = new System.Windows.Forms.Label();
            this.ChannelsImageList = new System.Windows.Forms.ImageList(this.components);
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.ResultsLabel = new System.Windows.Forms.Label();
            this.SearchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchTextBox.Location = new System.Drawing.Point(4, 20);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(312, 22);
            this.SearchTextBox.TabIndex = 2;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(322, 19);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 3;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(2, 4);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(112, 13);
            this.SearchLabel.TabIndex = 2;
            this.SearchLabel.Text = "Search for a channel";
            // 
            // SearchInfoLabel
            // 
            this.SearchInfoLabel.AutoSize = true;
            this.SearchInfoLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.SearchInfoLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.SearchInfoLabel.Location = new System.Drawing.Point(2, 44);
            this.SearchInfoLabel.Name = "SearchInfoLabel";
            this.SearchInfoLabel.Size = new System.Drawing.Size(259, 13);
            this.SearchInfoLabel.TabIndex = 3;
            this.SearchInfoLabel.Text = "You can enter the channel name or url to the channel";
            // 
            // SearchPanel
            // 
            this.SearchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchPanel.Controls.Add(this.SearchTextBox);
            this.SearchPanel.Controls.Add(this.SearchButton);
            this.SearchPanel.Controls.Add(this.ResultsLabel);
            this.SearchPanel.Controls.Add(this.SearchInfoLabel);
            this.SearchPanel.Controls.Add(this.SearchLabel);
            this.SearchPanel.Location = new System.Drawing.Point(10, 6);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(399, 74);
            this.SearchPanel.TabIndex = 1;
            // 
            // ChannelsListView
            // 
            this.ChannelsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChannelColumn});
            this.ChannelsListView.FullRowSelect = true;
            this.ChannelsListView.HideSelection = false;
            this.ChannelsListView.Location = new System.Drawing.Point(14, 83);
            this.ChannelsListView.MultiSelect = false;
            this.ChannelsListView.Name = "ChannelsListView";
            this.ChannelsListView.Size = new System.Drawing.Size(393, 390);
            this.ChannelsListView.SmallImageList = this.ChannelsImageList;
            this.ChannelsListView.TabIndex = 4;
            this.ChannelsListView.TileSize = new System.Drawing.Size(1, 1);
            this.ChannelsListView.UseCompatibleStateImageBehavior = false;
            this.ChannelsListView.View = System.Windows.Forms.View.Details;
            this.ChannelsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ChannelsListView_MouseDoubleClick);
            // 
            // ChannelColumn
            // 
            this.ChannelColumn.Text = "Channel";
            this.ChannelColumn.Width = 365;
            // 
            // DoubleClickLabel
            // 
            this.DoubleClickLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DoubleClickLabel.AutoSize = true;
            this.DoubleClickLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.DoubleClickLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DoubleClickLabel.Location = new System.Drawing.Point(13, 477);
            this.DoubleClickLabel.Name = "DoubleClickLabel";
            this.DoubleClickLabel.Size = new System.Drawing.Size(158, 13);
            this.DoubleClickLabel.TabIndex = 6;
            this.DoubleClickLabel.Text = "Double-click to add the channel";
            // 
            // ChannelsImageList
            // 
            this.ChannelsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ChannelsImageList.ImageSize = new System.Drawing.Size(24, 24);
            this.ChannelsImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // LoadingLabel
            // 
            this.LoadingLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoadingLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadingLabel.Location = new System.Drawing.Point(36, 131);
            this.LoadingLabel.Name = "LoadingLabel";
            this.LoadingLabel.Size = new System.Drawing.Size(348, 26);
            this.LoadingLabel.TabIndex = 7;
            this.LoadingLabel.Text = "Loading...";
            this.LoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LoadingLabel.Visible = false;
            // 
            // ResultsLabel
            // 
            this.ResultsLabel.AutoSize = true;
            this.ResultsLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.ResultsLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ResultsLabel.Location = new System.Drawing.Point(1, 57);
            this.ResultsLabel.Name = "ResultsLabel";
            this.ResultsLabel.Size = new System.Drawing.Size(186, 13);
            this.ResultsLabel.TabIndex = 3;
            this.ResultsLabel.Text = "Results will be sorted by most relevant";
            // 
            // AddChannelForm
            // 
            this.AcceptButton = this.SearchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 500);
            this.Controls.Add(this.LoadingLabel);
            this.Controls.Add(this.DoubleClickLabel);
            this.Controls.Add(this.ChannelsListView);
            this.Controls.Add(this.SearchPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddChannelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Add a channel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddChannelForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddChannelForm_FormClosed);
            this.Load += new System.EventHandler(this.AddChannelForm_Load);
            this.LocationChanged += new System.EventHandler(this.AddChannelForm_LocationChanged);
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Label SearchInfoLabel;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.ListView ChannelsListView;
        private System.Windows.Forms.ColumnHeader ChannelColumn;
        private System.Windows.Forms.Label DoubleClickLabel;
        private System.Windows.Forms.ImageList ChannelsImageList;
        private System.Windows.Forms.Label LoadingLabel;
        private System.Windows.Forms.Label ResultsLabel;
    }
}