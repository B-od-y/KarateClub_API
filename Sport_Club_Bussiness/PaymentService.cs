using Microsoft.EntityFrameworkCore;
using Sport_Club_Business.Interface;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness.Services
{
    public class PaymentService : PaymentInterface
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentDTos>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Member)
                    .ThenInclude(m => m.Person)
                .Include(p => p.Member.LastBeltRank)
                .Select(p => new PaymentDTos
                {
                    PaymentID = p.PaymentID,
                    Data = p.Data,
                    Amount = p.Amount,
                    Member = new MemberDTos
                    {
                        MemberID = p.Member.MemberID,
                        EmergencyContactInfo = p.Member.EmergencyContactInfo,
                        Date = p.Member.Date,
                        IsActive = p.Member.IsActive,
                        Person = new PersonDTos
                        {
                            PersonID = p.Member.Person.ID,
                            Name = p.Member.Person.Name,
                            ContactInfo = p.Member.Person.ContactInfo,
                            Address = p.Member.Person.Address
                        },
                        LastBeltRank = p.Member.LastBeltRank == null ? null : new BeltRankDTos
                        {
                            BeltRankID = p.Member.LastBeltRank.BeltRankID,
                            BeltName = p.Member.LastBeltRank.BeltName,
                            BeltTestFees = p.Member.LastBeltRank.BeltTestFees
                        }
                    }
                }).ToListAsync();
        }
       public Task<List<PaymentDTos>> GetAllByMemberIDAsync(int MemberID)
        {
            return _context.Payments
                .Include(p => p.Member)
                    .ThenInclude(m => m.Person)
                .Include(p => p.Member.LastBeltRank)
                .Where(p => p.MemberID == MemberID)
                .Select(p => new PaymentDTos
                {
                    PaymentID = p.PaymentID,
                    Data = p.Data,
                    Amount = p.Amount,
                    Member = new MemberDTos
                    {
                        MemberID = p.Member.MemberID,
                        EmergencyContactInfo = p.Member.EmergencyContactInfo,
                        Date = p.Member.Date,
                        IsActive = p.Member.IsActive,
                        Person = new PersonDTos
                        {
                            PersonID = p.Member.Person.ID,
                            Name = p.Member.Person.Name,
                            ContactInfo = p.Member.Person.ContactInfo,
                            Address = p.Member.Person.Address
                        },
                        LastBeltRank = p.Member.LastBeltRank == null ? null : new BeltRankDTos
                        {
                            BeltRankID = p.Member.LastBeltRank.BeltRankID,
                            BeltName = p.Member.LastBeltRank.BeltName,
                            BeltTestFees = p.Member.LastBeltRank.BeltTestFees
                        }
                    }
                }).ToListAsync();

        }
        public async Task<PaymentDTos?> GetByIdAsync(int id)
        {
            var p = await _context.Payments
                .Include(p => p.Member)
                    .ThenInclude(m => m.Person)
                .Include(p => p.Member.LastBeltRank)
                .FirstOrDefaultAsync(p => p.PaymentID == id);

            if (p == null) return null;

            return new PaymentDTos
            {
                PaymentID = p.PaymentID,
                Data = p.Data,
                Amount = p.Amount,
                Member = new MemberDTos
                {
                    MemberID = p.Member.MemberID,
                    EmergencyContactInfo = p.Member.EmergencyContactInfo,
                    Date = p.Member.Date,
                    IsActive = p.Member.IsActive,
                    Person = new PersonDTos
                    {
                        PersonID = p.Member.Person.ID,
                        Name = p.Member.Person.Name,
                        ContactInfo = p.Member.Person.ContactInfo,
                        Address = p.Member.Person.Address
                    },
                    LastBeltRank = p.Member.LastBeltRank == null ? null : new BeltRankDTos
                    {
                        BeltRankID = p.Member.LastBeltRank.BeltRankID,
                        BeltName = p.Member.LastBeltRank.BeltName,
                        BeltTestFees = p.Member.LastBeltRank.BeltTestFees
                    }
                }
            };
        }
        private async Task<MemberDTos?> GetMemberByID(int id)
        {
            return await _context.Members.
                AsNoTracking().
                Include(m => m.Person)
                .Include(m => m.LastBeltRank)
                .Where(i => i.MemberID == id)
                .Select(selector => new MemberDTos
                {
                    MemberID = selector.MemberID,
                    Person = new PersonDTos
                    {
                        PersonID = selector.Person.ID,
                        Name = selector.Person.Name,
                        ContactInfo = selector.Person.ContactInfo,
                        Address = selector.Person.Address
                    },
                    LastBeltRank = new BeltRankDTos
                    {
                        BeltRankID = selector.LastBeltRank.BeltRankID,
                        BeltName = selector.LastBeltRank.BeltName,
                        BeltTestFees = selector.LastBeltRank.BeltTestFees
                    },
                    EmergencyContactInfo = selector.EmergencyContactInfo,
                    Date = selector.Date,
                    IsActive = selector.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<PaymentDTos> AddAsync(PaymentDTos paymentDto)
        {
            var entity = new Payments
            {
                MemberID = paymentDto.Member.MemberID,
                Data = paymentDto.Data,
                Amount = paymentDto.Amount
            };

            await _context.Payments.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new PaymentDTos
            {
                PaymentID = entity.PaymentID,
                Data = entity.Data,
                Amount = entity.Amount,
                Member = await GetMemberByID(entity.MemberID),
            };
        }

        public async Task<PaymentDTos?> UpdateAsync(int id, PaymentDTos paymentDto)
        {
            var entity = await _context.Payments.FindAsync(id);
            if (entity == null) return null;

            entity.MemberID = paymentDto.Member.MemberID;
            entity.Data = paymentDto.Data;
            entity.Amount = paymentDto.Amount;

            await _context.SaveChangesAsync();

            return new PaymentDTos
            {
                PaymentID = entity.PaymentID,
                Data = entity.Data,
                Amount = entity.Amount,
                Member = paymentDto.Member
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Payments.FindAsync(id);
            if (entity == null) return false;

            _context.Payments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
