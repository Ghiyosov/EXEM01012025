using System.Net;
using DoMAin.DTOs;
using DoMAin.Entities;
using DoMAin.Enums;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class WorkoutSessionService(Context _context) : IWorkoutSessionService
{
    public async Task<Responce<List<ReadWorkoutSessionDTO>>> ReadWorkoutSessions(SessionFilter filter)
    {
        var c = _context.WorkoutSessions
            .Include(q=>q.Workout)
            .Include(w=>w.Client)
            .Include(e=>e.Workout).AsQueryable();
        
        if (filter.Status != null)
            c.Where(q => q.Status == filter.Status);

        var sessions = c.Select(v => new ReadWorkoutSessionDTO()
        {
            WorkoutSessionId =v.WorkoutSessionId,
            TrainerId = v.TrainerId,
            WorkoutId = v.WorkoutId,
            ClientId = v.ClientId,
            SessionDate = v.SessionDate,
            StartTime = v.StartTime,
            EndTime = v.EndTime,
            Status = v.Status,
            MaxCapacity = v.MaxCapacity,
            CurrentParticipants = v.CurrentParticipants,
            TypeComment = v.TypeComment,
            CreatedAt = v.CreatedAt,
            Trainer = new CreateTrainerDTO()
            {
                FirstName = v.Trainer.FirstName,
                LastName = v.Trainer.LastName,
                PhoneNumber = v.Trainer.PhoneNumber,
                Experience = v.Trainer.Experience,
                Status = v.Trainer.Status,
                Specialization  = v.Trainer.Specialization
            },
            Workout = new CreateWorkoutDTO()
            {
                Name = v.Workout.Name,
                Description = v.Workout.Description,
                Duration = v.Workout.Duration,
                MaxParticipants = v.Workout.MaxParticipants,
                Difficulty = v.Workout.Difficulty,
                IsActive  = v.Workout.IsActive
            },
            Client = new CreateClientDTO()
            {
                FirstName = v.Client.FirstName,
                LastName = v.Client.LastName,
                PhoneNumber = v.Client.PhoneNumber,
                Email = v.Client.Email,
                DateOfBirth = v.Client.DateOfBirth,
                MembershipStatus = v.Client.MembershipStatus
            }
        }).ToList();
        
        return new Responce<List<ReadWorkoutSessionDTO>>(sessions);
    }

    public async Task<Responce<string>> AddWorkoutSession(CreateWorkoutSessionDTO dto)
    {
        var session = new WorkoutSession()
        {
            TrainerId = dto.TrainerId,
            WorkoutId = dto.WorkoutId,
            ClientId = dto.ClientId,
            SessionDate = dto.SessionDate,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = dto.Status,
            MaxCapacity = dto.MaxCapacity,
            CurrentParticipants = dto.CurrentParticipants,
            TypeComment = dto.TypeComment,
            CreatedAt = dto.CreatedAt
        };
        await _context.WorkoutSessions.AddAsync(session);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Workout Session Created");
    }

    public async Task<Responce<string>> UpdateWorkoutSessionStatus(int id, SessionStatus status)
    {
        var session = await _context.WorkoutSessions.FirstOrDefaultAsync(c => c.WorkoutSessionId == id);
        if (session == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Workout Session Not Found");
        session.Status = status;
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Workout Session status updated");

    }

    public async Task<Responce<string>> AddClientToWorkoutSession(int clientId, int workoutSessionId)
    {
        var session = await _context.WorkoutSessions.FirstOrDefaultAsync(c => c.WorkoutSessionId == workoutSessionId);
        if (session == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Workout Session Not Found");

        var sessionClient = new WorkoutSession()
        {
            TrainerId = session.TrainerId,
            WorkoutId = session.WorkoutId,
            ClientId = clientId,
            SessionDate = session.SessionDate,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Status = session.Status,
            MaxCapacity = session.MaxCapacity,
            CurrentParticipants = session.CurrentParticipants,
            TypeComment = session.TypeComment,
            CreatedAt = DateTime.Now
        };
        await _context.WorkoutSessions.AddAsync(session);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Client added to  Workout Session ");


    }

    public async Task<Responce<string>> RemoveClientFromWorkoutSession(int clientId)
    {
        var session =  _context.WorkoutSessions.Where(c => c.ClientId == clientId);
        if (session == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Workout Session Not Found");
        _context.WorkoutSessions.RemoveRange(session);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Client   Workout Session  deleted");

    }

    public async Task<Responce<string>> CanceledWorkoutSession(int id)
    {
        var session = await _context.WorkoutSessions.FirstOrDefaultAsync(c => c.WorkoutSessionId == id);
        if (session == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Workout Session Not Found");
        session.Status = SessionStatus.Cancelled;
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Workout Session status canceled");
    }
}