using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.EntityFrameworkCore;
using ResQueDal.Common.Reading;
using ResQueDal.Common.Repositories;
using ResQueDal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResQueDal.RoleDomain;

public sealed class RoleRepository : Repository<Role>,
    IRoleRepository
{
    public RoleRepository(ResQueDbContext dbContext, Reader<Role> reader) 
        : base(dbContext, reader)
    {
    }

    public Task<List<RoleClaim>> GetClaimsForRole(int roleId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RoleClaim>> GetClaimsForRoles(IEnumerable<int> roleIds)
    {
        throw new NotImplementedException();
    }
}