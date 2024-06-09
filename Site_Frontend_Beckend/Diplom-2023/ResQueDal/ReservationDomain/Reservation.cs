using ResQueDal.ClientDomain;
using ResQueDal.Common;
using ResQueDal.Common.Interfaces;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.ReservationDomain
{
    public sealed class Reservation : IEntity<int>,
        IClientId,
        IRestaurantId
    {
        public Reservation(int id, 
            int? restaurantId, 
            Restaurant? restaurant, 
            int? clientId, 
            Client? client, 
            string description, 
            string name, 
            int companySize, 
            DateTime dateAndTime,
            bool isReservationApproved,
            bool isReservationCompleted)
        {
            Id = id;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            ClientId = clientId;
            Client = client;
            Description = description;
            Name = name;
            CompanySize = companySize;
            DateAndTime = dateAndTime;
            IsReservationApproved = isReservationApproved;
            IsReservationCompleted = isReservationCompleted;
        }

        public Reservation()
        {
            //Just for EF.
        }

        public int Id { get; set; }

        public int? RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }

        public int? ClientId { get; set; }

        public Client? Client { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int CompanySize { get; set; }

        public DateTime DateAndTime { get; set; }

        public bool IsReservationApproved { get; set; }

        public bool IsReservationCompleted { get; set; }
    }
}
