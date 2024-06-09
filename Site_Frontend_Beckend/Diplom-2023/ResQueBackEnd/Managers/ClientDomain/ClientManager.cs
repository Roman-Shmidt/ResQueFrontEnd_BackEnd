using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using ResQueBackEnd.Common.Managers;
using ResQueBackEnd.Common.Mappers;
using ResQueBackEnd.Managers.ClientDomain.Mappers;
using ResQueDal.ClientDomain;
using ResQueDal.Common.Repositories;

namespace ResQueBackEnd.Managers.ClientDomain
{
    public class ClientManager : ManagerForModelWithoutNumber<ClientDto,Client>, IClientManager
    {
        public ClientManager(IClientRepository repository, IClientDtoMapper mapper) : base(repository, mapper)
        {
        }

        public bool ClientExist(string email, string password)
        {
            throw new NotImplementedException();
        }

        public override Task<Result<ClientDto>> Create(ClientDto dto)
        {
            return base.Create(dto);
        }

        public override Task<Result> DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public override Task<Maybe<ClientDto>> GetById(int id, IEnumerable<string> relatedItems)
        {
            return base.GetById(id, relatedItems);
        }

        public override Task<IEnumerable<ClientDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems)
        {
            return base.GetByIds(ids, relatedItems);
        }

        public override Task<IEnumerable<ClientDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetItems(multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<PaginatedList<ClientDto>> GetPaginatedItems(PaginationOptions paginationOptions, IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetPaginatedItems(paginationOptions, multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<Result<ClientDto>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            return base.UpdateById(id, updatedValues);
        }
    }
}
