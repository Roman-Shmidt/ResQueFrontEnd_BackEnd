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

namespace ResQueBackEnd.Managers.RestaurantQueueDomain
{
    public class RestaurantQueueDto : BaseDto, IDto
    {
        public RestaurantQueueDto(int id,
            int? restaurantId,
            RestaurantDto? restaurant,
            int? clientId,
            ClientDto? client,
            int companySize,
            int placeInQueue,
            DateTime estimatedTime)
        {
            Id = id;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            ClientId = clientId;
            Client = client;
            CompanySize = companySize;
            PlaceInQueue = placeInQueue;
            EstimatedTime = estimatedTime;
        }

        public int Id { get; set; }

        public int? RestaurantId { get; set; }

        public RestaurantDto? Restaurant { get; set; }

        public int? ClientId { get; set; }

        public ClientDto? Client { get; set; }

        public int CompanySize { get; set; }

        public int PlaceInQueue { get; set; }

        public DateTime EstimatedTime { get; set; }
    }
}
