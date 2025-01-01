using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoMAin.Enums;

namespace DoMAin.Entities;

public class Trainer
{
    [Key]
    public int TrainerId { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public int Experience { get; set; }
    public TrainerStatus Status { get; set; }
    public int Type { get; set; }
    public string Specialization { get; set; }
    
    [ForeignKey("TrainerId")]
    public List<WorkoutSession> WorkoutSession { get; set; }
}