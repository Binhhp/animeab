using AnimeAB.Reponsitories.Reponsitories.Account;
using AnimeAB.Reponsitories.Reponsitories.Anime;
using AnimeAB.Reponsitories.Reponsitories.AnimeDetail;
using AnimeAB.Reponsitories.Reponsitories.AnimeSeries;
using AnimeAB.Reponsitories.Reponsitories.Category;
using AnimeAB.Reponsitories.Reponsitories.Collection;
using AnimeAB.Reponsitories.Reponsitories.MessageHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Interface
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
    }
}
