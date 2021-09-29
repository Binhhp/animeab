using AutoAnimeAB.Models;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnimeAB.Reponsitory.Anime
{
    public class ReponsitoryAnime
    {
        private IFirebaseClient database;

        public ReponsitoryAnime()
        {
            database = FirebaseManager.Database();
        }

        public async Task<IEnumerable<Animes>> GetAnimes()
        {
            try
            {
                var list = new List<Animes>();

                var data = await database.GetAsync(Table.ANIME);
                if (data.Body == "null")
                {
                    return list;
                }

                var animes = data.ResultAs<Dictionary<string, Animes>>();
                return animes.Values.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
