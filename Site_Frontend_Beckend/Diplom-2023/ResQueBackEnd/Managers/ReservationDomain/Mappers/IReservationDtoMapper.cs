using ResQueBackEnd.Common.Mappers;
using ResQueDal.ReservationDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ReservationDomain.Mappers
{
    public interface IReservationDtoMapper : IDtoMapper<ReservationDto, Reservation>
    {
    }
}
