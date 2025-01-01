using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoMAin.Enums;

namespace DoMAin.Entities;

public class Workout
{
    [Key]
    public int WorkoutId { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    
    public int Duration { get; set; }
    public int MaxParticipants { get; set; }
    public WorkoutDifficulty Difficulty { get; set; }
    public bool IsActive { get; set; }
    
    [ForeignKey("WorkoutId")]
    public List<WorkoutSession> WorkoutSession { get; set; }
    
}