using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.ApiIntegration.Filters
{
    public static class AnimeRandom
    {
        public static List<Animes> Random(this List<Animes> animes, int random, string id)
        {
            Random rd = new Random();
            if(string.IsNullOrWhiteSpace(id)) return animes.OrderBy(item => rd.Next()).Take(random).ToList();

            return animes.Where(x => x.Key != id).OrderBy(item => rd.Next()).Take(random).ToList();
        }
    }
}
