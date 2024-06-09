using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Managers.ReservationDomain;
using ResQueBackEnd.Managers.RestaurantQueueDomain;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueBackEnd.Managers.UserDomain;
using ResQueDal.QueueDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.ReviewDomain;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ClientDomain
{
    public class ClientDto : BaseDto, IDto
    {
        public ClientDto(int id,
            int userId,
            UserDto user,
            int companySize, 
            decimal rating, 
            string telephone)
        {
            Id = id;
            UserId = userId;
            User = user;
            CompanySize = companySize;
            Rating = rating;
            Telephone = telephone;
        }

        public ClientDto() 
        {
            
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public UserDto User { get; set; }

        public int CompanySize { get; set; }

        public decimal Rating { get; set; }

        public string Telephone { get; set; }
    }
}
