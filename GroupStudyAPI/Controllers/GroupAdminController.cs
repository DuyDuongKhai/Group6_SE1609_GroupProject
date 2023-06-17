using System;
using AutoMapper;
using System.Linq;
using Repositories;
using BusinessObject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Sub_Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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
        public GroupAdminController(IGroupRepository groupRepository, IUserRepository userRepository, IGroupMemberRepository groupMemberRepository, IJoinRequestRepository joinRequestRepository,IMapper mapper,IPostRepository postRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
            this.groupMemberRepository = groupMemberRepository;
            this.joinRequestRepository = joinRequestRepository;
            this.postRepository = postRepository;
            _mapper = mapper;
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
        public async Task<List<Group>> GetAll()
        {
            var list=new List<Group>();
            try
            {
                list = groupRepository.GetGroups();
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
            int requestId = joinRequestRepository.GetRequestId(groupId, memberId);
            JoinRequestModel joinRequestModel = new JoinRequestModel
            {
                RequestId = requestId,
                GroupId = groupId,
                UserId = memberId,
                Status= "Approved"
            };
            var joinRequest = _mapper.Map<JoinRequest>(joinRequestModel);
            try
            {
                joinRequestRepository.UpdateJoinRequest(joinRequest);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok("Approve");
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
            }
            catch (Exception ex)
            {
                msg=ex.Message;
            }
            return Ok("Delete success");
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

        
        
    }
}
