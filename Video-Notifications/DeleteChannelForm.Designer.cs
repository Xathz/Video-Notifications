namespace VideoNotifications {
    partial class DeleteChannelForm {
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
            this.DoubleClickLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChannelsListView
            // 
            this.ChannelsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChannelColumn});
            this.ChannelsListView.FullRowSelect = true;
            this.ChannelsListView.HideSelection = false;
            this.ChannelsListView.Location = new System.Drawing.Point(12, 12);
            this.ChannelsListView.MultiSelect = false;
            this.ChannelsListView.Name = "ChannelsListView";
            this.ChannelsListView.Size = new System.Drawing.Size(255, 327);
            this.ChannelsListView.SmallImageList = this.ChannelsImageList;
            this.ChannelsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ChannelsListView.TabIndex = 1;
            this.ChannelsListView.TileSize = new System.Drawing.Size(1, 1);
            this.ChannelsListView.UseCompatibleStateImageBehavior = false;
            this.ChannelsListView.View = System.Windows.Forms.View.Details;
            this.ChannelsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ChannelsListView_MouseDoubleClick);
            // 
            // ChannelColumn
            // 
            this.ChannelColumn.Text = "Channel";
            this.ChannelColumn.Width = 225;
            // 
            // ChannelsImageList
            // 
            this.ChannelsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ChannelsImageList.ImageSize = new System.Drawing.Size(24, 24);
            this.ChannelsImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // DoubleClickLabel
            // 
            this.DoubleClickLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DoubleClickLabel.AutoSize = true;
            this.DoubleClickLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.DoubleClickLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.DoubleClickLabel.Location = new System.Drawing.Point(12, 343);
            this.DoubleClickLabel.Name = "DoubleClickLabel";
            this.DoubleClickLabel.Size = new System.Drawing.Size(168, 13);
            this.DoubleClickLabel.TabIndex = 2;
            this.DoubleClickLabel.Text = "Double-click to delete the channel";
            // 
            // DeleteChannelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 366);
            this.Controls.Add(this.DoubleClickLabel);
            this.Controls.Add(this.ChannelsListView);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DeleteChannelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Delete a channel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeleteChannelForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DeleteChannelForm_FormClosed);
            this.Load += new System.EventHandler(this.DeleteChannelForm_Load);
            this.LocationChanged += new System.EventHandler(this.DeleteChannelForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ChannelsListView;
        private System.Windows.Forms.ColumnHeader ChannelColumn;
        private System.Windows.Forms.Label DoubleClickLabel;
        private System.Windows.Forms.ImageList ChannelsImageList;
    }
}