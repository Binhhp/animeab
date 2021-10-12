
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Application.Reponsitories.Base
{
    public interface IUnitOfWork
    {
        IReponsitoryAccount AccountEntity { get; }
        IReponsitoryCollection CollectionEntity { get; }
        IReponsitoryCategories CategoriesEntity { get; }
        IReponsitoryAnime AnimeEntity { get; }
        IReponsitoryAnimeDetail AnimeDetailEntity { get; }
        IReponsitoryAnimeSeries AnimeSeries { get; }
        ICommentPlugin CommentPlugin { get; }
        IRefreshTokenGuide RefreshToken { get; }
        IReponsitoryFavorite AnimeFavorite { get; }
    }
}
