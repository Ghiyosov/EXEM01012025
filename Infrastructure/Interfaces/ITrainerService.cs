using DoMAin.DTOs;
using DoMAin.Entities;
using DoMAin.Filters;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface ITrainerService
{
    public Task<Responce<List<ReadTrainerDTO>>> GetTrainers(TrainerFilter trainerFilter);
    public Task<Responce<ReadTrainerDTO>> ReadTrainer(int id);
    public Task<Responce<string>> AddTrainer(CreateTrainerDTO trainerDTO);
    public Task<Responce<string>> UpdateTrainer(UpdateTrainerDTO trainerDTO);
    public Task<Responce<string>> DeleteTrainer(int id);
}