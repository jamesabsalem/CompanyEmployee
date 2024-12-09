using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContest RepositoryContest;

        public RepositoryBase(RepositoryContest repositoryContest) 
            => this.RepositoryContest = repositoryContest;

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            RepositoryContest.Set<T>()
            .AsNoTracking() :
            RepositoryContest.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            RepositoryContest.Set<T>()
            .Where(expression)
            .AsNoTracking() :
             RepositoryContest.Set<T>()
            .Where(expression);

        public void Create(T entity) => RepositoryContest.Set<T>().Add(entity);

        public void Update(T entity) => RepositoryContest.Set<T>().Update(entity);

        public void Delete(T entity) => RepositoryContest.Set<T>().Remove(entity);
    }
}
