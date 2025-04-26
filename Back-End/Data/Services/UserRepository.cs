using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

public class UserRepository : Repository<User>
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<UserDto>> GetAllWithPersonAsync()
    {
        return await _dbSet
            .Include(u => u.person)
            .Where(u => EF.Property<bool>(u, "isdeleted") == false)
            .Select(u => new UserDto
            {
                id = u.id,
                UserName = u.username,
                Email = u.email,
                Password = u.password,
                PersonId = u.personid,
                PersonName = u.person.firstname + " " + u.person.lastname
            })
            .ToListAsync();
    }
}
