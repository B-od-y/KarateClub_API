using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_Club_Bussiness;
using Sport_Club_Bussiness.DTOs; 

namespace Sport_Club_Business.Interface
{
    public interface MemberInterface
    {
       public Task<List<MemberDTos>> GetAllAsync();
       public Task<List<MemberDTos?>> GetAllMemberwithBeltRankAsync(int BeltRankID);
       public Task<MemberDTos?> GetByIdAsync(int id);
       public Task<MemberDTos> AddAsync(MemberDTos memberDto);
      public Task<MemberDTos> UpdateAsync(int id, MemberDTos memberDto);
      public Task<bool> DeleteAsync(int id);

    }
}