
namespace AutoAnimeAB
{
    partial class GetLink
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
            this.gridviewLinkEpisode = new System.Windows.Forms.DataGridView();
            this.gridviewEpisodes = new System.Windows.Forms.DataGridView();
            this.btnUpdateLink = new System.Windows.Forms.Button();
            this.cbAnimesForm = new System.Windows.Forms.ComboBox();
            this.btnEpisode = new System.Windows.Forms.Button();
            this.btnLink = new System.Windows.Forms.Button();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.cbServerMovie = new System.Windows.Forms.ComboBox();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbTypeVuighe = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewLinkEpisode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewEpisodes)).BeginInit();
            this.SuspendLayout();
            // 
            // gridviewLinkEpisode
            // 
            this.gridviewLinkEpisode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewLinkEpisode.Location = new System.Drawing.Point(16, 309);
            this.gridviewLinkEpisode.Name = "gridviewLinkEpisode";
            this.gridviewLinkEpisode.RowHeadersWidth = 51;
            this.gridviewLinkEpisode.RowTemplate.Height = 24;
            this.gridviewLinkEpisode.Size = new System.Drawing.Size(705, 228);
            this.gridviewLinkEpisode.TabIndex = 0;
            // 
            // gridviewEpisodes
            // 
            this.gridviewEpisodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewEpisodes.Location = new System.Drawing.Point(14, 150);
            this.gridviewEpisodes.Name = "gridviewEpisodes";
            this.gridviewEpisodes.RowHeadersWidth = 51;
            this.gridviewEpisodes.RowTemplate.Height = 24;
            this.gridviewEpisodes.Size = new System.Drawing.Size(706, 153);
            this.gridviewEpisodes.TabIndex = 3;
            // 
            // btnUpdateLink
            // 
            this.btnUpdateLink.Location = new System.Drawing.Point(539, 112);
            this.btnUpdateLink.Name = "btnUpdateLink";
            this.btnUpdateLink.Size = new System.Drawing.Size(182, 32);
            this.btnUpdateLink.TabIndex = 5;
            this.btnUpdateLink.Text = "Thêm/Cập nhật tập phim";
            this.btnUpdateLink.UseVisualStyleBackColor = true;
            this.btnUpdateLink.Click += new System.EventHandler(this.btnUpdateLink_Click);
            // 
            // cbAnimesForm
            // 
            this.cbAnimesForm.FormattingEnabled = true;
            this.cbAnimesForm.Location = new System.Drawing.Point(105, 117);
            this.cbAnimesForm.Name = "cbAnimesForm";
            this.cbAnimesForm.Size = new System.Drawing.Size(428, 24);
            this.cbAnimesForm.TabIndex = 6;
            // 
            // btnEpisode
            // 
            this.btnEpisode.Location = new System.Drawing.Point(611, 20);
            this.btnEpisode.Name = "btnEpisode";
            this.btnEpisode.Size = new System.Drawing.Size(110, 48);
            this.btnEpisode.TabIndex = 2;
            this.btnEpisode.Text = "Lấy tập phim";
            this.btnEpisode.UseVisualStyleBackColor = true;
            this.btnEpisode.Click += new System.EventHandler(this.btnEpisode_Click);
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(611, 74);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(110, 32);
            this.btnLink.TabIndex = 4;
            this.btnLink.Text = "Video/Iframe";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // cbServer
            // 
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Location = new System.Drawing.Point(435, 17);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(154, 24);
            this.cbServer.TabIndex = 7;
            // 
            // cbServerMovie
            // 
            this.cbServerMovie.FormattingEnabled = true;
            this.cbServerMovie.Location = new System.Drawing.Point(104, 79);
            this.cbServerMovie.Name = "cbServerMovie";
            this.cbServerMovie.Size = new System.Drawing.Size(178, 24);
            this.cbServerMovie.TabIndex = 8;
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(105, 20);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(249, 22);
            this.txtLink.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "URL lấy tập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Chọn anime";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Tập bắt đầu";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(104, 48);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(183, 22);
            this.txtStart.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tập kết thúc";
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(406, 48);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(183, 22);
            this.txtEnd.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Video/Iframe Vuighe.Net";
            // 
            // cbTypeVuighe
            // 
            this.cbTypeVuighe.FormattingEnabled = true;
            this.cbTypeVuighe.Location = new System.Drawing.Point(465, 79);
            this.cbTypeVuighe.Name = "cbTypeVuighe";
            this.cbTypeVuighe.Size = new System.Drawing.Size(124, 24);
            this.cbTypeVuighe.TabIndex = 20;
            // 
            // GetLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 546);
            this.Controls.Add(this.cbTypeVuighe);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUpdateLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbAnimesForm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEpisode);
            this.Controls.Add(this.btnLink);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.cbServer);
            this.Controls.Add(this.cbServerMovie);
            this.Controls.Add(this.gridviewEpisodes);
            this.Controls.Add(this.gridviewLinkEpisode);
            this.Name = "GetLink";
            this.Text = "Thêm/Cập nhật link Server Anime47 - Vuighe";
            ((System.ComponentModel.ISupportInitialize)(this.gridviewLinkEpisode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewEpisodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridviewLinkEpisode;
        private System.Windows.Forms.DataGridView gridviewEpisodes;
        private System.Windows.Forms.Button btnUpdateLink;
        private System.Windows.Forms.ComboBox cbAnimesForm;
        private System.Windows.Forms.Button btnEpisode;
        private System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.ComboBox cbServerMovie;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbTypeVuighe;
    }
}