using ResQueDal.Common.Reading;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.Common.Repositories;
using ResQueDal.Infrastructure;
using ResQueDal.QueueDomain;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.ReservationDomain
{
    public class ReservationRepository : Repository<Reservation>,
        IReservationRepository
    {
        public ReservationRepository(ResQueDbContext dbContext, 
            Reader<Reservation> reader,
            FilteringByIdHandler<Reservation> filteringByIdHandler,
            FilteringByRestaurantIdHandler<Reservation> filteringByRestaurantIdHandler,
            FilteringByClientIdHandler<Reservation> filteringByClientIdHandler)
            : base(dbContext, reader)
        {
            AddClientFilteringHandlerForProperty(filteringByIdHandler);
            AddClientFilteringHandlerForProperty(filteringByRestaurantIdHandler);
            AddClientFilteringHandlerForProperty(filteringByClientIdHandler);
        }
    }
}
