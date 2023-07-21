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
            
            var userIdValue = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out int userId))
            {
                
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
                
                return Page();
            }
        }

        
        public async Task<IActionResult> OnPostUpdateStatusAsync(int taskId, string newStatus)
        {
           
            var userIdValue = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdValue) || !int.TryParse(userIdValue, out int userId))
            {
                
                return RedirectToPage("/Account/Login");
            }

            try
            {
                
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44340/api/GroupMember/UpdateTaskStatus/{taskId}", newStatus);

                if (response.IsSuccessStatusCode)
                {
                    
                    return RedirectToPage("/GroupMember/GetTask");
                }
                else
                {
                    
                    ModelState.AddModelError(string.Empty, "Cập nhật trạng thái thất bại. Vui lòng thử lại sau.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                return Page();
            }
        }
    }
}
