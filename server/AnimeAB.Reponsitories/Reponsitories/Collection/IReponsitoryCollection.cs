using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeAB.Reponsitories.Entities;

namespace AnimeAB.Reponsitories.Reponsitories.Collection
{
    public interface IReponsitoryCollection
    {
        Task<IEnumerable<Collections>> GetCollectionsAsync();
        Task<Response> CreateCollectionAsync(Collections collection, Stream image);
        Task<Response> UpdateCollectionAsync(Collections collectionUpdate, Stream image);
        Task<Response> DeleteCollectionAsync(string collectionKey);
    }
}
