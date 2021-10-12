
using AnimeAB.Application.Reponsitories.Base;
using AnimeAB.Domain.DTOs;
using Microsoft.AspNetCore.SignalR;
using System;

namespace AnimeAB.ApiIntegration.HubContainer
{
    public class CommentHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// like comment
        /// </summary>
        /// <param name="commentDto"></param>
        /// <exception cref="HubException"></exception>
        public void LikeComment(CommentLikeDto commentDto)
        {
            try
            {
                var res = _unitOfWork.CommentPlugin.LikeComment(commentDto.id, commentDto.idComment, commentDto.uid);
                Clients.Others.SendAsync(commentDto.id + "_" + "like_comment", res);
            }
            catch(Exception ex)
            {
                throw new HubException(ex.Message);
            }
        }
        /// <summary>
        /// quit liked of comment
        /// </summary>
        /// <param name="commentDto"></param>
        /// <exception cref="HubException"></exception>
        public void QuitLikeComment(CommentLikeDto commentDto)
        {
            try
            {
                var res = _unitOfWork.CommentPlugin.LikeComment(commentDto.id, commentDto.idComment, commentDto.uid, true);
                Clients.Others.SendAsync(commentDto.id + "_" + "quit_like_comment", res);
            }
            catch (Exception ex)
            {
                throw new HubException(ex.Message);
            }
        }
    }
}
