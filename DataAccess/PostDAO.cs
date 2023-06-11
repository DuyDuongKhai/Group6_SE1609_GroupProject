using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PostDAO
    {
        public static List<Post> GetPosts()
        {
            var listPosts = new List<Post>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listPosts = context.Posts.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPosts;
        }
        public static Post FindPostById(int Id)
        {
            Post c = new Post();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.Posts.SingleOrDefault(x => x.PostId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public static void SavePost(Post c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Posts.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdatePost(Post c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<Post>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeletePost(Post c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.Posts.SingleOrDefault(u => u.PostId == c.PostId);
                    context.Posts.Remove(c1);
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
