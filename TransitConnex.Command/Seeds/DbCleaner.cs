using TransitConnex.Infrastructure.Data;

namespace TransitConnex.Infrastructure.Seeds;

public static class DbCleaner
{
    public static void DeleteEntireDb(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }
}
