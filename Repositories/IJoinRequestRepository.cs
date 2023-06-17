using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IJoinRequestRepository
    {
        
        JoinRequest GetJoinRequestById(int id);
        void DeleteJoinRequest(JoinRequest c);
        void UpdateJoinRequest(JoinRequest c);
        void SaveJoinRequest(JoinRequest joinRequest);

        List<User> GetUsers();
        List<Group> GetGroups();
        List<JoinRequest> GetJoinRequests();
    }
}
