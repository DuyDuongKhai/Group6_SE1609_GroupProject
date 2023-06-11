using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ChatMessageDAO
    {
        public static List<ChatMessage> GetChatMessages()
        {
            var listChatMessages = new List<ChatMessage>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listChatMessages = context.ChatMessages.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listChatMessages;
        }
        public static ChatMessage FindChatMessageById(int Id)
        {
            ChatMessage c = new ChatMessage();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.ChatMessages.SingleOrDefault(x => x.MessageId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }
       
        public static void SaveChatMessage(ChatMessage c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.ChatMessages.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateChatMessage(ChatMessage c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<ChatMessage>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteChatMessage(ChatMessage c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.ChatMessages.SingleOrDefault(u => u.MessageId == c.MessageId);
                    context.ChatMessages.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
