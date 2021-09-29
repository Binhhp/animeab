using AutoAnimeAB.Message;
using AutoAnimeAB.Models;
using AutoAnimeAB.Reponsitory.Anime;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAnimeAB
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            var combobox = Task.Run(() => ComboboxAnimes());
            var dataGridView = Task.Run(() => DataGridAnime());
            Task.WaitAll(combobox, dataGridView);
            txtStart.Text = "1";
            txtEnd.Text = "1";
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUrl.Text) && txtUrl.Text.IndexOf("https://") == -1)
                {
                    JMessageBox.ErrorMessage("Nhập url get anime");
                }
                else
                {

                    btnRun.Text = "Loading...";
                    btnRun.Enabled = false;

                    var auto = new AutoMappingControl();
                    int episodeStart = 1, episodeEnd = 1;
                    try
                    {
                        episodeStart = Convert.ToInt32(txtStart.Text);
                        episodeEnd = Convert.ToInt32(txtEnd.Text);
                    }
                    catch
                    {
                        JMessageBox.ErrorMessage("Số tập bắt đầu bằng kiểu số");
                    }

                    var result = await auto.GetAnimeAsync(txtUrl.Text, cbAnime.SelectedValue.ToString(), episodeStart, episodeEnd);
                    btnRun.Text = "Run";
                    btnRun.Enabled = true;
                    if (result)
                    {
                        JMessageBox.SuccessMessage("Hoàn tất get anime");
                        await DataGridAnime();
                    }
                    else
                    {
                        JMessageBox.ErrorMessage("Error");
                    }

                }
            }
            catch (Exception ex)
            {
                btnRun.Text = "Run";
                btnRun.Enabled = true;
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
                bindingSource.DataSource = data.Where(x => x.IsStatus < 3).ToList();
                cbAnime.DataSource = bindingSource.DataSource;
                cbAnime.DisplayMember = "Title";
                cbAnime.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                JMessageBox.ErrorMessage(ex.Message);
            }
        }

        public async Task DataGridAnime()
        {
            try
            {
                var animes = new ReponsitoryAnime();
                var bindingSource = new BindingSource();

                var data = await animes.GetAnimes();
                var datagrid = AutoMapping.GetAnimeDomains(data);
                bindingSource.DataSource = datagrid;
                dataGridView1.DataSource = bindingSource.DataSource;

            }
            catch (Exception ex)
            {
                JMessageBox.ErrorMessage(ex.Message);
            }
        }

        private void getLinkToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var formGetLink = new GetLink();
            formGetLink.Show();
        }

        private void thêmURLAnimeServerAnimeVsubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formAnimeVsub = new AnimeVsub();
            formAnimeVsub.Show();
        }

        private void serverHoathinh247ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formHoatHinh247 = new HoatHinh247();
            formHoatHinh247.Show();
        }
    }

    public class AnimeDomain
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public int Episode { get; set; }
        public int EpisodeMoment { get; set; }
        public string Series { get; set; }
    }
}
