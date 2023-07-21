using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IChatMessageRepository _chatMessageRepository;


        public GroupMemberController(ICommentRepository commentRepository, IPostRepository postRepository, ITaskRepository taskRepository, IMeetingRepository meetingRepository, IGroupRepository groupRepository, IChatMessageRepository chatMessageRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _taskRepository = taskRepository;
            _meetingRepository = meetingRepository;
            _groupRepository = groupRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        [HttpPost("CreatePost")]
        public IActionResult CreatePost(PostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post
            {
                
                PostId = _postRepository.GetPosts().Count + 1,
                GroupId = postDto.GroupId,
                UserId = postDto.UserId,
                Content = postDto.Content,
                CreatedAt = DateTime.UtcNow,
               
            };

            _postRepository.SavePost(post);

            return CreatedAtAction(nameof(GetPostById), new { id = post.PostId }, post);
        }

        [HttpGet("GetPostById/{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = _postRepository.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost("{postId}/comments")]
        public IActionResult CreateComment(int postId, CommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _postRepository.GetPostById(postId);
            if (post == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                CommentId = _commentRepository.GetComments().Count + 1,
                PostId = postId,
                UserId = commentDto.UserId,
                Content = commentDto.Content
            };

            _commentRepository.SaveComment(comment);

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.CommentId }, comment);
        }

        [HttpGet("GetCommentById/{id}")]
        public IActionResult GetCommentById(int id)
        {
            var comment = _commentRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }
        [HttpGet("GetCommentsByPostId/{postId}")]
        public IActionResult GetCommentsByPostId(int postId)
        {
            var comments = _commentRepository.GetCommentsByPostId(postId);

            if (comments == null || !comments.Any())
            {
                return NotFound();
            }

            return Ok(comments);
        }




        [HttpGet("GetAllComments")]
        public ActionResult<IEnumerable<Comment>> GetAllComments()
        {
            var comments = _commentRepository.GetComments();
            return Ok(comments);
        }
        [HttpPost("CreateTask")]
        public IActionResult CreateTask(TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int nextTaskId = _taskRepository.GetNextTaskId(); // Lấy ID tiếp theo cho Task

            var task = new Task
            {
                TaskId = nextTaskId,
                TaskTitle = taskDto.TaskTitle,
                Description = taskDto.Description,
                AssignedToUserId = taskDto.AssignedToUserId,
                Status = taskDto.Status
            };

            _taskRepository.SaveTask(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, task);
        }
        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
        {
            var tasks = _taskRepository.GetTasks();
            return Ok(tasks);
        }


        [HttpGet("GetTaskById/{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut("UpdateTask/{id}")]
        public IActionResult UpdateTask(int id, TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            task.TaskTitle = taskDto.TaskTitle;
            task.Description = taskDto.Description;
            task.AssignedToUserId = taskDto.AssignedToUserId;
            task.Status = taskDto.Status;

            _taskRepository.UpdateTask(task);

            return Ok(task);
        }

        [HttpDelete("DeleteTask/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _taskRepository.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            _taskRepository.DeleteTask(task);

            return NoContent();
        }
        [HttpPost("JoinMeeting")]
        public IActionResult JoinMeeting(int meetingId, int groupId)
        {
            var meeting = _meetingRepository.GetMeetingById(meetingId);
            if (meeting == null)
            {
                return NotFound("Meeting not found");
            }

            var group = _groupRepository.GetGroupById(groupId);
            if (group == null)
            {
                return NotFound("Group not found");
            }

            // Thực hiện thao tác tham gia Group vào cuộc họp tại đây
            // Ví dụ:
            meeting.GroupId = groupId;
            _meetingRepository.UpdateMeeting(meeting);

            return Ok();
        }

        [HttpGet("GetMeetingById/{id}")]
        public IActionResult GetMeetingById(int id)
        {
            var meeting = _meetingRepository.GetMeetingById(id);

            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }
        [HttpPost("SendChatMessage")]
        public IActionResult SendChatMessage(ChatMessageDto chatMessageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chatMessage = new ChatMessage
            {
                MessageId = ChatMessageDAO.GetNextMessageId(),
                GroupId = chatMessageDto.GroupId,
                UserId = chatMessageDto.UserId,
                Content = chatMessageDto.Content,
                SentAt = DateTime.Now
            };

            _chatMessageRepository.SaveChatMessage(chatMessage);

            return CreatedAtAction(nameof(GetChatMessageById), new { id = chatMessage.MessageId }, chatMessage);
        }
        [HttpGet("GetChatMessageById/{id}")]
        public IActionResult GetChatMessageById(int id)
        {
            var chatMessage = _chatMessageRepository.GetChatMessageById(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return Ok(chatMessage);
        }
        [HttpGet("GetAllPosts")]
        public IActionResult GetAllPosts()
        {
            var posts = _postRepository.GetPosts();

            if (posts == null || posts.Count == 0)
            {
                return NotFound();
            }

            return Ok(posts);
        }
        [HttpGet("GetAllPostsByGroupId/{groupId}")]
        public IActionResult GetAllPostsByGroupId(int groupId)
        {
            try
            {
                var posts = _postRepository.GetPostsByGroupId(groupId);

                if (posts == null || posts.Count == 0)
                {
                    return NotFound(); // Trả về NotFound nếu không tìm thấy bài viết trong nhóm
                }

                return Ok(posts); // Trả về danh sách bài viết nếu tìm thấy
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Trả về BadRequest nếu groupId không hợp lệ
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message); // Trả về lỗi server nếu có lỗi xảy ra
            }
        }
        [HttpGet("GetTaskByUserId/{userId}")]
        public ActionResult<IEnumerable<Task>> GetTasksByUserId(int userId)
        {
            try
            {
                var tasks = TaskDAO.GetListTaskByUserId(userId);
                if (tasks.Count > 0)
                {
                    return Ok(tasks);
                }
                else
                {
                    return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy công việc nào cho userId cung cấp
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về mã lỗi 400 nếu có lỗi xảy ra trong quá trình xử lý yêu cầu
            }
        }

        [HttpPut("UpdateTaskStatus/{taskId}")]
        public IActionResult UpdateTaskStatus(int taskId, [FromBody] string newStatus)
        {
            try
            {
                var task = TaskDAO.FindTaskById(taskId);
                if (task == null)
                {
                    return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy công việc
                }

                // Cập nhật trạng thái mới cho công việc
                task.Status = newStatus;
                TaskDAO.UpdateTask(task);

                return Ok(); // Trả về mã lỗi 200 nếu cập nhật thành công
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Trả về mã lỗi 400 nếu có lỗi xảy ra trong quá trình xử lý yêu cầu
            }
        }




    }
}



