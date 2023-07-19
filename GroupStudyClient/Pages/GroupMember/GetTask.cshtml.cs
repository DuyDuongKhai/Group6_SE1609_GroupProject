using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GroupStudyClient.Pages.GroupMember
{
    public class GetTaskModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<BusinessObject.Models.Task> Tasks { get; set; }

        public GetTaskModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44340/api/GroupMember/GetAllTasks");

            if (response.IsSuccessStatusCode)
            {
                Tasks = await response.Content.ReadFromJsonAsync<List<BusinessObject.Models.Task>>();
                return Page();
            }
            else
            {
                // Xử lý lỗi khi lấy danh sách công việc
                return Page();
            }
        }
    }
}
