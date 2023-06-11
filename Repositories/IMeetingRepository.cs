using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IMeetingRepository
    {
        void SaveMeeting(Meeting c);
        Meeting GetMeetingById(int id);
        void DeleteMeeting(Meeting c);
        void UpdateMeeting(Meeting c);

        List<Meeting> GetMeetings();
        List<Group> GetGroups();
    }
}
