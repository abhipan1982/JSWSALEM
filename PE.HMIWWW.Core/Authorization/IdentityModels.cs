using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PE.HMIWWW.Core.Authorization
{
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : IdentityUser
  {
    [ForeignKey("LanguageId")] public virtual SMFLanguage Language { get; set; }

    public short HMIViewOrientation { get; set; }
    public long LanguageId { get; set; }
  }

  public class SMFLanguage
  {
    [Key] public long LanguageId { get; set; } // LanguageId (Primary key) 

    public string LanguageCode { get; set; } // LanguageCode (length: 10) 
  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.HasDefaultSchema("smf");
      builder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Id");
      builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
      builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
      builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
      builder.Entity<IdentityRole>().ToTable("Roles");
      builder.Entity<SMFLanguage>().ToTable("Languages").Property(p => p.LanguageId).HasColumnName("LanguageId");
    }
  }
}
