using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
