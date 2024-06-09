using ResQueDal.Common;
using ResQueDal.Common.Interfaces;
using ResQueDal.MenuDomain;
using ResQueDal.QueueDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.ReviewDomain;
using ResQueDal.UserDomain;

namespace ResQueDal.RestaurantDomain;

public sealed class Restaurant : IEntity<int>,
    IUserId
{     
    public Restaurant(int id, 
        int userId, 
        User user, 
        ICollection<Reservation> reservations, 
        ICollection<RestaurantQueue> restaurantQueues, 
        ICollection<Review> reviews, 
        ICollection<Menu> menus, 
        bool isQueueOpen, 
        bool isReservationOpen, 
        string about, 
        string telephone, 
        string name, 
        string address, 
        decimal rating,
        TimeSpan openingTime,
        TimeSpan closingTime,
        decimal longitude,
        decimal latitude,
        string image)
    {
        Id = id;
        UserId = userId;
        User = user;
        Reservations = reservations;
        RestaurantQueues = restaurantQueues;
        Reviews = reviews;
        Menus = menus;
        IsQueueOpen = isQueueOpen;
        IsReservationOpen = isReservationOpen;
        About = about;
        Telephone = telephone;
        Name = name;
        Address = address;
        Rating = rating;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        LongitudeGoogleMap = longitude;
        LatitudeGoogleMap = latitude;
        Image = image;
    }

    public Restaurant()
    {
        //Just for EF.
    }

    public int Id { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<Reservation> Reservations { get; private set; } =
        new List<Reservation>();

    public ICollection<RestaurantQueue> RestaurantQueues { get; private set; } =
        new List<RestaurantQueue>();

    public ICollection<Review> Reviews { get; private set; } =
        new List<Review>();

    public ICollection<Menu> Menus { get; private set; } =
        new List<Menu>();

    public bool IsQueueOpen { get; set; }

    public bool IsReservationOpen { get; set; }

    public string About { get; set; }

    public string Telephone { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public decimal Rating { get; set; }

    public TimeSpan OpeningTime { get; set; }

    public TimeSpan ClosingTime { get; set; }

    public decimal LongitudeGoogleMap { get; set; }

    public decimal LatitudeGoogleMap { get; set; }

    public string Image { get; set; }
}
