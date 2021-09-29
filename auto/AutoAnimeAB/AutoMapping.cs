using AutoAnimeAB.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnimeAB
{
    public static class AutoMapping
    {
        public static IEnumerable<AnimeDomain> GetAnimeDomains(IEnumerable<Animes> animes)
        {
            animes = animes.Where(x => x.IsStatus < 3).ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Animes, AnimeDomain>());
            var mapper = config.CreateMapper();
            var animeDomains = mapper.Map<IEnumerable<AnimeDomain>>(animes);
            return animeDomains;
        }
    }
}
