using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_Club_Bussiness;
using Sport_Club_Bussiness.DTOs; 

namespace Sport_Club_Business.Interface
{
    public interface BeltRankInterface
    {
       public Task<List<BeltRankDTos>> GetAllAsync();
       public Task<BeltRankDTos?> GetByIdAsync(int id);
       public Task<BeltRankDTos> AddAsync(BeltRankDTos beltrankDto);
       public Task<BeltRankDTos> UpdateAsync(int id, BeltRankDTos beltrankDto);
       public Task<bool> DeleteAsync(int id);
    }
}