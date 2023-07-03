using Courses.Shared.Dtos;
using Courses.Web.Models;
using IdentityModel.Client;

namespace Courses.Web.Servies.Interfaces
{
    interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
