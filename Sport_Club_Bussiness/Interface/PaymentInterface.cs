using Sport_Club_Bussiness.DTOs;

namespace Sport_Club_Business.Interface
{
    public interface PaymentInterface
    {
        Task<List<PaymentDTos>> GetAllAsync();
        Task<PaymentDTos?> GetByIdAsync(int id);
        Task<List<PaymentDTos>> GetAllByMemberIDAsync(int MemberID);
        Task<PaymentDTos> AddAsync(PaymentDTos paymentDto);
        Task<PaymentDTos?> UpdateAsync(int id, PaymentDTos paymentDto);
        Task<bool> DeleteAsync(int id);
    }
}
