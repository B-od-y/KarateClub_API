using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Data.Configuration
{
    public class InstructorConfig : IEntityTypeConfiguration<Instructors>
    {
        public void Configure(EntityTypeBuilder<Instructors> builder)
        {
            builder.ToTable("Instructors");

            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Qualification)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.HasOne(builder => builder.Person)
                .WithOne(person => person.Instructor)
                .HasForeignKey<Instructors>(i => i.PersonID);
                
            //builder.HasData(LoadInstructors());
        }
        public static List<Instructors> LoadInstructors()
        {
            return new List<Instructors>
            {
                new Instructors() { ID = 1, PersonID = 1, Qualification = "Certified Fitness Trainer" },
                new Instructors() { ID = 2, PersonID = 2, Qualification = "Yoga Instructor" },
                new Instructors() { ID = 3, PersonID = 3, Qualification = "Martial Arts Expert" }
            };
        }
    }
}
