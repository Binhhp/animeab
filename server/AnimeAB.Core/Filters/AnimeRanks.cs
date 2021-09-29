
using AnimeAB.Core.ApiResponse;
using AnimeAB.Reponsitories.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Core.Filters
{
    public static class AnimeRanks
    {
        public static AnimeTrending GetRanks(this List<Animes> animes, IMapper mapper)
        {
            var animeTrending = new AnimeTrending();

            var taskDays = Task.Run(() =>
                {
                    var animeDays = animes.Where(x => x.IsStatus == 2 || x.IsStatus > 2)
                        .OrderByDescending(x => x.ViewDays)
                        .ThenByDescending(x => x.DateRelease)
                        .Take(10)
                        .ToList();

                    animeTrending.days = mapper.Map<List<AnimeRankDayResponse>>(animeDays);
                });


            var taskWeeks = Task.Run(() =>
                {
                    var animeWeeks = animes.Where(x => x.IsStatus == 2 || x.IsStatus > 2)
                        .OrderByDescending(x => x.ViewWeeks)
                        .ThenByDescending(x => x.DateRelease)
                        .Take(10)
                        .ToList();
                    animeTrending.weeks = mapper.Map<List<AnimeRankWeekResponse>>(animeWeeks);
                });


            var taskMonths = Task.Run(() =>
            {
                var animeMonths = animes.Where(x => x.IsStatus == 2 || x.IsStatus > 2)
                        .OrderByDescending(x => x.ViewMonths)
                        .ThenByDescending(x => x.DateRelease)
                        .Take(10)
                        .ToList();

                animeTrending.months = mapper.Map<List<AnimeRankMonthResponse>>(animeMonths);
            });

            Task.WaitAll(taskDays, taskMonths, taskWeeks);
            return animeTrending;
        }
    }
}
