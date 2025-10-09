
using FunctionConsultorio.Domain.Interfaces;
using FunctionConsultorio.Persistence.Context;

namespace FunctionConsultorio.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
        //return (await _context.SaveChangesAsync(cancellationToken)) == 1;
    }
}
