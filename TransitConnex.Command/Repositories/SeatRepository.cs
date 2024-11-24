using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Seat;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories;

public class SeatRepository : BaseRepository<Seat, SeatUpdateCommand>, ISeatRepository
{
    private readonly AppDbContext _db;

    public SeatRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public IQueryable<Seat> QueryById(Guid id)
    {
        return QueryAll().Where(x => x.Id == id);
    }

    public async Task<List<Seat>> QueryAvailableSeats(ScheduledRoute scheduledRoute, List<Guid>? SeatIds)
    {
        var takenSeatIds = _db.ScheduledRouteSeats.Where(x => x.ScheduledRouteId == scheduledRoute.Id).Select(x => x.SeatId).ToList();

        var query = QueryAll().Where(x => !takenSeatIds.Contains(x.Id));

        if (SeatIds != null && SeatIds.Any())
        {
            query = query.Where(x => SeatIds.Contains(x.Id));
        }

        return await query.ToListAsync();
    }

    public async Task AddReservations(List<ScheduledRouteSeat> scheduledRouteSeats)
    {
        _db.ScheduledRouteSeats.AddRange(scheduledRouteSeats);
        await _db.SaveChangesAsync();
    }
}
