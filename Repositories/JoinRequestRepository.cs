using System;
using DataAccess;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public  List<JoinRequest> ListJoinRequestByGroupId(int groupId) => JoinRequestDAO.ListJoinRequestByGroupId(groupId);
        public int GetRequestId(int groupId, int memberId)=> JoinRequestDAO.GetRequestId(groupId, memberId);

        public void SaveJoinRequest(JoinRequest joinRequest)
        {
            JoinRequestDAO.SaveJoinRequest(joinRequest);
        }
        public bool CheckJoinRequest(int requestId)=> JoinRequestDAO.CheckJoinRequest(requestId);

    }
}
