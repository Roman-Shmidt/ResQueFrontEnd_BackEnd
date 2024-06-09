using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ResQueDal.ClientDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.RestaurantDomain
{
    internal sealed class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder
                .HasKey(ur => ur.Id);

            builder
                .HasMany(ur => ur.RestaurantQueues)
                .WithOne(r => r.Restaurant)
                .HasForeignKey(ur => ur.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ur => ur.Reservations)
                .WithOne(u => u.Restaurant)
                .HasForeignKey(ur => ur.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ur => ur.Reviews)
                .WithOne(u => u.Restaurant)
                .HasForeignKey(ur => ur.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(ur => ur.Menus)
                .WithOne(ur => ur.Restaurant)
                .HasForeignKey(ur => ur.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
