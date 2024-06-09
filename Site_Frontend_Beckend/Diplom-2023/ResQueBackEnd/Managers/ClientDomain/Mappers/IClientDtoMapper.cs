using ResQueBackEnd.Common.Mappers;
using ResQueDal.ClientDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ClientDomain.Mappers
{
    public interface IClientDtoMapper : IDtoMapper<ClientDto, Client>
    {
    }
}
