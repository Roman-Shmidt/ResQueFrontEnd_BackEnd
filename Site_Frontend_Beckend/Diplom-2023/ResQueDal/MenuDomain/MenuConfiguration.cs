using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ResQueDal.ClientDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.MenuDomain
{
    internal sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder
                .HasKey(ur => ur.Id);

            builder
                .HasMany(ur => ur.Dishes)
                .WithOne(r => r.Menu)
                .HasForeignKey(ur => ur.MenuId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
