using Common.Core.Models;

namespace Common.Core.Providers;

public interface IRepository : IProvider<IHasId>
{
    ValueTask<IHasId> GetByIdAsync(IHasId entity);
    void Update(IHasId entity);
    ValueTask SaveAsync();
}
