using ResQueDal.ClientDomain;
using ResQueDal.Common;
using ResQueDal.Common.Interfaces;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.QueueDomain
{
    public sealed class RestaurantQueue : IEntity<int>,
        IClientId,
        IRestaurantId
    {
        public RestaurantQueue(int id, 
            int? restaurantId, 
            Restaurant? restaurant, 
            int? clientId, 
            Client? client, 
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

        public RestaurantQueue()
        {
            //Just for EF.
        }

        public int Id { get; set; }

        public int? RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }
        
        public int? ClientId { get; set; }

        public Client? Client { get; set; }

        public int CompanySize { get; set; }

        public int PlaceInQueue { get; set; }

        public DateTime EstimatedTime { get; set; }
    }
}
