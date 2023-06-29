using GroupStudyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;


namespace GroupStudyClient.Pages
{
    
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public AuthModel Input { get; set; }

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

            // Send login request to the API
            var response = await httpClient.PostAsJsonAsync("https://localhost:44340/api/Auth/Login", Input);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

                if (apiResponse.Success)
                {
                    var token = apiResponse.Data as string;

                    if (!string.IsNullOrEmpty(token))
                    {
                        // Decode the token to get user information
                        var jwtTokenHandler = new JwtSecurityTokenHandler();
                        var decodedToken = jwtTokenHandler.ReadJwtToken(token);
                        var claims = decodedToken.Claims;

                        var emailClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
                        var roleClaim = claims.FirstOrDefault(c => c.Type == "Role");
                        var userIdClaim = claims.FirstOrDefault(c => c.Type == "UserId");

                        if (emailClaim != null && roleClaim != null)
                        {
                            // Save the token and role in Session to use in subsequent requests
                            HttpContext.Session.SetString("Token", token);
                            HttpContext.Session.SetString("Role", roleClaim.Value);
                            HttpContext.Session.SetString("UserId", userIdClaim.Value);

                            if (roleClaim.Value == "User")
                            {
                                return RedirectToPage("/Users/Index");
                            }
                            else if (roleClaim.Value == "Admin")
                            {
                                return RedirectToPage("/Admin/Index");
                            }
                            else if (roleClaim.Value == "GroupAdmin")
                            {
                                return RedirectToPage("/GroupAdmin/Index");
                            }
                            else if (roleClaim.Value == "GroupMember")
                            {
                                return RedirectToPage("/GroupMember/Index");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Không nhận được token từ phản hồi.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, apiResponse.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi gửi yêu cầu đăng nhập.");
            }

            return Page();
        }
    }
}
