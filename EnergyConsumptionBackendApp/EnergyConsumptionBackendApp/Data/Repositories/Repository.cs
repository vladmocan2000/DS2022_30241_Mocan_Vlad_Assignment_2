using EnergyConsumptionBackendApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EnergyConsumptionBackendApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext m_dbContext;
        private readonly DbSet<T> entities;

        public Repository(DataContext dbContext)
        {
            m_dbContext = dbContext;
            entities = m_dbContext.Set<T>();
        }

        public IQueryable<T> GetAllEntities()
        {
            return entities;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await entities.AddAsync(entity);

            await m_dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entities.Update(entity);

            await m_dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            entities.Remove(entity);

            await m_dbContext.SaveChangesAsync();
        }
    }
}
