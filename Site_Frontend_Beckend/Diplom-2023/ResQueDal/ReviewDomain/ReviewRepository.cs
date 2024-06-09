using ResQueDal.Common.Reading;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.Common.Repositories;
using ResQueDal.Infrastructure;
using ResQueDal.ReservationDomain;
using ResQueDal.RoleDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.ReviewDomain
{
    public class ReviewRepository : Repository<Review>,
        IReviewRepository
    {
        public ReviewRepository(ResQueDbContext dbContext, 
            Reader<Review> reader,
            FilteringByIdHandler<Review> filteringByIdHandler,
            FilteringByRestaurantIdHandler<Review> filteringByRestaurantIdHandler,
            FilteringByClientIdHandler<Review> filteringByClientIdHandler)
            : base(dbContext, reader)
        {
            AddClientFilteringHandlerForProperty(filteringByIdHandler);
            AddClientFilteringHandlerForProperty(filteringByRestaurantIdHandler);
            AddClientFilteringHandlerForProperty(filteringByClientIdHandler);
        }
    }
}
