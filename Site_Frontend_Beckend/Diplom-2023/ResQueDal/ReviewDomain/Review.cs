using ResQueDal.ClientDomain;
using ResQueDal.Common;
using ResQueDal.Common.Interfaces;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.ReviewDomain
{
    public sealed class Review : IEntity<int>,
        IClientId,
        IRestaurantId
    {
        public Review(int id, 
            int? clientId, 
            Client? client, 
            int? restaurantId, 
            Restaurant? restaurant, 
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

        public Review() 
        {
            //Just for EF.
        }

        public int Id { get; set; }

        public int? ClientId { get; set; }

        public Client? Client { get; set; }

        public int? RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }

        public decimal Rating { get; set; }

        public string Description { get; set; }
    }
}
