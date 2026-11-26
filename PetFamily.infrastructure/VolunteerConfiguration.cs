using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain;

namespace PetFamily.infrastructure;

public sealed class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasColumnName("id")
            .ValueGeneratedNever(); // Id создаётся в домене, не БД
        
        builder.OwnsOne(v => v.FullName, fn =>
        {
            fn.Property(x => x.FirstName).HasColumnName("first_name").HasMaxLength(100).IsRequired();
            fn.Property(x => x.LastName).HasColumnName("last_name").HasMaxLength(100).IsRequired();
            fn.Property(x => x.MiddleName).HasColumnName("middle_name").HasMaxLength(100);
        });
        builder.Navigation(v => v.FullName).IsRequired();

        builder.OwnsOne(v => v.Email, e =>
        {
            e.Property(x => x.Value).HasColumnName("email").HasMaxLength(254).IsRequired();
        });
        builder.Navigation(v => v.Email).IsRequired();

        builder.OwnsOne(v => v.PhoneNumber, p =>
        {
            p.Property(x => x.Value).HasColumnName("phone_number").HasMaxLength(20);
        });

        builder.OwnsMany(v => v.SocialNetworks, sn =>
        {
            sn.ToJson("social_networks");
            sn.Property(x => x.Name).HasJsonPropertyName("name");
            sn.Property(x => x.Link).HasJsonPropertyName("url");
        });
        
        builder.Navigation(v => v.SocialNetworks)
            .HasField("_socialNetworks")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Property(v => v.ExperienceYears).HasColumnName("experience_years");
        builder.Property(v => v.Description).HasColumnName("description").HasMaxLength(1000);
        builder.Ignore(v => v.Pets); // [W3] — pets на этой неделе не мапим

        builder.OwnsMany(v => v.Requisites, r => r.ToJson("requisites"));
        builder.Navigation(v => v.Requisites)
            .HasField("_requisites")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}