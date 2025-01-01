using DoMAin.DTOs;
using DoMAin.Filters;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IWorkoutSessionService
{
    public Task<Responce<List<ReadWorkoutSessionDTO>>> ReadWorkoutSessions(SessionFilter filter);
    public Task<Responce<ReadWorkoutSessionDTO>> ReadWorkoutSession(int id);
    public Task<Responce<string>> AddWorkoutSession(CreateWorkoutSessionDTO dto);
    public Task<Responce<string>> UpdateWorkoutSession(UpdateWorkoutSessionDTO dto);
    public Task<Responce<string>> DeleteWorkoutSession(int id);
}