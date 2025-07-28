using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Club_Business.Interface;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Bussiness
{
    public class MemberService : MemberInterface
    {
        private readonly AppDbContext _context;
        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MemberDTos>> GetAllAsync()
        {
            return await _context.Members.AsNoTracking().Include(m => m.Person)
                .Include(m => m.LastBeltRank)
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
                }).ToListAsync();
        }

        public  Task<List<MemberDTos>> GetAllMemberwithBeltRankAsync(int BeltRankID)
        {
            return _context.Members.AsNoTracking().Include(m => m.Person)
                .Include(m => m.LastBeltRank)
                .Where(i => i.LastBeltRank.BeltRankID == BeltRankID)
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
                }).ToListAsync();
        }

        public async Task<MemberDTos?> GetByIdAsync(int id)
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
        public async Task<MemberDTos> AddAsync(MemberDTos memberDto)
        {
            var existingPerson = await _context.Persons.FindAsync(memberDto.Person.PersonID);
            var BeltRankDTO = await _context.BeltRankss.FindAsync(memberDto.LastBeltRank.BeltRankID);
            var BeltRankDto = new BeltRankDTos
            {
                BeltRankID = BeltRankDTO.BeltRankID,
                BeltName = BeltRankDTO.BeltName,
                BeltTestFees = BeltRankDTO.BeltTestFees
            };

            if (existingPerson != null)
            {
                var newMember = new Members
                {
                    Person = existingPerson,
                    EmergencyContactInfo = memberDto.EmergencyContactInfo,
                    LastBeltRank = BeltRankDTO,
                    LastBeltRankID = BeltRankDTO.BeltRankID,
                    Date = memberDto.Date,
                    IsActive = memberDto.IsActive
                };
                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
                return new MemberDTos
                {
                    MemberID = newMember.MemberID,
                    Person = memberDto.Person,
                    EmergencyContactInfo = memberDto.EmergencyContactInfo,
                    LastBeltRank = memberDto.LastBeltRank,
                    Date = memberDto.Date,
                    IsActive = memberDto.IsActive
                };
            }
            else
            {
                var newperson = new Persons
                {
                    Name = memberDto.Person.Name,
                    ContactInfo = memberDto.Person.ContactInfo,
                    Address = memberDto.Person.Address
                };
                await _context.Persons.AddAsync(newperson);
                await _context.SaveChangesAsync();
                var personDtos = new PersonDTos
                {
                    PersonID = newperson.ID,
                    Name = newperson.Name,
                    ContactInfo = newperson.ContactInfo,
                    Address = newperson.Address
                };

                var newMember = new Members
                {
                    Person = newperson,
                    EmergencyContactInfo = memberDto.EmergencyContactInfo,
                    LastBeltRank = BeltRankDTO,
                    LastBeltRankID = BeltRankDTO.BeltRankID,
                    Date = memberDto.Date,
                    IsActive = memberDto.IsActive
                };

                await _context.Members.AddAsync(newMember);
                await _context.SaveChangesAsync();


                return new MemberDTos
                {
                    MemberID = newMember.MemberID,
                    Person  = personDtos,
                    EmergencyContactInfo = newMember.EmergencyContactInfo,
                    LastBeltRank = BeltRankDto,
                    Date = newMember.Date,
                    IsActive = newMember.IsActive
                };
            }
            return null;
        }
        public async Task<MemberDTos> UpdateAsync(int id, MemberDTos member)
        {
            var BeltRankDTO = await _context.BeltRankss.FindAsync(member.LastBeltRank.BeltRankID);
            if (BeltRankDTO == null)
                return null;
            var BeltRankDto = new BeltRankDTos
            {
                BeltRankID = BeltRankDTO.BeltRankID,
                BeltName = BeltRankDTO.BeltName,
                BeltTestFees = BeltRankDTO.BeltTestFees
            };


            var existingMember = await _context.Members
                .Include(i => i.Person)
                .Include(g => g.LastBeltRank)
                .FirstOrDefaultAsync(i => i.MemberID == id);

            if (existingMember == null)
            {
                throw new KeyNotFoundException($"member with ID {id} not found.");
            }
            existingMember.EmergencyContactInfo = member.EmergencyContactInfo;
            existingMember.Date = member.Date;
            existingMember.IsActive = member.IsActive;
            if (member.Person != null)
            {
                existingMember.Person.Name = member.Person.Name;
                existingMember.Person.ContactInfo = member.Person.ContactInfo;
                existingMember.Person.Address = member.Person.Address;
            }
            await _context.SaveChangesAsync();
            return new MemberDTos
            {
                MemberID = existingMember.MemberID,
                EmergencyContactInfo = existingMember.EmergencyContactInfo,
                Date = existingMember.Date,
                IsActive = existingMember.IsActive,
                LastBeltRank = BeltRankDto,
                Person = new PersonDTos
                {
                    PersonID = existingMember.Person.ID,
                    Name = existingMember.Person.Name,
                    ContactInfo = existingMember.Person.ContactInfo,
                    Address = existingMember.Person.Address
                },

            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return false; // Instructor not found
            }
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true; // Deletion successful
        }

    }
}

