using AnimeAB.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using AnimeAB.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AnimeAB.Application.Common.Interface.Reponsitories.Base;
using AnimeAB.ApiIntegration.ChatHubs;
using AnimeAB.Infrastructure.ApiResponse;
using AnimeAB.ApiIntegration.Filters;

namespace AnimeAB.Core.Apis
{
    [Route("api")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<CommentHub> _hubContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageController(
            [NotNull] IHubContext<CommentHub> hubContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Route("comments")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments(
            [FromQuery]string id, [FromQuery]string sort = "", 
            [FromQuery]string user_reply = "")
        {
            try
            {
                IEnumerable<Comment> comments = await _unitOfWork.CommentPlugin.GetAsync(id);
                if (!string.IsNullOrWhiteSpace(user_reply)) comments = comments.Where(x => x.ReplyComment.Equals(user_reply)).ToList();

                IEnumerable<AnimeUser> users = await _unitOfWork.AccountEntity.GetUsersAsync();

                IEnumerable<CommentResponse> responses = comments.ToListResponse(users, sort,
                    (x, sort) =>
                    {
                        return sort == "lastest" 
                        ? x.OrderByDescending(x => x.When).ToList()
                        : x.OrderBy(x => x.When).ToList();
                    });

                return Ok(responses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("comments")]
        [HttpPost]
        public IActionResult PostComment(
            [FromBody]CommentDto commentDto, 
            [FromQuery]string id, 
            [FromQuery]string receiver = "", 
            [FromQuery]string link_notify = "")
        {
            try
            {
                Comment comment = _mapper.Map<Comment>(commentDto);
                Comment commentAdded = _unitOfWork.CommentPlugin.CreateAsync(comment, id);

                AnimeUser user = _unitOfWork.AccountEntity.GetUsersAsync()
                        .Result.FirstOrDefault(x => x.LocalId == commentAdded.UserLocal);

                if (user == null) return BadRequest("Interval Error Server");

                CommentResponse response = commentAdded.ToResponse(user);

                _hubContext.Clients.All.SendAsync(id, response);
                if(!string.IsNullOrWhiteSpace(receiver) && !string.IsNullOrWhiteSpace(link_notify) && !commentDto.user_local.Equals(receiver))
                {
                    var notification = new Notification
                    {
                        UserRevice = receiver,
                        Message = commentDto.name + " đã nhắc bạn trong bình luận của họ.",
                        LinkNotify = link_notify
                    };

                    Notification notifyAdded = _unitOfWork.CommentPlugin.AddNotifiAsync(notification);
                    _hubContext.Clients.All.SendAsync(notification.UserRevice, notifyAdded);
                }
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("notification")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotifications([FromQuery]string user, [FromQuery] string notify = "", [FromQuery]bool count = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user)) return BadRequest("User is not valid!");
                
                if(!string.IsNullOrWhiteSpace(notify))
                {
                    await _unitOfWork.CommentPlugin.ReadNotifiAsync(user, notify);
                }

                var notifies = await _unitOfWork.CommentPlugin.GetNotificationAsync(user);
                if (count)
                {
                    int notifiesNotRead = notifies.Where(x => !x.IsRead).Count();
                    return Ok(notifiesNotRead);
                }
                return Ok(notifies);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("likes")]
        [HttpGet]
        public IActionResult LikeComment([FromQuery]string id, [FromQuery]string idComment)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(idComment))
                {
                    return BadRequest();
                }

                var comment = _unitOfWork.CommentPlugin.LikeComment(id, idComment);
                _hubContext.Clients.All.SendAsync( id + "_" + "like_comment", comment);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
