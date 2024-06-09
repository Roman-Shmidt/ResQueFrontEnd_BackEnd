using DryIoc;
using Microsoft.EntityFrameworkCore;
using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.ClientDomain.Mappers;
using ResQueBackEnd.Managers.DishDomain;
using ResQueBackEnd.Managers.DishDomain.Mappers;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.MenuDomain.Mappers;
using ResQueBackEnd.Managers.ReservationDomain;
using ResQueBackEnd.Managers.ReservationDomain.Mappers;
using ResQueBackEnd.Managers.RestaurantDomain;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueBackEnd.Managers.RestaurantQueueDomain;
using ResQueBackEnd.Managers.RestaurantQueueDomain.Mappers;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueBackEnd.Managers.ReviewDomain.Mappers;
using ResQueBackEnd.Managers.RoleDomain;
using ResQueBackEnd.Managers.RoleDomain.Mappers;
using ResQueBackEnd.Managers.UserDomain;
using ResQueBackEnd.Managers.UserDomain.Mappers;
using ResQueDal.ClientDomain;
using ResQueDal.DishDomain;
using ResQueDal.Infrastructure;
using ResQueDal.MenuDomain;
using ResQueDal.QueueDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.RestaurantDomain;
using ResQueDal.ReviewDomain;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;

namespace ResQueBackEnd.Helpers
{
    internal static class IocContainerExtensionMethods
    {
        public static void RegisterDbContextStuff(this Container container)
        {
            container.Register<ResQueDbContext>(Reuse.Scoped);

            container.RegisterMapping<DbContext, ResQueDbContext>();
        }

        public static void RegisterManagers(this Container container)
        {
            container.Register<IClientManager, ClientManager>(Reuse.Scoped);
            container.Register<IDishManager, DishManager>(Reuse.Scoped);
            container.Register<IMenuManager, MenuManager>(Reuse.Scoped);
            container.Register<IReservationManager, ReservationManager>(Reuse.Scoped);
            container.Register<IRestaurantManager, RestaurantManager>(Reuse.Scoped);
            container.Register<IRestaurantQueueManager, RestaurantQueueManager>(Reuse.Scoped);
            container.Register<IReviewManager, ReviewManager>(Reuse.Scoped);
            container.Register<IRoleManager, RoleManager>(Reuse.Scoped);
            container.Register<IUserManager, UserManager>(Reuse.Scoped);
        }

        public static void RegisterMappers(this Container container)
        {
            container.Register<IClientDtoMapper, ClientDtoMapper>(Reuse.Scoped);
            container.Register<IDishDtoMapper, DishDtoMapper>(Reuse.Scoped);
            container.Register<IMenuDtoMapper, MenuDtoMapper>(Reuse.Scoped);
            container.Register<IReservationDtoMapper, ReservationDtoMapper>(Reuse.Scoped);
            container.Register<IRestaurantDtoMapper, RestaurantDtoMapper>(Reuse.Scoped);
            container.Register<IRestaurantQueueDtoMapper, RestaurantQueueDtoMapper>(Reuse.Scoped);
            container.Register<IReviewDtoMapper, ReviewDtoMapper>(Reuse.Scoped);
            container.Register<IRoleDtoMapper, RoleDtoMapper>(Reuse.Scoped);
            container.Register<IUserDtoMapper, UserDtoMapper>(Reuse.Scoped);
        }

        public static void RegisterRepositories(this Container container)
        {
            container.Register<IClientRepository, ClientRepository>(Reuse.Scoped);
            container.Register<IDishRepository, DishRepository>(Reuse.Scoped);
            container.Register<IMenuRepository, MenuRepository>(Reuse.Scoped);
            container.Register<IReservationRepository, ReservationRepository>(Reuse.Scoped);
            container.Register<IRestaurantRepository, RestaurantRepository>(Reuse.Scoped);
            container.Register<IQueueRepository, QueueRepository>(Reuse.Scoped);
            container.Register<IReviewRepository, ReviewRepository>(Reuse.Scoped);
            container.Register<IRoleRepository, RoleRepository>(Reuse.Scoped);
            container.Register<IUserRepository, UserRepository>(Reuse.Scoped);
        }
    }
}
