using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.ReservationDomain;
using ResQueBackEnd.Managers.RestaurantDomain;
using ResQueBackEnd.Managers.RestaurantQueueDomain;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueDal.UserDomain;

namespace ResQue.Controllers.Restaurants
{
    public class RestaurantDtoWithPossibleActions
    {
        public RestaurantDtoWithPossibleActions(RestaurantDto restaurantDto)
        {
            Id = restaurantDto.Id;
            UserId = restaurantDto.UserId;
            User = null;
            Reservations = null;
            RestaurantQueues = null;
            Reviews = null;
            Menus = null;
            IsQueueOpen = restaurantDto.IsQueueOpen;
            IsReservationOpen = restaurantDto.IsReservationOpen;
            About = restaurantDto.About;
            Telephone = restaurantDto.Telephone;
            Name = restaurantDto.Name;
            Address = restaurantDto.Address;
            Rating = restaurantDto.Rating;
            OpeningTime = restaurantDto.OpeningTime;
            ClosingTime = restaurantDto.ClosingTime;
            Longitude = restaurantDto.LongitudeGoogleMap;
            Latitude = restaurantDto.LatitudeGoogleMap;
            Image = restaurantDto.Image;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<ReservationDto> Reservations { get; private set; } =
            new List<ReservationDto>();

        public ICollection<RestaurantQueueDto> RestaurantQueues { get; private set; } =
            new List<RestaurantQueueDto>();

        public ICollection<ReviewDto> Reviews { get; private set; } =
            new List<ReviewDto>();

        public ICollection<MenuDto> Menus { get; private set; } =
            new List<MenuDto>();

        public bool IsQueueOpen { get; set; }

        public bool IsReservationOpen { get; set; }

        public string About { get; set; }

        public string Telephone { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Rating { get; set; }

        public TimeSpan OpeningTime { get; set; }

        public TimeSpan ClosingTime { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string Image { get; set; }
    }
}
