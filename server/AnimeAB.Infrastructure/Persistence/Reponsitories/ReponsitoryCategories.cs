using AnimeAB.Application.Common.Behaviour;
using AnimeAB.Application.Common.Interface.Reponsitories;
using AnimeAB.Domain.Entities;
using AnimeAB.Domain.Settings;
using AnimeAB.Domain.ValueObject;
using AnimeAB.Infrastructure.Services;
using Firebase.Storage;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeAB.Infrastructure.Persistence.Reponsitories
{
    public class ReponsitoryCategories : Request, IReponsitoryCategories
    {
        private readonly AppSettingFirebase _appSetting;
        private readonly IFirebaseClient database;

        public ReponsitoryCategories(AppSettingFirebase appSetting)
        {
            //Database
            _appSetting = appSetting;
            database = FirebaseManager.Database(_appSetting.AuthSecret, _appSetting.DatabaseURL);
        }
        /// <summary>
        /// List categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Categories>> GetCategoriesAsync()
        {
            var data = await database.GetAsync(Table.CATEGORIES);
            IEnumerable<Categories> categories = data.ResultAs<Dictionary<string, Categories>>().Values.ToList();

            return categories;
        }
        /// <summary>
        /// Create categories
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public async Task<Response> CreateCategoryAsync(Categories categories)
        {
            try
            {
                var data = await database.GetAsync(Table.CATEGORIES + "/" + categories.Key);
                if (data.Body != "null") return Error("Bản ghi đã tồn tại.");

                await database.SetAsync(Table.CATEGORIES + "/" + categories.Key, categories);
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Update categories
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public async Task<Response> UpdateCategoryAsync(Categories categories)
        {
            try
            {
                var data = await database.GetAsync(Table.CATEGORIES + "/" + categories.Key);

                if (data.Body == "null") return Error("Bản ghi không tồn tại.");
                Categories collect = data.ResultAs<Categories>();

                await database.UpdateAsync(Table.CATEGORIES + "/" + categories.Key, categories);
                return Success(categories);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Delete categories
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Response> DeleteCategoryAsync(string key)
        {
            try
            {
                var data = await database.GetAsync(Table.CATEGORIES + "/" + key);

                if (data.Body == "null") return Error("Bản ghi không tồn tại.");
                Categories categories = data.ResultAs<Categories>();

                await database.DeleteAsync(Table.CATEGORIES + "/" + categories.Key);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
