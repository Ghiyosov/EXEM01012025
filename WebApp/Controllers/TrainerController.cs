using DoMAin.DTOs;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class TrainerController(ITrainerService _service)
{
    [HttpGet("GetTrainers")]
    public async Task<Responce<List<ReadTrainerDTO>>> GetTrainers([FromQuery]TrainerFilter trainerFilter)
        => await _service.GetTrainers(trainerFilter);
    
    [HttpGet("GetTrainer")]
    public async Task<Responce<ReadTrainerDTO>> ReadTrainer(int id)
        => await _service.ReadTrainer(id);
    
    [HttpPost("AddTrainer")]
    public async Task<Responce<string>> AddTrainer(CreateTrainerDTO trainerDTO)
        => await _service.AddTrainer(trainerDTO);
    
    [HttpPut("UpdateTrainer")]
    public async Task<Responce<string>> UpdateTrainer(UpdateTrainerDTO trainerDTO)
        => await _service.UpdateTrainer(trainerDTO);
    
    [HttpDelete("DeleteTrainer")]
    public async Task<Responce<string>> DeleteTrainer(int id)
        => await _service.DeleteTrainer(id);
}