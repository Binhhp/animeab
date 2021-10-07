using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeAB.Application.Common.Behaviour;
using AnimeAB.Domain.Entities;

namespace AnimeAB.Application.Common.Interface.Reponsitories
{
    public interface IReponsitoryCategories
    {
        Task<IEnumerable<Categories>> GetCategoriesAsync();
        Task<Response> CreateCategoryAsync(Categories categories);
        Task<Response> UpdateCategoryAsync(Categories categories);
        Task<Response> DeleteCategoryAsync(string key);
    }
}
