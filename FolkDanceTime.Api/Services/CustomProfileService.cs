using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using System.Security.Claims;

namespace MyNamespace.Services
{
    public class CustomProfileService : IProfileService
    {
        public CustomProfileService(){}

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var roleClaims = context.Subject.FindAll(ClaimTypes.Role);
            context.IssuedClaims.AddRange(roleClaims);

            context.IssuedClaims.AddRange(context.Subject.Claims);
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}