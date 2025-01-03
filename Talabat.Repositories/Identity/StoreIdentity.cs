using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;

namespace Talabat.Repositories.Identity
{
    public class StoreIdentity : IdentityDbContext<UserApplication>
    {

        public StoreIdentity(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
