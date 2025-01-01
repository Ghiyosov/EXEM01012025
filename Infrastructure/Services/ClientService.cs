using System.Net;
using DoMAin.DTOs;
using DoMAin.Entities;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ClientService(Context _context) : IClientService
{
    public async Task<Responce<List<ReadClientDTO>>> RedaClients(ClienFilter filter)
    {
        var c =  _context.Clients.Include(v => v.WorkoutSession).AsQueryable();
        if (filter.Status != null)
            c = c.Where(z=>z.MembershipStatus == filter.Status);
        if (filter.Name != null)
            c=c.Where(z=>z.FirstName.ToLower().Contains(filter.Name.ToLower()) || z.LastName.ToLower().Contains(filter.Name.ToLower()) );
        
        var clients = c.Select(x => new ReadClientDTO()
        {
            ClientId = x.ClientId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            MembershipStatus = x.MembershipStatus,
            Sessions = x.WorkoutSession.Select(w=> new UpdateWorkoutSessionDTO()
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
        
        return new Responce<List<ReadClientDTO>>(clients);
    }

    public async Task<Responce<ReadClientDTO>> ReadClient(int Id)
    {
        var x = await _context.Clients.Include(v => v.WorkoutSession).FirstOrDefaultAsync(c=>c.ClientId==Id);
        if (x == null)
            return new Responce<ReadClientDTO>(HttpStatusCode.NotFound , "Not Found");
        var client = new ReadClientDTO()
        {
            ClientId = x.ClientId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            MembershipStatus = x.MembershipStatus,
            Sessions = x.WorkoutSession.Select(w => new UpdateWorkoutSessionDTO()
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
        return new Responce<ReadClientDTO>(client);
    }

    public async Task<Responce<string>> AddClient(CreateClientDTO dto)
    {
        var client = new Client()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            MembershipStatus = dto.MembershipStatus
        };
        await _context.Clients.AddAsync(client);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Client Added");
    }

    public async Task<Responce<string>> UpdateClient(UpdateClientDTO dto)
    {
        var x = await _context.Clients.Include(v => v.WorkoutSession).FirstOrDefaultAsync(c=>c.ClientId==dto.ClientId);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound , "Not Found");
        
        x.FirstName = dto.FirstName;
        x.LastName = dto.LastName;
        x.PhoneNumber = dto.PhoneNumber;
        x.Email = dto.Email;
        x.DateOfBirth = dto.DateOfBirth;
        x.MembershipStatus = dto.MembershipStatus;
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Client Updated");

    }

    public async Task<Responce<string>> DeleteClient(int Id)
    {
        var x = await _context.Clients.Include(v => v.WorkoutSession).FirstOrDefaultAsync(c=>c.ClientId==Id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound , "Not Found");
        
        _context.Clients.Remove(x);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Client deleted");

    }
}