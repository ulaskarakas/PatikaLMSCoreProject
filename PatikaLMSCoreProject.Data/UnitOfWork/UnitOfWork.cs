using Microsoft.EntityFrameworkCore.Storage;
using PatikaLMSCoreProject.Data.Context;

namespace PatikaLMSCoreProject.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PatikaLMSCoreProjectDbContext _db;

        private IDbContextTransaction _transaction;

        public UnitOfWork(PatikaLMSCoreProjectDbContext db)
        {
            _db = db;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose(); // Cleaning permission granted to Garbage Collector
        }

        public async Task RollbackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}