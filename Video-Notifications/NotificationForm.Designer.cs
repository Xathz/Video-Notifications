namespace VideoNotifications {
    partial class NotificationForm {
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
            this.VideoPictureBox = new System.Windows.Forms.PictureBox();
            this.ChannelPictureBox = new System.Windows.Forms.PictureBox();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.ChannelLabel = new System.Windows.Forms.Label();
            this.WatchedButton = new System.Windows.Forms.Button();
            this.DismissButton = new System.Windows.Forms.Button();
            this.DurationLabel = new System.Windows.Forms.Label();
            this.EnableControlsTimer = new System.Windows.Forms.Timer(this.components);
            this.EnableControlsCountdownTimer = new System.Windows.Forms.Timer(this.components);
            this.GeneralToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.VideoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // VideoPictureBox
            // 
            this.VideoPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.VideoPictureBox.ErrorImage = null;
            this.VideoPictureBox.InitialImage = null;
            this.VideoPictureBox.Location = new System.Drawing.Point(160, -1);
            this.VideoPictureBox.Name = "VideoPictureBox";
            this.VideoPictureBox.Size = new System.Drawing.Size(288, 162);
            this.VideoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VideoPictureBox.TabIndex = 17;
            this.VideoPictureBox.TabStop = false;
            this.VideoPictureBox.Click += new System.EventHandler(this.VideoPictureBox_Click);
            // 
            // ChannelPictureBox
            // 
            this.ChannelPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ChannelPictureBox.ErrorImage = null;
            this.ChannelPictureBox.InitialImage = null;
            this.ChannelPictureBox.Location = new System.Drawing.Point(-1, -1);
            this.ChannelPictureBox.Name = "ChannelPictureBox";
            this.ChannelPictureBox.Size = new System.Drawing.Size(162, 162);
            this.ChannelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ChannelPictureBox.TabIndex = 17;
            this.ChannelPictureBox.TabStop = false;
            this.ChannelPictureBox.Click += new System.EventHandler(this.ChannelPictureBox_Click);
            // 
            // LinePanel
            // 
            this.LinePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LinePanel.Location = new System.Drawing.Point(439, -17);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(10, 216);
            this.LinePanel.TabIndex = 100;
            // 
            // ChannelLabel
            // 
            this.ChannelLabel.BackColor = System.Drawing.Color.Transparent;
            this.ChannelLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelLabel.Location = new System.Drawing.Point(453, 2);
            this.ChannelLabel.Name = "ChannelLabel";
            this.ChannelLabel.Size = new System.Drawing.Size(254, 96);
            this.ChannelLabel.TabIndex = 19;
            this.ChannelLabel.Text = "Message";
            // 
            // WatchedButton
            // 
            this.WatchedButton.Enabled = false;
            this.WatchedButton.Location = new System.Drawing.Point(455, 101);
            this.WatchedButton.Name = "WatchedButton";
            this.WatchedButton.Size = new System.Drawing.Size(252, 23);
            this.WatchedButton.TabIndex = 1;
            this.WatchedButton.TabStop = false;
            this.WatchedButton.Text = "Open video && mark as watched";
            this.WatchedButton.UseVisualStyleBackColor = true;
            this.WatchedButton.Click += new System.EventHandler(this.WatchedButton_Click);
            // 
            // DismissButton
            // 
            this.DismissButton.Enabled = false;
            this.DismissButton.Location = new System.Drawing.Point(455, 130);
            this.DismissButton.Name = "DismissButton";
            this.DismissButton.Size = new System.Drawing.Size(252, 23);
            this.DismissButton.TabIndex = 2;
            this.DismissButton.TabStop = false;
            this.DismissButton.Text = "Dismiss notification";
            this.DismissButton.UseVisualStyleBackColor = true;
            this.DismissButton.Click += new System.EventHandler(this.DismissButton_Click);
            // 
            // DurationLabel
            // 
            this.DurationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DurationLabel.BackColor = System.Drawing.Color.Transparent;
            this.DurationLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DurationLabel.Location = new System.Drawing.Point(395, 145);
            this.DurationLabel.Name = "DurationLabel";
            this.DurationLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DurationLabel.Size = new System.Drawing.Size(56, 16);
            this.DurationLabel.TabIndex = 101;
            this.DurationLabel.Text = "00:00:00";
            this.DurationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EnableControlsTimer
            // 
            this.EnableControlsTimer.Interval = 3000;
            this.EnableControlsTimer.Tick += new System.EventHandler(this.EnableControlsTimer_Tick);
            // 
            // EnableControlsCountdownTimer
            // 
            this.EnableControlsCountdownTimer.Interval = 1000;
            this.EnableControlsCountdownTimer.Tick += new System.EventHandler(this.EnableControlsCountdownTimer_Tick);
            // 
            // GeneralToolTip
            // 
            this.GeneralToolTip.BackColor = System.Drawing.SystemColors.Control;
            // 
            // NotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 160);
            this.Controls.Add(this.DurationLabel);
            this.Controls.Add(this.DismissButton);
            this.Controls.Add(this.WatchedButton);
            this.Controls.Add(this.ChannelLabel);
            this.Controls.Add(this.ChannelPictureBox);
            this.Controls.Add(this.VideoPictureBox);
            this.Controls.Add(this.LinePanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NotificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Video Notifications for YouTube";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotificationForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NotificationForm_FormClosed);
            this.Load += new System.EventHandler(this.NotificationForm_Load);
            this.LocationChanged += new System.EventHandler(this.NotificationForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.VideoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox VideoPictureBox;
        private System.Windows.Forms.PictureBox ChannelPictureBox;
        private System.Windows.Forms.Panel LinePanel;
        private System.Windows.Forms.Label ChannelLabel;
        private System.Windows.Forms.Button WatchedButton;
        private System.Windows.Forms.Button DismissButton;
        private System.Windows.Forms.Label DurationLabel;
        private System.Windows.Forms.Timer EnableControlsTimer;
        private System.Windows.Forms.Timer EnableControlsCountdownTimer;
        private System.Windows.Forms.ToolTip GeneralToolTip;
    }
}