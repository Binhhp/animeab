using Firebase.Storage;
using FireSharp;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnimeAB.Reponsitory
{
    public static class FirebaseManager
    {
        public static IFirebaseClient Database()
        {
            IFirebaseConfig databaseConfig = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "L2AvJZ8ajlZuwwrs9xwAc0EhR6BmK397GooDdhT3",
                BasePath = "https://animeab-default-rtdb.asia-southeast1.firebasedatabase.app"
            };

            IFirebaseClient database = new FirebaseClient(databaseConfig);
            return database;
        }
        /// <summary>
        /// Database Firebase
        /// </summary>
        /// <returns></returns>
        public static FirebaseStorage Storage()
        {
            string storageBucket = "animeab.appspot.com";
            FirebaseStorage storage = new FirebaseStorage(storageBucket);
            return storage;
        }
    }
}
