using TransitConnex.Command.Commands.Vehicle;
using TransitConnex.Domain.DTOs.Vehicle;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetAllVehicles();
    Task<VehicleDto> GetVehicleById(Guid id);
    Task<Vehicle> CreateVehicle(VehicleCreateCommand createCommand);
    

    Task<Vehicle> EditVehicle(VehicleUpdateCommand editCommand);
    Task DeleteVehicle(Guid id);
    Task<IEnumerable<ScheduledRoute>> ReplaceVehicleOnScheduledRoutes(VehicleReplaceOnScheduledCommand replaceCommand);
}
