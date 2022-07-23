using Courses.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _usermanager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> usermanager)
        {
            _usermanager = usermanager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _usermanager.FindByEmailAsync(context.UserName);
            var passwordCheck = await _usermanager.CheckPasswordAsync(existUser, context.Password);

            if (existUser == null || !passwordCheck)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Bu bilgilerle kullanıcı kaydı bulunamadı" });
                context.Result.CustomResponse = errors;
                return;
            }
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}