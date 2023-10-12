using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PE.HMIWWW.Core.Authorization
{
  public class CustomClaimFactory : UserClaimsPrincipalFactory<ApplicationUser>
  {
    public RoleManager<IdentityRole> RoleManager { get; private set; }

    public CustomClaimFactory(
      UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager,
      IOptions<IdentityOptions> optionsAccessor)
      : base(userManager, optionsAccessor)
    {
      RoleManager = roleManager;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
      var id = await base.GenerateClaimsAsync(user);
      if (UserManager.SupportsUserRole)
      {
        var roles = await UserManager.GetRolesAsync(user);
        foreach (var roleName in roles)
        {
          id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
        }
      }

      id.AddClaim(new Claim("HMIViewOrientation", user.HMIViewOrientation.ToString()));

      return id;
    }
  }
}
