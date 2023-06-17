using BusinessObject.Models;
using GroupStudyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IJoinRequestRepository _joinRequestRepository;

        public UserController(IUserRepository userRepository, IGroupRepository groupRepository, IJoinRequestRepository joinRequestRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _joinRequestRepository = joinRequestRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                return NotFound();

            return user;
        }


        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
                return NotFound();

            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.DateOfBirth = updatedUser.DateOfBirth;
            existingUser.PhoneNumber = updatedUser.PhoneNumber;
            existingUser.Address = updatedUser.Address;

            _userRepository.UpdateUser(existingUser);
            return NoContent();
        }

        [HttpPost("{id}")]
        public IActionResult PostUser(int id, [FromBody] User user)
        {
            if (user == null)
            {

                return BadRequest();
            }

            if (id != user.UserId)
            {

                return BadRequest();
            }


            return NoContent();
        }

        [HttpGet("search/{keyword}")]
        public ActionResult<List<Group>> SearchGroups(string keyword)
        {
            var searchResults = _userRepository.SearchGroups(keyword);
            return searchResults;
        }
        [HttpPost("{userId}/groups/{groupId}/join")]
        public IActionResult JoinGroup(int userId, int groupId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                return NotFound("User not found.");

            var group = _groupRepository.GetGroupById(groupId);
            if (group == null)
                return NotFound("Group not found.");

            // Check if the user is already a member of the group
            if (group.GroupMembers.Any(m => m.UserId == userId))
                return BadRequest("User is already a member of the group.");

            // Check if the user has already sent a join request to the group
            if (group.JoinRequests.Any(jr => jr.UserId == userId))
                return BadRequest("User has already sent a join request to the group.");

            // Create a new join request
            var joinRequest = new JoinRequest
            {
                UserId = userId,
                GroupId = groupId,
                Status = "Pending"
            };

            _joinRequestRepository.SaveJoinRequest(joinRequest);

            return Ok("Join request sent successfully.");




        }
    }
}
