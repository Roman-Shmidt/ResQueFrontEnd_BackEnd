using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.RestaurantDomain;
using ResQueBackEnd.Managers.ReviewDomain;

namespace ResQue.Controllers.Reviews
{
    public class ReviewDtoWithPossibleActions
    {
        public ReviewDtoWithPossibleActions(ReviewDto reviewDto)
        {
            Id = reviewDto.Id;
            ClientId = reviewDto.ClientId;
            Client = reviewDto.Client;
            RestaurantId = reviewDto.RestaurantId;
            Restaurant = reviewDto.Restaurant;
            Rating = reviewDto.Rating;
            Description = reviewDto.Description;
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
