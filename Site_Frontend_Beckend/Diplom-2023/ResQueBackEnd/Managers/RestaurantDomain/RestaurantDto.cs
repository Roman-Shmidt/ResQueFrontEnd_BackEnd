using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.ReservationDomain;
using ResQueBackEnd.Managers.RestaurantQueueDomain;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueBackEnd.Managers.UserDomain;
using ResQueDal.MenuDomain;
using ResQueDal.QueueDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.ReviewDomain;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RestaurantDomain
{
    public class RestaurantDto : BaseDto, IDto
    {
        public RestaurantDto(int id,
            int userId,
            UserDto user,
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

        public RestaurantDto()
        {

        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public UserDto User { get; set; }

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
}
