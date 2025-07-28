using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sport_Club_Business.Interface;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;
using Sport_Club_Data.Helper;
namespace Sport_Club_Bussiness
{
    public class UserService : UserInterface
    {
        private readonly AppDbContext _appDbContext;
        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<UserDTos> GetID(int id)
        {
            var userAccount = _appDbContext.UserAccounts.FirstOrDefault(u => u.Id == id);

            if (userAccount == null)
                return null;

            var names = userAccount.FullName?.Split(' ');
            var firstName = names != null && names.Length > 0 ? names[0] : "";
            var lastName = names != null && names.Length > 1 ? string.Join(" ", names.Skip(1)) : "";

            var userDto = new UserDTos
            {
                UserID = userAccount.Id,
                Username = userAccount.Username,
                FirstName = firstName,
                LastName = lastName,
                Email = userAccount.Email
            };

            return Task.FromResult(userDto);
        }
        public Task<UserDTos> CreateAsync(UserDTos userDto)
        {
            
           var userAccount = new UserAccounts
           {
               Username = userDto.Username,
               FullName = $"{userDto.FirstName} {userDto.LastName}",
               Password = PasswordHash.HashPassword(userDto.Password),
               Email = userDto.Email
               
           };
            // Assuming _appDbContext is your database context
            _appDbContext.UserAccounts.Add(userAccount);

            _appDbContext.SaveChangesAsync();

            var  user = new UserDTos
            {
                UserID = userAccount.Id,
                Username = userAccount.Username,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userAccount.Email
            };

            return Task.FromResult(user);

        }
        public Task<UserDTos> UpdateAsync(int id, UserDTos userDto)
        {
            var userAccount = _appDbContext.UserAccounts.FirstOrDefault(u => u.Id == id);

            if (userAccount == null)
                return null;
            userAccount.Username = userDto.Username;
            userAccount.FullName = $"{userDto.FirstName} {userDto.LastName}";
            userAccount.Email = userDto.Email;
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                userAccount.Password = PasswordHash.HashPassword(userDto.Password);
            }

            // Assuming _appDbContext is your database context
            _appDbContext.UserAccounts.Add(userAccount);

            _appDbContext.SaveChangesAsync();

            var user = new UserDTos
            {
                UserID = userAccount.Id,
                Username = userAccount.Username,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userAccount.Email
            };

            return Task.FromResult(user);
        }
        public Task<bool> DeleteAsync(int id)
        {
            var userAccount = _appDbContext.UserAccounts.FirstOrDefault(u => u.Id == id);

            if (userAccount == null)
                return null;
            _appDbContext.UserAccounts.Remove(userAccount);
            _appDbContext.SaveChangesAsync();
            return Task.FromResult(true);
        }

    }
}
