using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Sport_Club_Data.Entitys;

namespace Sport_Club_Data.Configuration
{
    public class BeltTestConfig : IEntityTypeConfiguration<BeltTests>
    {
        public void Configure(EntityTypeBuilder<BeltTests> builder)
        {
            builder.ToTable("BeltTests");
            
            builder.HasKey(i => i.TestID);
            builder.Property(i => i.TestID).ValueGeneratedOnAdd();

            builder.HasOne(i => i.Member).WithMany(m => m.BeltTests).HasForeignKey(i => i.MemberID);

            builder.HasOne(i => i.Instructor).WithMany(I => I.BeltTest).HasForeignKey(i => i.TestedByInstructorID);

            builder.HasOne(i => i.BeltRank).WithMany(BeltRank => BeltRank.Belttests).HasForeignKey(i => i.RankID);

            builder.HasOne(i => i.Payment).WithOne(payment => payment.BeltTest).HasForeignKey<BeltTests>(i => i.PaymentID);

        }

    }
}
