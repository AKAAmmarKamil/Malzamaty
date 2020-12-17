using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Model
{
    public class TheContext:DbContext 
    {
        public TheContext(DbContextOptions<TheContext> options) : base(options)
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
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<ClassType> ClassType { get; set; }

    }
}