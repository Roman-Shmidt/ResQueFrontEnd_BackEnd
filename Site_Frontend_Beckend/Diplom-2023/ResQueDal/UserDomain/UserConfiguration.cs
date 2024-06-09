using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResQueDal.UserDomain;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(user => user.Number)
            .IsUnique();

        builder.HasIndex(x => x.UserName).IsClustered(false);
        builder.HasIndex(x => x.UserType).IsClustered(false);
        builder.HasIndex(x => x.Email).IsClustered(false);
        builder.HasIndex(x => x.FirstName).IsClustered(false);
        builder.HasIndex(x => x.LastName).IsClustered(false);
        builder.HasIndex(x => x.IsActive).IsClustered(false);
    }
}