using ResQueDal.Common.Reading;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.Common.Repositories;
using ResQueDal.Infrastructure;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.ClientDomain
{
    public class ClientRepository : Repository<Client>,
        IClientRepository
    {
        public ClientRepository(ResQueDbContext dbContext, 
            Reader<Client> reader,
            FilteringByIdHandler<Client> filteringByIdHandler,
            FilteringByUserIdHandler<Client> filteringByUserIdHandler)
            : base(dbContext, reader)
        {
            AddClientFilteringHandlerForProperty(filteringByIdHandler);
            AddClientFilteringHandlerForProperty(filteringByUserIdHandler);
        }
    }
}
