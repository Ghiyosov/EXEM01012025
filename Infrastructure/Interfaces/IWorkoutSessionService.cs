using DoMAin.DTOs;
using DoMAin.Enums;
using DoMAin.Filters;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IWorkoutSessionService
{
    public Task<Responce<List<ReadWorkoutSessionDTO>>> ReadWorkoutSessions(SessionFilter filter);
    public Task<Responce<string>> AddWorkoutSession(CreateWorkoutSessionDTO dto);
    public Task<Responce<string>> UpdateWorkoutSessionStatus(int id, SessionStatus status);
    
    public Task<Responce<string>> AddClientToWorkoutSession(int clientId, int workoutSessionId);
    
    public Task<Responce<string>> RemoveClientFromWorkoutSession(int clientId);
    
    public Task<Responce<string>> CanceledWorkoutSession(int id);
}