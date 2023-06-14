using System;
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

        public GroupAdminController(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        [HttpPost]
        [Route("CreateGroup")]
        public async Task<IActionResult> CreateGroup(GroupModel g)
        {
            try
            {
                Group newGroup = new Group
                { 
                    GroupId = g.GroupId,
                    GroupName  = g.GroupName,
         GroupAdminId = g.GroupAdminId,
         CreatedAt = g.CreatedAt,
         Description =g.Description,
    };

                groupRepository.SaveGroup(newGroup);
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
    }
}
