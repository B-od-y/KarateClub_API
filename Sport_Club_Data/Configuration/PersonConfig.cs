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
    public class PersonConfig :IEntityTypeConfiguration<Persons>
    {
        public void Configure(EntityTypeBuilder<Persons> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(p => p.ID);

            builder.Property(p => p.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.ContactInfo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Address)
                .IsRequired();


            builder.HasData(LoadPerson());
        }
        private static List<Persons> LoadPerson()
        {
            return new List<Persons>
            {
                new Persons() { ID = 1, Name= "AbdallahFouad" ,ContactInfo = "01025119221" ,Address = "Elsadat 20Th"},
                new Persons() { ID = 2, Name= "AhmedMohamed" ,ContactInfo = "01025119222" ,Address = "Cairo" },
                new Persons() { ID = 3, Name= "MohamedAli" ,ContactInfo = "01025119223" ,Address = "Aswan" },
                 new Persons() { ID = 11, Name= "AbdallahFouad" ,ContactInfo = "01025119221" ,Address = "Elsadat 20Th"},
                new Persons() { ID = 22, Name= "AhmedMohamed" ,ContactInfo = "01025119222" ,Address = "Cairo" },
                new Persons() { ID = 33, Name= "MohamedAli" ,ContactInfo = "01025119223" ,Address = "Aswan" }
            };
        }
    }
}
