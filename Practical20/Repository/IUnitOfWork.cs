namespace Practical20.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveChangesAsync();
    }
}
