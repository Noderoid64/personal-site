using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Ports;

public interface IPostChangesGateway
{
    public Task PostChanged(int id);
    public Task PostDeleted(int id);
    public Task PostAdded(int id);
}