namespace Service.Contracts.Repository;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Delete(Guid id);
    void Update(T entity);
    T? GetById(Guid id);
    List<T> GetAll();
}