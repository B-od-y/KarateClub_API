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
    public class SubscriptionPeriodsConfig : IEntityTypeConfiguration<SubscriptionPeriods>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPeriods> builder)
        {
            builder.ToTable("SubscriptionPeriods");

            builder.HasKey(i => i.PeriodID);

            builder.Property(i => i.PeriodID).ValueGeneratedOnAdd();

            builder.HasOne(i => i.Member).WithOne(m => m.SubscriptionPeriods).HasForeignKey<Members>(i => i.MemberID);

            builder.HasOne(i => i.payment).WithOne(payment => payment.SubscriptionPeriod).HasForeignKey<SubscriptionPeriods>(i => i.PaymentID);

        }
    }
}
