using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ResQueDal.RoleDomain;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasMany(r => r.Claims)
            .WithOne()
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(r => new { RoleNumber = r.Number })
            .IsUnique();

        builder.HasIndex(x => x.MainRole).IsClustered(false);
    }
}