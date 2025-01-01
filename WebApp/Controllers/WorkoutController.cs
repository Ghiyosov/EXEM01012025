using DoMAin.DTOs;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class WorkoutController(IWorkoutService _service)
{
    [HttpGet("GetWorkouts")]
    public async Task<Responce<List<ReadWorkoutDTO>>> ReadWorkouts([FromQuery]WorkoutFilter filter) 
        => await _service.ReadWorkouts(filter);
    
    [HttpGet("GetWorkouts/{id}")]
    public async Task<Responce<ReadWorkoutDTO>> ReadWorkout(int id)
        => await _service.ReadWorkout(id);
    
    [HttpPost("AddWorkout")]
    public async Task<Responce<string>> CreateWorkout(CreateWorkoutDTO dto)
        => await _service.CreateWorkout(dto);
    
    [HttpPut("UpdateWorkout")]
    public async Task<Responce<string>> UpdateWorkout(UpdateWorkoutDTO dto)
        => await _service.UpdateWorkout(dto);
    
    [HttpDelete("DeleteWorkout/{id}")]
    public async Task<Responce<string>> DeleteWorkout(int id)
        => await _service.DeleteWorkout(id);
}