using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.RestaurantDomain;
using ResQueDal.ClientDomain;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ReservationDomain
{
    public class ReservationDto : BaseDto, IDto
    {
        public ReservationDto(int id, 
            int? restaurantId, 
            RestaurantDto? restaurant, 
            int? clientId, 
            ClientDto? client, 
            string description, 
            string name, 
            int companySize, 
            DateTime dateAndTime,
            bool isReservationApproved,
            bool isReservationCompleted)
        {
            Id = id;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            ClientId = clientId;
            Client = client;
            Description = description;
            Name = name;
            CompanySize = companySize;
            DateAndTime = dateAndTime;
            IsReservationApproved = isReservationApproved;
            IsReservationCompleted = isReservationCompleted;
        }

        public int Id { get; set; }

        public int? RestaurantId { get; set; }

        public RestaurantDto? Restaurant { get; set; }

        public int? ClientId { get; set; }

        public ClientDto? Client { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int CompanySize { get; set; }

        public DateTime DateAndTime { get; set; }

        public bool IsReservationApproved { get; set; }

        public bool IsReservationCompleted { get; set; }
    }
}
