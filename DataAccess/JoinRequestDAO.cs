using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class JoinRequestDAO
    {
        public static List<JoinRequest> GetJoinRequests()
        {
            var listJoinRequests = new List<JoinRequest>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listJoinRequests = context.JoinRequests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listJoinRequests;
        }
        public static JoinRequest FindJoinRequestById(int Id)
        {
            JoinRequest c = new JoinRequest();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.JoinRequests.SingleOrDefault(x => x.RequestId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public static void SaveJoinRequest(JoinRequest joinRequest)
        {
            try
            {
                joinRequest.RequestId = GetNextJoinRequestId();

                using (var context = new GroupStudyContext())
                {
                    context.JoinRequests.Add(joinRequest);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateJoinRequest(JoinRequest c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<JoinRequest>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteJoinRequest(JoinRequest c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.JoinRequests.SingleOrDefault(u => u.RequestId == c.RequestId);
                    context.JoinRequests.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static int GetNextJoinRequestId()
        {
            int nextJoinRequestId = 0;
            try
            {
                using (var context = new GroupStudyContext())
                {
                    nextJoinRequestId = context.JoinRequests.Max(jr => jr.RequestId) + 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nextJoinRequestId;
        }



    }
}
