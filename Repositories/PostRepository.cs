using System;
using DataAccess;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public List<Post> GetPostByGroupId(int groupId) => PostDAO.GetPostByGroupId(groupId);

    }
}
