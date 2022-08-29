using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Models.Entities.Enums;
using PersonalSite.Core.Ports;
using PersonalSite.Infrastructure.EF;

namespace PersonalSite.Services.FullTextSearch;

public class PostChangesGateway : IPostChangesGateway
{
    private ApplicationContext _context;

    public PostChangesGateway(ApplicationContext context)
    {
        _context = context;
    }
    public async Task PostChanged(int id)
    {
        _context.PostChanges.Add(new FileObjectChangeEntity(id, ChangeType.Change));
        await _context.SaveChangesAsync();
    }

    public async Task PostDeleted(int id)
    {
        _context.PostChanges.Add(new FileObjectChangeEntity(id, ChangeType.Delete));
        await _context.SaveChangesAsync();
    }

    public async Task PostAdded(int id)
    {
        _context.PostChanges.Add(new FileObjectChangeEntity(id, ChangeType.Add));
        await _context.SaveChangesAsync();
    }
}