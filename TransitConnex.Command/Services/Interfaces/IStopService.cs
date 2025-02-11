using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services.Interfaces;

public interface IStopService
{
    Task<Stop?> GetStopById(Guid id);
    Task<List<StopDto>> GetFilteredStops(StopFilteredQuery filter);

    Task<Stop> CreateStop(StopCreateCommand createCommand);
    Task<List<Stop>> CreateStops(List<StopCreateCommand> createCommands);
    Task<Stop> EditStop(StopUpdateCommand editCommand);
    Task DeleteStop(Guid id);

    Task AssignStopToLocation(StopLocationCommand addCommand);
    Task RemoveStopFromLocation(StopLocationCommand removeCommand);
} 
