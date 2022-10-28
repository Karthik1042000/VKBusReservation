using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace VKBusReservation.Models
{
    public class VKBusReservationDbContext : DbContext
    {
        public VKBusReservationDbContext()
        {

        }
        public VKBusReservationDbContext(DbContextOptions<VKBusReservationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
