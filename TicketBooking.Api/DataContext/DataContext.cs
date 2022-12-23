using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TicketBooking.Api.Entities;

namespace TicketBooking.Api.DataContext
{
    public class TicketBookingDataContext : DbContext
    {
        public TicketBookingDataContext(DbContextOptions<TicketBookingDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User Mapping
            modelBuilder.Entity<User_Role>()
               .HasOne(x => x.Role)
               .WithMany(y => y.UserRoles)
               .HasForeignKey(x => x.RoleID);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserID);


            //--Bus Coach 1 to Many
            modelBuilder.Entity<Bus>()
               .HasOne<Coach>(s => s.Coach)
               .WithMany(g => g.Buses)
               .HasForeignKey(s => s.CoachID);

            modelBuilder.Entity<Coach>()
                .HasMany<Bus>(g => g.Buses)
                .WithOne(s => s.Coach)
                .HasForeignKey(s => s.CoachID)
                .OnDelete(DeleteBehavior.NoAction);


            //BusRoute to Coach and City
            modelBuilder.Entity<BusRoute>()
               .HasOne<Coach>(s => s.Coach)
               .WithMany(g => g.BusRoutes)
               .HasForeignKey(s => s.CoachID);

            modelBuilder.Entity<BusRoute>()
               .HasOne<City>(s => s.FromCity)
               .WithMany(g => g.FromBusRoutes)
               .HasForeignKey(s => s.FromCityID);

            modelBuilder.Entity<BusRoute>()
              .HasOne<City>(s => s.ToCity)
              .WithMany(g => g.ToBusRoutes)
              .HasForeignKey(s => s.ToCityID);


            modelBuilder.Entity<Coach>()
               .HasMany<BusRoute>(g => g.BusRoutes)
               .WithOne(s => s.Coach)
               .HasForeignKey(s => s.CoachID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
               .HasMany<BusRoute>(g => g.FromBusRoutes)
               .WithOne(s => s.FromCity)
               .HasForeignKey(s => s.FromCityID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
               .HasMany<BusRoute>(g => g.ToBusRoutes)
               .WithOne(s => s.ToCity)
               .HasForeignKey(s => s.ToCityID)
               .OnDelete(DeleteBehavior.Restrict);

            //Bus Schedule To Bus and Route mapping
            modelBuilder.Entity<BusSchedule>()
              .HasOne<Bus>(s => s.Bus)
              .WithMany(g => g.BusSchedules)
              .HasForeignKey(s => s.BusID);

            modelBuilder.Entity<BusSchedule>()
              .HasOne<BusRoute>(s => s.BusRoute)
              .WithMany(g => g.BusSchedules)
              .HasForeignKey(s => s.RouteID);

            modelBuilder.Entity<Bus>()
              .HasMany<BusSchedule>(g => g.BusSchedules)
              .WithOne(s => s.Bus)
              .HasForeignKey(s => s.BusID)
              .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<BusRoute>()
              .HasMany<BusSchedule>(g => g.BusSchedules)
              .WithOne(s => s.BusRoute)
              .HasForeignKey(s => s.RouteID)
              .OnDelete(DeleteBehavior.NoAction);

            //BookingDetails to Schdule 1-Many
            modelBuilder.Entity<BookingDetails>()
               .HasOne<BusSchedule>(s => s.BusSchedule)
               .WithMany(g => g.BookingDetails)
               .HasForeignKey(s => s.ScheduleID);

            modelBuilder.Entity<BusSchedule>()
                .HasMany<BookingDetails>(g => g.BookingDetails)
                .WithOne(s => s.BusSchedule)
                .HasForeignKey(s => s.ScheduleID)
                .OnDelete(DeleteBehavior.NoAction);

            //BookingDetails to SeatDetails 1- Many
            modelBuilder.Entity<BookingSeatDetails>()
               .HasOne<BookingDetails>(s => s.BookingDetails)
               .WithMany(g => g.BookingSeatDetails)
               .HasForeignKey(s => s.BookingID);

            modelBuilder.Entity<BookingDetails>()
                .HasMany<BookingSeatDetails>(g => g.BookingSeatDetails)
                .WithOne(s => s.BookingDetails)
                .HasForeignKey(s => s.BookingID)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<City>()
                    .HasData(
                        new City
                        {
                            CityID = Guid.NewGuid(),
                            CityName = "Pune",
                            UpdateTime = DateTime.Now
                        },
                        new City
                        {
                            CityID = Guid.NewGuid(),
                            CityName = "Banglore",
                            UpdateTime = DateTime.Now
                        },
                         new City
                         {
                             CityID = Guid.NewGuid(),
                             CityName = "Surat",
                             UpdateTime = DateTime.Now
                         }
                    );

            modelBuilder.Entity<Coach>()
            .HasData(
                new Coach
                {
                    CoachID = Guid.NewGuid(),
                    CoachName = "AC",
                    UpdateTime = DateTime.Now
                },
                new Coach
                {
                    CoachID = Guid.NewGuid(),
                    CoachName = "Non-AC",
                    UpdateTime = DateTime.Now
                }
            );

            modelBuilder.Entity<User>()
           .HasData(
               new User
               {
                   UserID = new Guid("787BBF44-D8A6-4EEB-297A-08DAE2067A33"),
                   UserName = "readwrite@user.com",
                   EmailAddress = "readwrite@user.com",
                   Password = "readwrite@user",
                   FirstName = "Read Write",
                   LastName = "User"
               },
               new User
               {
                   UserID = new Guid("D31D6CB7-36D2-4434-2F49-08DAE211C0AB"),
                   UserName = "radonly@user.com",
                   EmailAddress = "radonly@user.com",
                   Password = "radonly@user",
                   FirstName = "Read Only",
                   LastName = "User"
               }
           );

            modelBuilder.Entity<Role>()
           .HasData(
               new Role
               {
                   ID = new Guid("EF77DFC4-CEDA-4F97-CE98-11DAE1F1F94C"),
                   Name = "reader",
               },
               new Role
               {
                   ID = new Guid("16820D32-1F50-4D99-CE9A-12DAE1F1F94C"),
                   Name = "writter",
               }
             );

            modelBuilder.Entity<User_Role>()
            .HasData(
                new User_Role
                {
                    ID = new Guid("787BBF44-D8A6-4EEB-297A-08DAE1111A33"),
                    UserID = new Guid("787BBF44-D8A6-4EEB-297A-08DAE2067A33"),
                    RoleID = new Guid("16820D32-1F50-4D99-CE9A-12DAE1F1F94C")
                },
                new User_Role
                {
                    ID = new Guid("706CDEDE-D481-4E2F-CE11-08DAE1F1F94C"),
                    UserID = new Guid("787BBF44-D8A6-4EEB-297A-08DAE2067A33"),
                    RoleID = new Guid("EF77DFC4-CEDA-4F97-CE98-11DAE1F1F94C")
                },
                 new User_Role
                 {
                     ID = new Guid("693575B0-7639-1010-F2B0-08DAE1F58ABF"),
                     UserID = new Guid("D31D6CB7-36D2-4434-2F49-08DAE211C0AB"),
                     RoleID = new Guid("EF77DFC4-CEDA-4F97-CE98-11DAE1F1F94C")
                 }
            );
        }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<BookingSeatDetails> BookingSeatDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
    }
}
