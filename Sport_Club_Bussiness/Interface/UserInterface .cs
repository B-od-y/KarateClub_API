using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_Club_Bussiness;
using Sport_Club_Bussiness.DTOs; 

namespace Sport_Club_Business.Interface
{
    public interface UserInterface
    {
        public Task<UserDTos> GetID(int id);
       public Task<UserDTos> CreateAsync(UserDTos userDto);
       public Task<UserDTos> UpdateAsync(int id, UserDTos userDto);
       public Task<bool> DeleteAsync(int id);
    }
}