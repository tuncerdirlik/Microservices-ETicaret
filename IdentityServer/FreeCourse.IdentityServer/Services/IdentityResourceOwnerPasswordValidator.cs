using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var exitUser = await _userManager.FindByEmailAsync(context.UserName);
            if(exitUser == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("error", new List<string> { "Email/Şifre hatalı" });

                context.Result.CustomResponse = errors;

                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(exitUser, context.Password);
            if (!passwordCheck)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("error", new List<string> { "Email/Şifre hatalı" });

                context.Result.CustomResponse = errors;

                return;
            }

            context.Result = new GrantValidationResult(exitUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

        }
    }
}
