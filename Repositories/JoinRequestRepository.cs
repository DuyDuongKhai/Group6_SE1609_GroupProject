using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class JoinRequestRepository : IJoinRequestRepository
    { 
        public void DeleteJoinRequest(JoinRequest c) => JoinRequestDAO.DeleteJoinRequest(c);


        public void UpdateJoinRequest(JoinRequest c) => JoinRequestDAO.UpdateJoinRequest(c);

        public List<User> GetUsers() => UserDAO.GetUsers();


        public JoinRequest GetJoinRequestById(int id) => JoinRequestDAO.FindJoinRequestById(id);

        public List<Group> GetGroups() => GroupDAO.GetGroups();
        public List<JoinRequest> GetJoinRequests() => JoinRequestDAO.GetJoinRequests();

        public void SaveJoinRequest(JoinRequest joinRequest)
        {
            JoinRequestDAO.SaveJoinRequest(joinRequest);
        }

    }
}
