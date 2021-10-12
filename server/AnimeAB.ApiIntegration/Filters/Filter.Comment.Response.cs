using AnimeAB.Core.ApiResponse;
using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeAB.ApiIntegration.Filters
{
    public static class CustomCommentResponse
    {
        public static CommentResponse ToResponse(this Comment comment, AnimeUser user)
        {
            return new CommentResponse
            {
                UserLocal = user.LocalId,
                DisplayName = user.DisplayName,
                PhotoUrl = user.PhotoUrl,
                Key = comment.Key,
                When = comment.When,
                Likes = comment.Likes,
                Message = comment.Message,
                ReplyComment = comment.ReplyComment,
                UserLiked = comment.UserLiked
            };
        }

        public static IEnumerable<CommentResponse> ToListResponse(
            this IEnumerable<Comment> comments, 
            IEnumerable<AnimeUser> users,
            string sort,
            Func<IEnumerable<CommentResponse>, string, IEnumerable<CommentResponse>> elementSelector)
        {
            var response = comments.Join(users,
                    cmt => cmt.UserLocal,
                    user => user.LocalId,
                    (cmt, user) => new CommentResponse
                    {
                        UserLocal = user.LocalId,
                        DisplayName = user.DisplayName,
                        PhotoUrl = user.PhotoUrl,
                        Key = cmt.Key,
                        When = cmt.When,
                        Likes = cmt.Likes,
                        Message = cmt.Message,
                        ReplyComment = cmt.ReplyComment,
                        UserLiked = cmt.UserLiked
                    });
           return elementSelector(response, sort);
        }
    }
}
