using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.ReservationDomain;
using ResQueBackEnd.Managers.RestaurantDomain;

namespace ResQue.Controllers.Reservations
{
    public class ReservationDtoWithPossibleActions
    {
        public ReservationDtoWithPossibleActions(ReservationDto reservationDto)
        {
            Id = reservationDto.Id;
            RestaurantId = reservationDto.RestaurantId;
            Restaurant = reservationDto.Restaurant;
            ClientId = reservationDto.ClientId;
            Client = reservationDto.Client;
            Description = reservationDto.Description;
            Name = reservationDto.Name;
            CompanySize = reservationDto.CompanySize;
            DateAndTime = reservationDto.DateAndTime;
            IsReservationApproved= reservationDto.IsReservationApproved;
            IsReservationCompleted= reservationDto.IsReservationCompleted;
        }

        public int Id { get; set; }

        public int? RestaurantId { get; set; }

        public RestaurantDto? Restaurant { get; set; }

        public int? ClientId { get; set; }

        public ClientDto? Client { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public int CompanySize { get; set; }

        public DateTime DateAndTime { get; set; }

        public bool IsReservationApproved { get; set; }

        public bool IsReservationCompleted { get; set; }
    }
}
