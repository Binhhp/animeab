using Firebase.Storage;
using FireSharp;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                AuthSecret = ConfigurationManager.AppSettings["authSecret"],
                BasePath = ConfigurationManager.AppSettings["database"]
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
            string storageBucket = ConfigurationManager.AppSettings["bucket"];
            FirebaseStorage storage = new FirebaseStorage(storageBucket);
            return storage;
        }
    }
}
