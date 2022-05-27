using HRIS.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRIS.API
{
    public class ApiDbContext : IdentityDbContext
    {
        public virtual DbSet<RefreshToken> RefreshTokens {get;set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
            
        }
    }
}