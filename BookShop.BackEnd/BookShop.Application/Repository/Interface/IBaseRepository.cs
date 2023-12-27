namespace BookShop.Application.Repository.Interface;

public interface IBaseRepository<T>
{
    public IQueryable<T> Get();
    public Task Create(T entity);
    public Task Update(T  entity);
    public Task Delete(T entity);
}