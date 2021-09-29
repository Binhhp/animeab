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
using System.Threading;
using System.Threading.Tasks;

namespace AutoAnimeAB.AutoAnime
{
    public class AutoMappingAnimeVsub
    {
        public List<Episode> GetEpisodes(
            string url, int start, int end, int split, int serverSub)
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
                    var episodeMeta = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("ul[class='list-episode tab-pane']")));

                    Thread.Sleep(1000);

                    HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
                    web.LoadHtml(driver.PageSource);

                    var servers = web.DocumentNode.Descendants("ul").Where(sv => sv.HasClass("list-episode")).ToList();

                    int index = 0;
                    if (servers.Count > 1) index = serverSub;
                    
                    var nodes = servers[index].Descendants("li").Where(n => n.HasClass("episode"));
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
                        var episode = new Episode();

                        var elementTagHref = item.Descendants("a").FirstOrDefault();
                        episode.Number = count;
                        episode.Link = elementTagHref.Attributes["href"].Value.ToString();

                        episodes.Add(episode);
                        count++;
                    }

                    driver.Close();
                }

                return episodes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Episode>> GetLinkEpisodes(List<Episode> episodes, int server)
        {
            try
            {
                string chromePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chrome");
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--test-type", "--start-maximized");
                options.BinaryLocation = Path.Combine(chromePath, "chrome.exe");
                string apiAnimeVsub = "http://animevietsub.tv/ajax/player?v=2019a";

                List<Episode> episodeArray = new List<Episode>();
                foreach (var item in episodes)
                {
                    try
                    {
                        using (IWebDriver driver = new ChromeDriver(chromePath, options))
                        {
                            driver.Navigate().GoToUrl(item.Link);

                            WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            var episodeMeta = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div[id='links-backup']")));
                            Thread.Sleep(1000);

                            HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
                            web.LoadHtml(driver.PageSource);

                            Thread.Sleep(1000);

                            var linksBackup = web.GetElementbyId("links-backup");
                            var elementTagHrefs = linksBackup.Descendants("a").ToList();

                            var dict = new Dictionary<string, string>();
                            PayloadAnimeVsub request = new PayloadAnimeVsub
                            {
                                link = elementTagHrefs[server].Attributes["data-href"].Value.ToString(),
                                play = elementTagHrefs[server].Attributes["data-play"].Value.ToString(),
                                id = elementTagHrefs[server].Attributes["data-id"].Value.ToString()
                            };

                            dict.Add("link", request.link);
                            dict.Add("play", request.play);
                            dict.Add("id", request.id);
                            dict.Add("backuplinks", request.backuplinks);
                            HttpClient client = new HttpClient();

                            client.DefaultRequestHeaders.Clear();
                            
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                            var res = await client.PostAsync(apiAnimeVsub, new FormUrlEncodedContent(dict));

                            var dataPreview = await res.Content.ReadAsStringAsync();
                            ResponseAnimeVsub response = JsonConvert.DeserializeObject<ResponseAnimeVsub>(dataPreview);

                            var episode = new Episode();
                            episode.Number = item.Number;
                            episode.Link = response.link[0].file;

                            episodeArray.Add(episode);
                            driver.Close();
                        }
                        Thread.Sleep(500);
                    }
                    catch
                    {
                        continue;
                    }
                }

                return episodeArray;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
