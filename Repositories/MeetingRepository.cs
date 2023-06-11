using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        public void DeleteMeeting(Meeting c) => MeetingDAO.DeleteMeeting(c);

        public void SaveMeeting(Meeting c) => MeetingDAO.SaveMeeting(c);

        public void UpdateMeeting(Meeting c) => MeetingDAO.UpdateMeeting(c);

        public Meeting GetMeetingById(int id) => MeetingDAO.FindMeetingById(id);

        public List<Group> GetGroups() => GroupDAO.GetGroups();
        public List<Meeting> GetMeetings() => MeetingDAO.GetMeetings();
    }
}
