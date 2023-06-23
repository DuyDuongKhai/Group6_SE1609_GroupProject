using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
        
    {
        public void DeleteChatMessage(ChatMessage c) => ChatMessageDAO.DeleteChatMessage(c);

        public void SaveChatMessage(ChatMessage c) => ChatMessageDAO.SaveChatMessage(c);

        public void UpdateChatMessage(ChatMessage c) => ChatMessageDAO.UpdateChatMessage(c);

        public List<User> GetUsers() => UserDAO.GetUsers();

        public List<Group> GetGroups() => GroupDAO.GetGroups();
        public ChatMessage GetChatMessageById(int id) => ChatMessageDAO.FindChatMessageById(id);

        public List<ChatMessage> GetChatMessages() => ChatMessageDAO.GetChatMessages();
        public int GetNextMessageId()
        {
            int nextMessagedId = ChatMessageDAO.GetNextMessageId();
            return nextMessagedId;
        }
    }
}
