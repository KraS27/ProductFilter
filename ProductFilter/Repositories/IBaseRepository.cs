namespace ProductFilter.Repositories
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}
