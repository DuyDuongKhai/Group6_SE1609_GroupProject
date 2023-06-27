using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
                    listJoinRequests = context.JoinRequests
                         .Include(x => x.User)
                        .Include(x => x.Group)
                        .ToList();
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
                throw new Exception("No request to approve");
            }
        }
        public static void DeleteJoinRequest(JoinRequest c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.JoinRequests.SingleOrDefault(u => u.UserId == c.UserId && u.GroupId==c.GroupId);
                    context.JoinRequests.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("No request to disapprove");
            }
        }

        public static List<JoinRequest> ListJoinRequestByGroupId(int groupId)
        {
            List<JoinRequest> list= new List<JoinRequest>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    list = context.JoinRequests
                        .Include(x=>x.User)
                        .Include(x=>x.Group)
                        .Where(x=>x.GroupId==groupId).ToList();
                    if(list.Count()==0)
                    {
                        throw new Exception("No request in that group");
                    }

                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }
        public static int GetRequestId(int groupId, int memberId)
        {
            int requestId = 0;
            try
            {
                using (var context = new GroupStudyContext())
                {
                     requestId = context.JoinRequests
               .Where(r => r.GroupId == groupId && r.UserId == memberId)
               .Select(r => r.RequestId)
               .FirstOrDefault();
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return requestId;
        }
        public static int GetNextJoinRequestId()
        {
            int nextJoinRequestId = 0;
            try
            {
                using (var context = new GroupStudyContext())
                {
                    if (context.JoinRequests.Count() > 0)
                    {
                        nextJoinRequestId = context.JoinRequests.Max(jr => jr.RequestId) + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nextJoinRequestId;
        }

        public static bool CheckJoinRequest(int requestId)
        {
                using (var context = new GroupStudyContext())
                {
                    JoinRequest existedJr= context.JoinRequests.FirstOrDefault(x=>x.RequestId== requestId);
                    if(existedJr.Status.Equals("Pending"))
                    {
                        return false;
                    }
                }
            return true;
        }



    }
}
