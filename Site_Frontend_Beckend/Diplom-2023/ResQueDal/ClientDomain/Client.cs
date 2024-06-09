using ResQueDal.Common;
using ResQueDal.Common.Interfaces;
using ResQueDal.QueueDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.ReviewDomain;
using ResQueDal.UserDomain;

namespace ResQueDal.ClientDomain;

public sealed class Client : IEntity<int>,
    IUserId
{
    public Client(int id, 
        int userId, 
        User user, 
        ICollection<Reservation> reservations, 
        ICollection<RestaurantQueue> restaurantQueues, 
        ICollection<Review> reviews, 
        int companySize, 
        decimal rating, 
        string telephone)
    {
        Id = id;
        UserId = userId;
        User = user;
        Reservations = reservations;
        RestaurantQueues = restaurantQueues;
        Reviews = reviews;
        CompanySize = companySize;
        Rating = rating;
        Telephone = telephone;
    }

    public Client()
    {
        //Just for EF.
    }

    public int Id { get; set; }

    public int UserId { get; set; }

    public User User { get; private set; }

    public ICollection<Reservation> Reservations { get; private set; } =
        new List<Reservation>();

    public ICollection<RestaurantQueue> RestaurantQueues { get; private set; } =
        new List<RestaurantQueue>();

    public ICollection<Review> Reviews { get; private set; } =
        new List<Review>();

    public int CompanySize { get; set; }

    public decimal Rating { get; set; }

    public string Telephone { get; set; }
}
