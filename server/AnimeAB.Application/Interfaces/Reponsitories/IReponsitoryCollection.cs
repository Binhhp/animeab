using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeAB.Application.Behavious;
using AnimeAB.Domain.Entities;

namespace AnimeAB.Application.Reponsitories
{
    public interface IReponsitoryCollection
    {
        Task<IEnumerable<Collections>> GetCollectionsAsync();
        Task<Response> CreateCollectionAsync(Collections collection, Stream image);
        Task<Response> UpdateCollectionAsync(Collections collectionUpdate, Stream image);
        Task<Response> DeleteCollectionAsync(string collectionKey);
    }
}
