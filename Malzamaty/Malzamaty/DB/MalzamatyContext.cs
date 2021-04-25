using Malzamaty.Model;
using Microsoft.EntityFrameworkCore;

namespace Malzamaty
{
    public class MalzamatyContext : DbContext
    {
        public MalzamatyContext(DbContextOptions<MalzamatyContext> options) : base(options)
        {

        }
        public DbSet<Class> Class { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Interests> Interests { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<ClassType> ClassType { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Mahallah> Mahallah { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Taxes> Taxes { get; set; }

    }
}