using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CommentDAO
    {
        public static List<Comment> GetComments()
        {
            var listComments = new List<Comment>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listComments = context.Comments.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listComments;
        }
        public static Comment FindCommentById(int Id)
        {
            Comment c = new Comment();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.Comments.SingleOrDefault(x => x.CommentId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public static void SaveComment(Comment c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Comments.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateComment(Comment c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<Comment>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteComment(Comment c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.Comments.SingleOrDefault(u => u.CommentId == c.CommentId);
                    context.Comments.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static List<Comment> GetCommentsByPostId(int postId)
        {
            var comments = new List<Comment>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    comments = context.Comments.Where(c => c.PostId == postId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return comments;
        }

    }
}
