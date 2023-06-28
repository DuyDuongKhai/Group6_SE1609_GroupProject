using System.Net.Http;
using Newtonsoft.Json;
using BusinessObject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Sub_Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        public List<UserModel> UserModels { get; set; }

        public async Task<IActionResult> OnPostGroupMemberAsync(int GroupId)
        {
            string groupMemberUrl = $"https://localhost:44340/api/GroupAdmin/GetGroupMember/{GroupId}";
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync(groupMemberUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserModels = JsonConvert.DeserializeObject<List<UserModel>>(content);
            }
            else
            {
                UserModels = new List<UserModel>();
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
    }
}
