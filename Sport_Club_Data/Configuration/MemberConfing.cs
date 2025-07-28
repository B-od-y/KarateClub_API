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
    public class MemberConfig : IEntityTypeConfiguration<Members>
    {
        public void Configure(EntityTypeBuilder<Members> builder)
        {
            builder.ToTable("Members");

            builder.HasKey(i => i.MemberID);

            builder.Property(i => i.MemberID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Date)
                .IsRequired(false);

            builder.Property(i => i.EmergencyContactInfo)
                .IsRequired(false);

            builder.Property(i => i.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(builder => builder.Person)
                .WithOne(person => person.Member)
                .HasForeignKey<Members>(i => i.PersonID);
                

            builder.HasOne(m => m.LastBeltRank)
             .WithMany(i => i.members) 
             .HasForeignKey(m => m.LastBeltRankID);

            //builder.HasData(LoadMembers());
        }
        public static List<Members> LoadMembers()
        {
            return new List<Members>
            {
               new Members{
                     MemberID = 1,
                     PersonID = 11,
                     EmergencyContactInfo = "Mom: 0100000000",
                     LastBeltRankID = 2, // Yellow
                     Date = new DateTime(2024, 5, 15),
                     IsActive = true },
               new Members
               {
                   MemberID = 2,
                   PersonID = 22,
                   EmergencyContactInfo = "Dad: 0101111111",
                   LastBeltRankID = 3, // Green
                   Date = new DateTime(2024, 6, 10),
                   IsActive = false
               }
            };
        }
    }
}
