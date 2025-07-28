using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Data.Configuration
{
    public class MemberInstructorsConfig : IEntityTypeConfiguration<MemberInstructors>
    {
        public void Configure(EntityTypeBuilder<MemberInstructors> builder)
        {
            builder.ToTable("MemberInstructors");

            builder.HasKey(mi => new { mi.MemberID, mi.InstructorID});

            builder.Property(mi => mi.MemberID).IsRequired();
            builder.Property(mi => mi.AssignDate).IsRequired();

            builder.HasOne(mi => mi.Member)
             .WithMany(m => m.MemberInstructors)
             .HasForeignKey(mi => mi.MemberID);

            builder.HasOne(mi => mi.Instructor)
                   .WithOne(i => i.MemberInstructor)
                   .HasForeignKey<MemberInstructors>(mi => mi.InstructorID);
        }
    

    }
}
