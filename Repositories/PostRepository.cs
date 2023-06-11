using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PostRepository : IPostRepository
    {
        public void DeletePost(Post c) => PostDAO.DeletePost(c);

        public void SavePost(Post c) => PostDAO.SavePost(c);

        public void UpdatePost(Post c) => PostDAO.UpdatePost(c);

        public Post GetPostById(int id) => PostDAO.FindPostById(id);

        public List<Group> GetGroups() => GroupDAO.GetGroups();
        public List<User> GetUsers() => UserDAO.GetUsers();
        public List<Post> GetPosts() => PostDAO.GetPosts();
    }
}
