using ResQueDal.Common.Repositories;

namespace ResQueDal.RoleDomain;

public interface IRoleRepository : IRepository<Role>
{
    Task<List<RoleClaim>> GetClaimsForRole(int roleId);

    Task<List<RoleClaim>> GetClaimsForRoles(IEnumerable<int> roleIds);
}