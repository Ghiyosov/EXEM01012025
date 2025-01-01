using DoMAin.Entities;
using DoMAin.Enums;

namespace DoMAin.DTOs;

public class TrainerBaseDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int Experience { get; set; }
    public TrainerStatus Status { get; set; }
    public string Specialization { get; set; }
}


public class CreateTrainerDTO : TrainerBaseDTO;

public class UpdateTrainerDTO : TrainerBaseDTO
{
    public int TrainerId { get; set; }
}

public class ReadTrainerDTO : UpdateTrainerDTO
{
    public List<UpdateWorkoutSessionDTO> Sessions { get; set; }
}