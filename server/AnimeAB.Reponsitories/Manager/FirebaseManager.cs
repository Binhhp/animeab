using Firebase.Auth;
using Firebase.Storage;
using FireSharp;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Interface
{
    public static class FirebaseManager
    {
        /// <summary>
        /// Database Firebase
        /// </summary>
        /// <returns></returns>
        public static IFirebaseClient Database(string AuthSecret, string DatabaseURL)
        {
            IFirebaseConfig databaseConfig = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = AuthSecret,
                BasePath = DatabaseURL
            };

            IFirebaseClient database = new FirebaseClient(databaseConfig);
            return database;
        }
        /// <summary>
        /// Database Firebase
        /// </summary>
        /// <returns></returns>
        public static FirebaseStorage Storage(string StorageBucket)
        {
            FirebaseStorage storage = new FirebaseStorage(StorageBucket);
            return storage;
        }
        /// <summary>
        /// Authenticate Firebase
        /// </summary>
        /// <returns>Authenticate Firebase</returns>
        public static FirebaseAuthProvider Authenticate(string ApiKey)
        {
            var authConfig = new FirebaseConfig(ApiKey);

            FirebaseAuthProvider authProvider = new FirebaseAuthProvider(authConfig);
            return authProvider;
        }
    }
}
