using BusinessLogic.Repositories.Interfaces;
using DataAccess;

namespace BusinessLogic.Repositories.Implementations;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    public async Task BeginTransactionAsync()
    {
        await context.Database.BeginTransactionAsync();
    }
    public async Task CommitTransactionAsync()
    {
        if (context.Database.CurrentTransaction is null) return;

        await context.Database.CommitTransactionAsync();
    }
    public async Task RollbackTransactionAsync()
    {
        if (context.Database.CurrentTransaction is null) return;

        await context.Database.RollbackTransactionAsync();
    }

}