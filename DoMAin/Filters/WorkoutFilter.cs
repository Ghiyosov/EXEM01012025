using DoMAin.Enums;

namespace DoMAin.Filters;

public class WorkoutFilter
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public WorkoutDifficulty? Difficulty { get; set; }
    public bool IsActive { get; set; }
}