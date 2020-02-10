using YoutubeSearch.Web.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace YoutubeSearch.Web.Data
{
    public class IdentityDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public IdentityDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
