using Newtonsoft.Json;
using System.Net.Http;
using BusinessObject.Models;
using System.Threading.Tasks;
using BusinessObject.Sub_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupStudyClient.Pages.GroupAdmin
{
    public class ManageTaskModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public ManageTaskModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public List<Group> Groups { get; set; }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> GroupId { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public async Task<IActionResult> OnGetAsync(int GroupId)
        {
            string userId = HttpContext.Session.GetString("UserId");
            string getGroupByIdurl = $"https://localhost:44340/api/GroupAdmin/GetAllByGroupAdminId/{userId}";
            string getTaskByGroupIdurl = $"https://localhost:44340/api/GroupAdmin/GetTasksByGroupId/{GroupId}";

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync(getGroupByIdurl);
            var response1 = await httpClient.GetAsync(getTaskByGroupIdurl);

            if (response.IsSuccessStatusCode&&response1.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Groups = JsonConvert.DeserializeObject<List<Group>>(content);
                var selectList = new SelectList(Groups, "GroupId", "GroupName");

                // Pass the SelectList to the view
                ViewData["GroupsSelectList"] = selectList;
                var content1 = await response1.Content.ReadAsStringAsync();
                Tasks = JsonConvert.DeserializeObject<List<TaskModel>>(content1);

            }
            else
            {
                Groups = new List<Group>();
                Tasks = new List<TaskModel>();

            }

            return Page();
        }
        public async Task<IActionResult> OnPostTaskByGroupAsync(int GroupId)
        {
            string userId = HttpContext.Session.GetString("UserId");

            string getGroupByIdurl = $"https://localhost:44340/api/GroupAdmin/GetAllByGroupAdminId/{userId}";
            string getTaskByGroupIdurl = $"https://localhost:44340/api/GroupAdmin/GetTasksByGroupId/{GroupId}";

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync(getGroupByIdurl);
            var response1 = await httpClient.GetAsync(getTaskByGroupIdurl);
            if (response.IsSuccessStatusCode&&response1.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Groups = JsonConvert.DeserializeObject<List<Group>>(content);
                var selectList = new SelectList(Groups, "GroupId", "GroupName");

                // Pass the SelectList to the view
                ViewData["GroupsSelectList"] = selectList;
                var content1 = await response1.Content.ReadAsStringAsync();
                Tasks = JsonConvert.DeserializeObject<List<TaskModel>>(content1);


            }
            else
            {
                Groups = new List<Group>();
                Tasks= new List<TaskModel>();
            }

            return Page();
        }
    }
}
