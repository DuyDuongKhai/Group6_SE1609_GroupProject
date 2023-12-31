﻿using System;
using AutoMapper;
using System.Linq;
using System.Data;
using Repositories;
using BusinessObject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Sub_Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace GroupStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupAdminController : ControllerBase
    {
        private readonly IGroupRepository groupRepository;
        private readonly IUserRepository userRepository;
        private readonly IGroupMemberRepository groupMemberRepository;
        private readonly IJoinRequestRepository joinRequestRepository;
        private readonly IPostRepository postRepository;
        private readonly IMapper _mapper;
        private readonly ITaskRepository taskRepository;
        public GroupAdminController(IGroupRepository groupRepository, IUserRepository userRepository, IGroupMemberRepository groupMemberRepository, IJoinRequestRepository joinRequestRepository,IMapper mapper,IPostRepository postRepository,ITaskRepository taskRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.groupMemberRepository = groupMemberRepository;
            this.joinRequestRepository = joinRequestRepository;
            this.postRepository = postRepository;
            _mapper = mapper;
            this.taskRepository = taskRepository;
        }

        [HttpPost]
        [Route("CreateGroup")]
        public async Task<IActionResult> CreateGroup(GroupModel g)
        {
            try
            {
                var numberofGroup = groupRepository.GetGroups().Count;

                Group newGroup = new Group
                { 
                    GroupId = numberofGroup+1,
                    GroupName  = g.GroupName,
         GroupAdminId = g.GroupAdminId,
         CreatedAt = g.CreatedAt,
         Description =g.Description,
    };

               groupRepository.SaveGroup(newGroup);
                var numberofGroupmember = groupMemberRepository.GetGroupMembers().Count;

                GroupMember groupMember = new GroupMember
                {
                    GroupMemberId = numberofGroupmember+1,
                    GroupId = newGroup.GroupId,
                    JoinedAt = newGroup.CreatedAt,
                    UserId = g.GroupAdminId,
                    Status= "Active",
                    Role = userRepository.GetUserById((int)newGroup.GroupAdminId).Role
                };
                groupMemberRepository.CreateGroupMemberAdmin(groupMember);

            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Ok("Create Group success");
        }
        [HttpGet]
       [Route("GetAllByGroupAdminId/{groupAdminId}")]
        public async Task<List<Group>> GetAllByAdminId(int groupAdminId)
        {
            var list=new List<Group>();
            try
            {
                list = groupRepository.GetGroupsByGroupAdminId(groupAdminId);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return list;
        }

        [HttpPost]
        [Route("AddMemberToGroup/{groupId}/{memberId}")]
        public async Task<IActionResult> AddMember( int groupId, int memberId)
        {
            string msg = "";
            try
            {
                var user = userRepository.GetUserById(memberId);
                var group = groupRepository.GetGroupById(groupId);
                var numberofGroupmember = groupMemberRepository.GetGroupMembers().Count;
                if (user == null)
                {
                    throw new Exception("User does not exist");
                }
                if(group== null)
                {
                    throw new Exception("Group does not exist");
                }
                GroupMember groupMember = new GroupMember
                {
                    GroupMemberId = numberofGroupmember + 1,
                    GroupId= group.GroupId,
                    Status = "Active",
                    JoinedAt= DateTime.Now,
                    Role = user.Role,
                    UserId= user.UserId
                    
                    
                };
                groupMemberRepository.SaveGroupMember(groupMember);
                msg = $"Add Member {user.FirstName} to {group.GroupName}";
            }catch(Exception ex)
            {
                msg = ex.Message;
                return BadRequest(msg);
            }
            return Ok(msg);
        }
        [HttpGet]
        [Route("GetGroupMember/{groupId}")]
        public async Task<IActionResult> GetGroupMemberById(int groupId)
        {
            string msg = "";
            var list = new List<User>();
            var newList= new List<UserModel>();
            try
            {
                var group = groupRepository.GetGroupById(groupId);
                if(group==null)
                {
                    throw new Exception("Group doesnt exist");
                }
                list = groupMemberRepository.GetMemberFromGroup(groupId);
                foreach(var user in list)
                {
                    newList.Add(_mapper.Map<UserModel>(user));
                }
            }catch(Exception ex)
            {
                msg=ex.Message;
                return BadRequest(msg);
            }
            return Ok(newList);
        }

        [HttpPut]
        [Route("ApproveRequest/{memberId}/{groupId}")]
        public async Task<IActionResult> ApproveRequest(int memberId,int groupId)
        {
            var msg = "";
            int requestId = joinRequestRepository.GetRequestId(groupId, memberId);
            JoinRequestModel joinRequestModel = new JoinRequestModel
            {
                RequestId = requestId,
                GroupId = groupId,
                UserId = memberId,
                Status= "Approved"
            };
            var joinRequest = _mapper.Map<JoinRequest>(joinRequestModel);
            var checkJr = joinRequestRepository.CheckJoinRequest(joinRequestModel.RequestId);
            try
            {
                if (!checkJr)
                {
                    joinRequestRepository.UpdateJoinRequest(joinRequest);
                    await AddMember(groupId, memberId);
                    msg = "Approve and Added to group";
                }else
                {
                    throw new Exception("Already Approved");
                }
            }catch(Exception ex)
            {
                msg = ex.Message;   
            }
            return Ok(msg);
        }

        [HttpDelete]
        [Route("DisapproveRequest/{memberId}/{groupId}")]
        public async Task<IActionResult> DisapproveRequest(int memberId, int groupId)
        {
            var msg = "";
            int requestId = joinRequestRepository.GetRequestId(groupId, memberId);

            JoinRequestModel joinRequestModel = new JoinRequestModel
            {
                
                GroupId = groupId,
                UserId = memberId,
            };
            var checkJr = joinRequestRepository.CheckJoinRequest(requestId);
            var joinRequest = _mapper.Map<JoinRequest>(joinRequestModel);
            try
            {
                if (!checkJr)
                {
                    joinRequestRepository.DeleteJoinRequest(joinRequest);
                    msg = "Disapprove";
                }
                else
                {
                    throw new Exception("Can't dissapprove when it already approved");
                }
            }
            catch (Exception ex)
            {
                msg=ex.Message;
            }
            return Ok(msg);
        }

        [HttpGet]
        [Route("ViewJoinRequest/{groupId}")]
        public async Task<IActionResult> ListRequestFromGroup(int groupId)
        {
            string msg = "";
            var list = new List<JoinRequest>();
            var list1 = new List<JoinRequestModel>();
            try
            {
                list = joinRequestRepository.ListJoinRequestByGroupId(groupId);

                foreach(var joinRequest in list)
                {
                    list1.Add(_mapper.Map<JoinRequest,JoinRequestModel>(joinRequest));
                }
            }catch (Exception ex)
            {
                msg = ex.Message;
                return BadRequest(msg);
            }
            return Ok(list1);
        }

        [HttpGet]
        [Route("ViewJoinRequest/")]
        public async Task<IActionResult> GetAllRequest()
        {
            string msg = "";
            var list = new List<JoinRequest>();
            var list1 = new List<JoinRequestModel>();
            try
            {
                list = joinRequestRepository.GetJoinRequests();

                foreach (var joinRequest in list)
                {
                    list1.Add(_mapper.Map<JoinRequest, JoinRequestModel>(joinRequest));
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return BadRequest(msg);
            }
            return Ok(list1);
        }

        [HttpDelete]
        [Route("RemoveMember/{groupId}/{memberId}")]
        public async Task<IActionResult> RemoveMember(int groupId,int memberId)
        {
            string msg = "";
            try
            {
                var member = userRepository.GetUserById(memberId);
                if(member==null)
                {
                    return BadRequest("No member has that id");
                }
                GroupMember c = new GroupMember
                {
                    GroupId = groupId,
                    UserId = memberId
                };
                groupMemberRepository.DeleteGroupMember(c);
                msg = "Delete Sucess";
            }
            catch (Exception ex)
            {
                msg=ex.Message;
            }
            return Ok(msg);
        }
        [HttpGet]
        [Route("GroupPosts/{groupId}")]
        public async Task<IActionResult> GroupPosts(int groupId)
        {
            List<PostModel> posts = new List<PostModel>();
           try
            {
                posts= _mapper.Map<List<PostModel>>(postRepository.GetPostByGroupId(groupId));
            }catch
            (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(posts);
        }
        [HttpDelete]
        [Route("DeletePost/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                var p = new Post
                {
                    PostId= postId
                };
                postRepository.DeletePost(p);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Delete Post success");
        }

        [HttpPost]
        [Route("AssignTask")]
        public async Task<IActionResult> AssignTaskToMember(TaskModel task)
        {
            var msg = "";
            try
            {
                var user = userRepository.GetUserById((int)task.AssignedToUserId);
                var group = groupRepository.GetGroupById((int)task.GroupId);
                if (user == null)
                {
                    return BadRequest("No user to assign");
                }
                else if (group == null)
                {
                    return BadRequest("No group exist");
                }
                BusinessObject.Models.Task newTask = _mapper.Map<BusinessObject.Models.Task>(task); 
                newTask.TaskId= taskRepository.GetNextTaskId();
                taskRepository.SaveTask(newTask);
                var getTask = taskRepository.GetTaskById(newTask.TaskId);
                msg = $"Assign success to User {getTask.AssignedToUser.LastName} from Group {getTask.Group.GroupName}";
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return Ok(msg);
        }
        [HttpGet]
        [Route("GetTasksByGroupId/{groupId}")]
        public async Task<IActionResult> GetTasksByGroupId(int groupId)
        {
            List<TaskModel> list = _mapper.Map<List<TaskModel>>(taskRepository.GetTasksByGroupId(groupId));
            return Ok(list);
        }

        [HttpGet]
        [Route("GetTasksByGroupAndUserId/{groupId}/{memberId}")]
        public async Task<IActionResult> GetTasksByGroupId(int groupId,int memberId)
        {
            List<TaskModel> list = _mapper.Map<List<TaskModel>>(taskRepository.GetListTaskByGroupAndUserId(groupId,memberId));
            return Ok(list);
        }

        [HttpDelete]
        [Route("DeleteTask/{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var task = taskRepository.GetTaskById(taskId);
            try
            {
                task.Status = "Deleted";
                taskRepository.UpdateTask(task);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Delete Success");
        }



    }
}
