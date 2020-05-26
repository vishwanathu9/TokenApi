using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace TokenApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //AspNetUsers->User
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User")                ;

            //AspNetRoles->Role
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");

            //AspNetUserRole->UserRole            
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");

            //AspNetUserClaim->UserClaim
            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");

            //AspNetUserLogin->UserLogin
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("Userlogin");

        }
    }
}