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
    public class ChatMessagesController : ControllerBase
    {
        private IChatMessageRepository _chatMessageRepository = new ChatMessageRepository();


        public ChatMessagesController()
        {

        }

        // GET: api/ChatMessages
        [HttpGet]
        public ActionResult<IEnumerable<ChatMessage>> GetChatMessages()
        {
            return _chatMessageRepository.GetChatMessages();
        }

        // GET: api/ChatMessages/5
        [HttpGet("{id}")]
        public ActionResult<ChatMessage> GetChatMessage(int id)
        {
            var chatMessage = _chatMessageRepository.GetChatMessageById(id);

            if (chatMessage == null)
            {
                return NotFound();
            }

            return chatMessage;
        }

        // PUT: api/ChatMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutChatMessage(int id, ChatMessage chatMessage)
        {
            if (id != chatMessage.MessageId)
            {
                return BadRequest();
            }

            if (!ChatMessageExists(id))
            {
                return NotFound();
            }
            else
            {

                try
                {
                    _chatMessageRepository.UpdateChatMessage(chatMessage);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return NoContent();
        }

        // POST: api/ChatMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ChatMessage> PostChatMessage(ChatMessage chatMessage)
        {
            if (ChatMessageExists(chatMessage.MessageId))
            {
                return Conflict();
            }
            else
            {
                try
                {
                    _chatMessageRepository.SaveChatMessage(chatMessage);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return CreatedAtAction("GetCustomer", new { id = chatMessage.MessageId }, chatMessage);
        }

        // DELETE: api/ChatMessages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteChatMessage(int id)
        {
            var chatMessage = _chatMessageRepository.GetChatMessageById(id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            _chatMessageRepository.DeleteChatMessage(chatMessage);

            return NoContent();
        }

        private bool ChatMessageExists(int id)
        {
            return _chatMessageRepository.GetChatMessageById(id) != null;
        }
    }
}
