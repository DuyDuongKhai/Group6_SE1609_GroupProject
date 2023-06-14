using System;
using DataAccess;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public void DeleteComment(Comment c) => CommentDAO.DeleteComment(c);

        public void SaveComment(Comment c) => CommentDAO.SaveComment(c);

        public void UpdateComment(Comment c) => CommentDAO.UpdateComment(c);

        public List<User> GetUsers() => UserDAO.GetUsers();

        public List<Post> GetPosts() => PostDAO.GetPosts();
        public Comment GetCommentById(int id) => CommentDAO.FindCommentById(id);

        public List<Comment> GetComments() => CommentDAO.GetComments();
    }
}
