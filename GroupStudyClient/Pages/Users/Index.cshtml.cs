using System.Net.Http;
using Newtonsoft.Json;
using BusinessObject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupStudyClient.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }

        public List<Group> Groups { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Keyword { get; set; }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Index");
            }

            var apiUrlWithUserId = $"https://localhost:44340/api/User/{userId}";

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.GetAsync(apiUrlWithUserId);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<User>(content);
            }
            else
            {
                // Handle error if needed
                User = new User();
            }

            if (string.IsNullOrEmpty(Keyword))
            {
                // Lấy danh sách tất cả các nhóm
                var groupResponse = await httpClient.GetAsync("https://localhost:44340/api/Groups");

                if (groupResponse.IsSuccessStatusCode)
                {
                    var groupContent = await groupResponse.Content.ReadAsStringAsync();
                    Groups = JsonConvert.DeserializeObject<List<Group>>(groupContent);
                }
                else
                {
                    Groups = new List<Group>();
                }
            }
            else
            {
                // Tìm kiếm nhóm dựa trên từ khóa
                var searchResponse = await httpClient.GetAsync($"https://localhost:44340/api/User/search/{Keyword}");

                if (searchResponse.IsSuccessStatusCode)
                {
                    var searchContent = await searchResponse.Content.ReadAsStringAsync();
                    Groups = JsonConvert.DeserializeObject<List<Group>>(searchContent);
                }
                else
                {
                    Groups = new List<Group>();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostJoinGroup(int groupId)
        {
            // Lấy UserId từ Session hoặc từ một nguồn khác tùy vào cách bạn lưu trữ UserId
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                // Xử lý khi không tìm thấy UserId
                // Có thể chuyển hướng người dùng đến trang đăng nhập hoặc hiển thị thông báo lỗi
                return RedirectToPage("/Login");
            }

            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.PostAsync($"https://localhost:44340/api/User/{userId}/groups/{groupId}/join", null);

            if (response.IsSuccessStatusCode)
            {
                // Join request sent successfully
                // Có thể thêm thông báo hoặc xử lý khác ở đây
                TempData["Message"] = "Join request sent successfully.";
            }
            else
            {
                // Có lỗi xảy ra khi gửi join request
                // Có thể xử lý lỗi hoặc hiển thị thông báo tùy ý ở đây
                var responseContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = responseContent.ToString();
            }

            return RedirectToPage();
        }
    }
}
