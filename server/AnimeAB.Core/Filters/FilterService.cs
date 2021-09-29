using AnimeAB.Reponsitories.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.Core.Filters
{
    public static class FilterService
    {
        public static List<Animes> FilterType(this List<Animes> animes, string type)
        {
            switch (type)
            {
                case "tv":
                    type = "TV";
                    break;
                case "ova":
                    type = "OVA";
                    break;
                case "movie":
                    type = "Movie";
                    break;
                case "special":
                    type = "Special";
                    break;
            }

            animes = animes.Where(x => x.Type.Equals(type)).ToList();
            return animes;
        }
        public static List<Animes> FilterStatus(this List<Animes> animes, string rootStatus)
        {
            //less:lt, greater:gt, range:[and]

            if (rootStatus.Contains("[and]"))
            {
                int less = 0, greater = 0;
                rootStatus.Split("[and]").ToList().ForEach((item) =>
                {
                    if (item.Contains("lt"))
                    {
                        var index = item.IndexOf(":");
                        less = int.Parse(item.Substring(index + 1));
                    }
                    if (item.Contains("gt"))
                    {
                        var index = item.IndexOf(":");
                        greater = int.Parse(item.Substring(index + 1));
                    }
                });

                animes = animes.Where(x => x.IsStatus < less && x.IsStatus > greater).ToList();
            }
            else
            {
                if (rootStatus.Contains(":") && (rootStatus.Contains("lt") || rootStatus.Contains("gt")))
                {
                    if (rootStatus.Contains("lt") && rootStatus.Contains(":"))
                    {
                        var index = rootStatus.IndexOf(":");
                        int status = int.Parse(rootStatus.Substring(index + 1));
                        animes = animes.Where(x => x.IsStatus < status).ToList();
                    }
                    if (rootStatus.Contains("gt") && rootStatus.Contains(":"))
                    {
                        var index = rootStatus.IndexOf(":");
                        int status = int.Parse(rootStatus.Substring(index + 1));
                        animes = animes.Where(x => x.IsStatus < status || x.IsStatus > status).ToList();
                    }
                }
                else
                {
                    int status = int.Parse(rootStatus);
                    animes = animes.Where(x => x.IsStatus == status).ToList();
                }

            }
            return animes;
        }
    }
}
