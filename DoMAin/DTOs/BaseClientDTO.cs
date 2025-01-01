using DoMAin.Entities;
using DoMAin.Enums;

namespace DoMAin.DTOs;

public class BaseClientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public DateTime DateOfBirth { get; set; }
    public MembershipStatus MembershipStatus { get; set; }
}

public class CreateClientDTO : BaseClientDTO;

public class UpdateClientDTO : BaseClientDTO
{
    public int ClientId { get; set; }
}

public class ReadClientDTO : UpdateClientDTO
{
    public List<UpdateWorkoutSessionDTO> Sessions { get; set; }

}