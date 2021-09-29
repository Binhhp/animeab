using AnimeAB.Reponsitories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.Core.Filters
{
    public static class AnimeRandom
    {
        public static List<Animes> Random(this List<Animes> animes, int random)
        {
            Random rd = new Random();
            return animes.OrderBy(item => rd.Next()).Take(random).ToList();
        }
    }
}
