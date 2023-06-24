using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Sub_Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupStudyClient.Pages
{
    public class GroupAdminIndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public GroupAdminIndexModel( IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public List<Group> Groups { get; set; }
        public List<JoinRequestModel> JoinRequests { get; set; }
        [BindProperty(SupportsGet = true)]
        public int GroupId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }
        public List<UserModel> UserModels { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string GetGroupUrl = "https://localhost:44340/api/GroupAdmin";
            string getAllRequestUrl = "https://localhost:44340/api/GroupAdmin/ViewJoinRequest";

            var token = HttpContext.Session.GetString("Token");

            // Add the token to the request headers
            var httpClient = _clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(GetGroupUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Groups = JsonConvert.DeserializeObject<List<Group>>(content);
            }
            else
            {
                Groups = new List<Group>();
            }
            var getAllRequestResponse = await httpClient.GetAsync(getAllRequestUrl);
            if (getAllRequestResponse.IsSuccessStatusCode)
            {
                var joinRequestContent = await getAllRequestResponse.Content.ReadAsStringAsync();
                JoinRequests = JsonConvert.DeserializeObject<List<JoinRequestModel>>(joinRequestContent);
            }
            else
            {
                JoinRequests = new List<JoinRequestModel>();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostApproveAsync(int UserId, int GroupId)
        {
            var apiUrl = $"https://localhost:44340/api/GroupAdmin/ApproveRequest/{UserId}/{GroupId}";

           
                var httpClient = _clientFactory.CreateClient();
                var response = await httpClient.PutAsync(apiUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = msg;
                }
                else
                {
                    // Handle unsuccessful response
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Log or handle the error accordingly
                    TempData["ErrorMessage"] = responseContent.ToString();
                }
                    return RedirectToPage();

        }
        public async Task<IActionResult> OnPostDisapproveAsync(int UserId, int GroupId)
        {
            var apiUrl = $"https://localhost:44340/api/GroupAdmin/DisapproveRequest/{UserId}/{GroupId}";


            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                TempData["Message"] = msg;
            }
            else
            {
                // Handle unsuccessful response
                var responseContent = await response.Content.ReadAsStringAsync();
                // Log or handle the error accordingly
                TempData["ErrorMessage"] = responseContent.ToString();
            }
            return RedirectToPage();

        }
     


    }
}

