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

        // --- Одиночные VO: OwnsOne -> колонки в той же таблице volunteers ---

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

        // --- Коллекция VO: OwnsMany + ToJson -> один jsonb-столбец ---

        builder.OwnsMany(v => v.SocialNetworks, sn =>
        {
            sn.ToJson("social_networks");
            sn.Property(x => x.Name).HasJsonPropertyName("name");
            sn.Property(x => x.Link).HasJsonPropertyName("url");
        });

        // Пишем/читаем через backing-поле, чтобы не ломать инкапсуляцию агрегата.
        builder.Navigation(v => v.SocialNetworks)
            .HasField("_socialNetworks")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}