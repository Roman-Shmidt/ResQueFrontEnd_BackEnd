using Microsoft.EntityFrameworkCore;
using ResQueDal.Common;
using ResQueDal.Common.Reading;
using ResQueDal.Common.Repositories;
using ResQueDal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResQueDal.UserDomain;

public sealed class UserRepository : Repository<User>,
    IUserRepository
{
    public UserRepository(ResQueDbContext dbContext, Reader<User> reader) 
        : base(dbContext, reader)
    {
    }

    public Task<string> GetNextUserNumber()
    {
        var userNumbers = DbContext.Set<User>().AsNoTracking().Select(x => x.Number);

        var userNumber = userNumbers.Select(x => Convert.ToInt32(x)).Any() ? 
            userNumbers.Select(x => Convert.ToInt32(x)).Max() : 1;
        
        return Task.FromResult((userNumber + 1).ToString());
    }
}