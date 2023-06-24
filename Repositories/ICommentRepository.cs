using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICommentRepository
    {
        void SaveComment(Comment c);
        Comment GetCommentById(int id);
        void DeleteComment(Comment c);
        void UpdateComment(Comment c);

        List<User> GetUsers();
        List<Post> GetPosts();
        List<Comment> GetComments();
        List<Comment> GetCommentsByPostId(int postId);
    }
}
