using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using DataAccess;

namespace GroupStudyClient.Pages.GroupMember
{
    public class CreateTaskModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateTaskModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public TaskDto TaskDto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var jsonTask = JsonConvert.SerializeObject(TaskDto);
            var content = new StringContent(jsonTask, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/GroupMember/CreateTask", content);

            if (response.IsSuccessStatusCode)
            {
                // Thành công
                return RedirectToPage("/GroupMember/GetTask");  // Chuyển hướng người dùng đến trang Index (danh sách công việc)
            }
            else
            {
                // Lỗi
                var errorResponse = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, "Lỗi khi tạo công việc: " + errorResponse);
                return Page();
            }
        }
    }
}
