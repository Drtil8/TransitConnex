using System.ComponentModel.DataAnnotations;
using TransitConnex.Domain.Enums;

namespace TransitConnex.Command.Commands.Stop;

public class StopUpdateCommand : IStopCommand
{
    public required Guid Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public required StopTypeEnum StopType { get; set; }
}
