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
    public class BeltRankConfig : IEntityTypeConfiguration<BeltRanks>
    {
        public void Configure(EntityTypeBuilder<BeltRanks> builder)
        {
            builder.ToTable("BeltRanks");

            builder.HasKey(i => i.BeltRankID);
            builder.Property(i => i.BeltRankID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.BeltName)
                .HasMaxLength(200)
                .IsRequired(false);


            
           builder.HasData(LoadBeltRanks());
        }
        public static List<BeltRanks> LoadBeltRanks()
        {
            return new List<BeltRanks>
            {
               new BeltRanks() { BeltRankID = 1, BeltName = "White Belt", BeltTestFees = 10 },
               new BeltRanks() { BeltRankID = 2, BeltName = "Yellow Belt", BeltTestFees = 15 },
               new BeltRanks() { BeltRankID = 3, BeltName = "Green Belt", BeltTestFees = 20 },
               new BeltRanks() { BeltRankID = 4, BeltName = "Blue Belt", BeltTestFees = 25 },
               new BeltRanks() { BeltRankID = 5, BeltName = "Brown Belt", BeltTestFees = 30 },
               new BeltRanks() { BeltRankID = 6, BeltName = "Black Belt", BeltTestFees = 40 },
               new BeltRanks() { BeltRankID = 7, BeltName = "Certified Fitness Trainer", BeltTestFees = 50 }
            };
        }
    }
}
