using System.ComponentModel.DataAnnotations;
using DoMAin.Entities;
using DoMAin.Enums;

namespace DoMAin.DTOs;

public class BaseWorkoutSessionDTO
{
    public int TrainerId  { get; set; }
    public int WorkoutId { get; set; }
    public int ClientId { get; set; }
    public DateTime SessionDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public SessionStatus Status { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentParticipants  { get; set; }
    public int TypeComment { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateWorkoutSessionDTO : BaseWorkoutSessionDTO;

public class UpdateWorkoutSessionDTO : BaseWorkoutSessionDTO
{
    public int WorkoutSessionId { get; set; }
}

public class ReadWorkoutSessionDTO : UpdateWorkoutSessionDTO
{
    public CreateTrainerDTO Trainer { get; set; }
    public CreateWorkoutDTO Workout { get; set; }
    public CreateClientDTO Client { get; set; }
}