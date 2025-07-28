using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Bussiness.Interface;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness
{
    public class MemberInstructorService : MemberInstructorInterface
    {
        private readonly AppDbContext _context;

        public MemberInstructorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MemberInstructorDTos>> GetAllAssigment()
        {
            return await _context.MemberInstructors
              .Include(m => m.Member).ThenInclude(p => p.Person)
              .Include(m => m.Member.LastBeltRank)
              .Include(i => i.Instructor).ThenInclude(p => p.Person)
              .Select(mi => new MemberInstructorDTos
              {
                  AssignDate = mi.AssignDate,
                  member = new MemberDTos
                  {
                      MemberID = mi.Member.MemberID,
                      EmergencyContactInfo = mi.Member.EmergencyContactInfo,
                      Date = mi.Member.Date,
                      IsActive = mi.Member.IsActive,
                      Person = new PersonDTos
                      {
                          PersonID = mi.Member.Person.ID,
                          Name = mi.Member.Person.Name,
                          ContactInfo = mi.Member.Person.ContactInfo,
                          Address = mi.Member.Person.Address
                      },
                      LastBeltRank = mi.Member.LastBeltRank == null ? null : new BeltRankDTos
                      {
                          BeltRankID = mi.Member.LastBeltRank.BeltRankID,
                          BeltName = mi.Member.LastBeltRank.BeltName,
                          BeltTestFees = mi.Member.LastBeltRank.BeltTestFees
                      }
                  },
                  instructor = new instructorDTos
                  {
                      InstructorID = mi.Instructor.ID,
                      qualification = mi.Instructor.Qualification,
                      person = new PersonDTos
                      {
                          PersonID = mi.Instructor.Person.ID,
                          Name = mi.Instructor.Person.Name,
                          ContactInfo = mi.Instructor.Person.ContactInfo,
                          Address = mi.Instructor.Person.Address
                      }
                  }
              }).ToListAsync();
        }
        public async Task<List<MemberInstructorDTos>>? GetAllAssigmentByMemberID(int MemberID)
        {
            return await _context.MemberInstructors
           .Include(m => m.Member).ThenInclude(p => p.Person)
           .Include(m => m.Member.LastBeltRank)
           .Include(i => i.Instructor).ThenInclude(p => p.Person)
           .Where(m => m.MemberID == MemberID)
           .Select(mi => new MemberInstructorDTos
           {
               AssignDate = mi.AssignDate,
               member = new MemberDTos
               {
                   MemberID = mi.Member.MemberID,
                   EmergencyContactInfo = mi.Member.EmergencyContactInfo,
                   Date = mi.Member.Date,
                   IsActive = mi.Member.IsActive,
                   Person = new PersonDTos
                   {
                       PersonID = mi.Member.Person.ID,
                       Name = mi.Member.Person.Name,
                       ContactInfo = mi.Member.Person.ContactInfo,
                       Address = mi.Member.Person.Address
                   },
                   LastBeltRank = mi.Member.LastBeltRank == null ? null : new BeltRankDTos
                   {
                       BeltRankID = mi.Member.LastBeltRank.BeltRankID,
                       BeltName = mi.Member.LastBeltRank.BeltName,
                       BeltTestFees = mi.Member.LastBeltRank.BeltTestFees
                   }
               },
               instructor = new instructorDTos
               {
                   InstructorID = mi.Instructor.ID,
                   qualification = mi.Instructor.Qualification,
                   person = new PersonDTos
                   {
                       PersonID = mi.Instructor.Person.ID,
                       Name = mi.Instructor.Person.Name,
                       ContactInfo = mi.Instructor.Person.ContactInfo,
                       Address = mi.Instructor.Person.Address
                   }
               }
           }).ToListAsync();

        }
        public async Task<List<MemberInstructorDTos>>? GetAllAssigmentByInstructorID(int InstructorID)
        {
            return await _context.MemberInstructors
           .Include(m => m.Member).ThenInclude(p => p.Person)
           .Include(m => m.Member.LastBeltRank)
           .Include(i => i.Instructor).ThenInclude(p => p.Person)
           .Where(i => i.InstructorID == InstructorID)
           .Select(mi => new MemberInstructorDTos
           {
               AssignDate = mi.AssignDate,
               member = new MemberDTos
               {
                   MemberID = mi.Member.MemberID,
                   EmergencyContactInfo = mi.Member.EmergencyContactInfo,
                   Date = mi.Member.Date,
                   IsActive = mi.Member.IsActive,
                   Person = new PersonDTos
                   {
                       PersonID = mi.Member.Person.ID,
                       Name = mi.Member.Person.Name,
                       ContactInfo = mi.Member.Person.ContactInfo,
                       Address = mi.Member.Person.Address
                   },
                   LastBeltRank = mi.Member.LastBeltRank == null ? null : new BeltRankDTos
                   {
                       BeltRankID = mi.Member.LastBeltRank.BeltRankID,
                       BeltName = mi.Member.LastBeltRank.BeltName,
                       BeltTestFees = mi.Member.LastBeltRank.BeltTestFees
                   }
               },
               instructor = new instructorDTos
               {
                   InstructorID = mi.Instructor.ID,
                   qualification = mi.Instructor.Qualification,
                   person = new PersonDTos
                   {
                       PersonID = mi.Instructor.Person.ID,
                       Name = mi.Instructor.Person.Name,
                       ContactInfo = mi.Instructor.Person.ContactInfo,
                       Address = mi.Instructor.Person.Address
                   }
               }
           }).ToListAsync();
        }
        public async Task<MemberInstructorDTos?> GetAssignByIdAsync(int memberId, int instructorId)
        {
            var entity = await _context.MemberInstructors
                .Include(m => m.Member).ThenInclude(p => p.Person)
                .Include(m => m.Member.LastBeltRank)
                .Include(i => i.Instructor).ThenInclude(p => p.Person)
                .FirstOrDefaultAsync(x => x.MemberID == memberId && x.InstructorID == instructorId);

            if (entity == null) return null;

            return new MemberInstructorDTos
            {
                AssignDate = entity.AssignDate,
                member = new MemberDTos
                {
                    MemberID = entity.Member.MemberID,
                    EmergencyContactInfo = entity.Member.EmergencyContactInfo,
                    Date = entity.Member.Date,
                    IsActive = entity.Member.IsActive,
                    Person = new PersonDTos
                    {
                        PersonID = entity.Member.Person.ID,
                        Name = entity.Member.Person.Name,
                        ContactInfo = entity.Member.Person.ContactInfo,
                        Address = entity.Member.Person.Address
                    },
                    LastBeltRank = entity.Member.LastBeltRank == null ? null : new BeltRankDTos
                    {
                        BeltRankID = entity.Member.LastBeltRank.BeltRankID,
                        BeltName = entity.Member.LastBeltRank.BeltName,
                        BeltTestFees = entity.Member.LastBeltRank.BeltTestFees
                    }
                },
                instructor = new instructorDTos
                {
                    InstructorID = entity.Instructor.ID,
                    qualification = entity.Instructor.Qualification,
                    person = new PersonDTos
                    {
                        PersonID = entity.Instructor.Person.ID,
                        Name = entity.Instructor.Person.Name,
                        ContactInfo = entity.Instructor.Person.ContactInfo,
                        Address = entity.Instructor.Person.Address
                    }
                }
            };
        }
        public async Task<MemberInstructorDTos> AddAsssignAsync(MemberInstructorDTos dto)
        {
            // التحقق من وجود العضو
            var member = await _context.Members.FindAsync(dto.member.MemberID);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {dto.member.MemberID} was not found.");

            // التحقق من وجود المدرب
            var instructor = await _context.Instructors.FindAsync(dto.instructor.InstructorID);
            if (instructor == null)
                throw new KeyNotFoundException($"Instructor with ID {dto.instructor.InstructorID} was not found.");

            // إنشاء الكيان
            var entity = new MemberInstructors
            {
                MemberID = dto.member.MemberID,
                InstructorID = dto.instructor.InstructorID,
                AssignDate = dto.AssignDate
            };

            try
            {
                await _context.MemberInstructors.AddAsync(entity);
                await _context.SaveChangesAsync();

                // إعادة العنصر المضاف
                return await GetAssignByIdAsync(entity.MemberID, entity.InstructorID)
                       ?? throw new Exception("Assignment was created but failed to retrieve it.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while assigning instructor to member: " + ex.Message);
            }
        }

        public async Task<bool> DeleteAssignAsync(int memberId, int instructorId)
        {
            var entity = await _context.MemberInstructors
                .FirstOrDefaultAsync(x => x.MemberID == memberId && x.InstructorID == instructorId);
            if (entity == null) return false;

            _context.MemberInstructors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

     
    }
}
