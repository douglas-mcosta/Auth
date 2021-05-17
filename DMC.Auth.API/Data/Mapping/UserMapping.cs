using DMC.Auth.API.Models;
using DMC.Core.DomainObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMC.Auth.API.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
              .IsRequired()
              .HasColumnType("varchar(100)");

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(u => u.FullName)
               .IsRequired()
               .HasColumnType("varchar(250)");

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(email => email.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.OwnsOne(u => u.BirthDate, user =>
            {
                user.Property(user => user.Date)
                    .IsRequired()
                    .HasColumnName("BirthDate");
            });

            builder.Property(u => u.Education)
            .IsRequired();

            builder.ToTable("Users");
        }
    }
}