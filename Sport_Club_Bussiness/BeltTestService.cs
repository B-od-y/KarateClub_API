using Microsoft.EntityFrameworkCore;
using Sport_Club_Business.Interface;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness.Services
{
    public class BeltTestService : BeltTestInterface
    {
        private readonly AppDbContext _context;

        public BeltTestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BeltTestDTos>> GetAllAsync()
        {
            return await _context.BeltTests
                .Include(b => b.Member).ThenInclude(m => m.Person)
                .Include(b => b.Member.LastBeltRank)
                .Include(b => b.BeltRank)
                .Include(b => b.Instructor).ThenInclude(i => i.Person)
                .Include(b => b.Payment)
                .Select(b => new BeltTestDTos
                {
                    TestID = b.TestID,
                    Result = b.Result,
                    Member = new MemberDTos
                    {
                        MemberID = b.Member.MemberID,
                        EmergencyContactInfo = b.Member.EmergencyContactInfo,
                        Date = b.Member.Date,
                        IsActive = b.Member.IsActive,
                        Person = new PersonDTos
                        {
                            PersonID = b.Member.Person.ID,
                            Name = b.Member.Person.Name,
                            ContactInfo = b.Member.Person.ContactInfo,
                            Address = b.Member.Person.Address
                        },
                        LastBeltRank = b.Member.LastBeltRank == null ? null : new BeltRankDTos
                        {
                            BeltRankID = b.Member.LastBeltRank.BeltRankID,
                            BeltName = b.Member.LastBeltRank.BeltName,
                            BeltTestFees = b.Member.LastBeltRank.BeltTestFees
                        }
                    },
                    BeltRank = new BeltRankDTos
                    {
                        BeltRankID = b.BeltRank.BeltRankID,
                        BeltName = b.BeltRank.BeltName,
                        BeltTestFees = b.BeltRank.BeltTestFees
                    },
                    TestedByInstructor = new instructorDTos
                    {
                        InstructorID = b.Instructor.ID,
                        qualification = b.Instructor.Qualification,
                        person = new PersonDTos
                        {
                            PersonID = b.Instructor.Person.ID,
                            Name = b.Instructor.Person.Name,
                            ContactInfo = b.Instructor.Person.ContactInfo,
                            Address = b.Instructor.Person.Address
                        }
                    },
                    Payment = new PaymentDTos
                    {
                        PaymentID = b.Payment.PaymentID,
                        Data = b.Payment.Data,
                        Amount = b.Payment.Amount
                    }
                }).ToListAsync();
        }

        public async Task<BeltTestDTos?> GetByIdAsync(int id)
        {
            var b = await _context.BeltTests
                .Include(b => b.Member).ThenInclude(m => m.Person)
                .Include(b => b.Member.LastBeltRank)
                .Include(b => b.BeltRank)
                .Include(b => b.Instructor).ThenInclude(i => i.Person)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.TestID == id);

            if (b == null) return null;

            return new BeltTestDTos
            {
                TestID = b.TestID,
                Result = b.Result,
                Member = new MemberDTos
                {
                    MemberID = b.Member.MemberID,
                    EmergencyContactInfo = b.Member.EmergencyContactInfo,
                    Date = b.Member.Date,
                    IsActive = b.Member.IsActive,
                    Person = new PersonDTos
                    {
                        PersonID = b.Member.Person.ID,
                        Name = b.Member.Person.Name,
                        ContactInfo = b.Member.Person.ContactInfo,
                        Address = b.Member.Person.Address
                    },
                    LastBeltRank = b.Member.LastBeltRank == null ? null : new BeltRankDTos
                    {
                        BeltRankID = b.Member.LastBeltRank.BeltRankID,
                        BeltName = b.Member.LastBeltRank.BeltName,
                        BeltTestFees = b.Member.LastBeltRank.BeltTestFees
                    }
                },
                BeltRank = new BeltRankDTos
                {
                    BeltRankID = b.BeltRank.BeltRankID,
                    BeltName = b.BeltRank.BeltName,
                    BeltTestFees = b.BeltRank.BeltTestFees
                },
                TestedByInstructor = new instructorDTos
                {
                    InstructorID = b.Instructor.ID,
                    qualification = b.Instructor.Qualification,
                    person = new PersonDTos
                    {
                        PersonID = b.Instructor.Person.ID,
                        Name = b.Instructor.Person.Name,
                        ContactInfo = b.Instructor.Person.ContactInfo,
                        Address = b.Instructor.Person.Address
                    }
                },
                Payment = new PaymentDTos
                {
                    PaymentID = b.Payment.PaymentID,
                    Data = b.Payment.Data,
                    Amount = b.Payment.Amount
                }
            };
        }

        public async Task<BeltTestDTos> AddAsync(BeltTestDTos dto)
        {
            var entity = new BeltTests
            {
                MemberID = dto.Member.MemberID,
                RankID = dto.BeltRank.BeltRankID,
                Result = dto.Result,
                TestedByInstructorID = dto.TestedByInstructor.InstructorID,
                PaymentID = dto.Payment.PaymentID
            };

            await _context.BeltTests.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.TestID) ?? throw new Exception("Failed to retrieve created BeltTest.");
        }

        public async Task<BeltTestDTos?> UpdateAsync(int id, BeltTestDTos dto)
        {
            var entity = await _context.BeltTests.FindAsync(id);
            if (entity == null) return null;

            entity.MemberID = dto.Member.MemberID;
            entity.RankID = dto.BeltRank.BeltRankID;
            entity.Result = dto.Result;
            entity.TestedByInstructorID = dto.TestedByInstructor.InstructorID;
            entity.PaymentID = dto.Payment.PaymentID;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BeltTests.FindAsync(id);
            if (entity == null) return false;

            _context.BeltTests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
