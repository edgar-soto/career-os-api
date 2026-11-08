using CareerOs.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerOs.Api.Data;

public class CareerOsDbContext : DbContext
{
    public CareerOsDbContext(DbContextOptions<CareerOsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Goal> Goals => Set<Goal>();
}