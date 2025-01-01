using DoMAin.DTOs;
using DoMAin.Filters;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IWorkoutService
{
    public Task<Responce<List<ReadWorkoutDTO>>> ReadWorkouts(WorkoutFilter filter);
    public Task<Responce<ReadWorkoutDTO>> ReadWorkout(int id);
    public Task<Responce<string>> CreateWorkout(CreateWorkoutDTO dto);
    public Task<Responce<string>> UpdateWorkout(UpdateWorkoutDTO dto);
    public Task<Responce<string>> DeleteWorkout(int id);
}