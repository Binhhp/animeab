using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Application.Reponsitories
{
    public interface IReponsitoryFavorite
    {
        /// <summary>
        /// Get anime favorite
        /// </summary>
        /// <param name="uid">user uid</param>
        /// <returns></returns>
        Task<IEnumerable<Animes>> GetAnimes(string uid);
        /// <summary>
        /// Add anime for favorite
        /// </summary>
        /// <param name="animeFavorite">Anime detail</param>
        void AddAnime(AnimeFavorite animeFavorite);
        /// <summary>
        /// Remove anime favorite
        /// </summary>
        /// <param name="id">id anime</param>
        /// <param name="uid">uid user logined</param>
        void RemoveAnime(string idAnime, string uid);
    }
}
