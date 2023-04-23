namespace Gymby.Persistence.Data;

public class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
