using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.RestaurantDomain;
using ResQueBackEnd.Managers.RestaurantQueueDomain;

namespace ResQue.Controllers.RestaurantQueues
{
    public class RestaurantQueueDtoWithPossibleActions
    {
        public RestaurantQueueDtoWithPossibleActions(RestaurantQueueDto restaurantQueueDto)
        {
            Id = restaurantQueueDto.Id;
            RestaurantId = restaurantQueueDto.RestaurantId;
            Restaurant = restaurantQueueDto.Restaurant;
            ClientId = restaurantQueueDto.ClientId;
            Client = restaurantQueueDto.Client;
            CompanySize = restaurantQueueDto.CompanySize;
            PlaceInQueue = restaurantQueueDto.PlaceInQueue;
            EstimatedTime = restaurantQueueDto.EstimatedTime;
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
