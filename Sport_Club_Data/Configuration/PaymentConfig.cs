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
    public class PaymentConfig : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(i => i.PaymentID);
            builder.Property(i => i.PaymentID)
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Member)
             .WithMany(m => m.paymentDto)
            .HasForeignKey(p => p.MemberID);
            

          // builder.HasData(LoadPayments());
        }
        public static List<Payments> LoadPayments()
        {
            return new List<Payments>
            {
              new Payments
              {
                  PaymentID = 1,
                  MemberID = 11,
                  Data = new DateTime(2025, 7, 1),
                  Amount = 150.00m
              },
              new Payments
              {
                  PaymentID = 3,
                  MemberID = 11,
                  Data = new DateTime(2025, 7, 10),
                  Amount = 100.00m
              },
              new Payments
              {
                  PaymentID = 4,
                  MemberID = 33,
                  Data = new DateTime(2025, 7, 20),
                  Amount = 250.00m
              }
            };
        }
    }
}
