using Microsoft.EntityFrameworkCore;
using Sport_Club_Business.Interface;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness.Services
{
    public class SubscriptionPeriodService : SubscriptionPeriodInterface
    {
        private readonly AppDbContext _context;

        public SubscriptionPeriodService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PeriodDTos>> GetAllAsync()
        {
            return await _context.SubscriptionPeriods
                .Include(p => p.Member).ThenInclude(m => m.Person)
                .Include(p => p.Member.LastBeltRank)
                .Include(p => p.payment)
                .Select(p => new PeriodDTos
                {
                    PeriodID = p.PeriodID,
                    StartDate = p.StartData,
                    EndDate = p.EndData,
                    TestFees = p.TestFees,
                    member = new MemberDTos
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
                    },
                    payment = new PaymentDTos
                    {
                        PaymentID = p.payment.PaymentID,
                        Data = p.payment.Data,
                        Amount = p.payment.Amount
                    }
                }).ToListAsync();
        }

        public async Task<PeriodDTos?> GetByIdAsync(int id)
        {
            var p = await _context.SubscriptionPeriods
                .Include(p => p.Member).ThenInclude(m => m.Person)
                .Include(p => p.Member.LastBeltRank)
                .Include(p => p.payment)
                .FirstOrDefaultAsync(p => p.PeriodID == id);

            if (p == null) return null;

            return new PeriodDTos
            {
                PeriodID = p.PeriodID,
                StartDate = p.StartData,
                EndDate = p.EndData,
                TestFees = p.TestFees,
                member = new MemberDTos
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
                },
                payment = new PaymentDTos
                {
                    PaymentID = p.payment.PaymentID,
                    Data = p.payment.Data,
                    Amount = p.payment.Amount
                }
            };
        }

        public async Task<PeriodDTos> AddAsync(PeriodDTos dto)
        {
            var entity = new SubscriptionPeriods
            {
                MemberID = dto.member.MemberID,
                PaymentID = dto.payment.PaymentID,
                StartData = dto.StartDate,
                EndData = dto.EndDate,
                TestFees = dto.TestFees
            };

            await _context.SubscriptionPeriods.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.PeriodID) ?? throw new Exception("Creation failed.");
        }

        public async Task<PeriodDTos?> UpdateAsync(int id, PeriodDTos dto)
        {
            var entity = await _context.SubscriptionPeriods.FindAsync(id);
            if (entity == null) return null;

            entity.StartData = dto.StartDate;
            entity.EndData = dto.EndDate;
            entity.TestFees = dto.TestFees;
            entity.MemberID = dto.member.MemberID;
            entity.PaymentID = dto.payment.PaymentID;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SubscriptionPeriods.FindAsync(id);
            if (entity == null) return false;

            _context.SubscriptionPeriods.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
