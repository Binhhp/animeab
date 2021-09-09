using AnimeAB.Reponsitories.DTO;
using AnimeAB.Reponsitories.Entities;
using AnimeAB.Reponsitories.Interface;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.MessageHub
{
    public class CommentPlugin : ICommentPlugin
    {
        private readonly IFirebaseClient database;

        public CommentPlugin(AppSettingFirebase appSetting)
        {
            database = FirebaseManager.Database(appSetting.AuthSecret, appSetting.DatabaseURL);
        }
        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="person">object</param>
        /// <returns></returns>
        public Comment CreateAsync(Comment comment, string animeKey)
        {
            try
            {
                int count = 1;
                var data = Task.Run(() => database.GetAsync(Table.COMMENT + "/" + animeKey));
                if (data.Result.Body != "null") count += data.Result.ResultAs<Dictionary<string, Comment>>().Count;
                comment.Key = "comment-" + count;
                Task.Factory.StartNew(() => database.SetAsync(Table.COMMENT + "/" + animeKey + "/" + comment.Key, comment));
                return comment;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get comments
        /// </summary>
        /// <param name="animeKey">object</param>
        /// <returns></returns>
        public async Task<IEnumerable<Comment>> GetAsync(string animeKey)
        {
            try
            {
                var data = await database.GetAsync(Table.COMMENT + "/" + animeKey);
                if (data.Body != "null")
                {
                    var comments = data.ResultAs<Dictionary<string, Comment>>().Values.ToList();
                    return comments;
                }
                else
                {
                    return new List<Comment>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get notification
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetNotificationAsync(string user)
        {
            try
            {
                var data = await database.GetAsync(Table.NOTIFICATION + "/" + user);
                if (data.Body == "null") return new List<Notification>();
                List<Notification> notifications = data.ResultAs<Dictionary<string, Notification>>().Values.OrderByDescending(x => x.When).ToList();
                return notifications;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Add notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public Notification AddNotifiAsync(Notification notification)
        {
            try
            {
                int count = 1;
                var data = Task.Run(() => database.GetAsync(Table.NOTIFICATION + "/" + notification.UserRevice));
                if (data.Result.Body != "null") count += data.Result.ResultAs<Dictionary<string, Notification>>().Values.Count();
                string notifyKey = "notify-" + count;
                notification.Key = notifyKey;
                Task.Factory.StartNew(() => database.SetAsync(Table.NOTIFICATION + "/" + notification.UserRevice + "/" + notifyKey, notification));
                return notification;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Read notify
        /// </summary>
        /// <param name="user"></param>
        /// <param name="notify"></param>
        /// <returns></returns>
        public async Task ReadNotifiAsync(string user, string notify)
        {
            try
            {
                await database.SetAsync(Table.NOTIFICATION + "/" + user + "/" + notify + "/IsRead", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Like comment
        /// </summary>
        /// <param name="animeKey"></param>
        /// <param name="commentKey"></param>
        public Comment LikeComment(string animeKey, string commentKey)
        {
            try
            {
                var data = Task.Run(() => database.GetAsync(Table.COMMENT + "/" + animeKey + "/" + commentKey));

                Comment comment = data.Result.ResultAs<Comment>();
                comment.Likes += 1;

                database.SetAsync(Table.COMMENT + "/" + animeKey + "/" + commentKey + "/Likes", comment.Likes);
                return comment;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
