using Microsoft.AspNetCore.Http;

namespace Courses.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //1.adım
        //public string GetUserId => _httpContextAccessor.HttpContext.User.Claims.Where(x=>x.Type == "sub").FirstOrDefault().Value;

        //best practies
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}