using AnimeAB.Application.Common.Interface.Reponsitories.Base;
using AnimeAB.Domain.DTOs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeAB.ApiIntegration.ChatHubs
{
    public class CommentHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void LikeComment(CommentLikeDto commentDto)
        {
            var comment = _unitOfWork.CommentPlugin.LikeComment(commentDto.id, commentDto.idComment);
            Clients.Caller.SendAsync(commentDto.id + "_" + "like_comment", comment);
        }
    }
}
