﻿namespace Service.Services.Interfaces;

public interface IService<T>
{
    Task Add(T entity);
    Task Update(Guid id, T entity);
    Task<T> GetById(Guid id);
    Task<List<T>> GetAll();
}