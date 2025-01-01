using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoMAin.Enums;

namespace DoMAin.Entities;

public class Client
{
    [Key]
    public int ClientId { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    public DateTime DateOfBirth { get; set; }
    public MembershipStatus MembershipStatus { get; set; }
    
    [ForeignKey("ClientId")]
    public List<WorkoutSession> WorkoutSession { get; set; }

}