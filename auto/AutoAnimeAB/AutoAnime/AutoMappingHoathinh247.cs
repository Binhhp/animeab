using AutoAnimeAB.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutoAnimeAB.AutoAnime
{
    public class AutoMappingHoathinh247
    {
        public async Task<List<Episode>> GetEpisodes(
            string url, int start, int end, int split, int serverSub, int type = 0)
        {
            try
            {
                string chromePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chrome");
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--test-type", "--start-maximized");
                options.BinaryLocation = Path.Combine(chromePath, "chrome.exe");

                var episodes = new List<Episode>();

                using (IWebDriver driver = new ChromeDriver(chromePath, options))
                {
                    driver.Navigate().GoToUrl(url);

                    WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    var episodeMeta = driverWait.Until(
                        SeleniumExtras.WaitHelpers.ExpectedConditions
                        .ElementExists(By.CssSelector("ul[class='list-episode']")));

                    Thread.Sleep(1000);

                    HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
                    web.LoadHtml(driver.PageSource);

                    var servers = web.DocumentNode.Descendants("ul")
                        .Where(sv => sv.HasClass("list-episode")).ToList();

                    driver.Close();
                    int index = 0;
                    if (servers.Count > 1) index = serverSub;
                    
                    var nodes = servers[index].Descendants("li")
                        .Where(n => n.HasClass("episode"));
                    int count = 1;

                    if(split > 0)
                    {
                        nodes = nodes.Skip(split);
                    }

                    if (nodes.Count() > start && start > 1)
                    {
                        nodes = nodes.Skip(start - 1);
                        count = start;
                    }

                    if (nodes.Count() > start && end > 1 && end > start)
                    {
                        nodes = nodes.Take((end + 1) - start);
                    }

                    foreach (var item in nodes)
                    {
                        var elementTagHref = item.Descendants("a").FirstOrDefault();
                        string id = elementTagHref.Attributes["data-id"].Value.ToString();
                        string episode = elementTagHref.Attributes["data-ep"].Value.ToString();

                        var episodeModel = new Episode { Number = count };
                        count++;
                        try
                        {
                            episodeModel.Link = await GetLinkEpisodes(id, episode, type);
                        }
                        catch
                        {
                            continue;
                        }

                        episodes.Add(episodeModel);
                    }

                }

                return episodes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GetLinkEpisodes(string id, string episode, int type)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri("https://hoathinh247tv.com/ajax/5AB412E5033F55E");
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id", id),
                    new KeyValuePair<string, string>("ep", episode)
                });

                var result = await client.PostAsync(uri, formContent);
                string rep = await result.Content.ReadAsStringAsync();
                Regex regx = new Regex("https://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;]*)?",
                    RegexOptions.IgnoreCase);
                MatchCollection match = regx.Matches(rep);

                string testMatch = match[type].Value.ToString();
                if(type != 1) testMatch = testMatch.Replace("306084399", "167335343");
                return testMatch;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
