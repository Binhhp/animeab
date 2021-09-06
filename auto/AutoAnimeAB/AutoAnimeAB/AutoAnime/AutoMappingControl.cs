using AutoAnimeAB.Models;
using AutoAnimeAB.Reponsitory.AnimeDetail;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoAnimeAB
{
    public class AutoMappingControl
    {
        public async Task<bool> GetAnimeAsync(string url, string animeKey, int episodeStart, int episodeEnd)
        {
            string chromePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chrome");
            var result = TaskAnime(url, chromePath, episodeStart, episodeEnd);

            for (var i = 0; i < result.Count(); i++)
            {
                var anime = new AnimeModel();
                anime.Key = animeKey + "-ep-" + (i + 1);
                anime.Image = result[i].Images;
                anime.Link = "https://www.fembed.com/v/qgdl1be3d0mg85p";
                anime.Title = result[i].Title;
                anime.Episode = (i + 1);

                try
                {
                    var response = new ReponsitoryAnimeDetail();
                    await response.CreateMovieAsync(anime, animeKey);
                }
                catch
                {
                    continue;
                }
            }
            return true;
        }

        public List<AnimeResponseControl> TaskAnime(string url, string pathChromeExe, int episodeStart, int episodeEnd)
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--test-type", "--start-maximized");
                options.BinaryLocation = Path.Combine(pathChromeExe, "chrome.exe");


                List<AnimeResponseControl> animes = new List<AnimeResponseControl>();
                string dir = pathChromeExe;
                using (IWebDriver driver = new ChromeDriver(dir, options))
                {
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(1000);

                    WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    var episodeMeta = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div[3]/div[2]/div[2]/div[2]")));
                    Thread.Sleep(1500);

                    HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
                    web.LoadHtml(driver.PageSource);

                    var nodes = web.DocumentNode.Descendants("div").Where(n => n.HasClass("episode-item"));
                    if(nodes.Count() > episodeStart && episodeStart > 1)
                    {
                        nodes = nodes.Skip(episodeStart - 1);
                    }

                    if(nodes.Count() > episodeStart && episodeEnd > 1 && episodeEnd > episodeStart)
                    {
                        nodes = nodes.Take((episodeEnd + 1) - episodeStart);
                    }

                    foreach (var item in nodes)
                    {
                        var anime = new AnimeResponseControl();

                        var eleImage = item.Descendants("div").Where(x => x.GetAttributeValue("class", "").Contains("episode-item-thumbnail")).FirstOrDefault();
                        anime.Images = eleImage.SelectSingleNode(".//img").Attributes["src"].Value;

                        anime.Title = item.Descendants("div").Where(x => x.GetAttributeValue("class", "").Contains("episode-item-title")).FirstOrDefault().InnerText;
                        animes.Add(anime);
                    }

                    driver.Close();
                }
                return animes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get episodes
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public List<Episode> GetEpisodesAsync(string url, int server, int start, int end)
        {
            try
            {
                string chromePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chrome");
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--test-type", "--start-maximized");
                options.BinaryLocation = Path.Combine(chromePath, "chrome.exe");

                List<Episode> episodeArray = new List<Episode>();
                using (IWebDriver driver = new ChromeDriver(chromePath, options))
                {
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(3000);

                    if (server != 5)
                    {
                        WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        var fa = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[5]/div[3]/div[1]/div[1]/div/div[2]/span[1]")));

                        var elementServer = driver.FindElement(By.XPath("/html/body/div[5]/div[3]/div[1]/div[1]/div/div[5]"));
                        if (elementServer != null)
                        {
                            var elementEpisodes = elementServer.FindElements(By.TagName("ul"));

                            if (elementEpisodes.Count > 0)
                            {
                                var firstElement = elementEpisodes[server];
                                var listEpisode = firstElement.FindElements(By.TagName("li"));
                                if (listEpisode.Count > 0)
                                {
                                    int x = 0;
                                    int y = listEpisode.Count;
                                    int count = 1;

                                    if (listEpisode.Count() > start && start > 1)
                                    {
                                        x = start - 1;
                                        count = start;
                                    }

                                    if (listEpisode.Count() > start && end > 1 && end > start)
                                    {
                                        y = end;
                                    }

                                    for (var li = x; li < y; li++)
                                    {
                                        var button = listEpisode[li].FindElement(By.TagName("a"));
                                        if (button != null)
                                        {
                                            var episode = new Episode();
                                            episode.Number = count;
                                            episode.Link = button.GetAttribute("href");

                                            episodeArray.Add(episode);
                                            count++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        var episodeMeta = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div[3]/div[2]/div[2]/div[2]")));
                        Thread.Sleep(5000);

                        HtmlAgilityPack.HtmlDocument web = new HtmlAgilityPack.HtmlDocument();
                        web.LoadHtml(driver.PageSource);

                        var nodes = web.DocumentNode.Descendants("div").Where(n => n.HasClass("episode-item"));
                        int count = 1;
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
                            var tagA = item.Descendants("a").FirstOrDefault();
                            var episode = new Episode();
                            episode.Number = count;
                            episode.Link = "https://vuighe.net" + tagA.Attributes["href"].Value.ToString();

                            episodeArray.Add(episode);
                            count++;
                        }
                    }

                    driver.Close();
                }

                return episodeArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Ge link episode
        /// </summary>
        /// <param name="episodes"></param>
        /// <returns></returns>
        public List<Episode> GetLinkEpisodeMovie(List<Episode> episodes, int server, int typeVuighe)
        {
            try
            {
                string chromePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chrome");
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--test-type", "--start-maximized");
                options.BinaryLocation = Path.Combine(chromePath, "chrome.exe");

                List<Episode> episodeArray = new List<Episode>();

                foreach (var item in episodes)
                {
                    using (IWebDriver driver = new ChromeDriver(chromePath, options))
                    {
                        driver.Navigate().GoToUrl(item.Link);
                        Thread.Sleep(3000);

                        WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        try
                        {
                            var fa = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("video")));
                        }
                        catch
                        {
                            continue;
                        }

                        if (server == 1)
                        {
                            try
                            {
                                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                Thread.Sleep(1000);
                                js.ExecuteScript("window.scroll(200, 200);");
                                Thread.Sleep(1000);
                                driver.FindElement(By.CssSelector("img[id='img1']")).Click();
                                Thread.Sleep(4000);
                                //Run server FE
                                var listServer = driver.FindElement(By.CssSelector("div[id='clicksv']"));

                                if (listServer != null)
                                {
                                    var tagFE = driver.FindElement(By.CssSelector("span[id='sv4']"));
                                    tagFE.Click();
                                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                                    Thread.Sleep(9000);
                                    try
                                    {
                                        var skip = 
                                            driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[5]/div[3]/div[1]/div[1]/div/div[1]/div/div/div/div/div[2]/div[11]/div[5]")));
                                        skip.Click();

                                        Thread.Sleep(2500);
                                        var iframe = driver.FindElement(By.TagName("iframe"));
                                        var episode = new Episode();
                                        episode.Number = item.Number;
                                        episode.Link = iframe.GetAttribute("src");
                                        episodeArray.Add(episode);
                                        continue;
                                    }
                                    catch
                                    {
                                        driver.Close();
                                        continue;
                                    }
                                }
                            }
                            catch
                            {
                                //Run server FA
                                var autoPlay = driver.FindElement(By.CssSelector("div[id='player']"));

                                if (autoPlay != null)
                                {
                                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                    Thread.Sleep(1000);
                                    driver.FindElement(By.CssSelector("img[id='img1']")).Click();
                                    Thread.Sleep(2000);
                                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                                    Thread.Sleep(1000);
                                    js.ExecuteScript("document.querySelector('video').play();");
                                    Thread.Sleep(9000);
                                    try
                                    {
                                        var skip = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[5]/div[3]/div[1]/div[1]/div/div[1]/div/div/div/div/div[2]/div[11]/div[5]")));
                                        if (skip != null)
                                        {
                                            skip.Click();
                                        }
                                    }
                                    catch
                                    {
                                        driver.Close();
                                        continue;
                                    }
                                }
                            }
                        }
                        if(server == 0)
                        {
                            //Run server FA
                            var autoPlay = driver.FindElement(By.CssSelector("div[id='player']"));

                            if (autoPlay != null)
                            {
                                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                                Thread.Sleep(1000);
                                driver.FindElement(By.CssSelector("img[id='img1']")).Click();
                                Thread.Sleep(2000);
                                driver.SwitchTo().Window(driver.WindowHandles[0]);
                                Thread.Sleep(1000);
                                js.ExecuteScript("document.querySelector('video').play();");
                                Thread.Sleep(9000);
                                try
                                {
                                    var skip = driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[5]/div[3]/div[1]/div[1]/div/div[1]/div/div/div/div/div[2]/div[11]/div[5]")));

                                    if (skip != null)
                                    {
                                        skip.Click();
                                    }
                                }
                                catch
                                {
                                    driver.Close();
                                    continue;
                                }
                            }
                        }

                        Thread.Sleep(3500);
                        try
                        {
                            var cssSelector = "video[class='jw-video jw-reset']";
                            string linkMovie = "";
                            if (server == 2)
                            {
                                if(typeVuighe == 2)
                                {
                                    cssSelector = "video[class='player-video']";
                                }
                                else
                                {
                                    cssSelector = "iframe[class='player-embed']";
                                }

                                try
                                {
                                    IWebElement elementVideo = driver.FindElement(By.CssSelector(cssSelector));
                                    linkMovie = elementVideo.GetAttribute("src");
                                }
                                catch
                                {
                                    if (typeVuighe == 3)
                                    {
                                        cssSelector = "video[class='player-video']";
                                        IWebElement elementVideo = driver.FindElement(By.CssSelector(cssSelector));
                                        linkMovie = elementVideo.GetAttribute("src");
                                    }
                                }
                            }
                            else
                            {
                                IWebElement elementVideo = driver.FindElement(By.CssSelector(cssSelector));
                                linkMovie = elementVideo.GetAttribute("src");
                            }
                            var episode = new Episode();
                            if(server == 2 && cssSelector == "iframe[class='player-embed']" && !string.IsNullOrWhiteSpace(linkMovie))
                            {
                                int start = linkMovie.IndexOf("url=") + 4;
                                int end = linkMovie.IndexOf(".m3u8") + 5;
                                string m3u8 = linkMovie.Substring(start, end - start);
                                m3u8 = m3u8.Replace("mephimanh.com", "ima21.xyz");
                                linkMovie = m3u8;
                            }
                            episode.Number = item.Number;
                            episode.Link = linkMovie;

                            episodeArray.Add(episode);
                        }
                        catch
                        {
                            driver.Close();
                            continue;
                        }
                        driver.Close();
                    }
                    Thread.Sleep(1000);
                }

                return episodeArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
