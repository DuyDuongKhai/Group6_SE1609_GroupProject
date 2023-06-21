using GroupStudyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GroupStudyClient.Pages
{
    public class CreateAccountModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateAccountModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public RegisterModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = _clientFactory.CreateClient();

            // Send registration request to the API
            var response = await httpClient.PostAsJsonAsync("https://localhost:44340/api/Auth/Register", Input);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

                if (apiResponse.Success)
                {
                    
                    return RedirectToPage("/Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, apiResponse.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi gửi yêu cầu đăng ký.");
            }

            return Page();
        }
    }
}
