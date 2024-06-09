using ResQueBackEnd.Common.Mappers;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ReviewDomain.Mappers
{
    public interface IReviewDtoMapper : IDtoMapper<ReviewDto, Review>
    {
    }
}
