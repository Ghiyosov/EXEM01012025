using System.Net;
using DoMAin.DTOs;
using DoMAin.Entities;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TrainerService(Context _context) : ITrainerService
{
    public async Task<Responce<List<ReadTrainerDTO>>> GetTrainers(TrainerFilter trainerFilter)
    {
        
        
        var c = _context.Trainers.Include(x=>x.WorkoutSession).AsQueryable();
        
        if (trainerFilter.Status != null)
            c = c.Where(x => x.Status == trainerFilter.Status);
        if (trainerFilter.Name != null)
            c = c.Where(b=>b.FirstName.ToLower().Contains(trainerFilter.Name.ToLower()) || b.LastName.ToLower().Contains(trainerFilter.Name.ToLower()));
        
        var trainers = c.Select(f=> new ReadTrainerDTO()
        {
            TrainerId = f.TrainerId,
            FirstName = f.FirstName,
            LastName = f.LastName,
            PhoneNumber = f.PhoneNumber,
            Experience = f.Experience,
            Status = f.Status,
            Specialization = f.Specialization,
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
        return new Responce<List<ReadTrainerDTO>>(trainers);
    }

    public async Task<Responce<ReadTrainerDTO>> ReadTrainer(int id)
    {
        var f = await _context.Trainers.Include(x=>x.WorkoutSession).FirstOrDefaultAsync(x => x.TrainerId == id);
        if (f == null)
            return new Responce<ReadTrainerDTO>(HttpStatusCode.NotFound, "Trainer not found");
        var trainer = new ReadTrainerDTO()
        {
            TrainerId = f.TrainerId,
            FirstName = f.FirstName,
            LastName = f.LastName,
            PhoneNumber = f.PhoneNumber,
            Experience = f.Experience,
            Status = f.Status,
            Specialization = f.Specialization,
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
        return new Responce<ReadTrainerDTO>(trainer);
    }

    public async Task<Responce<string>> AddTrainer(CreateTrainerDTO trainerDTO)
    {
        var trainer = new Trainer()
        {
            FirstName = trainerDTO.FirstName,
            LastName = trainerDTO.LastName,
            PhoneNumber = trainerDTO.PhoneNumber,
            Experience = trainerDTO.Experience,
            Status = trainerDTO.Status,
            Specialization = trainerDTO.Specialization
        };
        _context.Trainers.Add(trainer);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Trainer Added");

    }

    public async Task<Responce<string>> UpdateTrainer(UpdateTrainerDTO trainerDTO)
    {
        var f = await _context.Trainers.FirstOrDefaultAsync(x => x.TrainerId == trainerDTO.TrainerId);
        if (f == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Trainer not found");
        
        f.FirstName = trainerDTO.FirstName;
        f.LastName = trainerDTO.LastName;
        f.PhoneNumber = trainerDTO.PhoneNumber;
        f.Experience = trainerDTO.Experience;
        f.Status = trainerDTO.Status;
        f.Specialization = trainerDTO.Specialization;
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Trainer updated");

    }

    public async Task<Responce<string>> DeleteTrainer(int id)
    {
        var f = await _context.Trainers.FirstOrDefaultAsync(x => x.TrainerId == id);
        if (f == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Trainer not found");
        _context.Trainers.Remove(f);
        var result = await _context.SaveChangesAsync(); 
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Trainer deleted");
    }
}