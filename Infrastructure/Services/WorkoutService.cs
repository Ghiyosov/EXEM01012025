using System.Net;
using DoMAin.DTOs;
using DoMAin.Entities;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class WorkoutService(Context _context) : IWorkoutService
{
    public async Task<Responce<List<ReadWorkoutDTO>>> ReadWorkouts(WorkoutFilter filter)
    {
        
        var c = _context.Workouts.Include(x=>x.WorkoutSession).AsQueryable();
        if (filter.Name != null)
            c = c.Where(b => b.Name.ToLower().Contains(filter.Name.ToLower()));
        if (filter.Description != null)
            c = c.Where(b => b.Description.ToLower().Contains(filter.Description.ToLower()));
        if (filter.IsActive != null)
            c = c.Where(b => b.IsActive == filter.IsActive);
        if (filter.Difficulty != null)
            c = c.Where(b => b.Difficulty == filter.Difficulty);

        
        
        var workouts = c.Select(f=> new ReadWorkoutDTO()
        {
            WorkoutId = f.WorkoutId,
            Name = f.Name,
            Description = f.Description,
            IsActive = f.IsActive,
            Difficulty = f.Difficulty,
            Duration = f.Duration,
            MaxParticipants = f.MaxParticipants,
            Sessions = f.WorkoutSession.Select(w => new UpdateWorkoutSessionDTO()
            {
                WorkoutSessionId = w.WorkoutSessionId,
                TrainerId = w.TrainerId,
                WorkoutId = w.WorkoutId,
                ClientId = w.ClientId,
                SessionDate = w.SessionDate,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                Status = w.Status,
                MaxCapacity = w.MaxCapacity,
                CurrentParticipants = w.CurrentParticipants,
                TypeComment = w.TypeComment,
                CreatedAt = w.CreatedAt
            }).ToList()
        }).ToList();
        return new Responce<List<ReadWorkoutDTO>>(workouts);
    }

    public async Task<Responce<ReadWorkoutDTO>> ReadWorkout(int id)
    {
        var f = await _context.Workouts.Include(x=>x.WorkoutSession).FirstOrDefaultAsync(j=>j.WorkoutId == id);
        if (f == null)
            return new Responce<ReadWorkoutDTO>(HttpStatusCode.NotFound, "Workout not found");

        var workout = new ReadWorkoutDTO()
        {
            WorkoutId = f.WorkoutId,
            Name = f.Name,
            Description = f.Description,
            IsActive = f.IsActive,
            Difficulty = f.Difficulty,
            Duration = f.Duration,
            MaxParticipants = f.MaxParticipants,
            Sessions = f.WorkoutSession.Select(w => new UpdateWorkoutSessionDTO()
            {
                WorkoutSessionId = w.WorkoutSessionId,
                TrainerId = w.TrainerId,
                WorkoutId = w.WorkoutId,
                ClientId = w.ClientId,
                SessionDate = w.SessionDate,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                Status = w.Status,
                MaxCapacity = w.MaxCapacity,
                CurrentParticipants = w.CurrentParticipants,
                TypeComment = w.TypeComment,
                CreatedAt = w.CreatedAt
            }).ToList()
        };
        
        return new Responce<ReadWorkoutDTO>(workout);
    }

    public async Task<Responce<string>> CreateWorkout(CreateWorkoutDTO dto)
    {
        var work = new Workout()
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive,
            Difficulty = dto.Difficulty,
            Duration = dto.Duration,
            MaxParticipants = dto.MaxParticipants
        };
        await _context.Workouts.AddAsync(work);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Workout created");

    }

    public async Task<Responce<string>> UpdateWorkout(UpdateWorkoutDTO dto)
    {
        var f = await _context.Workouts.FirstOrDefaultAsync(j=>j.WorkoutId == dto.WorkoutId);
        if (f == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Workout not found");
        
        f.Name = dto.Name;
        f.Description = dto.Description;
        f.IsActive = dto.IsActive;
        f.Duration = dto.Duration;
        f.MaxParticipants = dto.MaxParticipants;
        f.Difficulty = dto.Difficulty;
        var result = await _context.SaveChangesAsync();
            return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Workout updated");

    }

    public async Task<Responce<string>> DeleteWorkout(int id)
    {
        var f = await _context.Workouts.Include(x=>x.WorkoutSession).FirstOrDefaultAsync(j=>j.WorkoutId == id);
        if (f == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Workout not found");
        _context.Workouts.Remove(f);
        var result = await _context.SaveChangesAsync();
        return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Workout deleted");

    }
}