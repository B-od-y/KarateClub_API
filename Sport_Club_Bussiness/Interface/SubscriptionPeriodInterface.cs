using Sport_Club_Bussiness.DTOs;

namespace Sport_Club_Business.Interface
{
    public interface SubscriptionPeriodInterface
    {
        Task<List<PeriodDTos>> GetAllAsync();
        Task<PeriodDTos?> GetByIdAsync(int id);
        Task<PeriodDTos> AddAsync(PeriodDTos dto);
        Task<PeriodDTos?> UpdateAsync(int id, PeriodDTos dto);
        Task<bool> DeleteAsync(int id);
    }
}
