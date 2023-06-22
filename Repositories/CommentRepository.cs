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
        private readonly CommentDAO _commentDAO;
        private readonly UserDAO _userDAO;
        private readonly PostDAO _postDAO;

        public CommentRepository()
        {
            _commentDAO = new CommentDAO();
            _userDAO = new UserDAO();
            _postDAO = new PostDAO();
        }

        public void DeleteComment(Comment c) => DataAccess.CommentDAO.DeleteComment(c);


        public void SaveComment(Comment c) => DataAccess.CommentDAO.SaveComment(c);

        public void UpdateComment(Comment c) => DataAccess.CommentDAO.UpdateComment(c);

        public List<User> GetUsers() => UserDAO.GetUsers();


        public List<Post> GetPosts() => PostDAO.GetPosts();

        public Comment GetCommentById(int id) => DataAccess.CommentDAO.FindCommentById(id);

        public List<Comment> GetComments() => DataAccess.CommentDAO.GetComments();
    }
}
