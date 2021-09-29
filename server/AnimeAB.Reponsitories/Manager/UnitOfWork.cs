using AnimeAB.Reponsitories.Reponsitories.Account;
using AnimeAB.Reponsitories.Reponsitories.Anime;
using AnimeAB.Reponsitories.Reponsitories.AnimeDetail;
using AnimeAB.Reponsitories.Reponsitories.AnimeSeries;
using AnimeAB.Reponsitories.Reponsitories.Category;
using AnimeAB.Reponsitories.Reponsitories.Collection;
using AnimeAB.Reponsitories.Reponsitories.Favorite;
using AnimeAB.Reponsitories.Reponsitories.MessageHub;
using AnimeAB.Reponsitories.Reponsitories.TokenManager;

namespace AnimeAB.Reponsitories.Interface
{
    public class UnitOfWork : IUnitOfWork
    {
        public IReponsitoryAccount AccountEntity { get; private set; }
        public IReponsitoryCollection CollectionEntity { get; private set; }
        public IReponsitoryCategories CategoriesEntity { get; private set; }
        public IReponsitoryAnime AnimeEntity { get; private set; }
        public IReponsitoryAnimeDetail AnimeDetailEntity { get; private set; }
        public IReponsitoryAnimeSeries AnimeSeries { get; private set; }
        public ICommentPlugin CommentPlugin { get; private set; }
        public IRefreshTokenGuide RefreshToken { get; private set; }
        public IReponsitoryFavorite AnimeFavorite { get; private set; }
        public UnitOfWork(AppSettingFirebase _appSetting)
        {
            AccountEntity = new ReponsitoryAccount(_appSetting);
            CollectionEntity = new ReponsitoryCollection(_appSetting);
            CategoriesEntity = new ReponsitoryCategories(_appSetting);
            AnimeEntity = new ReponsitoryAnime(_appSetting);
            AnimeDetailEntity = new ReponsitoryAnimeDetail(_appSetting);
            AnimeSeries = new ReponsitoryAnimeSeries(_appSetting);
            CommentPlugin = new CommentPlugin(_appSetting);
            RefreshToken = new RefreshTokenGuide(_appSetting.EndpointRefreshToken);
            AnimeFavorite = new ReponsitoryFavorite(_appSetting);
        }
    }
}
