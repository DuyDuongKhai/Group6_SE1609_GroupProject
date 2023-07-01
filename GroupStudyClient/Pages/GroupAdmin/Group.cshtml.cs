using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using BusinessObject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Sub_Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupStudyClient.Pages.GroupAdmin
{
    public class GroupModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public GroupModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        [BindProperty(SupportsGet = true)]
        public int GroupId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int MemberId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int AssignedToUserId { get; set; }
        public List<UserModel> UserModels { get; set; }

        public List<PostModel> PostModels { get; set; }

        public async Task<IActionResult> OnPostGroupMemberAsync(int GroupId)
        {
            string groupMemberUrl = $"https://localhost:44340/api/GroupAdmin/GetGroupMember/{GroupId}";
            string GroupPostUrl = $"https://localhost:44340/api/GroupAdmin/GroupPosts/{GroupId}";

            HttpContext.Session.SetString("GroupId", GroupId.ToString());
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync(groupMemberUrl);
            var response1 = await httpClient.GetAsync(GroupPostUrl);

            if (response.IsSuccessStatusCode&&response1.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserModels = JsonConvert.DeserializeObject<List<UserModel>>(content);
                var content1 = await response1.Content.ReadAsStringAsync();
                PostModels = JsonConvert.DeserializeObject<List<PostModel>>(content1);
                var selectList = new SelectList(UserModels, "UserId", "LastName");

                // Pass the SelectList to the view
                ViewData["UserModelsSelectList"] = selectList;
            }
            else
            {
                UserModels = new List<UserModel>();
                PostModels = new List<PostModel>();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostRemoveMemberAsync([FromQuery]int GroupId, int MemberId)
        {
            string removeMemberurl = $"https://localhost:44340/api/GroupAdmin/RemoveMember/{GroupId}/{MemberId}";
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.DeleteAsync(removeMemberurl);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = content.ToString();
            }else
            {
                TempData["ErrorMessage"] = content.ToString();

            }

            return await OnPostGroupMemberAsync(GroupId);
        }
        public async Task<IActionResult> OnPostAssignTaskAsync(TaskModel task)
        {
            string assignTaskurl = $"https://localhost:44340/api/GroupAdmin/AssignTask";

            var httpClient = _clientFactory.CreateClient();

            task.Status = "Assigned";
            // Serialize the newGroup object to JSON
            string jsonPayload = JsonConvert.SerializeObject(task);

            // Create the request content
            StringContent requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send the POST request
            HttpResponseMessage response = await httpClient.PostAsync(assignTaskurl, requestContent);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Handle the successful response (e.g., redirect to a success page)
                string responseContent1 = await response.Content.ReadAsStringAsync();

                TempData["Message"] = responseContent1.ToString();
                return await OnPostGroupMemberAsync((int)task.GroupId);
            }

            // Handle the failure response (e.g., show an error message)
            var responseContent = await response.Content.ReadAsStringAsync();
            // Log or handle the error accordingly
            TempData["ErrorMessage"] = responseContent.ToString();
            return Page();
        }
    }
}
