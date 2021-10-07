using AnimeAB.ApiIntegration.Filters;
using AnimeAB.Domain.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.ApiIntegration.Filters
{
    public static class Sorting
    {
        public static List<Animes> SingleSort(this List<Animes> animes, string sort_by, string order)
        {
            if(order == "asc")
            {
                if (sort_by == "views") animes = animes.OrderBy(anis => anis.Views).ToList();
                if(sort_by == "date") animes = animes.OrderBy(anis => anis.DateRelease).ToList();
            }
            else {
                if (sort_by == "views") animes = animes.OrderByDescending(anis => anis.Views).ToList();
                if (sort_by == "date") animes = animes.OrderByDescending(anis => anis.DateRelease).ToList();
            }

            return animes;
        }

        public static List<Animes> MultipleSort(this List<Animes> animes, string sort)
        {
            if (sort.Contains(","))
            {
                List<string> sortList = sort.Split(",").ToList();
                var sortModel = new SortModel();
                sortList.ForEach(s =>
                {
                    string sortType = s.Split(":")[1].ToString();
                    if (s.Contains("views")) sortModel.views = sortType;
                    if (s.Contains("date")) sortModel.date = sortType;
                    if (s.Contains("status")) sortModel.status = sortType;
                });
                //sort view and date
                if(!string.IsNullOrWhiteSpace(sortModel.views) 
                    && !string.IsNullOrWhiteSpace(sortModel.date))
                {
                    animes = animes.SortBy(sortModel.views, x => x.Views)
                        .ThenSortBy(sortModel.date, date => date.DateRelease)
                        .ToList();
                }
                //sort view and status
                if (!string.IsNullOrWhiteSpace(sortModel.views)
                    && !string.IsNullOrWhiteSpace(sortModel.status))
                {
                    animes = animes.SortBy(sortModel.views, x => x.Views)
                        .ThenSortBy(sortModel.status, date => date.IsStatus)
                        .ToList();
                }
                //sort date and status
                if (!string.IsNullOrWhiteSpace(sortModel.status)
                    && !string.IsNullOrWhiteSpace(sortModel.date))
                {
                    animes = animes.SortBy(sortModel.date, x => x.DateRelease)
                        .ThenSortBy(sortModel.status, date => date.IsStatus)
                        .ToList();
                }
                //sort date and status and view
                if (!string.IsNullOrWhiteSpace(sortModel.views)
                    && !string.IsNullOrWhiteSpace(sortModel.date)
                    && !string.IsNullOrWhiteSpace(sortModel.status))
                {
                    animes = animes.SortBy(sortModel.views, x => x.Views)
                        .ThenSortBy(sortModel.date, date => date.DateRelease)
                        .ThenSortBy(sortModel.status, date => date.IsStatus)
                        .ToList();
                }
            }

            return animes;
        }
    }
    public class SortModel
    {
        public string views { get; set; } = "";
        public string date { get; set; } = "";
        public string status { get; set; } = "";
    }
}
