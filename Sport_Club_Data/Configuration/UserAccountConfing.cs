using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sport_Club_Data.Entitys;
using Sport_Club_Data.Helper;
namespace Sport_Club_Data.Configuration
{
    internal class UserAccountConfing : IEntityTypeConfiguration<UserAccounts>
    {
        public void Configure(EntityTypeBuilder<UserAccounts> builder)
        {
            builder.ToTable("UserAccounts");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.HasData([new UserAccounts {
                    Id = 1,
                Username = "admin",
                    Password = PasswordHash.HashPassword("admin123"),
                    FullName = "Administrator"
                }]);
            builder.HasData([new UserAccounts {
                    Id = 2,
                Username = "admin1",
                    Password = PasswordHash.HashPassword("admin1234"),
                    FullName = "Administrator"
                }]);
        }
    }
}