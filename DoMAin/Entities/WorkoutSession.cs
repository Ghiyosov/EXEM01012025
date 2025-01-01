using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoMAin.Enums;

namespace DoMAin.Entities;

public class WorkoutSession
{
    [Key]
    public int WorkoutSessionId { get; set; }

    public int TrainerId  { get; set; }
    public int WorkoutId { get; set; }
    public int ClientId { get; set; }
    public DateTime SessionDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public SessionStatus Status { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentParticipants  { get; set; }
    [MaxLength(200)]
    public int TypeComment { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey("TrainerId")]
    public Trainer Trainer { get; set; }
    [ForeignKey("WorkoutId")]
    public Workout Workout { get; set; }
    [ForeignKey("ClientId")]
    public Client Client { get; set; }
}