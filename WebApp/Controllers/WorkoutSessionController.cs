using DoMAin.DTOs;
using DoMAin.Enums;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class WorkoutSessionController(IWorkoutSessionService _service)
{
    [HttpGet("GetWorkoutSessions")]
    public async Task<Responce<List<ReadWorkoutSessionDTO>>> ReadWorkoutSessions([FromQuery]SessionFilter filter)
        => await _service.ReadWorkoutSessions(filter);
    
    [HttpPost("AddWorkoutSession")]
    public async Task<Responce<string>> AddWorkoutSession(CreateWorkoutSessionDTO dto)
        => await _service.AddWorkoutSession(dto);
    
    [HttpPut("UpdateWorkoutSession")]
    public async Task<Responce<string>> UpdateWorkoutSessionStatus(int id, SessionStatus status)
        => await _service.UpdateWorkoutSessionStatus(id, status);
    
    [HttpDelete("AddClientToWorkoutSession")]
    public async Task<Responce<string>> AddClientToWorkoutSession(int clientId, int workoutSessionId)
        => await _service.AddClientToWorkoutSession(clientId, workoutSessionId);
    
    [HttpDelete("RemoveClientFromWorkoutSession")]
    public async Task<Responce<string>> RemoveClientFromWorkoutSession(int clientId)
        => await _service.RemoveClientFromWorkoutSession(clientId);
    
    [HttpPut("CanceledWorkoutSession")]
    public async Task<Responce<string>> CanceledWorkoutSession(int id)
        => await _service.CanceledWorkoutSession(id);

}