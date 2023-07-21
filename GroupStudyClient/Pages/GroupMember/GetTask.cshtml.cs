using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
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
            // Lấy User ID từ Session và kiểm tra giá trị
            var userIdValue = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out int userId))
            {
                // Xử lý khi giá trị User ID không hợp lệ hoặc không tồn tại trong Session
                // Ví dụ: Chuyển hướng đến trang đăng nhập
                return RedirectToPage("/Account/Login");
            }

            var response = await _httpClient.GetAsync($"https://localhost:44340/api/GroupMember/GetTaskByUserId/{int.Parse(userIdValue)}");

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

        // Xử lý POST khi người dùng nhấn nút "Update" trên trang
        public async Task<IActionResult> OnPostUpdateStatusAsync(int taskId, string newStatus)
        {
            // Lấy User ID từ Session và kiểm tra giá trị
            var userIdValue = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out int userId))
            {
                // Xử lý khi giá trị User ID không hợp lệ hoặc không tồn tại trong Session
                // Ví dụ: Chuyển hướng đến trang đăng nhập
                return RedirectToPage("/Account/Login");
            }

            try
            {
                // Gửi yêu cầu cập nhật trạng thái của task đến API
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44340/api/GroupMember/UpdateTaskStatus/{taskId}", newStatus);

                if (response.IsSuccessStatusCode)
                {
                    // Cập nhật trạng thái thành công, tải lại trang để hiển thị danh sách task mới
                    return RedirectToPage("/GroupMember/GetTask");
                }
                else
                {
                    // Xử lý lỗi khi gửi yêu cầu cập nhật trạng thái
                    ModelState.AddModelError(string.Empty, "Cập nhật trạng thái thất bại. Vui lòng thử lại sau.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi gửi yêu cầu cập nhật trạng thái
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                return Page();
            }
        }
    }
}
