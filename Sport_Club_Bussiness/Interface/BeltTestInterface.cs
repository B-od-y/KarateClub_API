using Sport_Club_Bussiness.DTOs;

namespace Sport_Club_Business.Interface
{
    public interface BeltTestInterface
    {
        Task<List<BeltTestDTos>> GetAllAsync();
        Task<BeltTestDTos?> GetByIdAsync(int id);
        Task<BeltTestDTos> AddAsync(BeltTestDTos dto);
        Task<BeltTestDTos?> UpdateAsync(int id, BeltTestDTos dto);
        Task<bool> DeleteAsync(int id);
    }
}
