using DryIoc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResQueBackEnd.Helpers;
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
using ResQueBackEnd.Managers.UserDomain.PasswordValidator;
using ResQueDal.ClientDomain;
using ResQueDal.Common.Reading;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.DishDomain;
using ResQueDal.Infrastructure;
using ResQueDal.MenuDomain;
using ResQueDal.QueueDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.RestaurantDomain;
using ResQueDal.ReviewDomain;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;
using System.ComponentModel;
using System.Xml.Linq;

namespace ResQue.Infrastructure
{
    public static class ExtensionMethods
    {
        public static void AddDbContextStuff(this IServiceCollection services)
        {
            services.AddDbContext<ResQueDbContext>(options =>
                options.UseSqlServer("Server=db;Database=ResQue;User Id=sa;Password=YourPassword123!;MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;"));
        }

        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<Reader<Client>>();
            services.AddScoped<Reader<Dish>>();
            services.AddScoped<Reader<Menu>>();
            services.AddScoped<Reader<Reservation>>();
            services.AddScoped<Reader<Restaurant>>();
            services.AddScoped<Reader<RestaurantQueue>>();
            services.AddScoped<Reader<User>>();
            services.AddScoped<Reader<Role>>();
            services.AddScoped<Reader<Review>>();

            services.AddScoped<ParseRequestParamsAttribute>();
            services.AddScoped<JsonApiQueryParser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IPasswordPolicyValidator, PasswordPolicyValidator>();
        }

        public static void AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IClientManager, ClientManager>();
            services.AddScoped<IDishManager, DishManager>();
            services.AddScoped<IMenuManager, MenuManager>();
            services.AddScoped<IReservationManager, ReservationManager>();
            services.AddScoped<IRestaurantManager, RestaurantManager>();
            services.AddScoped<IRestaurantQueueManager, RestaurantQueueManager>();
            services.AddScoped<IReviewManager, ReviewManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IUserManager, UserManager>();
        }

        public static void AddScopedMappers(this IServiceCollection services)
        {
            services.AddScoped<IClientDtoMapper, ClientDtoMapper>();
            services.AddScoped<IDishDtoMapper, DishDtoMapper>();
            services.AddScoped<IMenuDtoMapper, MenuDtoMapper>();
            services.AddScoped<IReservationDtoMapper, ReservationDtoMapper>();
            services.AddScoped<IRestaurantDtoMapper, RestaurantDtoMapper>();
            services.AddScoped<IRestaurantQueueDtoMapper, RestaurantQueueDtoMapper>();
            services.AddScoped<IReviewDtoMapper, ReviewDtoMapper>();
            services.AddScoped<IRoleDtoMapper, RoleDtoMapper>();
            services.AddScoped<IUserDtoMapper, UserDtoMapper>();
        }

        public static void AddScopedRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IQueueRepository, QueueRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddScopedFilters(this IServiceCollection services)
        {
            services.AddScoped<FilteringByIdHandler<Restaurant>>();
            services.AddScoped<FilteringByIdHandler<Dish>>();
            services.AddScoped<FilteringByIdHandler<Menu>>();
            services.AddScoped<FilteringByIdHandler<RestaurantQueue>>();
            services.AddScoped<FilteringByIdHandler<Reservation>>();
            services.AddScoped<FilteringByIdHandler<Review>>();
            services.AddScoped<FilteringByIdHandler<Client>>();

            services.AddScoped<FilteringByRestaurantIdHandler<Review>>();
            services.AddScoped<FilteringByRestaurantIdHandler<RestaurantQueue>>();
            services.AddScoped<FilteringByRestaurantIdHandler<Reservation>>();

            services.AddScoped<FilteringByClientIdHandler<Review>>();
            services.AddScoped<FilteringByClientIdHandler<RestaurantQueue>>();
            services.AddScoped<FilteringByClientIdHandler<Reservation>>();

            services.AddScoped<FilteringByUserIdHandler<Restaurant>>();
            services.AddScoped<FilteringByUserIdHandler<Client>>();

            services.AddScoped<FilteringByMenuIdHandler<Dish>>();

            services.AddScoped<FilteringByRestaurantIdMandatoryHandler<Menu>>();
        }
    }
}
