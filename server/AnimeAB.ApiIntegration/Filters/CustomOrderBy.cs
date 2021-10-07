using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.ApiIntegration.Filters
{
    public static class CustomOrderBy
    {
        public static IOrderedEnumerable<Animes> SortBy(
            this List<Animes> animes, string sortType, Func<Animes, object> callback)
        {
            if (sortType == "asc")
            {
                var sort = animes.OrderBy(callback);
                return sort;
            }
            var sortDesc = animes.OrderByDescending(callback);
            return sortDesc;
        }


        public static IOrderedEnumerable<Animes> ThenSortBy(
            this IOrderedEnumerable<Animes> source, string sortType, Func<Animes, object> callback)
        {
            if (sortType == "asc")
            {
                var sort = source.ThenBy(callback);
                return sort;
            }
            var sortDesc = source.ThenByDescending(callback);
            return sortDesc;
        }

    }
}
