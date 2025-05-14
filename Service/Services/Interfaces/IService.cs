namespace Service.Services.Interfaces;

public interface IService<T>
{
    void Add(T entity);
    void DeleteById(Guid id);
    void Update(Guid id, T entity);
    T GetById(Guid id);
    List<T> GetAll();
    void SeedDatabase();
    
}