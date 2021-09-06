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
    public partial class GetLink : Form
    {
        private List<Episode> episodes = new List<Episode>();
        private List<Episode> episodeLinks = new List<Episode>();

        public GetLink()
        {
            InitializeComponent();
            var combobox = Task.Run(() => ComboboxAnimes());
            var cbServer = Task.Run(() => ComboboxServer());
            var cbServerMovie = Task.Run(() => ComboboxServerMovie());
            var cbTypeMovie = Task.Run(() => ComboboxServerVuighe());
            Task.WaitAll(combobox, cbServer, cbServerMovie, cbTypeMovie);
            txtStart.Text = "1";
            txtEnd.Text = "1";
        }

        private void btnEpisode_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtLink.Text) && txtLink.Text.IndexOf("https://") == -1)
                {
                    JMessageBox.ErrorMessage("Nhập url get video");
                }
                else
                {
                    btnEpisode.Text = "Loading...";
                    btnEpisode.Enabled = false;

                    var auto = new AutoMappingControl();
                    int start = Convert.ToInt32(txtStart.Text);
                    int end = Convert.ToInt32(txtEnd.Text);
                    int server = Convert.ToInt32(cbServer.SelectedValue.ToString());
                    var result = auto.GetEpisodesAsync(txtLink.Text, server, start, end);
                    btnEpisode.Text = "Lấy tập phim";
                    btnEpisode.Enabled = true;

                    var bindingSource = new BindingSource();
                    bindingSource.DataSource = result;
                    gridviewEpisodes.DataSource = bindingSource.DataSource;
                    episodes = result;
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

        private void btnLink_Click(object sender, EventArgs e)
        {

            try
            {

                if(episodes.Count > 0)
                {
                    btnLink.Text = "Loading...";
                    btnLink.Enabled = false;

                    var auto = new AutoMappingControl();
                    int serverMovie = Convert.ToInt32(cbServerMovie.SelectedValue.ToString());
                    int typeMovie = Convert.ToInt32(cbTypeVuighe.SelectedValue.ToString());

                    var result = auto.GetLinkEpisodeMovie(episodes, serverMovie, typeMovie);
                    episodeLinks.Clear();
                    episodeLinks = result.ToList();
                    btnLink.Text = "Video/Iframe";
                    btnLink.Enabled = true;

                    var bindingSource = new BindingSource();
                    bindingSource.DataSource = result;
                    gridviewLinkEpisode.DataSource = bindingSource.DataSource;

                    JMessageBox.SuccessMessage("Hoàn thành get link video/iframe");
                }
                else
                {
                    JMessageBox.ErrorMessage("Không có list episodes nào! Run Get Episode để lấy list link");
                }
            }
            catch (Exception ex)
            {
                btnLink.Text = "Video/Iframe";
                btnLink.Enabled = true;
                JMessageBox.ErrorMessage(ex.Message);
            }
        }

        public async Task ComboboxAnimes()
        {

            try
            {
                var animes = new ReponsitoryAnime();

                var bindingSource = new BindingSource();

                var data = await animes.GetAnimes();

                bindingSource.DataSource = data.Where(x => x.IsStatus > 1).OrderByDescending(x => x.DateRelease).ToList();
                cbAnimesForm.DataSource = bindingSource.DataSource;
                cbAnimesForm.DisplayMember = "Title";
                cbAnimesForm.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                JMessageBox.ErrorMessage(ex.Message);
            }
        }

        public void ComboboxServer()
        {
            var servers = new List<Server> {
                new Server { ServerName = "Server Vuighe.Net", Number = 5 },
                new Server { ServerName = "Server 1 (Anime47)", Number = 0 },
                new Server { ServerName = "Server 2 (Anime47)", Number = 1 },
                new Server { ServerName = "Server 3 (Anime47)", Number = 2 },
                new Server { ServerName = "Server 4 (Anime47)", Number = 3 },
                new Server { ServerName = "Server 5 (Anime47)", Number = 4 }
            };

            var bindingSource = new BindingSource();
            bindingSource.DataSource = servers.ToList();
            cbServer.DataSource = bindingSource.DataSource;
            cbServer.DisplayMember = "ServerName";
            cbServer.ValueMember = "Number";
        }


        public void ComboboxServerMovie()
        {
            var serverMovies = new List<Server>
            {
               new Server { Number = 2, ServerName = "Server Vuighe.Net" },
               new Server { Number = 0, ServerName = "Server Anime47 (Fa)" },
               new Server { Number = 1, ServerName = "Server Fembed (Fe)" }
            };
            var bindingSource = new BindingSource();
            bindingSource.DataSource = serverMovies.ToList();
            cbServerMovie.DataSource = bindingSource.DataSource;
            cbServerMovie.DisplayMember = "ServerName";
            cbServerMovie.ValueMember = "Number";
        }

        public void ComboboxServerVuighe()
        {
            var server = new List<Server> {
                new Server { Number = 3, ServerName = "IFrame/Video" },
                new Server { Number = 1, ServerName = "IFrame" },
                new Server { Number = 2, ServerName = "Video" }
            };
            var bindingSource = new BindingSource();
            bindingSource.DataSource = server.ToList();
            cbTypeVuighe.DataSource = bindingSource.DataSource;
            cbTypeVuighe.DisplayMember = "ServerName";
            cbTypeVuighe.ValueMember = "Number";
        }
        private async void btnUpdateLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (episodes.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(cbAnimesForm.SelectedValue.ToString()))
                        JMessageBox.ErrorMessage("Chọn anime cần update");
                    else
                    {
                        btnUpdateLink.Text = "Loading...";
                        btnUpdateLink.Enabled = false;

                        var animeDetail = new ReponsitoryAnimeDetail();
                        var result = await animeDetail.UpdateLinkAsync(cbAnimesForm.SelectedValue.ToString(), episodeLinks);

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
    }
}
