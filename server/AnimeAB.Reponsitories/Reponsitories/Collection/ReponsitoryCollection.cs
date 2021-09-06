using AnimeAB.Reponsitories.Entities;
using AnimeAB.Reponsitories.Interface;
using Firebase.Storage;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.Collection
{
    public class ReponsitoryCollection : Request, IReponsitoryCollection
    {
        private readonly IFirebaseClient database;
        private readonly FirebaseStorage storage;

        public ReponsitoryCollection(AppSettingFirebase appSetting)
        {
            database = FirebaseManager.Database(appSetting.AuthSecret, appSetting.DatabaseURL);
            storage = FirebaseManager.Storage(appSetting.StorageBucket);
        }
        /// <summary>
        /// List collections
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Collections>> GetCollectionsAsync()
        {
            var data = await database.GetAsync(Table.COLLECTION);
            
            var collections = data.ResultAs<Dictionary<string, Collections>>();
            var list = new List<Collections>(); 
            if (collections != null)
            {
                foreach (var item in collections)
                {
                    list.Add(item.Value);
                }
            }

            return list;
        }
        /// <summary>
        /// Create collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task<Response> CreateCollectionAsync(Collections collection, Stream image)
        {
            try
            {
                var collectItem = await database.GetAsync(Table.COLLECTION + "/" + collection.Key);
                if (collectItem.Body != "null") return Error("Bản ghi đã tồn tại.");

                var cancellation = new CancellationTokenSource();

                await storage.Child(Table.COLLECTION).Child(collection.FileName).PutAsync(image, cancellation.Token);
                var pathImage = await storage.Child(Table.COLLECTION).Child(collection.FileName).GetDownloadUrlAsync();

                collection.Image = pathImage;

                await database.SetAsync(Table.COLLECTION + "/" + collection.Key, collection);
                return Success();
            }
            catch(Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Update collection
        /// </summary>
        /// <param name="collectionUpdate"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<Response> UpdateCollectionAsync(Collections collectionUpdate, Stream image)
        {
            try
            {
                var collectItem = await database.GetAsync(Table.COLLECTION + "/" + collectionUpdate.Key);

                if (collectItem.Body == "null") return Error("Bản ghi không tồn tại.");
                Collections collect = collectItem.ResultAs<Collections>();

                if(image != null)
                {
                    var cancellation = new CancellationTokenSource();

                    await storage.Child(Table.COLLECTION).Child(collect.FileName).DeleteAsync();

                    await storage.Child(Table.COLLECTION).Child(collectionUpdate.FileName).PutAsync(image, cancellation.Token);

                    var pathImage = await storage.Child(Table.COLLECTION).Child(collectionUpdate.FileName).GetDownloadUrlAsync();

                    collectionUpdate.Image = pathImage;
                }
                else
                {
                    collectionUpdate.Image = collect.Image;
                    collectionUpdate.FileName = collect.FileName;
                }

                collectionUpdate.DateCreated = collect.DateCreated;
                await database.UpdateAsync(Table.COLLECTION + "/" + collectionUpdate.Key, collectionUpdate);
                return Success(collectionUpdate);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// Delete collection
        /// </summary>
        /// <param name="collectionKey"></param>
        /// <returns></returns>
        public async Task<Response> DeleteCollectionAsync(string collectionKey)
        {
            try
            {
                var collectItem = await database.GetAsync(Table.COLLECTION + "/" + collectionKey);

                if (collectItem.Body == "null") return Error("Bản ghi không tồn tại.");
                Collections collect = collectItem.ResultAs<Collections>();

                await storage.Child(Table.COLLECTION).Child(collect.FileName).DeleteAsync();
                await database.DeleteAsync(Table.COLLECTION + "/" + collectionKey);
                return Success();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
