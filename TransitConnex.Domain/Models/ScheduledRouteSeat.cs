﻿namespace TransitConnex.Domain.Models;

public class ScheduledRouteSeat
{
    public Guid ScheduledRouteId { get; set; }
    public ScheduledRoute? ScheduledRoute { get; set; }
    public Guid SeatId { get; set; }
    public Seat? Seat { get; set; }
    public Guid? RouteTicketId { get; set; }
    public RouteTicket? RouteTicket { get; set; }
    public DateTime? ReservedUntil { get; set; }
    public Guid? ReservedById { get; set; }
    public User? ReservedBy { get; set; }
}
