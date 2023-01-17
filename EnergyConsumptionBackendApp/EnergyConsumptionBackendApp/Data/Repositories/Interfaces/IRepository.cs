namespace EnergyConsumptionBackendApp.Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAllEntities();
        //Task<T?> GetByIdAsync(Guid entityId);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
