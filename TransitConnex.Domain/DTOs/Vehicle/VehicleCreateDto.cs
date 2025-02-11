using TransitConnex.Domain.Enums;

namespace TransitConnex.Domain.DTOs.Vehicle;

public class VehicleCreateDto
{
    public required string Label { get; set; }
    public required string Spz { get; set; }
    public required string Manufacturer { get; set; }

    public required int Capacity { get; set; }

    // public required int VehicleType { get; set; }
    public required VehicleTypeEnum VehicleType { get; set; }
    public Guid? IconId { get; set; }
    public Guid? LineId { get; set; }
}
