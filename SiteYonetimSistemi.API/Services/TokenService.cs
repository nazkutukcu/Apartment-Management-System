using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Services.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiteYonetimSistemi.API.Services
{
    public class TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        public async Task<ResponseDto<TokenCreateResponseDto>> CreateTokenForUser(TokenCreateForUserRequestDto request)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.IdentityNumber == request.IdentityNumber);

            if (user == null)
            {
                return ResponseDto<TokenCreateResponseDto>.Fail("Username or password is wrong");
            }

            if (user.PhoneNumber != request.PhoneNumber)
            {
                return ResponseDto<TokenCreateResponseDto>.Fail("Username or password is wrong");
            }

            var signatureKey = configuration.GetSection("TokenOptions")["SignatureKey"]!;
            var tokenExpireAsHour = configuration.GetSection("TokenOptions")["Expire"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimList = new List<Claim>();

            var userIdAsClaim = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            var userNameAsClaim = new Claim(ClaimTypes.Name, user.UserName!);
            var idAsClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

            claimList.Add(userIdAsClaim);
            claimList.Add(userNameAsClaim);
            claimList.Add(idAsClaim);
            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(Convert.ToDouble(tokenExpireAsHour)),
                signingCredentials: signingCredentials,
                claims: claimList
                );

            var responseDto = new TokenCreateResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return ResponseDto<TokenCreateResponseDto>.Success(responseDto);

        }

        public async Task<ResponseDto<TokenCreateResponseDto>> CreateTokenForAdmin(TokenCreateForAdminRequestDto request)
        {
            var adminUser = await userManager.FindByNameAsync(request.UserName);

            if (adminUser == null)
            {
                return ResponseDto<TokenCreateResponseDto>.Fail("Admin user not found");
            }

            // Admin kullanıcısının parolasını doğrula
            var passwordValid = await userManager.CheckPasswordAsync(adminUser, request.Password);
            if (!passwordValid)
            {
                return ResponseDto<TokenCreateResponseDto>.Fail("Invalid password");
            }

            var signatureKey = configuration.GetSection("TokenOptions")["SignatureKey"]!;
            var tokenExpireAsHour = configuration.GetSection("TokenOptions")["Expire"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimList = new List<Claim>();

            var userIdAsClaim = new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString());
            var userNameAsClaim = new Claim(ClaimTypes.Name, adminUser.UserName!);
            var idAsClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

            claimList.Add(userIdAsClaim);
            claimList.Add(userNameAsClaim);
            claimList.Add(idAsClaim);
            foreach (var role in await userManager.GetRolesAsync(adminUser))
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(Convert.ToDouble(tokenExpireAsHour)),
                signingCredentials: signingCredentials,
                claims: claimList
            );

            var responseDto = new TokenCreateResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return ResponseDto<TokenCreateResponseDto>.Success(responseDto);
        }

    }
}
