using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IChatMessageRepository
    {
        void SaveChatMessage(ChatMessage c);
        ChatMessage GetChatMessageById(int id);
        void DeleteChatMessage(ChatMessage c);
        void UpdateChatMessage(ChatMessage c);

        List<User> GetUsers();
        List<Group> GetGroups();
        List<ChatMessage> GetChatMessages();
    }
}
