using DoMAin.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Client>()
            .HasMany(c=>c.WorkoutSession)
            .WithOne(c=>c.Client)
            .HasForeignKey(c=>c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Trainer>()
            .HasMany(t=>t.WorkoutSession)
            .WithOne(t=>t.Trainer)
            .HasForeignKey(t=>t.TrainerId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Workout>()
            .HasMany(w=>w.WorkoutSession)
            .WithOne(w=>w.Workout)
            .HasForeignKey(w=>w.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}