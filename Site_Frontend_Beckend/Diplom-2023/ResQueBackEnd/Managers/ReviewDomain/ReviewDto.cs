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

namespace ResQueBackEnd.Managers.ReviewDomain
{
    public class ReviewDto : BaseDto, IDto
    {
        public ReviewDto(int id,
            int? clientId,
            ClientDto? client,
            int? restaurantId,
            RestaurantDto? restaurant,
            decimal rating,
            string description)
        {
            Id = id;
            ClientId = clientId;
            Client = client;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            Rating = rating;
            Description = description;
        }

        public int Id { get; set; }

        public int? ClientId { get; set; }

        public ClientDto? Client { get; set; }

        public int? RestaurantId { get; set; }

        public RestaurantDto? Restaurant { get; set; }

        public decimal Rating { get; set; }

        public string Description { get; set; }
    }
}
