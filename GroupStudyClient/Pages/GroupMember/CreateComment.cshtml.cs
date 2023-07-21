using System.Net.Http;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Net.Http.Json;
using DataAccess;

namespace GroupStudyClient.Pages.GroupMember
{
    public class CreateCommentModel : PageModel
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentModel(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [BindProperty]
        public CommentDto CommentDto { get; set; }

        public async Task<IActionResult> OnPostAsync(int postId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiUrl = $"https://localhost:44340/api/GroupMember/{postId}/comments";
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(apiUrl, CommentDto);

            if (response.IsSuccessStatusCode)
            {
                var createdComment = await response.Content.ReadFromJsonAsync<Comment>();
                // Handle the created comment if needed
                return RedirectToPage("GetCommentById", new { id = createdComment.PostId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error creating comment.");
                return Page();
            }
        }
    }
}
