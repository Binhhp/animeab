using AnimeAB.Reponsitories.DTO;
using AnimeAB.Reponsitories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories.MessageHub
{
    public interface ICommentPlugin
    {
        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Comment CreateAsync(Comment comment, string animeKey);
        /// <summary>
        /// Get comments
        /// </summary>
        /// <param name="animeKey"></param>
        /// <returns></returns>
        Task<IEnumerable<Comment>> GetAsync(string animeKey);
        /// <summary>
        /// get notification
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetNotificationAsync(string user);

        /// <summary>
        /// Add notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        Notification AddNotifiAsync(Notification notification);
        /// <summary>
        /// Read notify
        /// </summary>
        /// <param name="user"></param>
        /// <param name="notify"></param>
        /// <returns></returns>
        Task ReadNotifiAsync(string user, string notify);
    }
}
