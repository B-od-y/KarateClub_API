using Microsoft.EntityFrameworkCore;
using Sport_Club_Bussiness;
using Sport_Club_Bussiness.DTOs;
using Sport_Club_Data;
using Sport_Club_Data.Entitys;
using Sport_Club_Business.Interface;
using System;
namespace Sport_Club_Business.Services
{
    public class InsructorService : InstructorInterface
    {
        private readonly AppDbContext _context;

        public InsructorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<instructorDTos>> GetAllAsync()
        {
             return await _context.Instructors
                .AsNoTracking()
                .Include(i => i.Person).
                Select(selector => new instructorDTos
                {
                    InstructorID = selector.ID,
                    qualification = selector.Qualification,
                    person = new PersonDTos
                    {
                        PersonID = selector.Person.ID,
                        Name = selector.Person.Name,
                        ContactInfo = selector.Person.ContactInfo,
                        Address = selector.Person.Address
                    }
                }).ToListAsync();
        }
        public async Task<instructorDTos?> GetByIdAsync(int id)
        {
            return await _context.Instructors
                .AsNoTracking()
                .Include(i => i.Person)
                .Where(x => x.ID == id).
                Select(selector => new instructorDTos
                {
                    InstructorID = selector.ID,
                    qualification = selector.Qualification,
                    person = new PersonDTos
                    {
                        PersonID = selector.Person.ID,
                        Name = selector.Person.Name,
                        ContactInfo = selector.Person.ContactInfo,
                        Address = selector.Person.Address
                    }
                }).FirstOrDefaultAsync();
        }
        public async Task<instructorDTos> AddAsync(instructorDTos instructor)
        {
            var existingPerson = await _context.Persons.FindAsync(instructor.person.PersonID);

            if (existingPerson != null)
            {
                var instructorEntity = new Instructors
                {
                    PersonID = instructor.person.PersonID,
                    Qualification = instructor.qualification.ToString()
                };

                await _context.Instructors.AddAsync(instructorEntity);
                await _context.SaveChangesAsync();
            }
            else
            {
                var person = new Persons
                {
                    Name = instructor.person.Name,
                    ContactInfo = instructor.person.ContactInfo,
                    Address = instructor.person.Address
                };

                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();

                var instructorEntity = new Instructors
                {
                    PersonID = person.ID,
                    Qualification = instructor.qualification.ToString()
                };

                await _context.Instructors.AddAsync(instructorEntity);
                await _context.SaveChangesAsync();

               
                instructor.person.PersonID = person.ID;
            }

            return instructor;
        }

        public async Task<instructorDTos> UpdateAsync(int id,instructorDTos instructor)
        {
            var existingInstructor = await _context.Instructors
                .Include(i => i.Person)
                .FirstOrDefaultAsync(i => i.ID == id);
            if (existingInstructor == null)
            {
                throw new KeyNotFoundException($"Instructor with ID {id} not found.");
            }
            existingInstructor.Qualification = instructor.qualification;
            if (instructor.person != null)
            {
                existingInstructor.Person.Name = instructor.person.Name;
                existingInstructor.Person.ContactInfo = instructor.person.ContactInfo;
                existingInstructor.Person.Address = instructor.person.Address;
            }
            await _context.SaveChangesAsync();
            return new instructorDTos
            {
                InstructorID = existingInstructor.ID,
                qualification = existingInstructor.Qualification,
                person = new PersonDTos
                {
                    PersonID = existingInstructor.Person.ID,
                    Name = existingInstructor.Person.Name,
                    ContactInfo = existingInstructor.Person.ContactInfo,
                    Address = existingInstructor.Person.Address
                }
            };

        }
               

        public async Task<bool> DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return false; // Instructor not found
            }
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return true; // Deletion successful
        }



    }
}
