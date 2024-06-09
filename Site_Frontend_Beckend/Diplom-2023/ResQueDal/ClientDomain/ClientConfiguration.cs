using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.ClientDomain
{
    internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasKey(ur => ur.Id);

            builder
                .HasMany(ur => ur.RestaurantQueues)
                .WithOne(r => r.Client)
                .HasForeignKey(ur => ur.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ur => ur.Reservations)
                .WithOne(u => u.Client)
                .HasForeignKey(ur => ur.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ur => ur.Reviews)
                .WithOne(u => u.Client)
                .HasForeignKey(ur => ur.ClientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
