using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public interface IPostRepository
    {
        void SavePost(Post c);
        Post GetPostById(int id);
        void DeletePost(Post c);
        void UpdatePost(Post c);

        List<User> GetUsers();
        List<Group> GetGroups();
        List<Post> GetPosts();
        List<Post> GetPostByGroupId(int groupId);
        List<Post> GetPostsByGroupId(int groupId);
    }
}
