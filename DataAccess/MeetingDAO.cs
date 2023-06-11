using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MeetingDAO
    {
        public static List<Meeting> GetMeetings()
        {
            var listMeetings = new List<Meeting>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listMeetings = context.Meetings.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listMeetings;
        }
        public static Meeting FindMeetingById(int Id)
        {
            Meeting c = new Meeting();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.Meetings.SingleOrDefault(x => x.MeetingId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public static void SaveMeeting(Meeting c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Meetings.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateMeeting(Meeting c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<Meeting>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteMeeting(Meeting c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.Meetings.SingleOrDefault(u => u.MeetingId == c.MeetingId);
                    context.Meetings.Remove(c1);
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
