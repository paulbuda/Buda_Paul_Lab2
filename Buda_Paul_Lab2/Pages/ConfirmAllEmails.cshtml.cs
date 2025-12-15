using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Buda_Paul_Lab2.Pages
{
    public class ConfirmAllEmailsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ConfirmAllEmailsModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string EmailToConfirm { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(EmailToConfirm))
            {
                Message = "Please enter an email address.";
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(EmailToConfirm);
            if (user == null)
            {
                Message = $"User with email '{EmailToConfirm}' not found.";
                return Page();
            }

            if (user.EmailConfirmed)
            {
                Message = $"Email '{EmailToConfirm}' is already confirmed.";
                return Page();
            }

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                Message = $"Email '{EmailToConfirm}' has been confirmed successfully! You can now login.";
            }
            else
            {
                Message = $"Failed to confirm email: {string.Join(", ", result.Errors.Select(e => e.Description))}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostConfirmAllAsync()
        {
            var users = await _userManager.Users.Where(u => !u.EmailConfirmed).ToListAsync();
            
            if (!users.Any())
            {
                Message = "No unconfirmed emails found.";
                return Page();
            }

            int confirmedCount = 0;
            foreach (var user in users)
            {
                user.EmailConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    confirmedCount++;
                }
            }

            Message = $"Confirmed {confirmedCount} email(s) successfully!";
            return Page();
        }
    }
}
