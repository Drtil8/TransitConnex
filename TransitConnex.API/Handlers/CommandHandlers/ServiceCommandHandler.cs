using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Service;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class ServiceCommandHandler(IServiceService serviceService) : IBaseCommandHandler<IServiceCommand>
{
    public async Task<Guid> HandleCreate(IServiceCommand command) 
    {
        if (command is not ServiceCreateCommand createCommand)
        {
            throw new InvalidCastException("Invalid command given, expected ServiceCreateCommand.");
        }

        var created = await serviceService.CreateService(createCommand);

        return created.Id;
    }

    public async Task HandleUpdate(IServiceCommand command)
    {
        if (command is not ServiceUpdateCommand updateCommand)
        {
            throw new InvalidCastException("Invalid command given, expected ServiceUpdateCommand.");
        }

        await serviceService.EditService(updateCommand);
    }

    public async Task HandleDelete(Guid id)
    {
        await serviceService.DeleteService(id);
    }
}
