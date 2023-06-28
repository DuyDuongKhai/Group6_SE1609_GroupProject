using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Repositories;

namespace GroupStudyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private IGroupRepository _groupRepository = new GroupRepository();


        public GroupsController()
        {

        }

        // GET: api/Groups
        [HttpGet]
        public ActionResult<IEnumerable<Group>> GetGroups()
        {
            return _groupRepository.GetGroups();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public ActionResult<Group> GetGroup(int id)
        {
            var group = _groupRepository.GetGroupById(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutGroup(int id, Group group)
        {
            if (id != group.GroupId)
            {
                return BadRequest();
            }

            if (!GroupExists(id))
            {
                return NotFound();
            }
            else
            {

                try
                {
                    _groupRepository.UpdateGroup(group);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return NoContent();
        }

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Group> PostGroup(Group group)
        {
            if (GroupExists(group.GroupId))
            {
                return Conflict();
            }
            else
            {
                try
                {
                    group.GroupId = _groupRepository.GetNextId();
                    _groupRepository.SaveGroup(group);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok();
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            var group = _groupRepository.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }

            _groupRepository.DeleteGroup(group);

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return _groupRepository.GetGroupById(id) != null;
        }
    }
}
