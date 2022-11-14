using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Users.Domain;

namespace Users.Database.DomainEntitiesConfiguration;

public class UsersAggregateTypeConfiguration : IEntityTypeConfiguration<User>
{
    internal const string Credentials = "_credentials";
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", SchemaNames.Users);
        
        builder.HasKey("_id");
        builder.Property("_id").HasColumnName("Id").ValueGeneratedOnAdd();
        
        builder.HasAlternateKey(s => s.UniqueId);
        builder.Property(s => s.UniqueId)
            .ValueGeneratedNever();

        builder.Property("_emailAddress").HasColumnName("EmailAddress");
        builder.Property("_welcomeEmailWasSend").HasColumnName("WelcomeEmailWasSend");

        builder.OwnsOne<AuthCredentials>(Credentials, y => 
        {
           y.WithOwner().HasForeignKey("UserId");

           y.ToTable("Credentials", SchemaNames.Users);

           y.HasKey(s => s.Id);
           y.Property(s => s.Id).ValueGeneratedOnAdd();

           y.Property("_name").HasColumnName("UserName").IsRequired(false);
           y.Property("_password").HasColumnName("Password").IsRequired(false);
           
           var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
               x => x,
               x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : x);
           y.Property("_passwordExpirationDate")
               .HasConversion(nullableDateTimeConverter)
               .HasColumnName("PasswordExpirationDate").IsRequired(false);
        });
    }
}