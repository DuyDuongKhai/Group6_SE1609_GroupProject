using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public interface IJoinRequestRepository
    {
        void SaveJoinRequest(JoinRequest c);
        JoinRequest GetJoinRequestById(int id);
        void DeleteJoinRequest(JoinRequest c);
        void UpdateJoinRequest(JoinRequest c);

        List<User> GetUsers();
        List<Group> GetGroups();
        List<JoinRequest> GetJoinRequests();
        List<JoinRequest> ListJoinRequestByGroupId(int groupId);
        int GetRequestId(int groupId, int memberId);
    }
}
