
namespace AutoAnimeAB
{
    partial class HoatHinh247
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
            this.btnGetLink = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAnimes = new System.Windows.Forms.ComboBox();
            this.btnUpdateLink = new System.Windows.Forms.Button();
            this.gridViewEpisodes = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.btnEpisode = new System.Windows.Forms.Button();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSplit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbServerSub = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEpisodes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetLink
            // 
            this.btnGetLink.Location = new System.Drawing.Point(505, 63);
            this.btnGetLink.Name = "btnGetLink";
            this.btnGetLink.Size = new System.Drawing.Size(182, 32);
            this.btnGetLink.TabIndex = 2;
            this.btnGetLink.Text = "Video/m3u8";
            this.btnGetLink.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Chọn anime";
            // 
            // cbAnimes
            // 
            this.cbAnimes.FormattingEnabled = true;
            this.cbAnimes.Location = new System.Drawing.Point(113, 105);
            this.cbAnimes.Name = "cbAnimes";
            this.cbAnimes.Size = new System.Drawing.Size(386, 24);
            this.cbAnimes.TabIndex = 16;
            // 
            // btnUpdateLink
            // 
            this.btnUpdateLink.Location = new System.Drawing.Point(505, 97);
            this.btnUpdateLink.Name = "btnUpdateLink";
            this.btnUpdateLink.Size = new System.Drawing.Size(182, 32);
            this.btnUpdateLink.TabIndex = 17;
            this.btnUpdateLink.Text = "Thêm/Cập nhật tập phim";
            this.btnUpdateLink.UseVisualStyleBackColor = true;
            this.btnUpdateLink.Click += new System.EventHandler(this.btnUpdateLink_Click);
            // 
            // gridViewEpisodes
            // 
            this.gridViewEpisodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewEpisodes.Location = new System.Drawing.Point(27, 135);
            this.gridViewEpisodes.Name = "gridViewEpisodes";
            this.gridViewEpisodes.RowHeadersWidth = 51;
            this.gridViewEpisodes.RowTemplate.Height = 24;
            this.gridViewEpisodes.Size = new System.Drawing.Size(660, 181);
            this.gridViewEpisodes.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Link server";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(113, 18);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(197, 22);
            this.txtLink.TabIndex = 21;
            // 
            // btnEpisode
            // 
            this.btnEpisode.Location = new System.Drawing.Point(505, 18);
            this.btnEpisode.Name = "btnEpisode";
            this.btnEpisode.Size = new System.Drawing.Size(182, 43);
            this.btnEpisode.TabIndex = 22;
            this.btnEpisode.Text = "Lấy tập phim";
            this.btnEpisode.UseVisualStyleBackColor = true;
            this.btnEpisode.Click += new System.EventHandler(this.btnGetEpisodes_Click);
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(365, 47);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(134, 22);
            this.txtEnd.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 25;
            this.label6.Text = "Tập kết thúc";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(113, 47);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(142, 22);
            this.txtStart.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Tập bắt đầu";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "Loại bỏ";
            // 
            // txtSplit
            // 
            this.txtSplit.Location = new System.Drawing.Point(113, 74);
            this.txtSplit.Name = "txtSplit";
            this.txtSplit.Size = new System.Drawing.Size(142, 22);
            this.txtSplit.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Server";
            // 
            // cbServerSub
            // 
            this.cbServerSub.FormattingEnabled = true;
            this.cbServerSub.Location = new System.Drawing.Point(381, 18);
            this.cbServerSub.Name = "cbServerSub";
            this.cbServerSub.Size = new System.Drawing.Size(118, 24);
            this.cbServerSub.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "Định dạng";
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(365, 74);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(134, 24);
            this.cbType.TabIndex = 32;
            // 
            // HoatHinh247
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 334);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSplit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbServerSub);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnEpisode);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridViewEpisodes);
            this.Controls.Add(this.btnUpdateLink);
            this.Controls.Add(this.cbAnimes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGetLink);
            this.Name = "HoatHinh247";
            this.Text = "Thêm/Cập nhật link Server Hoathinh247";
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEpisodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGetLink;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbAnimes;
        private System.Windows.Forms.Button btnUpdateLink;
        private System.Windows.Forms.DataGridView gridViewEpisodes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Button btnEpisode;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSplit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbServerSub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbType;
    }
}