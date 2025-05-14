using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Data;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> options) : base(options) { }
    
    public DbSet<WeightMeasurement> WeightMeasurements { get; set; }
}