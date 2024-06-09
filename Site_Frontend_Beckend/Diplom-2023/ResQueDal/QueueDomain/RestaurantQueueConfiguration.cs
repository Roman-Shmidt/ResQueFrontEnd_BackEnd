using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ResQueDal.ClientDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.QueueDomain
{
    internal sealed class RestaurantQueueConfiguration : IEntityTypeConfiguration<RestaurantQueue>
    {
        public void Configure(EntityTypeBuilder<RestaurantQueue> builder)
        {
            builder
                .HasKey(ur => ur.Id);
        }
    }
}
