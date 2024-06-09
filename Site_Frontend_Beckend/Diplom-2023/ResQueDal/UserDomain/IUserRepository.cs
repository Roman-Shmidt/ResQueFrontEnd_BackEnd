using ResQueDal.Common.Repositories;
using System.Threading.Tasks;

namespace ResQueDal.UserDomain;

public interface IUserRepository : IRepository<User>
{
    Task<string> GetNextUserNumber();
}
