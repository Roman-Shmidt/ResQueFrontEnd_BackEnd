using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ResQueDal.ClientDomain;
using ResQueDal.DishDomain;
using ResQueDal.MenuDomain;
using ResQueDal.QueueDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.RestaurantDomain;
using ResQueDal.ReviewDomain;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;

namespace ResQueDal.Infrastructure;

public class ResQueDbContext
    : IdentityDbContext<User,Role,int,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
{
    public ResQueDbContext(DbContextOptions<ResQueDbContext> options)
        : base(options)
    {
    }

    /// <summary> 
    /// The tenants. 
    /// </summary> 
    public DbSet<Client> Clients { get; set; }

    /// <summary> 
    /// The domain services. 
    /// </summary> 
    public DbSet<Dish> Dishes { get; set; }

    ///// <summary> 
    ///// The contracts. 
    ///// </summary> 
    public DbSet<Menu> Menus { get; set; }

    /// <summary> 
    /// The domain event properties. 
    /// </summary> 
    public DbSet<RestaurantQueue> RestaurantQueues { get; set; }

    /// <summary> 
    /// The service commands. 
    /// </summary> 
    public DbSet<Reservation> Reservations { get; set; }

    /// <summary> 
    /// The service command properties. 
    /// </summary> 
    public DbSet<Restaurant> Restaurants { get; set; }

    /// <summary> 
    /// The service settings. 
    /// </summary> 
    public DbSet<Review> Reviews { get; set; }

    /// <summary> 
    /// The overridden service settings. 
    /// </summary> 
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        //optionsBuilder.UseSqlServer("Data Source=DESKTOP-RNBJH2M;Initial Catalog=ResQue;Integrated Security=True;MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;"); 
        optionsBuilder.UseSqlServer("Server=db;Database=ResQue;User Id=sa;Password=YourPassword123!;MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;",
            builder => builder.EnableRetryOnFailure());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}