using BookShop.Application.Context;
using BookShop.Application.Repository.Interface;
using BookShop.Domain.Entity;

namespace BookShop.Application.Repository.Implementation;

public class UserRepository : IBaseRepository<User>
{

    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }


    public IQueryable<User> Get() => _db.Users;

    public async Task Create(User entity)
    {
       await _db.Users.AddAsync(entity);
       await _db.SaveChangesAsync();
    }

    public async Task Update(User entity)
    {
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();
        
    }

    public async Task Delete(User entity)
    {
        _db.Users.Remove(entity);
       await  _db.SaveChangesAsync();
    }
}