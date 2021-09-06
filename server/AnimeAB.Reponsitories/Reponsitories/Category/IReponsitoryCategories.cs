using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeAB.Reponsitories.Entities;

namespace AnimeAB.Reponsitories.Reponsitories.Category
{
    public interface IReponsitoryCategories
    {
        Task<IEnumerable<Categories>> GetCategoriesAsync();
        Task<Response> CreateCategoryAsync(Categories categories);
        Task<Response> UpdateCategoryAsync(Categories categories);
        Task<Response> DeleteCategoryAsync(string key);
    }
}
