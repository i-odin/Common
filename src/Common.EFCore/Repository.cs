using Common.Core.Models;
using Common.Core.Providers;

namespace Common.EFCore;

public class Repository : IRepository, IDisposable
{
    private bool _disposed;
    private readonly DataBaseContext _context;
    public Repository(DataBaseContext context)
    {
        _context = context;
    }
    
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Add(IHasId entity)
    {
        _context.Add(entity);
    }

    public void Remove(IHasId entity)
    {
        _context.Remove(entity);
    }

    public IReadOnlyCollection<IHasId> Read()
    {
        throw new NotImplementedException();
    }

    public async ValueTask<IHasId> GetByIdAsync(IHasId entity)
    {
        var result = (IHasId) await _context.FindAsync(entity.GetType(), entity.Id);
        return result;
    }

    public void Update(IHasId entity)
    {
        _context.Update(entity);
    }

    public async ValueTask SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}