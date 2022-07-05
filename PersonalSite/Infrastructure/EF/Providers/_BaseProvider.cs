namespace PersonalSite.Infrastructure.EF.Providers;

public abstract class _BaseProvider
{
    protected readonly ApplicationContext _context;

    public _BaseProvider(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}