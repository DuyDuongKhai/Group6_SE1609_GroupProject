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
    public class UsersController : ControllerBase
    {


        private IUserRepository _userRepository = new UserRepository();


        public UsersController()
        {

        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {

                try
                {
                    _userRepository.UpdateUser(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (UserExists(user.UserId))
            {
                return Conflict();
            }
            else
            {
                try
                {
                    _userRepository.SaveUser(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return CreatedAtAction("GetCustomer", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(user);

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _userRepository.GetUserById(id) != null;
        }
    }
}
