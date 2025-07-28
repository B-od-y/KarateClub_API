using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Helper;
using Microsoft.EntityFrameworkCore;
using Sport_Club_Data.Entitys;
namespace Sport_Club_Bussiness.Services
{
    public class JwtService
    {

        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public JwtService(AppDbContext appDbContext, IConfiguration configuration)
        {

            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        //TOKEN
        public async Task<LoginResponseModel> Authenticate(LoginRequestModel Request)
        {
            if (string.IsNullOrEmpty(Request.Username) || string.IsNullOrEmpty(Request.Password))
                return null;
            var userAccount = await _appDbContext.UserAccounts.FirstOrDefaultAsync(x => x.Username == Request.Username);
            if (userAccount == null || !PasswordHash.VerifyPassword(userAccount.Password,Request.Password))
                return null;

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var TokenValidityMins = int.Parse(_configuration["Jwt:TokenValidityMins"]);
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(TokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, userAccount.Username),
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accesstoken = tokenHandler.WriteToken(securityToken);
            return new LoginResponseModel
            {
                Username = userAccount.Username,
                AccessToken = accesstoken,
                ExpiresIn = (int)(tokenExpiryTimeStamp - DateTime.UtcNow).TotalSeconds,
                //REFRESH TOKEN Generation
                RefreshToken = await GenerateRefreshToken(userAccount.Id)
            };
        }

        //REFRESH TOKEN 
        public async Task<LoginResponseModel?> ValidateRefreshToken(string token)
        {
            var refreshToken = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
            if (refreshToken is null || refreshToken.Expiration < DateTime.UtcNow)
                return null;

            _appDbContext.RefreshTokens.Remove(refreshToken);
            await _appDbContext.SaveChangesAsync();

            var user = await _appDbContext.UserAccounts.FirstOrDefaultAsync(u => u.Id == refreshToken.UserID);
            if (user is null)
                return null;

            return await GenerateJwtToken(user);
        }
        public async Task<LoginResponseModel> GenerateJwtToken(UserAccounts user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var tokenValidityMins = _configuration.GetValue<int>("JWT:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            // إنشاء التوكن
            var token = new JwtSecurityToken(
                issuer,
                audience,
                new[]
                {
            new Claim(JwtRegisteredClaimNames.Name, user.Username!)
                    // تقدر تضيف Claims زيادة هنا زي ID أو Roles لو حابب
                },
                expires: tokenExpiryTimeStamp,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // إنشاء الـ Refresh Token
            var refreshToken = await GenerateRefreshToken(user.Id);

            // إرجاع النتيجة النهائية
            return new LoginResponseModel
            {
                Username = user.Username!,
                AccessToken = accessToken,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                RefreshToken = refreshToken
            };
        }
        private async Task<string> GenerateRefreshToken(int userId)
        {
            // قراءة مدة صلاحية الـ Refresh Token من appsettings.json
            var refreshTokenValidityMins = _configuration.GetValue<int>("JWT:RefreshTokenValidityMins");

            // إنشاء كائن RefreshToken جديد
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins),
                UserID = userId
            };

            // حفظ التوكن في قاعدة البيانات
            await _appDbContext.RefreshTokens.AddAsync(refreshToken);
            await _appDbContext.SaveChangesAsync();

            // إرجاع قيمة التوكن
            return refreshToken.Token;
        }
    }
}
