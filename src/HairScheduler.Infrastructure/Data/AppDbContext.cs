using HairScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HairScheduler.Infrastructure.Data;

internal class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Schedule> Schedules { get; set; }

}
