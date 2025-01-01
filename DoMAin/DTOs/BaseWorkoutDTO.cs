using DoMAin.Entities;
using DoMAin.Enums;

namespace DoMAin.DTOs;

public class BaseWorkoutDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public int Duration { get; set; }
    public int MaxParticipants { get; set; }
    public WorkoutDifficulty Difficulty { get; set; }
    public bool IsActive { get; set; }
}

public class CreateWorkoutDTO : BaseWorkoutDTO;

public class UpdateWorkoutDTO : BaseWorkoutDTO
{
    public int WorkoutId { get; set; }
}

public class ReadWorkoutDTO : UpdateWorkoutDTO
{
    public List<UpdateWorkoutSessionDTO> Sessions { get; set; }
}