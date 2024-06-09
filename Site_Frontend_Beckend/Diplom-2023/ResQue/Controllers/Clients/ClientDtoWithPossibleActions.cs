using ResQue.Infrastructure;
using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.ReservationDomain;
using ResQueBackEnd.Managers.RestaurantQueueDomain;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueDal.UserDomain;

namespace ResQue.Controllers.Clients
{
    public class ClientDtoWithPossibleActions : BaseDtoWithPossibleActions
    {
        public ClientDtoWithPossibleActions(ClientDto clientDto)
        {
            Id = clientDto.Id;
            CompanySize = clientDto.CompanySize;
            Rating = clientDto.Rating;
            Telephone = clientDto.Telephone;
        }

        public ClientDtoWithPossibleActions()
        {

        }

        public int Id { get; set; }

        public User User { get; set; }

        public int CompanySize { get; set; }

        public decimal Rating { get; set; }

        public string Telephone { get; set; }
    }
}
