namespace Service.Contracts.Repository;

public interface IRepository<T> where T : class
{
    Task Add(T entity);
    Task Update(T entity);
    Task<T?> GetById(Guid id);
    Task<List<T>> GetAll();
}