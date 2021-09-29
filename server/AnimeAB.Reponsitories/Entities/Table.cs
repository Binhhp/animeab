using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Entities
{
    public static class Table
    {
        public const string COLLECTION = "Collections";
        public const string ANIME = "Anime";
        public const string ANIMEDETAIL = "AnimeDetail";
        public const string CATEGORIES = "Categories";
        public const string USERS = "Users";
        public const string ANIMESERIES = "Series";
        public const string COMMENT = "Comments";
        public const string NOTIFICATION = "Notification";
        public const string ANIMEFAVORITE = "AnimeFavorite";
        public static string PATHANIMEDETAILED(string animeKey, string animeDetailKey)
        {
            return ANIME + "/" + animeKey + "/" + ANIMEDETAIL + "/" + animeDetailKey;
        }
    }
}
