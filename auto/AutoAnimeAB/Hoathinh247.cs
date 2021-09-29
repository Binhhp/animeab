using AutoAnimeAB.AutoAnime;
using AutoAnimeAB.Message;
using AutoAnimeAB.Models;
using AutoAnimeAB.Reponsitory.Anime;
using AutoAnimeAB.Reponsitory.AnimeDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAnimeAB
{
    public partial class HoatHinh247 : Form
    {
        private List<Episode> episodeLinks = new List<Episode>();

        public HoatHinh247()
        {
            InitializeComponent();
            var combobox = Task.Run(() => ComboboxAnimes());
            var cbServerSub = Task.Run(() => ComboboxServerSub());
            var comboboxType = Task.Run(() => ComboboxType());
            Task.WaitAll(combobox, cbServerSub, comboboxType);
            txtStart.Text = "1";
            txtEnd.Text = "1";
            txtSplit.Text = "0";
        }

        void ComboboxType()
        {
            var servers = new List<Server>
            {
                new Server{ ServerName = "m3u8", Number = 0 },
                new Server{ ServerName = "mp4", Number = 1 },
                new Server{ ServerName = "m3u8 dự phòng", Number = 2 }
            };

            var bindingSource = new BindingSource();
            bindingSource.DataSource = servers.ToList();
            cbType.DataSource = bindingSource.DataSource;
            cbType.DisplayMember = "ServerName";
            cbType.ValueMember = "Number";
        }
        void ComboboxServerSub()
        {
            var servers = new List<Server>
            {
                new Server{ ServerName = "Server 1", Number = 0 },
                new Server{ ServerName = "Server 2", Number = 1 },
                new Server{ ServerName = "Server 3", Number = 2 }
            };

            var bindingSource = new BindingSource();
            bindingSource.DataSource = servers.ToList();
            cbServerSub.DataSource = bindingSource.DataSource;
            cbServerSub.DisplayMember = "ServerName";
            cbServerSub.ValueMember = "Number";
        }

        async Task ComboboxAnimes()
        {

            try
            {
                var animes = new ReponsitoryAnime();

                var bindingSource = new BindingSource();

                var data = await animes.GetAnimes();

                bindingSource.DataSource = data.Where(x => x.IsStatus > 1).OrderByDescending(x => x.DateRelease).ToList();
                cbAnimes.DataSource = bindingSource.DataSource;
                cbAnimes.DisplayMember = "Title";
                cbAnimes.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                JMessageBox.ErrorMessage(ex.Message);
            }
        }

        private async void btnUpdateLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (episodeLinks.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(cbAnimes.SelectedValue.ToString()))
                        JMessageBox.ErrorMessage("Chọn anime cần update");
                    else
                    {
                        btnUpdateLink.Text = "Loading...";
                        btnUpdateLink.Enabled = false;

                        var animeDetail = new ReponsitoryAnimeDetail();
                        var result = await animeDetail.UpdateLinkAsync(cbAnimes.SelectedValue.ToString(), episodeLinks);

                        btnUpdateLink.Text = "Thêm/Cập nhật tập phim";
                        btnUpdateLink.Enabled = true;

                        if (!result.Success)
                        {
                            JMessageBox.ErrorMessage(result.Error);
                        }
                        else
                        {
                            JMessageBox.SuccessMessage("Hoàn tất update link video");
                        }
                    }
                }
                else
                {
                    JMessageBox.ErrorMessage("Không có list episodes nào! Run Get Episode để lấy list link");
                }
            }
            catch (Exception ex)
            {
                btnUpdateLink.Text = "Thêm/Cập nhật tập phim";
                btnUpdateLink.Enabled = true;
                JMessageBox.ErrorMessage(ex.Message);
            }
        }

        private async void btnGetEpisodes_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtLink.Text) && txtLink.Text.IndexOf("http") == -1)
                {
                    JMessageBox.ErrorMessage("Nhập url get video");
                }
                else
                {
                    btnEpisode.Text = "Loading...";
                    btnEpisode.Enabled = false;

                    var auto = new AutoMappingHoathinh247();
                    int start = Convert.ToInt32(txtStart.Text);
                    int end = Convert.ToInt32(txtEnd.Text);
                    int split = Convert.ToInt32(txtSplit.Text);
                    int server = Convert.ToInt32(cbServerSub.SelectedValue.ToString());
                    int type = Convert.ToInt32(cbType.SelectedValue.ToString());

                    var result = await auto.GetEpisodes(txtLink.Text, start, end, split, server);

                    btnEpisode.Text = "Lấy tập phim";
                    btnEpisode.Enabled = true;

                    var bindingSource = new BindingSource();
                    bindingSource.DataSource = result;
                    gridViewEpisodes.DataSource = bindingSource.DataSource;
                    gridViewEpisodes.Columns[0].Width = 75;
                    gridViewEpisodes.Columns[1].Width = 250;
                    episodeLinks = result;
                    JMessageBox.SuccessMessage("Hoàn thành lấy tập phim");
                }
            }
            catch (Exception ex)
            {
                btnEpisode.Text = "Lấy tập phim";
                btnEpisode.Enabled = true;
                JMessageBox.ErrorMessage(ex.Message);
            }
        }
    }
}
