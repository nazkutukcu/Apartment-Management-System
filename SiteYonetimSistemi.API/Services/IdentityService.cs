using Microsoft.AspNetCore.Identity;
using SiteYonetimSistemi.API.Models;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Services.DTOs;

namespace SiteYonetimSistemi.API.Services
{
    public class IdentityService(UserManager<User> userManager, RoleManager<AppRole>
            roleManager)
    {
        public UserManager<User> UserManager { get; set; } = userManager;
        public RoleManager<AppRole> RoleManager { get; set; } = roleManager;

        public async Task<ResponseDto<string>> CreateRole(RoleCreateRequestDto request)
        {
            var appRole = new AppRole
            {
                Name = request.RoleName
            };
           
            var hasRole = await roleManager.RoleExistsAsync(appRole.Name);

            IdentityResult? roleCreateResult = null;
            if (!hasRole)
            {
                roleCreateResult = await roleManager.CreateAsync(appRole);

            }

            if (roleCreateResult is not null && roleCreateResult.Succeeded)
            {
                var errorList = roleCreateResult.Errors.Select(x => x.Description).ToList();

                return ResponseDto<string>.Fail(errorList);
            }

            var hasUser = await userManager.FindByIdAsync(request.UserId);

            if(hasUser is null)
            {

                return ResponseDto<string>.Fail("kullanıcı bulunamadı.");
            }

            var roleAssignResult = await userManager.AddToRoleAsync(hasUser, appRole.Name);

            if (!roleAssignResult.Succeeded)
            {

                var errorList = roleAssignResult.Errors.Select(x => x.Description).ToList();

                return ResponseDto<string>.Fail(errorList);
            }

            return ResponseDto<string>.Success(string.Empty);
        }

    }
}
